(function () {
	"use strict";

	var guestPackagesController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.viewUrl = '/AdminApp/Views/Tournament/GuestPackages.html';

	    vm.Year = year;
	    vm.SaveGuestPackages = SaveGuestPackages;
	    vm.DeleteGuestPackage = DeleteGuestPackage;

	    dataService.LoadGuestPackages(year)
            .then(function (response) {
                vm.GuestPackages = response.data;
            });

	    function SaveGuestPackages() {
	        dataService.SaveGuestPackages(vm.Year, vm.GuestPackages)
                .then(function (response) {
                    vm.GuestPackages = response.data.GuestPackages;
                });
	    };

	    function DeleteGuestPackage(id) {
	        dataService.DeleteGuestPackage(vm.Year, id)
                .then(function (response) {
                    var guestPackages = vm.GuestPackages.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.GuestPackages.indexOf(guestPackages);
                    if (idx < 0)
                        return;

                    vm.GuestPackages.splice(idx, 1);
                });
	    };
	};

	app.controller("GuestPackagesController", ["$http", "$q", "$location", "dataService", guestPackagesController]);
}());