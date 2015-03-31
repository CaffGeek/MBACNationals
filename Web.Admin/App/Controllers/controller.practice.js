(function () {
    "use strict";

    var practiceController = function ($scope, $http, $q, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        $scope.model = {
            province: province,
            allowedTimes: [9, 10, 11, 12, 13, 14, 15],
            teams: [],
            practiceLocations: ['Rossmere', 'Academy', 'Coronation']
        };

        if (year && province) {
            dataService.LoadPracticePlan(year, province).
                success(function (contingentPracticePlan) {
                    $scope.model.id = contingentPracticePlan.Id;
                    $scope.model.province = contingentPracticePlan.Province;
                    $scope.model.teams = contingentPracticePlan.Teams;
                });
        } 

        $scope.savePracticePlan = function () {
            dataService.SavePracticePlan($scope.model);
        };
    };

    app.controller("PracticeController", ["$scope", "$http", "$q", "$location", "dataService", practiceController]);
}());