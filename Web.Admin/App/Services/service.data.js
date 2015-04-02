(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            RemoveTeam: removeTeam,
            SaveParticipant: saveParticipant,
            AssignParticipantToContingent: assignParticipantToContingent,
            AssignParticipantToTeam: assignParticipantToTeam,
            AssignAlternateToTeam: assignAlternateToTeam,
            AssignCoachToTeam: assignCoachToTeam,
            LoadContingent: loadContingent,
            LoadContingentEvents: loadContingentEvents,
            LoadTeam: loadTeam,
            LoadParticipant: loadParticipant,
            LoadParticipants: loadParticipants,
            LoadAllParticipants: loadAllParticipants,
            AssignParticipantToRoom: assignParticipantToRoom,
            RemoveParticipantFromRoom: removeParticipantFromRoom,
            LoadRooms: loadRooms,
            ChangeRoomType: changeRoomType,
            SaveInstructions: saveInstructions,
            LoadTravelPlans: loadTravelPlans,
            SaveTravelPlans: saveTravelPlans,
            LoadPracticePlan: loadPracticePlan,
            SavePracticePlan: savePracticePlan,
            SaveParticipantProfile: saveParticipantProfile,
            LoadParticipantProfile: loadParticipantProfile,
            LoadProfiles: loadProfiles,
            LoadSchedule: loadSchedule,
            SaveMatchResult: saveMatchResult,
            LoadMatch: loadMatch,
            UseAlternate: useAlternate,
            LoadTournaments: loadTournaments,
            CreateTournament: createTournament
        };

        function saveTeam(team, contingent) {
            //TODO: Use extend
            return $http.post('/Setup/Contingent/CreateTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id,
                Name: team.Name,
                Gender: team.Gender,
                SizeLimit: team.SizeLimit,
                RequiresShirtSize: team.RequiresShirtSize,
                RequiresCoach: team.RequiresCoach,
                RequiresAverage: team.RequiresAverage,
                RequiresBio: team.RequiresBio,
                RequiresGender: team.RequiresGender,
                IncludesSinglesRep: team.IncludesSinglesRep
            });
        }

        function removeTeam(team, contingent) {
            return $http.post('/Setup/Contingent/RemoveTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id
            });
        }

        function saveParticipant(participant) {
            return participant.Id
                ? $http.post('/Setup/Participant/Update', participant)
                : $http.post('/Setup/Participant/Create', participant);
        }

        function assignParticipantToContingent(participant, contingent) {
            return $http.post('/Setup/Contingent/AssignParticipantToContingent', {
                Id: participant.Id,
                ContingentId: contingent.Id
            });
        }

        function assignParticipantToTeam(participant, team) {
            return $http.post('/Setup/Contingent/AssignParticipantToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }

        function assignAlternateToTeam(participant, team) {
            return $http.post('/Setup/Contingent/AssignAlternateToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }

        function assignCoachToTeam(participant, team) {
            return $http.post('/Setup/Contingent/AssignCoachToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }

        function loadContingent(year, province) {
            return $http.get('/Setup/Contingent', {
                params: {
                    year: year,
                    province: province
                }
            });
        };

        function loadContingentEvents(province) {
            return $http.get('/Setup/Contingent/History', {
                params: { province: province }
            });
        };

        function loadTeam(year, province, teamName) {
            return $http.get('/Setup/Contingent/Team', {
                params: {
                    year: year,
                    province: province,
                    teamName: teamName
                }
            });
        };

        function loadParticipant(id) {
            return $http.get('/Setup/Participant', {
                params: { id: id }
            });
        };

        function loadParticipants(year, province) {
            return $http.get('/Setup/Participant/Contingent', {
                params: {
                    year: year,
                    province: province
                }
            });
        };

        function loadAllParticipants() {
            return $http.get('/Setup/Participant/All');
        };

        function assignParticipantToRoom(id, roomNumber) {
            return $http.post('/Setup/Participant/AssignToRoom', {
                Id: id,
                RoomNumber: roomNumber
            });
        };

        function removeParticipantFromRoom(id) {
            return $http.post('/Setup/Participant/RemoveFromRoom', {
                Id: id
            });
        };

        function loadRooms(year, province) {
            return $http.get('/Setup/Contingent/Rooms', {
                params: {
                    year: year,
                    province: province
                }
            });
        };

        function changeRoomType(year, province, roomNumber, type) {
            return $http.post('/Setup/Contingent/ChangeRoomType', {
                Year: year,
                Province: province,
                RoomNumber: roomNumber,
                Type: type
            });
        };

        function loadTravelPlans(year, province) {
            return $http.get('/Setup/Contingent/TravelPlans', {
                params: {
                    year: year,
                    province: province
                }
            });
        };

        function saveTravelPlans(travelPlans) {
            return $http.post('/Setup/Contingent/SaveTravelPlans', travelPlans);
        };

        function loadPracticePlan(year, province) {
            return $http.get('/Setup/Contingent/PracticePlan', {
                params: {
                    year: year,
                    province: province
                }
            });
        };

        function savePracticePlan(practicePlan) {
            return $http.post('/Setup/Contingent/SavePracticePlan', practicePlan);
        };

        function saveInstructions(year, province, instructions) {
            return $http.post('/Setup/Contingent/SaveReservationInstructions', {
                Year: year,
                Province: province,
                Instructions: instructions
            });
        };

        function saveParticipantProfile(participant) {
            return $http.post('/Setup/Participant/Profile', participant);
        };

        function loadParticipantProfile(participant) {
            return $http.get('/Setup/Participant/Profile', {
                params: { id: participant.Id }
            });
        };

        function loadProfiles() {
            return $http.get('/Setup/Participant/Profiles');
        };

        function loadSchedule(year, division) {
            return $http.get('/Setup/Scores/Schedule/' + year + '/' + division);
        };

        function saveMatchResult(match) {
            return $http.post('/Setup/Scores/SaveMatchResult', match);
        };

        function loadMatch(match) {
            return $http.get('/Setup/Scores/Match', {
                params: { matchId: match.Id }
            });
        };

        function useAlternate(participant, team) {
            return $http.post('/Setup/Participant/UseAlternate', {
                Id: participant.Id,
                AlternateId: team.Alternate
            });
        };

        function loadTournaments() {
            return $http.get('/Setup/Tournament/All');
        };

        function createTournament(tournament) {
            return $http.post('/Setup/Tournament/Create', tournament);
        };
    };

    app.factory('dataService', ['$http', dataService]);
}());