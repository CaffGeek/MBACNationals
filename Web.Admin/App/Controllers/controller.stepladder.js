(function () {
    "use strict";

    var stepladderController = function ($http, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var year = url.slice(lastSlash + 1) || new Date().getFullYear();

        var vm = this;

        var page = "list";
        vm.viewUrl = '/Setup/App/Views/Stepladder/' + page + '.html';
                
    };

    app.controller("StepladderController", ["$http", "$location", "dataService", stepladderController]);
}());