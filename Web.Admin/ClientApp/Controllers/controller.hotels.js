(function () {
    "use strict";

    var hotelsController = function (dataService, $timeout) {
        var vm = this;

        dataService.LoadHotels().then(function (response) {
            vm.Hotels = response.data;
        });
    };

    app.controller("HotelsController", ["dataService", "$timeout", hotelsController]);
}());