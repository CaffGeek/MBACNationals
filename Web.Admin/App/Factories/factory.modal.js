(function () {
    "use strict";

    var modalFactory = function ($modal) {
        return {
            Participant: getParticipantModal,
            Divisions: getDivisionsModal
        };

        function getParticipantModal(year, participant, team) {
            participant = participant || {};
            team = team || {};

            var openModal = $modal.open({
                templateUrl: '/Modals/Participant/Edit.html',
                backdrop: 'static',
                windowClass: 'modal',
                controller: 'ModalParticipantController',
                resolve: {
                    participant: function () { return participant; },
                    team: function () { return team; },
                    year: function () { return year; }
                }
            });

            return openModal.result;
        };

        function getDivisionsModal(divisions) {
            divisions = divisions || {};

            var openModal = $modal.open({
                templateUrl: '/Modals/Contingent/Divisions.html',
                backdrop: 'static',
                windowClass: 'modal',
                controller: 'ModalDivisionsController',
                resolve: {
                    title: function () { return 'Choose Divisions'; },
                    divisions: function () { return divisions; }
                }
            });

            return openModal.result;
        };
    };

    app.factory('modalFactory', ['$modal', modalFactory]);
}());