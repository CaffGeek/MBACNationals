(function () {
    "use strict";

    var tournamentController = function ($http, $q, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var year = url.slice(lastSlash + 1);

        var vm = this;

        vm.Year = year;

        vm.model = {
            tournaments: [],
        };
        
        vm.newTournament = { };
        vm.currentTournament = { };

        dataService.LoadTournaments()
            .then(function (response) {
                vm.model.tournaments = response.data;

                if (vm.Year) {
                    var matches = vm.model.tournaments.filter(function (x) { return x.Year == vm.Year; });
                    vm.currentTournament = matches && matches.length ? matches[0] : {
                        Year: vm.Year,
                        ChangeNotificationCutoff: 'Jun 1',
                        ChangeNotificationEmail: 'hostcommittee@someemail.com',
                        ScoreNotificationEmail: 'scores@someemail.com',
                    };
                }
            });
        
        vm.createTournament = function () {
            dataService.CreateTournament(vm.newTournament)
                .then(function (response) {
                    //HACK
                    dataService.LoadTournaments()
                        .then(function (response) {
                            vm.model.tournaments = response.data;
                        });
                });
        };

        vm.saveSettings = function () {
            dataService.SaveTournamentSettings(vm.Year, vm.currentTournament)
                .then(function (response) {
                    vm.currentTournament = response.data;
                });
        }
    };

    app.controller("TournamentController", ["$http", "$q", "$location", "dataService", tournamentController]);
}());