(function () {
	"use strict";

	var hotelController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.viewUrl = '/AdminApp/Views/Tournament/Hotels.html';

	    vm.Year = year;
	    vm.SaveHotel = SaveHotel;
	    vm.DeleteHotel = DeleteHotel;
        
	    dataService.LoadHotels(year)
            .then(function (response) {
                vm.Hotels = response.data;
            });

	    function SaveHotel() {
	        var newHotel = {
	            Id: vm.Id,
	            Name: vm.HotelName,
	            Website: vm.HotelWebsite,
	            Logo: vm.HotelLogos[0],
	            Image: vm.HotelImage[0]
	        };

	        dataService.SaveHotel(vm.Year, newHotel)
                .then(function (response) {
                    vm.Hotels.push(response.data);
                    vm.HotelName = '';
                    vm.HotelWebsite = '';
                    vm.HotelLogos = [];
                    vm.HotelImages = [];
                });
	    }

	    function DeleteHotel(id) {
	        dataService.DeleteHotel(vm.Year, id)
                .then(function (response) {
                    var sponsor = vm.Hotels.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.Hotels.indexOf(sponsor);
                    if (idx < 0)
                        return;

                    vm.Hotels.splice(idx, 1);
                });
	    }
	};

	app.controller("HotelController", ["$http", "$q", "$location", "dataService", hotelController]);
}());