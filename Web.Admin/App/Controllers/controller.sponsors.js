(function () {
	"use strict";

	var sponsorsController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.Year = year;
	    vm.SaveSponsor = SaveSponsor;

	    vm.Sponsors = [
            { Id: "", Name: "Burlington Hotel", Website: "http://www.tourismburlington.com/stay/hotels-motels/burlington-hotel-association/" },
            { Id: "", Name: "Holiday Inn", Website: "http://www.hiburlington.ca/" },
            { Id: "", Name: "C4 Centre", Website: "http://www.c4centre.com/" },
            { Id: "", Name: "Meridian", Website: "http://www.meridiancu.ca/find-us/our-locations/Pages/default.aspx?lid=93" },
            { Id: "", Name: "Jack Astors", Website: "http://jackastors.com/location-map/4/" },
            { Id: "", Name: "Sanders", Website: "http://www.PlayWithSanders.ca" },
            { Id: "", Name: "Reddarc", Website: "http://www.red-d-arc.com" },
            { Id: "", Name: "Connectrans", Website: "http://www.connectrans.com/en/" },
            { Id: "", Name: "Boston Pizza", Website: "http://bostonpizza.com" },
            { Id: "", Name: "Tourism Hamilton", Website: "http://www.tourismhamilton.com/" },
            { Id: "", Name: "Hoult", Website: "http://www.houlthellewell.com/" },
            { Id: "", Name: "Gator Teds", Website: "http://www.gatorteds.ca/" },
            { Id: "", Name: "Phipps", Website: "http://www.phippsbowling.com/" },
            { Id: "", Name: "Greens at Renton", Website: "http://www.greensatrenton.com/" },
            { Id: "", Name: "Golf Depot", Website: "http://www.thegolfdepot.ca/" },
            { Id: "", Name: "Sysco", Website: "http://www.sysco.ca/canada/home.cfm?id=2421" },
            { Id: "", Name: "Elmira Bowl", Website: "http://www.elmirabowl.ca/" }
	    ];

	    function SaveSponsor() {
	        for (var i = 0; i < vm.SponsorImages.length; i++) {
	            vm.SponsorImage = vm.SponsorImages[i];

	            dataService.SaveSponsor(vm.Year, {
                    Id: vm.Id,
	                Name: vm.SponsorName,
	                Website: vm.SponsorWebsite,
                    Image: vm.SponsorImage
	            });	            
	        }
	    }
	};

	app.controller("SponsorsController", ["$http", "$q", "$location", "dataService", sponsorsController]);
}());