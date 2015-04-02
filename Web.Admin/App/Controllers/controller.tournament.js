(function () {
    "use strict";

    var tournamentController = function ($http, $q, $location, dataService) {
        var vm = this;

        vm.model = {
            tournaments: []
        };
        
        vm.newTournament = {
            
        };

        dataService.LoadTournaments()
            .then(function (response) {
                vm.model.tournaments = response.data;
            });

        vm.createTournament = function () {
            dataService.CreateTournament(vm.newTournament);
        };
    };

    app.controller("TournamentController", ["$http", "$q", "$location", "dataService", tournamentController]);
}());