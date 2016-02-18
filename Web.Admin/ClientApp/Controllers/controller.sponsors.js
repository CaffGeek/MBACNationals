(function () {
    "use strict";

    var sponsorsController = function (dataService, $timeout) {
        var vm = this;
        vm.ImageBase = '';
        vm.CurrentIndex = 0;

        dataService.LoadSponsors().then(function (response) {
            vm.Sponsors = response.data;
            vm.CurrentSponsor = vm.Sponsors[vm.CurrentIndex];
            sliderFunc();
        });

        var delay = 1500;
        var timer;
        var sliderFunc = function () {
            timer = $timeout(function () {
                vm.CurrentIndex = vm.CurrentIndex == vm.Sponsors.length - 1 ? 0 : vm.CurrentIndex+1;
                vm.CurrentSponsor = vm.Sponsors[vm.CurrentIndex];
                timer = $timeout(sliderFunc, delay);
            }, delay);
        };
    };

    app.controller("SponsorsController", ["dataService", "$timeout", sponsorsController]);
}());