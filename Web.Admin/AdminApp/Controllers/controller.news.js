(function () {
	"use strict";

	var newsController = function ($http, $q, $location, dataService) {
	    var url = $location.absUrl();
	    var lastSlash = url.lastIndexOf('/');
	    var year = url.slice(lastSlash + 1);

	    var vm = this;
	    vm.Year = year;
	    vm.SaveNews = SaveNews;
	    vm.DeleteNews = DeleteNews;

	    dataService.LoadNews(year)
            .then(function (response) {
                vm.News = response.data;
            });

	    function SaveNews() {
	        var newsItem = {
	            Id: vm.Id,
	            Title: vm.Title,
	            Content: vm.Content
	        };

	        dataService.SaveNews(vm.Year, newsItem)
                .then(function (response) {
                    vm.News.push(response.data);
                    vm.Title = '';
                    vm.Content = '';
                });
	    };

	    function DeleteNews(id) {
	        dataService.DeleteNews(vm.Year, id)
                .then(function (response) {
                    var news = vm.News.filter(function (x) { return x.Id == id; })[0];
                    var idx = vm.News.indexOf(news);
                    if (idx < 0)
                        return;

                    vm.News.splice(idx, 1);
                });
	    };
	};

	app.controller("NewsController", ["$http", "$q", "$location", "dataService", newsController]);
}());