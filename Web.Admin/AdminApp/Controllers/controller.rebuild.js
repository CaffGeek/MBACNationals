(function () {
    "use strict";

    var rebuildController = function ($http, $q) {
        var vm = this;

        vm.readmodels = [
            { Name: 'CommandQueries' },
            { Name: 'ContingentPracticePlanQueries' },
            { Name: 'ContingentTravelPlanQueries' },
            { Name: 'ContingentViewQueries' },
            { Name: 'HighScoreQueries' },
            { Name: 'MatchQueries' },
            { Name: 'ParticipantProfileQueries' },
            { Name: 'ParticipantQueries' },
            { Name: 'ReservationQueries' },
            { Name: 'ScheduleQueries' },
            { Name: 'StandingQueries' },
            { Name: 'StepladderQueries' },
            { Name: 'TeamScoreQueries' },
            { Name: 'TournamentQueries' },
            { Name: 'ParticipantScoreQueries' }
        ];
        
        vm.Rebuild = function (readmodel) {
            if (readmodel) {
                rebuildModel(readmodel.Name)();
            } else {
                angular.forEach(vm.readmodels, function (model) {
                    model.StartedAt = null;
                    model.CompletedAt = null;
                    model.Status = null;
                });

                rebuildModel('CommandQueries')()
                    .then(rebuildModel('ContingentPracticePlanQueries'))
                    .then(rebuildModel('ContingentTravelPlanQueries'))
                    .then(rebuildModel('ContingentViewQueries'))
                    .then(rebuildModel('HighScoreQueries'))
                    .then(rebuildModel('MatchQueries'))
                    .then(rebuildModel('ParticipantProfileQueries'))
                    .then(rebuildModel('ParticipantQueries'))
                    .then(rebuildModel('ReservationQueries'))
                    .then(rebuildModel('ScheduleQueries'))
                    .then(rebuildModel('StandingQueries'))
                    .then(rebuildModel('StepladderQueries'))
                    .then(rebuildModel('TeamScoreQueries'))
                    .then(rebuildModel('TournamentQueries'))
                    .then(rebuildModel('ParticipantScoreQueries'));
            }
        };

        function rebuildModel(modelName) {
            return function () {
                var model = vm.readmodels.filter(function (x) { return x.Name == modelName; })[0];
                model.StartedAt = new Date();
                model.Status = "Started At " + model.StartedAt.toLocaleTimeString();
                return $http.get('/Setup/Admin/RebuildModel/' + modelName)
                    .then(doneBuilding(modelName));
            }
        }

        function doneBuilding(modelName) {
            return function () {
                var deferred = $q.defer();
                
                var model = vm.readmodels.filter(function (x) { return x.Name == modelName; })[0];
                model.CompletedAt = new Date();
                var seconds = Math.round((model.CompletedAt.getTime() - model.StartedAt.getTime()) / 1000);
                model.Status = "Completed in " + seconds + "s";
                deferred.resolve();
                return deferred.promise;
            };
        }
    };

    app.controller("RebuildController", ["$http", "$q", "$location", "dataService", rebuildController]);
}());