(function () {
    "use strict";

    var highscoresController = function ($scope, $http, dataService) {
        $scope.model = {};

        $scope.model.HighScores = $scope.model.HighScores || [];
        $scope.model.HighAverages = $scope.model.Averages || [];

        //TODO: Loop
        dataService.LoadHighScores('Tournament').then(function (data) {
            $scope.model.HighScores['Tournament'] = data.data;
        });

        dataService.LoadHighScores('Teaching').then(function (data) {
            $scope.model.HighScores['Teaching'] = data.data;
        });

        dataService.LoadHighScores('Senior').then(function (data) {
            $scope.model.HighScores['Senior'] = data.data;
        });

        dataService.LoadHighAverages('Tournament').then(function (data) {
            $scope.model.HighAverages['Tournament'] = data.data;
        });

        dataService.LoadHighAverages('Teaching').then(function (data) {
            $scope.model.HighAverages['Teaching'] = data.data;
        });

        dataService.LoadHighAverages('Senior').then(function (data) {
            $scope.model.HighAverages['Senior'] = data.data;
        });
        

        $scope.getRange = function (from, to) {
            var result = [];

            for (var i = from; i <= to; i++)
                result.push(i);

            return result;
        };
    };

    app.controller("HighscoresController", ["$scope", "$http", "dataService", highscoresController]);
}());