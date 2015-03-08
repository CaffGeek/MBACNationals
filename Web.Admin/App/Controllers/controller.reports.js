(function () {
	"use strict";

	var reportController = function ($scope, $http, $q, $location, modalFactory, dataService) {
		var url = $location.absUrl();
		var lastSlash = url.lastIndexOf('/');
		var province = url.slice(lastSlash+1);

		$scope.model = {};
		$scope.viewUrl = '/App/Views/Reports/Averages.html';

		$scope.loadParticipants = loadParticipants;
		$scope.loadProfiles = loadProfiles;

		$scope.plaqueFilter = plaqueFilter;

		loadParticipants();

		function loadParticipants() {
		    dataService.LoadAllParticipants().then(function (data) {
		        $scope.model.Participants = data.data;
		    });
		};

		function plaqueFilter(participant) {
		    return (participant.YearsQualifying) && (participant.YearsQualifying % 5 === 0);
		}

		function loadProfiles() {
		    dataService.LoadProfiles().then(function (data) {
		        $scope.model.Profiles = data.data;
		    });
		};
	};

	app.controller("ReportController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reportController]);
}());