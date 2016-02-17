(function () {
	"use strict";

	var reportController = function ($scope, $http, $q, $location, modalFactory, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    $scope.model = { year: year };
		$scope.viewUrl = '/App/Views/Reports/Reservations.html';

		$scope.loadParticipants = loadParticipants;
		$scope.loadProfiles = loadProfiles;
		$scope.loadRooms = loadRooms;
		$scope.loadTravelPlans = loadTravelPlans;
		$scope.loadPracticePlans = loadPracticePlans;

		$scope.plaqueFilter = plaqueFilter;
		$scope.roomTypeFilter = roomTypeFilter;

		loadParticipants();
		loadRooms();

		function loadParticipants() {
		    dataService.LoadAllParticipants($scope.model.year).then(function (data) {
		        $scope.model.Participants = data.data;
		    });
		};

		function plaqueFilter(participant) {
		    return (participant.YearsQualifying) && (participant.YearsQualifying % 5 === 0);
		}

		function loadProfiles() {
		    dataService.LoadProfiles($scope.model.year).then(function (data) {
		        $scope.model.Profiles = data.data;
		    });
		};

		function loadRooms() {
		    dataService.LoadRooms($scope.model.year).then(function (data) {
		        $scope.model.ContingentRooms = data.data;
		    });
		};

		function loadTravelPlans() {
		    dataService.LoadTravelPlans($scope.model.year).then(function (data) {
		        var travelPlans = data.data;
		        var flattenedTravelPlans = [];

		        for (var c = 0; c < travelPlans.length; c++) {
		            var contingent = travelPlans[c];

		            for (var p = 0; p < contingent.TravelPlans.length; p++) {
		                var plan = contingent.TravelPlans[p];

		                flattenedTravelPlans.push({
		                    Province: contingent.Province,
		                    ModeOfTransportation: plan.ModeOfTransportation,
		                    When: plan.When,
		                    FlightNumber: plan.FlightNumber,
		                    NumberOfPeople: plan.NumberOfPeople,
		                    Type: plan.Type
		                });
		            }
		        }
		        
		        $scope.model.TravelPlans = travelPlans;
		        $scope.model.FlattenedTravelPlans = flattenedTravelPlans.sort(function (a, b) { return a.When < b.When ? -1 : 1; });
		    });
		};

		function loadPracticePlans() {
		    dataService.LoadPracticePlan($scope.model.year).then(function (data) {
		        var practicePlans = data.data;
		        var flattenedPracticePlans = [];

		        for (var c = 0; c < practicePlans.length; c++) {
		            var contingent = practicePlans[c];

		            for (var p = 0; p < contingent.Teams.length; p++) {
		                var plan = contingent.Teams[p];

		                flattenedPracticePlans.push({
		                    Province: contingent.Province,
		                    Name: plan.Name,
		                    PracticeTime: plan.PracticeTime,
		                    PracticeLocation: plan.PracticeLocation
		                });
		            }
		        }

		        $scope.model.PracticePlans = practicePlans;
		        $scope.model.FlattenedPracticePlans = flattenedPracticePlans;
		    });
		};

		function roomTypeFilter(rooms, type) {
		    return rooms.filter(function (room) { return room.Type == type; }).length;
		};
	};

	app.controller("ReportController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reportController]);
}());