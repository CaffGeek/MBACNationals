(function () {
	"use strict";

	var sponsorsController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.Year = year;
        vm.SaveSponsor = SaveSponsor;
        vm.DeleteSponsor = DeleteSponsor;
        vm.MoveSponsor = MoveSponsor;

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
                        vm.SponsorImages = [];
                    });
	        }
	    }

	    function DeleteSponsor(id) {
	        dataService.DeleteSponsor(vm.Year, id)
                .then(function (response) {
                    var sponsor = vm.Sponsors.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.Sponsors.indexOf(sponsor);
                    if (idx < 0)
                        return;

                    vm.Sponsors.splice(idx, 1);
                });
        }

        function MoveSponsor(sponsors, index) {
            sponsors.splice(index, 1);

            angular.forEach(sponsors, function (sponsor, index) {
                console.log(index + sponsor.Name);

                dataService.ReorderSponsor(vm.Year, sponsor, index + 1).then(function (data) {
                    bowler.QualifyingPosition = data.data.QualifyingPosition;
                });
            });            
        }
	};

	app.controller("SponsorsController", ["$http", "$q", "$location", "dataService", sponsorsController]);
}());