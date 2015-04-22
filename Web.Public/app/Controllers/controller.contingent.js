(function () {
    "use strict";

    var contingentController = function ($scope, $http, dataService) {
        $scope.model = {};

        $scope.loadContingent = loadContingent;
        $scope.totalAverage = totalAverage;

        loadContingent('BC');

        function loadContingent(province) {
            $scope.model.Province = province;

            dataService.LoadContingent(province).then(function (data) {
                $scope.model = data.data;
            });
        };

        function totalAverage(team) {
            var total = 0;
            for (var i = 0; i < team.Bowlers.length; i++) {
                var bowler = team.Bowlers[i];
                if (bowler.ReplacedBy)
                    continue;

                total += bowler.Average;
            }
            return total;
        };
    };

    app.controller("ContingentController", ["$scope", "$http", "dataService", contingentController]);
}());