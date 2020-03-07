"use strict";

var app = angular.module('app', [
    'repeatFilters',
    'ui.router',
    'ngSanitize'
]);

app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("");

    $stateProvider
        .state('contingents', {
            url: "/contingents/{province}",
            templateUrl: "/ClientApp/views/contingents.html",
            controller: "ContingentController"
        })
        .state('standings', {
            url: "/standings/{division}",
            templateUrl: "/ClientApp/views/results/standings.html",
            controller: "ResultsController"
        })
        .state('highscores', {
            url: "/highscores/{division}",
            templateUrl: "/ClientApp/views/results/highscores.html",
            controller: "HighscoresController"
        })
        .state('highpoa', {
            url: "/highpoa/{division}",
            templateUrl: "/ClientApp/views/results/highpoa.html",
            controller: "HighscoresController"
        })
        .state('highaverage', {
            url: "/highaverage/{division}",
            templateUrl: "/ClientApp/views/results/highaverage.html",
            controller: "HighscoresController"
        })
        .state('highwins', {
            url: "/highwins/{division}",
            templateUrl: "/ClientApp/views/results/highwins.html",
            controller: "HighscoresController"
        })
        .state('highbygame', {
            url: "/highbygame/{division}",
            templateUrl: "/ClientApp/views/results/highscoresbygame.html",
            controller: "HighscoresController"
        })
        .state('stepladder', {
            url: "/stepladder/{year}",
            templateUrl: "/ClientApp/views/results/stepladder.html",
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