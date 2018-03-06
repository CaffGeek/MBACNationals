(function () {
    "use strict";

    var arrivalsController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        $scope.minDate = new Date(year, 5, 1);  //5 is June (yes...yes it is!)
        $scope.maxDate = new Date(year, 6, 31); //6 is July (seriously)

        var emptyArrival = { ModeOfTransportation: 'Air', When: $scope.minDate, Type: 1 };
        var emptyDeparture = { ModeOfTransportation: 'Air', When: $scope.maxDate, Type: 2 };

        $scope.model = {
            year: year,
            province: province,
            arrivals: [angular.copy(emptyArrival), angular.copy(emptyArrival), angular.copy(emptyArrival)],
            departures: [angular.copy(emptyDeparture), angular.copy(emptyDeparture), angular.copy(emptyDeparture)]
        };

        if (year && province) {
            dataService.LoadParticipants(year, province)
                .success(function (participants) {
                    $scope.model.participants = participants;
                });

            dataService.LoadTravelPlans(year, province)
                .success(function (contingentTravelPlans) {
                    $scope.model.id = contingentTravelPlans.Id;
                    $scope.model.province = contingentTravelPlans.Province;

                    angular.forEach(contingentTravelPlans.TravelPlans, function (travelPlan) {
                        var d = new Date(travelPlan.When);
                        //UGH, such a hack
                        travelPlan.When = new Date(d.getUTCFullYear(), d.getUTCMonth(), d.getUTCDate(), d.getUTCHours(), d.getUTCMinutes());
                    });

                    $scope.model.arrivals = contingentTravelPlans.TravelPlans.filter(x => x.Type == 1);
                    $scope.model.departures = contingentTravelPlans.TravelPlans.filter(x => x.Type == 2);
                });
        }

        $scope.addArrival = function () {
            $scope.model.arrivals.push(angular.copy(emptyArrival));
        };

        $scope.addDeparture = function () {
            $scope.model.departures.push(angular.copy(emptyDeparture));
        };

        $scope.saveTravelPlans = function () {
            dataService.SaveTravelPlans({
                id: $scope.model.id,
                year: $scope.model.year,
                province: $scope.model.province,
                travelPlans: $scope.model.arrivals.concat($scope.model.departures)
            });
        };

        $scope.removeRecord = function (travelPlan) {
            var idx;

            idx = $scope.model.arrivals.indexOf(travelPlan);
            if (idx >= 0) $scope.model.arrivals.splice(idx, 1);

            idx = $scope.model.departures.indexOf(travelPlan);
            if (idx >= 0) $scope.model.departures.splice(idx, 1);
        };

        $scope.addToArrival = function (participantId, travelPlanIndex) {
            $scope.removeFromArrival(participantId);

            var travelPlan = $scope.model.arrivals[travelPlanIndex];
            var participant = $scope.model.participants.find(x => x.Id == participantId);

            travelPlan.Occupants = travelPlan.Occupants || [];
            travelPlan.Occupants.push(participant);
        }

        $scope.addToDeparture = function (participantId, travelPlanIndex) {
            $scope.removeFromDeparture(participantId);

            var travelPlan = $scope.model.departures[travelPlanIndex];
            var participant = $scope.model.participants.find(x => x.Id == participantId);

            travelPlan.Occupants = travelPlan.Occupants || [];
            travelPlan.Occupants.push(participant);
        }

        $scope.removeFromArrival = function (participantId) {
            $scope.model.arrivals.forEach(function (travelPlan) {
                if (!travelPlan.Occupants) return;
                var participant = travelPlan.Occupants.find(x => x.Id == participantId);
                if (!participant) return;

                var idx = travelPlan.Occupants.indexOf(participant);
                travelPlan.Occupants.splice(idx, 1);
            });
        }

        $scope.removeFromDeparture = function (participantId) {
            $scope.model.departures.forEach(function (travelPlan) {
                if (!travelPlan.Occupants) return;
                var participant = travelPlan.Occupants.find(x => x.Id == participantId);
                if (!participant) return;

                var idx = travelPlan.Occupants.indexOf(participant);
                travelPlan.Occupants.splice(idx, 1);
            });
        }

        $scope.unassignedArrival = function (participant) {
            var occupants = $scope.model.arrivals
                .filter(x => x.Occupants)
                .map(x => x.Occupants)
                .reduce(function (flat, toFlatten) {
                    return flat.concat(toFlatten);
                }, []);
            
            return occupants.filter(x => x.Id == participant.Id).length <= 0;
        }

        $scope.unassignedDeparture = function (participant) {
            var occupants = $scope.model.departures
                .filter(x => x.Occupants)
                .map(x => x.Occupants)
                .reduce(function (flat, toFlatten) {
                    return flat.concat(toFlatten);
                }, []);

            return occupants.filter(x => x.Id == participant.Id).length <= 0;
        }
    };

    app.controller("ArrivalsController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", arrivalsController]);
}());