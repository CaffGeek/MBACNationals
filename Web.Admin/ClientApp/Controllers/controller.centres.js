(function () {
    "use strict";

    var centresController = function (dataService, $timeout) {
        var vm = this;

        dataService.LoadCentres().then(function (response) {
            vm.Centres = response.data;
        });
    };

    app.controller("CentresController", ["dataService", "$timeout", centresController]);
}());