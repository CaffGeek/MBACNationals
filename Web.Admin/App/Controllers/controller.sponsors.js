(function () {
	"use strict";

	var sponsorsController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.Year = year;
	    vm.SaveSponsor = SaveSponsor;

	    //    "Burlington Hotel", Website: "http://www.tourismburlington.com/stay/hotels-motels/burlington-hotel-association/" },
        //    "Holiday Inn", Website: "http://www.hiburlington.ca/" },
        //    "C4 Centre", Website: "http://www.c4centre.com/" },
	    //    "Meridian", Website: "http://www.meridiancu.ca/find-us/our-locations/Pages/default.aspx?lid=93" },
        //    "Jack Astors", Website: "http://jackastors.com/location-map/4/" },

        //    "Sanders", Website: "http://www.PlayWithSanders.ca" },
        //    "Reddarc", Website: "http://www.red-d-arc.com" },
        //    "Connectrans", Website: "http://www.connectrans.com/en/" },
        //    "Boston Pizza", Website: "http://bostonpizza.com" },
        //    "Tourism Hamilton", Website: "http://www.tourismhamilton.com/" },
        //    "Hoult", Website: "http://www.houlthellewell.com/" },
        //    "Gator Teds", Website: "http://www.gatorteds.ca/" },
        //    "Phipps", Website: "http://www.phippsbowling.com/" },
        //    "Greens at Renton", Website: "http://www.greensatrenton.com/" },
        //    "Golf Depot", Website: "http://www.thegolfdepot.ca/" },
        //    "Sysco", Website: "http://www.sysco.ca/canada/home.cfm?id=2421" },
        //    "Elmira Bowl", Website: "http://www.elmirabowl.ca/" }

	    dataService.LoadSponsors(year)
            .then(function (response) {
                vm.Sponsors = response.data;
            });

	    function SaveSponsor() {
	        for (var i = 0; i < vm.SponsorImages.length; i++) {
	            vm.SponsorImage = vm.SponsorImages[i];

	            var newSponsor = {
	                Id: vm.Id,
	                Name: vm.SponsorName,
	                Website: vm.SponsorWebsite,
	                Image: vm.SponsorImage
	            };

	            dataService.SaveSponsor(vm.Year, newSponsor)
                    .then(function (response) {
                        vm.Sponsors.push(response.data);
                        vm.SponsorName = '';
                        vm.SponsorWebsite = '';
                        vm.SponsorImage = '';
                    });
	        }
	    }
	};

	app.controller("SponsorsController", ["$http", "$q", "$location", "dataService", sponsorsController]);
}());