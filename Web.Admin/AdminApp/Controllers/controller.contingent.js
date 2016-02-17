(function () {
    "use strict";

    var contingentController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);
        
        $scope.viewUrl = '/AdminApp/Views/Contingent.html';

        $scope.model = {
            Teams: [],
            Guests: []
        };

        if (province) {
            dataService.LoadContingent(year, province).
                success(function (contingent) {
                    $scope.model = contingent;

                    if (!$scope.model.Teams.length) {
                        editDivisions($scope.model.Teams);
                    }
                }); 
        }

        $scope.getAllContingentMembers = getAllContingentMembers;
        $scope.editDivisions = editDivisions;
        $scope.editTeam = editTeam;
        $scope.editParticipant = editParticipant;
        $scope.assignAlternateToTeam = assignAlternateToTeam;
        $scope.editAlternate = editAlternate;
        $scope.addGuest = addGuest;
        $scope.isTeam = isTeam;
        $scope.loadHistory = loadHistory;

        function isTeam(x) {
            return x.SizeLimit > 1;
        }
        
        function editDivisions(teams) {
            var modalPromise = modalFactory.Divisions(teams);

            modalPromise.then(function (data) {
                //var currentTeams = $scope.model.Teams;
                //TODO: $scope.model.Teams = $.extend(true, [], currentTeams, data);
                
                //Remove division
                angular.forEach($scope.model.Teams, function (team, idx) {
                    if (data.filter(function (obj) { return obj.Name == team.Name; }).length === 0)
                    {
                        dataService.RemoveTeam(team, $scope.model); //Fire and forget
                        $scope.model.Teams.splice(idx);
                    }
                });

                angular.forEach(data, function (team, idx) {
                    if ($scope.model.Teams.filter(function (obj) { return obj.Name == team.Name; }).length === 0) {
                        $scope.model.Teams.push(team);
                    }
                });

                var saveTeamPromises = [];
                angular.forEach($scope.model.Teams, function (team) {
                    var saveTeamPromise = dataService.SaveTeam(team, $scope.model).then(function (data) {
                        team.Id = data.data.TeamId;

                        team.Bowlers = team.Bowlers || [];
                        while (team.Bowlers.length < team.SizeLimit) {
                            team.Bowlers.push({ Gender: team.Gender });
                        }
                    });

                    saveTeamPromises.push(saveTeamPromise);
                });

                $q.all(saveTeamPromises).then(function (data) {
                    editTeamModal(0);  //TODO: Only open new teams for editing
                });

                var editTeamModal = function (i) {
                    if (i >= $scope.model.Teams.length)
                        return;

                    var team = $scope.model.Teams[i];
                    if (!(team && team.Bowlers && team.Bowlers.length && team.Bowlers[0].Id)) {
                        editTeam(team).then(function () {
                            editTeamModal(i + 1);
                        });
                    } else {
                        editTeamModal(i + 1);
                    }
                };
            });
        };

        function editTeam(team) {
            var dfd = $q.defer();
            
            team.Bowlers = team.Bowlers || [];

            var editBowlerModal = function (i) {
                if (i >= team.SizeLimit)
                {
                    dfd.resolve();
                    return;
                }

                var participant = team.Bowlers[i];
                if (!participant) {
                    participant = { Gender: team.Gender }; //TODO: Use a Factory
                    team.Bowlers.push(participant);
                }

                var modalPromise = editParticipant(participant, team);
                modalPromise.then(function (data) {
                    editBowlerModal(i + 1);
                });
            };

            var modalPromise;
            if (team.RequiresCoach) {
                team.Coach = team.Coach || { IsCoach: true };
                modalPromise = editParticipant(team.Coach, team);
            }

            if (modalPromise)
                modalPromise.then(function () {
                    editBowlerModal(0);
                });
            else
                editBowlerModal(0);
            
            return dfd.promise;
        };

        function editParticipant(participant, team) {
            return dataService.LoadParticipant(participant.Id).then(function (data) {
                return modalFactory.Participant(year, data.data || participant, team);
            }).then(function (data) {
                participant.Name = data.Name;
                participant.IsDelegate = data.IsDelegate;
                participant.IsRookie = data.YearsQualifying == 1;
                participant.IsGuest = data.IsGuest;
            });
        };

        function assignAlternateToTeam(newAlternate, team) {
            var possibilities = getAllContingentMembers();
            var participant = possibilities.filter(function (obj) { return obj && obj.Id == newAlternate; })[0];
            return dataService.AssignAlternateToTeam(participant, team);
        };

        function editAlternate(alternateId, team) {
            var possibilities = getAllContingentMembers();
            var participant = possibilities.filter(function (obj) { return obj && obj.Id == alternateId; })[0];
            return editParticipant(participant, team);
        };

        function getAllContingentMembers() {
            var possibilities = [];

            possibilities = possibilities.concat($scope.model.Guests);
            angular.forEach($scope.model.Teams, function (team, idx) {
                possibilities = possibilities.concat(team.Bowlers);
                possibilities.push(team.Coach);
            });

            return possibilities.filter(function (obj) { return obj && obj.Name; });
        }

        function addGuest() {
            return modalFactory.Participant(year, { IsGuest: true }).then(function (data) {
                $scope.model.Guests.push(data);
                dataService.AssignParticipantToContingent(data, $scope.model);
            });
        };

        function loadHistory() {
            dataService.LoadContingentEvents(province).then(function (data) {
                $scope.model.Events = data.data;
            });
        }
    };

    app.controller("ContingentController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", contingentController]);
}());