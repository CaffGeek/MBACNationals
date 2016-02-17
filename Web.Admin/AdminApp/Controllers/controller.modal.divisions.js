(function () {
    "use strict";

    var modalDivisionsController = function ($scope, $modalInstance, $http, title, divisions) {
        $scope.model = {};

        $scope.model.title = title;
        $scope.model.divisions = [
                { Name: 'Tournament Men Single', Selected: true, Gender: 'M', SizeLimit: 1, IncludesSinglesRep: true, RequiresShirtSize: true, RequiresBio: true },
                { Name: 'Tournament Ladies Single', Selected: true, Gender: 'F', SizeLimit: 1, IncludesSinglesRep: true, RequiresShirtSize: true, RequiresBio: true },
                { Name: 'Tournament Men', Selected: true, Gender: 'M', SizeLimit: 5, RequiresCoach: true },
                { Name: 'Tournament Ladies', Selected: true, Gender: 'F', SizeLimit: 5, RequiresCoach: true },
                { Name: 'Teaching Men', Selected: true, Gender: 'M', SizeLimit: 5, RequiresCoach: true, IncludesSinglesRep: true, RequiresAverage: true },
                { Name: 'Teaching Ladies', Selected: true, Gender: 'F', SizeLimit: 5, RequiresCoach: true, IncludesSinglesRep: true, RequiresAverage: true },
                { Name: 'Seniors', Selected: true, SizeLimit: 5, RequiresCoach: true, IncludesSinglesRep: true, RequiresAverage: true, RequiresGender: true }
            ];

        if (divisions.length) {
            angular.forEach($scope.model.divisions, function (value) {
                var division = divisions.filter(function (obj) { return obj.Name === value.Name; });
                if (division.length) {
                    value.Selected = true;
                    //TODO: Merge division[0] over value rather than doing this piecemeal
                    value.Id = division[0].Id;
                    value.ContingentId = division[0].ContingentId;
                } else {
                    value.Selected = false;
                }
            });
        }

        $scope.save = function () {
            $modalInstance.close($scope.model.divisions.filter(function (obj) { return obj.Selected; }));
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    app.controller("ModalDivisionsController", ["$scope", "$modalInstance", "$http", "title", "divisions", modalDivisionsController]);
}());