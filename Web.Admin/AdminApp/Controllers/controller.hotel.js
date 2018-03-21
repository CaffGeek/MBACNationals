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
        vm.AddRoomType = addRoomType;

        vm.DefaultCheckin = new Date(new Date().getFullYear(), 5, 30);
        vm.DefaultCheckout = new Date(new Date().getFullYear(), 6, 5); 
        
	    dataService.LoadHotels(year)
            .then(function (response) {
                angular.forEach(response.data, function (hotel) {
                    var d1 = new Date(hotel.DefaultCheckin);
                    var d2 = new Date(hotel.DefaultCheckout);

                    //UGH, such a hack
                    hotel.DefaultCheckin = new Date(d1.getUTCFullYear(), d1.getUTCMonth(), d1.getUTCDate());
                    hotel.DefaultCheckout = new Date(d2.getUTCFullYear(), d2.getUTCMonth(), d2.getUTCDate());
                });

                vm.Hotels = response.data;
            });

	    function SaveHotel() {
	        var newHotel = {
	            Id: vm.Id,
	            Name: vm.HotelName,
	            Website: vm.HotelWebsite,
                PhoneNumber: vm.HotelPhoneNumber,
                DefaultCheckin: vm.DefaultCheckin,
                DefaultCheckout: vm.DefaultCheckout,
                RoomTypes: vm.RoomTypes,
	            Logo: vm.HotelLogos[0],
	            Image: vm.HotelImages[0]
	        };

	        dataService.SaveHotel(vm.Year, newHotel)
                .then(function (response) {
                    vm.Hotels.push(response.data);
                    vm.HotelName = '';
                    vm.HotelWebsite = '';
                    vm.HotelPhoneNumber = '';
                    vm.DefaultCheckin = new Date(new Date().getFullYear(), 5, 30);
                    vm.DefaultCheckout = new Date(new Date().getFullYear(), 6, 5); 
                    vm.RoomTypes = [];
                    vm.HotelLogos = [];
                    vm.HotelImages = [];
                });
	    }

	    function DeleteHotel(id) {
	        dataService.DeleteHotel(vm.Year, id)
                .then(function (response) {
                    var hotel = vm.Hotels.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.Hotels.indexOf(hotel);
                    if (idx < 0)
                        return;

                    vm.Hotels.splice(idx, 1);
                });
	    }

	    function addRoomType() {
	        vm.RoomTypes = vm.RoomTypes || [];
	        vm.RoomTypes.push({description:""});
	    }
	};

	app.controller("HotelController", ["$http", "$q", "$location", "dataService", hotelController]);
}());