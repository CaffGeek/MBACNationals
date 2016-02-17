"use strict";

var app = angular.module('app', [
    'repeatFilters',
    'ui.router'
]);

app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("");

    $stateProvider
        .state('standings', {
            url: "/standings/{division}",
            templateUrl: "/ClientApp/views/results/standings.html",
            controller: "ResultsController"
        })
        .state('match', {
            url: "/match/{match}",
            templateUrl: "/ClientApp/views/results/match.html",
            controller: "ResultsController"
        })
        .state('team', {
            url: "/team/{team}",
            templateUrl: "/ClientApp/views/results/team.html",
            controller: "ResultsController"
        })
        .state('bowler', {
            url: "/bowler/{bowler}",
            templateUrl: "/ClientApp/views/results/bowler.html",
            controller: "ResultsController"
        });
}]);

angular.module('repeatFilters', [])
    .filter('unique', function () {
        return function (collection, keyname) {
            var output = [],
                keys = [];

            angular.forEach(collection, function (item) {
                var key = item[keyname];
                if (keys.indexOf(key) === -1) {
                    keys.push(key);
                    output.push(item);
                }
            });

            return output;
        };
    });