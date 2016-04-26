(function () {
	"use strict";

	var reportController = function ($scope, $http, $q, $location, modalFactory, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    $scope.model = { 
	        year: year, 
	        Divisions: [
                'Tournament Men Single',
                'Tournament Ladies Single',
                'Tournament Men',
                'Tournament Ladies',
                'Teaching Men',
                'Teaching Ladies',
                'Seniors',
	        ]
	    };
	    $scope.viewUrl = '/AdminApp/Views/Reports/Reservations.html';

		$scope.loadParticipants = loadParticipants;
		$scope.loadProfiles = loadProfiles;
		$scope.loadRooms = loadRooms;
		$scope.loadTravelPlans = loadTravelPlans;
		$scope.loadPracticePlans = loadPracticePlans;
		$scope.loadAlternates = loadAlternates;
		$scope.loadSchedule = loadSchedule;

		$scope.plaqueFilter = plaqueFilter;
		$scope.rookieFilter = rookieFilter;
		$scope.roomTypeFilter = roomTypeFilter;
		$scope.bowlerFilter = bowlerFilter;

		loadParticipants();
		loadRooms();

		function loadParticipants() {
		    dataService.LoadAllParticipants($scope.model.year).then(function (data) {
		        $scope.model.Participants = data.data;
		    });
		};

		function loadAlternates() {
		    dataService.LoadAlternates($scope.model.year).then(function (data) {
		        $scope.model.Alternates = data.data;
		    });
		};

		function plaqueFilter(participant) {
		    return (participant.YearsQualifying) && (participant.YearsQualifying % 5 === 0);
		};

		function rookieFilter(participant) {
		    return (participant.YearsQualifying) && (participant.YearsQualifying == 1);
		};

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

		function loadSchedule(division) {
		    dataService.LoadSchedule($scope.model.year, division).then(function (data) {		        
		        //var schedule = $scope.model.Schedule = ;
		        
		        var simplified = {
		            Division: division,
		            Games: {}
		        };
		        data.data.Games.forEach(function (x) {
		            simplified.Games[x.Home] = simplified.Games[x.Home] || { HomeTotal: 0, AwayTotal: 0 };
		            simplified.Games[x.Away] = simplified.Games[x.Away] || { HomeTotal: 0, AwayTotal: 0 };
		            simplified.Games[x.Home].HomeTotal++;
		            simplified.Games[x.Away].AwayTotal++;
		            simplified.Games[x.Home][x.Away] = (simplified.Games[x.Home][x.Away] || 0) + 1;
		        });

		        $scope.model.Schedule = simplified;
		    });
		};

		function roomTypeFilter(rooms, type) {
		    return rooms.filter(function (room) { return room.Type == type; }).length;
		};

		function bowlerFilter(bowler) {
		    return bowler.IsAlternate || (bowler.TeamName && !bowler.IsCoach);
		}
	};

	app.controller("ReportController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reportController]);
}());