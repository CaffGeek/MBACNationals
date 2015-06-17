(function () {
    "use strict";

    var stepladderController = function ($http, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var year = url.slice(lastSlash + 1) || new Date().getFullYear();

        var vm = this;
        vm.createMatch = createMatch;
        vm.updateMatch = updateMatch;
        vm.deleteMatch = deleteMatch;

        var page = "list";
        vm.viewUrl = '/Setup/App/Views/Stepladder/' + page + '.html';

        dataService.LoadAllParticipants(year).then(function (data) {
            var participants = data.data
            vm.Singles = participants.filter(function (x) {
                return x.TeamName && x.TeamName.indexOf('Tournament') > -1 &&
                    x.TeamName.indexOf('Single') > -1;
            });
        });

        vm.Matches = [
            {
                Home: 'bowler1',
                HomeShots: '2112XX2/x5-5r/l/a22xxx', //251
                Away: 'bowler2',
                AwayShots: 'xxx12/x5/x5/x5/x',    //327
                Created: new Date()
            }
        ];
        dataService.GetStepladderMatches(year).then(function (data) {
            vm.Matches = data.data;
        });

        function createMatch(home, away) {
            if (!home || !away || home.Id == away.Id)
                return;

            dataService.CreateStepladderMatch(year, home, away)
                .then(function (response) {
                    vm.Matches.push(response.data);
                });
        };

        function updateMatch(match) {
            dataService.UpdateStepladderMatch(match);
            //TODO: Something on error
        };

        function deleteMatch(match) {
            if (!confirm('Are you sure you want to delete this match between ' + match.HomeName + ' and ' + match.AwayName + '?'))
                return;
            dataService.DeleteStepladderMatch(match)
                .then(function (response) {
                    var matchId = response.data.Id;
                    var match = vm.Matches.filter(function (x) { return x.Id == matchId; })[0];
                    var idx = vm.Matches.indexOf(match);
                    if (idx < 0)
                        return;

                    vm.Matches.splice(idx, 1);
                });
        }
    };

    app.controller("StepladderController", ["$http", "$location", "dataService", stepladderController]);

    app.directive('bowlinggame', ['$parse', function ($compile) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                shots: '='
            },
            template: '<div class="row game">' +
                '   <table class="frame col-xs-1" ng-repeat="frame in game.frames">' +
                '       <tr class="number"><td colspan="3">{{frame.number}}</td></tr>' +
                '       <tr class="shots">' +
                '           <td class="shot">{{frame.shots[0]}}</td>' +
                '           <td class="shot">{{frame.shots[1]}}</td>' +
                '           <td class="shot">{{frame.shots[2]}}</td>' +
                '       </tr>' +
                '       <tr class="score"><td colspan="3">{{frame.runningScore}}</td></tr>' +
                '   </table>' +
                '</div>',
            link: function (scope, element, attrs) {
                scope.$watch('shots', function () {
                    scope.shots = scope.shots.toUpperCase();

                    var shots = [];
                    for (var i = 0; i < scope.shots.length; i++)
                        shots.push(scope.shots[i] === '1' ? scope.shots[i] + scope.shots[++i] : scope.shots[i]);
                    
                    scope.game = { frames: [], score: 0 };

                    var calcShotScore = function (shots, i) {
                        var shot = shots[i];

                        switch (shot) {
                            case 'X': return 15;
                            case 'R': return 13;
                            case 'L': return 13;
                            case 'D': return 12;
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
                    for (var i = 0; i < shots.length && currentFrame.number <= 10; i++) {
                        var shot = shots[i];

                        var shotScore = calcShotScore(shots, i);
                        currentFrame.shots.push(shot);
                        if (currentFrame.number === 10 && shots[i + 1]) currentFrame.shots.push(shots[i + 1]);
                        if (currentFrame.number === 10 && shots[i + 2]) currentFrame.shots.push(shots[i + 2]);
                        
                        currentFrame.score += shotScore;
                                                
                        if (shot === 'X' || currentFrame.number === 10) {
                            if (shots[i + 1]) currentFrame.score += calcShotScore(shots, i + 1); 
                            if (shots[i + 2]) currentFrame.score += calcShotScore(shots, i + 2); 
                        }

                        if (shot === '/' && shots[i + 1])
                            currentFrame.score += calcShotScore(shots, i + 1);

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