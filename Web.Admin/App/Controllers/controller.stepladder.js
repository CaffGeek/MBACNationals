(function () {
    "use strict";

    var stepladderController = function ($http, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var year = url.slice(lastSlash + 1) || new Date().getFullYear();

        var vm = this;

        var page = "list";
        vm.viewUrl = '/Setup/App/Views/Stepladder/' + page + '.html';
        
        vm.Matches = [
            {
                Home: 'bowler1',
                HomeShots: 'XXX2/x5-5r/l/a22xxx', //281
                Away: 'bowler2',
                AwayShots: 'xxxxxxxxxxxxxx', //450
                //AwayShots: 'x5/x5/x5/x5/x5/x',    //300
                Created: new Date()
            }
        ];


    };

    app.controller("StepladderController", ["$http", "$location", "dataService", stepladderController]);

    app.directive('bowlinggame', ['$parse', function ($compile) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                name: '=',
                shots: '='
            },
            template: '<div class="row game">' +
                '   <div class="col-sm-2 name">{{name}}</div>' +
                '   <div class="col-sm-10 score">' +
                '       <table class="frame col-sm-1" ng-repeat="frame in game.frames">' +
                '           <tr class="number"><td colspan="3">{{frame.number}}</td></tr>' +
                '           <tr class="shots">' +
                '               <td class="shot">{{frame.shots[0]}}</td>' +
                '               <td class="shot">{{frame.shots[1]}}</td>' +
                '               <td class="shot">{{frame.shots[2]}}</td>' +
                '           </tr>' +
                '           <tr class="score"><td colspan="3">{{frame.runningScore}}</td></tr>' +
                '       </table>' +
                '       <div class="col-sm-1">' +
                '           <div class="row">Score</div>' +
                '           <div class="row">{{game.score}}</div>' +
                '       </div>' +
                '   </div>' +
                '</div>',
            link: function (scope, element, attrs) {
                scope.$watch('shots', function() {
                    scope.shots = scope.shots.toUpperCase();

                    //TODO: Translate shots into a nice game object with frames...and score properly
                    //TODO: Update template to use that nice game object
                    scope.game = { frames: [], score: 0 };

                    var calcShotScore = function (shots, i) {
                        var shot = shots[i];

                        switch (shot) {
                            case 'X': return 15;
                            case 'R': return 13;
                            case 'L': return 13;
                            case 'A': return 11;
                            case 'C': return 10;
                            case 'S': return 8;
                            case 'H': return 5;
                            case '-': return 0;
                            case '/': return 15 - calcShotScore(shots, i - 1)
                            default: return shot * 1;
                        }
                    };

                    var currentFrame = { number: 1, shots: [], score: 0 };
                    scope.game.frames.push(currentFrame);
                    for (var i = 0; i < scope.shots.length && currentFrame.number <= 10; i++) {
                        var shot = scope.shots[i];
                        var shotScore = calcShotScore(scope.shots, i);
                        currentFrame.shots.push(shot);
                        if (currentFrame.number === 10 && scope.shots[i + 1]) currentFrame.shots.push(scope.shots[i + 1]);
                        if (currentFrame.number === 10 && scope.shots[i + 2]) currentFrame.shots.push(scope.shots[i + 2]);
                        
                        currentFrame.score += shotScore;
                                                
                        if (shot === 'x' || currentFrame.number === 10) {
                            if (scope.shots[i + 1]) currentFrame.score += calcShotScore(scope.shots, i + 1);
                            if (scope.shots[i + 2]) currentFrame.score += calcShotScore(scope.shots, i + 2);
                        }

                        if (shot === '/' && scope.shots[i + 1])
                            currentFrame.score += calcShotScore(scope.shots, i + 1);

                        if (currentFrame.shots.length === 3 || currentFrame.score >= 15) {
                            scope.game.score += currentFrame.score;
                            currentFrame.runningScore = scope.game.score;

                            if (currentFrame.number === 10)
                                break;

                            currentFrame = { number: currentFrame.number + 1, shots: [], score: 0 };
                            scope.game.frames.push(currentFrame);
                        }
                    }
                });
            }
        };
    }]);
}());