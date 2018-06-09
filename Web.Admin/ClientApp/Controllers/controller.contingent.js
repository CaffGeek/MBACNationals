(function () {
    "use strict";

    var contingentController = function ($scope, $http, dataService, $stateParams, $state) {
        $scope.model = {};

        $scope.loadContingent = loadContingent;
        $scope.totalAverage = totalAverage;

        $scope.model.Province = $stateParams.province;

        if ($scope.model.Province)
            loadContingent();

        function loadContingent(province) {
            province = province || $scope.model.Province;

            dataService.LoadContingent(province).then(function (data) {
                $scope.model = data.data;
                $scope.managers = $scope.model.Guests.filter(function (x) { return x.IsManager; });
                $scope.model.Teams.forEach(function (team) {
                    if (team.Coach && team.Coach.IsManager) $scope.managers.push(team.Coach);
                    $scope.managers = $scope.managers.concat(team.Bowlers.filter(function (x) { return x.IsManager; }));
                });
                $scope.delegates = $scope.model.Guests.filter(function (x) { return x.IsDelegate; });
                $scope.model.Teams.forEach(function (team) {
                    if (team.Coach && team.Coach.IsDelegate) $scope.delegates.push(team.Coach);
                    $scope.delegates = $scope.delegates.concat(team.Bowlers.filter(function (x) { return x.IsDelegate; }));
                });
            });
        };               

        function totalAverage(team) {
            var total = 0;
            for (var i = 0; i < team.Bowlers.length; i++) {
                var bowler = team.Bowlers[i];
                if (bowler.ReplacedBy)
                    continue;

                if (!bowler.ReplacedBy)
                    total += bowler.Average;
            }
            return total;
        };
    };

    app.controller("ContingentController", ["$scope", "$http", "dataService", "$stateParams", "$state", contingentController]);
}());