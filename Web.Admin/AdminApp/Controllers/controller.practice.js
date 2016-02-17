(function () {
    "use strict";

    var practiceController = function ($scope, $http, $q, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        $scope.model = {
            province: province,
            allowedTimes : [],
            teams: [],
            practiceLocations: []
        };

        //TODO: Move to some config per year...
        if (year == 2014) {
            $scope.model.allowedTimes = [9, 10, 11, 12, 13, 14, 15];
            $scope.model.practiceLocations = ['Rossmere', 'Academy', 'Coronation'];
        }
        else if (year == 2015) {
            $scope.model.allowedTimes = [11, 12, 13, 14, 15, 16];
            $scope.model.practiceLocations = ['Sherwood'];
        }
            

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