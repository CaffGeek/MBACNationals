(function () {
    "use strict";

    var newsController = function (dataService) {
        var vm = this;
        var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

        vm.filterByMonth = function (month) {
            return function (value) {
                if (!month)
                    return true;

                var m = monthNames[new Date(value.Created.match(/\d+/)[0] * 1).getMonth()];
                return month == m;
            };
        };

        dataService.LoadNews().then(function (response) {
            vm.News = response.data;

            vm.Months = [];
            angular.forEach(vm.News, function (value) {
                var m = monthNames[new Date(value.Created.match(/\d+/)[0] * 1).getMonth()];
                if (vm.Months.indexOf({ name: m }) < 0)
                    vm.Months.push({ name: m });
            });
        });        
    };

    app.controller("NewsController", ["dataService", "$timeout", newsController]);
}());