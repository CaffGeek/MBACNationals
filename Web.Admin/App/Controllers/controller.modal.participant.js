(function () {
    "use strict";

    var modalParticipantController = function ($scope, $q, $modalInstance, dataService, participant, team) {
        $scope.model = {};
        
        $scope.model.title = team.Name;
        $scope.model.participant = participant || { Package: {} };
        $scope.model.team = team || {};

        $scope.save = function () {
            var deferred = $q.defer();

            dataService.SaveParticipant($scope.model.participant).then(function (response) {
                $scope.model.participant.Id = response.data.Id;
            }).then(function (response) {
                if ($scope.model.participant.IsCoach) {
                    dataService.AssignCoachToTeam($scope.model.participant, $scope.model.team);
                } else if ($scope.model.team.Id) {
                    dataService.AssignParticipantToTeam($scope.model.participant, $scope.model.team);
                }
            }).then(function (response) {
                $modalInstance.close($scope.model.participant);
            });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.togglePackages = function (isCompletePackage) {
            $scope.model.participant.Package = $scope.model.participant.Package || {};

            $scope.model.participant.Package.ManitobaDinner = isCompletePackage;
            $scope.model.participant.Package.ManitobaDance = isCompletePackage;
            $scope.model.participant.Package.FinalBanquet = isCompletePackage;
            $scope.model.participant.Package.Transportation = isCompletePackage;
        };

        $scope.useAlternate = function (participant, team) {
            dataService.UseAlternate(participant, team).then(function (response) {
                $modalInstance.dismiss('cancel');
            });
        };
    };

    app.controller("ModalParticipantController", ["$scope", "$q", "$modalInstance", "dataService", "participant", "team", modalParticipantController]);
}());