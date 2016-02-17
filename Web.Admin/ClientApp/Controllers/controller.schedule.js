(function () {
    "use strict";

    var scheduleController = function ($scope, $http, dataService) {
        $scope.model = {};

        $scope.loadLaneDraw = loadLaneDraw;

        $scope.HasGames = getHasGames;
        $scope.Opponent = getOpponent;
        $scope.IsHomeTeam = getIsHomeTeam;
        $scope.Lane = getLane;

        loadLaneDraw('Tournament Men Single');
        
        function loadLaneDraw(division) {
            $scope.model.Division = division;

            dataService.LoadLaneDraw(division).then(function (data) {
                $scope.model = data.data;
                $scope.model.Games.sort(function (a, b) {                    
                    return a.Number - b.Number;
                });
                var lastLocation = '';
                angular.forEach($scope.model.Games, function (value) {
                    value.ShowLocation = (value.CentreName != lastLocation);
                    lastLocation = value.CentreName;
                });
            });
        };

        function getMatch(province, game) {
            var match = $.grep($scope.model.Games, function (o) {
                return o.Number == game && (o.Home == province || o.Away == province);
            });
            return match.length ? match[0] : {};
        };

        function getHasGames(province) {
            return $.grep($scope.model.Games, function (o) {
                return o.Home == province || o.Away == province;
            }).length;
        };

        function getOpponent(province, game) {
            var match = getMatch(province, game);
            return match.Home == province ? match.Away : match.Home;
        };

        function getIsHomeTeam(province, game) {
            var match = getMatch(province, game);
            return match.Home == province;
        };

        function getLane(province, game) {
            var match = getMatch(province, game);
            return match.Home == province
                ? match.Lane + 1 
                : match.Lane;
        };
    };

    app.controller("ScheduleController", ["$scope", "$http", "dataService", scheduleController]);
}());