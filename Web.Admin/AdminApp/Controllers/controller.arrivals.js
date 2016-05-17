(function () {
    "use strict";

    var arrivalsController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        var emptyArrival = { ModeOfTransportation: 'Air', When: new Date('2016-06-28T09:00'), Type: 1 };
        var emptyDeparture = { ModeOfTransportation: 'Air', When: new Date('2016-07-03T09:00'), Type: 2 };

        $scope.model = {
            year: year,
            province: province,
            travelPlans: [
                angular.copy(emptyArrival), angular.copy(emptyArrival), angular.copy(emptyArrival),
                angular.copy(emptyDeparture), angular.copy(emptyDeparture), angular.copy(emptyDeparture)]
        };

        if (year && province) {
            dataService.LoadTravelPlans(year, province).
                success(function (contingentTravelPlans) {
                    $scope.model.id = contingentTravelPlans.Id;
                    $scope.model.province = contingentTravelPlans.Province;
                    $scope.model.travelPlans = contingentTravelPlans.TravelPlans;

                    angular.forEach(contingentTravelPlans.TravelPlans, function (travelPlan) {
                        var d = new Date(travelPlan.When);
                        //UGH, such a hack
                        travelPlan.When = new Date(d.getUTCFullYear(), d.getUTCMonth(), d.getUTCDate(), d.getUTCHours(), d.getUTCMinutes());
                    });                       
                });
        }

        $scope.addArrival = function () {
            $scope.model.travelPlans.push(angular.copy(emptyArrival));
        };

        $scope.addDeparture = function () {
            $scope.model.travelPlans.push(angular.copy(emptyDeparture));
        };

        $scope.saveTravelPlans = function () {
            dataService.SaveTravelPlans($scope.model);
        };

        $scope.removeRecord = function (travelPlan) {
            var idx = $scope.model.travelPlans.indexOf(travelPlan);
            if (idx < 0)
                return;

            $scope.model.travelPlans.splice(idx, 1);
        };
    };

    app.controller("ArrivalsController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", arrivalsController]);
}());