(function () {
    "use strict";

    var modalFactory = function ($modal) {
        return {
            Participant: getParticipantModal,
            Divisions: getDivisionsModal
        };

        function getParticipantModal(year, participant, team, guestpackages) {
            participant = participant || {};
            team = team || {};
            guestpackages = guestpackages || [];

            var openModal = $modal.open({
                templateUrl: '/Modals/Participant/Edit.html',
                backdrop: 'static',
                windowClass: 'modal',
                controller: 'ModalParticipantController',
                resolve: {
                    participant: function () { return participant; },
                    team: function () { return team; },
                    year: function () { return year; },
                    guestpackages: function () { return guestpackages; }
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