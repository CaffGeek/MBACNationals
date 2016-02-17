(function () {
    "use strict";

    var resultsController = function ($scope, $http, dataService, $stateParams) {
        $scope.model = {};

        $scope.minGame = 1;
        $scope.maxGame = 7;
        $scope.viewStandings = viewStandings;
        $scope.viewMatch = viewMatch;
        $scope.viewTeam = viewTeam;
        $scope.viewBowler = viewBowler;
        $scope.viewStepladder = viewStepladder;

        $scope.findMatchByNumber = findMatchByNumber;

        $scope.model.Division = $stateParams.division;
        $scope.model.MatchId = $stateParams.match;
        $scope.model.TeamId = $stateParams.team;
        $scope.model.BowlerId = $stateParams.bowler;

        if ($scope.model.MatchId)
            viewMatch();
        else if ($scope.model.BowlerId)
            viewBowler();
        else if ($scope.model.TeamId)
            viewTeam();
        else //if ($scope.model.Division)
            viewStandings();
                
        function viewStandings() {
            var division = $scope.model.Division || 'Tournament Men Single';
            dataService.LoadStandings($scope.model.Division).then(function (data) {
                $scope.model = data.data;
                $scope.model.Division = division;
            });
        };

        function viewStepladder() {
            var x = new Date();
            $scope.viewUrl = '/app/Views/Results/Stepladder.html?x='+x.toString();

            dataService.LoadStepladder().then(function (data) {
                $scope.model = data.data;
            });
        }

        function viewMatch() {
            dataService.LoadMatch($scope.model.MatchId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function viewTeam(team) {
            //$scope.viewUrl = '/app/Views/Results/Team.html';
            //$scope.model.TeamId = team.TeamId || team.Id;

            dataService.LoadTeamScores($scope.model.TeamId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function viewBowler(bowler) {
            //$scope.viewUrl = '/app/Views/Results/Bowler.html';
            //$scope.model.BowlerId = bowler.BowlerId || bowler.Id;

            dataService.LoadParticipantScores($scope.model.BowlerId).then(function (data) {
                $scope.model = data.data;
            });
        };

        function findMatchByNumber(team, number) {
            var match = ($.grep(team.Matches, function (o) { return o.Number == number; }) || [])[0];
            return match;
        };

        $scope.getRange = function (from, to) {
            var result = [];

            for (var i = from; i <= to; i++)
                result.push(i);

            return result;
        };
    };

    app.controller("ResultsController", ["$scope", "$http", "dataService", "$stateParams", resultsController]);

    app.directive('bowlinggame', ['$parse', function ($compile) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                shots: '='
            },
            template: '<div class="game">' +
                '   <table class="frame" ng-repeat="frame in game.frames">' +
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
                    scope.game = { frames: [], score: 0, fouls: 0 };

                    var shots = [];
                    for (var i = 0; i < scope.shots.length; i++) {
                        if (scope.shots[i] === '1') {
                            shots.push(scope.shots[i] + scope.shots[++i]);
                        } else if (scope.shots[i] === 'F') {
                            scope.game.fouls++;
                        } else {
                            shots.push(scope.shots[i]);
                        }
                    }

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

                        currentFrame.score += shotScore;

                        if (shot === 'X' && currentFrame.number != 10) {
                            if (shots[i + 1])
                                currentFrame.score += calcShotScore(shots, i + 1);
                            if (shots[i + 2])
                                currentFrame.score += calcShotScore(shots, i + 2);
                        }

                        if (shot === '/' && shots[i + 1] && currentFrame.number != 10) {
                            currentFrame.score += calcShotScore(shots, i + 1);
                        }

                        if (currentFrame.shots.length === 3 || (currentFrame.score >= 15 && currentFrame.number != 10)) {
                            scope.game.score += currentFrame.score;
                            currentFrame.runningScore = scope.game.score;

                            if (currentFrame.number !== 10) {
                                currentFrame = { number: currentFrame.number + 1, shots: [], score: 0 };
                                scope.game.frames.push(currentFrame);
                            }
                        }
                    }
                });
            }
        };
    }]);
}());
