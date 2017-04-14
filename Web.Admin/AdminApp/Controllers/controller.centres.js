(function () {
    "use strict";

	var centresController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.viewUrl = '/AdminApp/Views/Tournament/Centres.html';

	    vm.Year = year;
	    vm.SaveCentre = SaveCentre;
	    vm.DeleteCentre = DeleteCentre;
        
	    dataService.LoadCentres(year)
            .then(function (response) {
                vm.Centres = response.data;
            });

	    function SaveCentre() {
	        var newCentre = {
	            Id: vm.Id,
	            Name: vm.CentreName,
	            Website: vm.CentreWebsite,
	            PhoneNumber: vm.CentrePhoneNumber,
	            Address: vm.CentreAddress,
	            Image: vm.CentreImages[0]
	        };

	        dataService.SaveCentre(vm.Year, newCentre)
                .then(function (response) {
                    vm.Centres.push(response.data);
                    vm.CentreName = '';
                    vm.CentreWebsite = '';
                    vm.CentrePhoneNumber = '';
                    vm.CentreAddress = '';
                    vm.CentreImages = [];
                });
	    }

	    function DeleteCentre(id) {
	        dataService.DeleteCentre(vm.Year, id)
                .then(function (response) {
                    var centre = vm.Centres.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.Centres.indexOf(centre);
                    if (idx < 0)
                        return;

                    vm.Centres.splice(idx, 1);
                });
	    }
	};

	app.controller("CentresController", ["$http", "$q", "$location", "dataService", centresController]);
}());