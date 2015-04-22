(function () {
    "use strict";

    var resultsController = function ($scope, $http, dataService) {
        $scope.model = {};

        $scope.minGame = 1;
        $scope.maxGame = 7;
        $scope.viewStandings = viewStandings;
        $scope.viewMatch = viewMatch;
        $scope.viewTeam = viewTeam;
        $scope.viewBowler = viewBowler;

        $scope.findMatchByNumber = findMatchByNumber;
        
        viewStandings('Tournament Men Single');
                
        function viewStandings(division) {
            $scope.viewUrl = '/app/Views/Results/Standings.html';
            $scope.model.Division = division;

            dataService.LoadStandings(division).then(function (data) {
                $scope.model = data.data;
                $scope.model.Division = division;
            });
        };

        function viewMatch(match) {
            $scope.viewUrl = '/app/Views/Results/Match.html';
            $scope.model.MatchId = match.MatchId || match.Id;

            dataService.LoadMatch($scope.model.MatchId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function viewTeam(team) {
            $scope.viewUrl = '/app/Views/Results/Team.html';
            $scope.model.TeamId = team.TeamId || team.Id;

            dataService.LoadTeamScores($scope.model.TeamId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function viewBowler(bowler) {
            $scope.viewUrl = '/app/Views/Results/Bowler.html';
            $scope.model.BowlerId = bowler.BowlerId || bowler.Id;

            dataService.LoadParticipantScores($scope.model.BowlerId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function findMatchByNumber(team, number) {
            var match = ($.grep(team.Matches, function (o) { return o.Number == number; }) || [])[0];
            return match;
        };

        $scope.getRange = function (from, to) {
            var result = [];

            for (var i = from; i <= to; i++)
                result.push(i);

            return result;
        };
    };

    app.controller("ResultsController", ["$scope", "$http", "dataService", resultsController]);
}());