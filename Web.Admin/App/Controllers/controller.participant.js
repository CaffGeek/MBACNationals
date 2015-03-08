(function () {
    "use strict";

    var participantController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        $scope.model = {
            Teams: []
        };
        $scope.includesSinglesRep = includesSinglesRep;
        $scope.loadActiveParticipant = loadActiveParticipant;
        $scope.saveParticipantProfile = saveParticipantProfile;
        
        if (province) {
            dataService.LoadContingent(province).success(function (contingent) {
                $scope.model = contingent;
            });
        }

        function includesSinglesRep(x) {
            return x.IncludesSinglesRep;
        }
        
        function loadActiveParticipant(participant) {
            dataService.LoadParticipantProfile(participant).then(function (data) {
                $scope.model.ActiveParticipant = data.data;
            });
        };

        function saveParticipantProfile() {
            dataService.SaveParticipantProfile($scope.model.ActiveParticipant);
        }
    };

    app.controller("ParticipantController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", participantController]);
}());