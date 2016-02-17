(function () {
    "use strict";

    var scoreEntryController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var year = url.slice(lastSlash + 1) || new Date().getFullYear();

        var pages = [
            'Division',
            'Game',
            'Match',
            'Score',
            'Result',
            'Submit'
        ];

        $scope.model = {};
        navigate('Division');

        $scope.Back = function () {
            var currentIndex = pages.indexOf($scope.page);
            var prevPage = pages[currentIndex - 1];
            navigate(prevPage);
        };

        $scope.Page = function (page, data) {
            switch (page) {
                case 'Game': {
                    dataService.LoadSchedule(year, data.Division).
                        success(function (divisionSchedule) {
                            $scope.model.Schedule = divisionSchedule;
                            var currentGame = 1;
                            while ($scope.AllGamesComplete(currentGame) && currentGame++ <= 21) { }
                            $scope.model.CurrentGame = currentGame;
                        });
                    $scope.model.Division = data.Division;
                    break;
                }
                case 'Match': {
                    $scope.model.Number = data.Number;
                    break;
                }
                case 'Score': {
                    $scope.model.Game = data.Game;

                    $scope.model.AwayProvince = data.Game.Away;
                    $scope.model.HomeProvince = data.Game.Home;
                    
                    $q.all([
                        dataService.LoadTeam(year, $scope.model.AwayProvince, $scope.model.Division),
                        dataService.LoadTeam(year, $scope.model.HomeProvince, $scope.model.Division),
                        dataService.LoadMatch(data.Game)
                    ]).then(function (results) {
                        $scope.model.Away = results[0].data;
                        $scope.model.Home = results[1].data;
                        var match = results[2].data;

                        if ($scope.model.Away.Bowlers.length == 1) $scope.model.Away.Bowlers[0].Position = 1;
                        if ($scope.model.Home.Bowlers.length == 1) $scope.model.Home.Bowlers[0].Position = 1;

                        if (!data.Game.IsComplete) 
                            return;

                        for (var i = 0; i < $scope.model.Away.Bowlers.length; i++) {
                            var awayBowlerResult = $.grep(match.Away.Bowlers, function (x) { return x.Id == $scope.model.Away.Bowlers[i].Id });
                            if (awayBowlerResult.length) {
                                $scope.model.Away.Bowlers[i].Score = awayBowlerResult[0].Score;
                                $scope.model.Away.Bowlers[i].Position = awayBowlerResult[0].Position;
                            }
                        }
                        
                        for (var i = 0; i < $scope.model.Home.Bowlers.length; i++) {
                            var homeBowlerResult = $.grep(match.Home.Bowlers, function (x) { return x.Id == $scope.model.Home.Bowlers[i].Id });
                            if (homeBowlerResult.length) {
                                $scope.model.Home.Bowlers[i].Score = homeBowlerResult[0].Score;
                                $scope.model.Home.Bowlers[i].Position = homeBowlerResult[0].Position;
                            }
                        }
                    });
                                        
                    break;
                }
                case 'Result': {
                    var teamSize = Math.max($scope.model.Away.SizeLimit, $scope.model.Home.SizeLimit) || 0;
                    $scope.model.Away.Score = $scope.model.Away.POA = $scope.model.Away.TotalPoints = 0;
                    $scope.model.Home.Score = $scope.model.Home.POA = $scope.model.Home.TotalPoints = 0;

                    $scope.model.IsPOA = $scope.model.Home.RequiresAverage;
                    $scope.model.IsScratch = !$scope.model.IsPOA;
                    $scope.model.IsSingles = teamSize == 1;
                    $scope.model.IsTeam = !$scope.model.IsSingles;
                    
                    for (var i = 1; i <= teamSize; i++) {
                        updateScoreInfo(i);
                    }
                    if (teamSize > 1) {
                        $scope.model.Home.TotalPoints += $scope.model.Home.Point;
                        $scope.model.Away.TotalPoints += $scope.model.Away.Point;
                    }
                    break;
                }
                case 'Submit': {
                    dataService.SaveMatchResult({
                        Id: $scope.model.Game.Id,
                        Division: $scope.model.Division,
                        Game: $scope.model.Game,
                        Home: $scope.model.Home,
                        Away: $scope.model.Away
                    });
                    break;
                }
                default: {
                    page = 'Division';
                }
            };

            navigate(page);
        };

        $scope.ValidForm = function () {
            var teamSize = Math.max($scope.model.Away.SizeLimit || 1, $scope.model.Home.SizeLimit || 1) || 1;
            var isValid = true;

            isValid = isValid && $scope.model.Away.Bowlers && !!$scope.model.Away.Bowlers.length;
            isValid = isValid && $scope.model.Home.Bowlers && !!$scope.model.Home.Bowlers.length;
            if (!isValid)
                return false;

            isValid = isValid && !$.grep($scope.model.Away.Bowlers, function (o) { return o.Score < 0 || o.Score > 450; }).length;
            isValid = isValid && !$.grep($scope.model.Home.Bowlers, function (o) { return o.Score < 0 || o.Score > 450; }).length;
            if (!isValid)
                return false;

            for (var i = 1; i <= teamSize; i++) {
                isValid = isValid && !!$.grep($scope.model.Away.Bowlers, function (o) { return o.Position == i; }).length;
                isValid = isValid && !!$.grep($scope.model.Home.Bowlers, function (o) { return o.Position == i; }).length;
            }
            
            return isValid;
        };

        $scope.AllGamesComplete = function (number) {
            return !$.grep($scope.model.Schedule.Games, function (o) { return o.Number == number && !o.IsComplete; }).length;
        };

        function updateScoreInfo(position) {
            var homeBowler = ($.grep($scope.model.Home.Bowlers, function (o) { return o.Position == position; }) || [])[0];
            var awayBowler = ($.grep($scope.model.Away.Bowlers, function (o) { return o.Position == position; }) || [])[0];

            if ($scope.model.IsTeam) {
                $scope.model.Home.Score += homeBowler.Score;
                $scope.model.Away.Score += awayBowler.Score;
            }

            homeBowler.POA = homeBowler.Score - homeBowler.Average;
            awayBowler.POA = awayBowler.Score - awayBowler.Average;

            if ($scope.model.IsTeam) {
                $scope.model.Home.POA += homeBowler.POA;
                $scope.model.Away.POA += awayBowler.POA;
            }

            if (($scope.model.IsPOA && homeBowler.POA > awayBowler.POA)
                || ($scope.model.IsScratch && homeBowler.Score > awayBowler.Score)) {
                homeBowler.Point = 1;
                awayBowler.Point = 0;
            }
            else if (($scope.model.IsPOA && homeBowler.POA < awayBowler.POA)
                || ($scope.model.IsScratch && homeBowler.Score < awayBowler.Score)) {
                homeBowler.Point = 0;
                awayBowler.Point = 1;
            }
            else {
                homeBowler.Point = .5;
                awayBowler.Point = .5;
            }
            
            $scope.model.Home.TotalPoints += homeBowler.Point;
            $scope.model.Away.TotalPoints += awayBowler.Point;

            if ($scope.model.IsTeam) {
                if (($scope.model.IsPOA && $scope.model.Home.POA > $scope.model.Away.POA)
                    || ($scope.model.IsScratch && $scope.model.Home.Score > $scope.model.Away.Score)) {
                    $scope.model.Home.Point = 3;
                    $scope.model.Away.Point = 0;
                }
                else if (($scope.model.IsPOA && $scope.model.Home.POA < $scope.model.Away.POA)
                    || ($scope.model.IsScratch && $scope.model.Home.Score < $scope.model.Away.Score)) {
                    $scope.model.Home.Point = 0;
                    $scope.model.Away.Point = 3;
                }
                else {
                    $scope.model.Home.Point = 1.5;
                    $scope.model.Away.Point = 1.5;
                }
            }
        };

        function navigate(page) {
            $scope.page = page;
            $scope.viewUrl = '/AdminApp/Views/ScoreEntry/' + page + '.html';
        };
    };

    app.controller("ScoreEntryController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", scoreEntryController]);
}());