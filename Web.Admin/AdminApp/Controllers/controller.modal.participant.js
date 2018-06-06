(function () {
    "use strict";

    var modalParticipantController = function ($scope, $q, $modalInstance, dataService, participant, team, year, guestpackages) {
        $scope.model = {};
        
        $scope.model.title = team.Name;
        $scope.model.participant = participant || { Package: {} };
        $scope.model.team = team || {};
        $scope.model.year = year || 0;
        $scope.model.guestpackages = guestpackages || [];

        $scope.save = function () {
            var deferred = $q.defer();

            dataService.SaveParticipant($scope.model.participant).then(function (response) {
                $scope.model.participant.Id = response.data.Id;
            }).then(function (response) {
                if ($scope.model.participant.IsCoach) {
                    dataService.AssignCoachToTeam($scope.model.participant, $scope.model.team);
                } else if ($scope.model.team.Id && !$scope.model.participant.IsGuest && $scope.model.participant.TeamId != $scope.model.team.Id) {
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

            if ($scope.model.year === 2014) {
                $scope.model.participant.Package.ManitobaDinner = isCompletePackage;
                $scope.model.participant.Package.ManitobaDance = isCompletePackage;
                $scope.model.participant.Package.FinalBanquet = isCompletePackage;
                $scope.model.participant.Package.Transportation = isCompletePackage;
            } else {
                $scope.model.participant.Package.Option1 = isCompletePackage;
                $scope.model.participant.Package.Option2 = isCompletePackage;
                $scope.model.participant.Package.Option3 = isCompletePackage;
                $scope.model.participant.Package.Option4 = isCompletePackage;
            }
        };

        $scope.useAlternate = function (participant, team) {
            if (!confirm('Are you sure you want to replace this bowler with the Emergency Spare? THIS CAN NOT BE UNDONE'))
                return;

            dataService.UseAlternate(participant, team)
                .then(function (response) {
                    var alternate = response.data;
                    
                    var replacedParticipant = $scope.model.team.Bowlers.filter(function (x) { return x.Id == participant.Id; })[0];
                    replacedParticipant.ReplacedBy = alternate.Id;

                    $scope.model.team.Bowlers.push(alternate);
                    $modalInstance.dismiss('cancel');
                });
        };
    };

    app.controller("ModalParticipantController", ["$scope", "$q", "$modalInstance", "dataService", "participant", "team", "year", "guestpackages", modalParticipantController]);
}());