(function () {
    "use strict";

    var dataService = function ($http, ngUpload, moment) {
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
            LoadAlternates: loadAlternates,
            AssignParticipantToRoom: assignParticipantToRoom,
            RemoveParticipantFromRoom: removeParticipantFromRoom,
            ReorderTeam: reorderTeam,
            LoadRooms: loadRooms,
            ChangeRoomType: changeRoomType,
            ChangeRoomCheckin: changeRoomCheckin,
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
            CreateTournament: createTournament,
            SaveTournamentSettings: saveTournamentSettings,
            SaveSponsor: saveSponsor,
            LoadSponsors: loadSponsors,
            DeleteSponsor: deleteSponsor,
            ReorderSponsor: reorderSponsor,
            SaveNews: saveNews,
            LoadNews: loadNews,
            DeleteNews: deleteNews,
            CreateStepladderMatch: createStepladderMatch,
            GetStepladderMatches: getStepladderMatches,
            UpdateStepladderMatch: updateStepladderMatch,
            DeleteStepladderMatch: deleteStepladderMatch,
            LoadHotels: loadHotels,
            SaveHotel: saveHotel,
            DeleteHotel: deleteHotel,
            LoadGuestPackages: loadGuestPackages,
            SaveGuestPackages: saveGuestPackages,
            LoadCentres: loadCentres,
            SaveCentre: saveCentre,
            DeleteCentre: deleteCentre
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
            return $http
                .get('/Setup/Participant', {
                    params: { id: id }
                })
                .success(function (data) {
                    data.Birthday = new Date(parseInt(('' + data.Birthday).substr(6)));
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

        function loadAllParticipants(year) {
            return $http.get('/Setup/Participant/All', {
                params: {
                    year: year
                }
            });
        };

        function loadAlternates(year) {
            return $http.get('/Setup/Participant/Alternates', {
                params: {
                    year: year
                }
            });
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

        function reorderTeam(team, bowler, position) {
            return $http.post('/Setup/Participant/ReorderParticipant', {
                Id: bowler.Id,
                TeamId: team.Id,
                Name: bowler.Name,
                QualifyingPosition: position
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

        function changeRoomType(contingentId, province, roomNumber, type) {
            return $http.post('/Setup/Contingent/ChangeRoomType', {
                Id: contingentId,
                Province: province,
                RoomNumber: roomNumber,
                Type: type
            });
        };

        function changeRoomCheckin(contingentId, province, roomNumber, checkin, checkout) {
            var mcheckin = moment.utc(moment(checkin).format('YYYY-MM-DDTHH:mm'));
            var mcheckout = moment.utc(moment(checkout).format('YYYY-MM-DDTHH:mm'));

            return $http.post('/Setup/Contingent/ChangeRoomCheckin', {
                Id: contingentId,
                Province: province,
                RoomNumber: roomNumber,
                Checkin: mcheckin,
                Checkout: mcheckout
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
            angular.forEach(travelPlans.travelPlans, function (travelPlan) {
                var d = moment(travelPlan.When);
                travelPlan.When = moment.utc(d.format('YYYY-MM-DDTHH:mm'));
            });
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

        function saveInstructions(contingentId, province, instructions) {
            return $http.post('/Setup/Contingent/SaveReservationInstructions', {
                Id: contingentId,
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

        function loadProfiles(year) {
            return $http.get('/Setup/Participant/Profiles/', {
                params: {
                    year: year
                }
            });
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

        function saveTournamentSettings(year, tournamentSettings) {
            return $http.post('/Setup/Tournament/Settings/' + year, tournamentSettings);
        };

        function saveSponsor(year, sponsor) {
            return ngUpload.upload({
                url: '/Setup/Sponsors/Save/' + year,
                fields: { id: sponsor.Id, name: sponsor.Name, website: sponsor.Website },
                file: sponsor.Image
            });
        };

        function loadSponsors(year) {
            return $http.get('/Setup/Sponsors/List/' + year);
        };

        function deleteSponsor(year, id) {
            return $http.post('/Setup/Sponsors/Delete/' + year, {
                id: id
            });
        };

        function reorderSponsor(year, sponsor, position) {
            return $http.post('/Setup/Sponsors/Reorder/' + year, {
                Id: sponsor.Id,
                Name: sponsor.Name,
                Position: position
            });
        };

        function saveNews(year, news) {
            return $http.post('/Setup/News/Save/' + year, news);
        };

        function loadNews(year) {
            return $http.post('/Setup/News/List/' + year);
        };

        function deleteNews(year, id) {
            return $http.post('/Setup/News/Delete/' + year, {
                id: id
            });
        };

        function createStepladderMatch(year, home, away) {
            return $http.post('/Setup/Scores/CreateStepladderMatch/' + year, {
                Year: year,
                HomeBowlerId: home.Id,
                AwayBowlerId: away.Id,
            });
        };

        function getStepladderMatches(year) {
            return $http.get('/Setup/Scores/StepladderMatches/' + year);
        };

        function updateStepladderMatch(match) {
            return $http.post('/Setup/Scores/UpdateStepladderMatch', match);
        };

        function deleteStepladderMatch(match) {
            return $http.post('/Setup/Scores/DeleteStepladderMatch/', match);
        };

        function loadHotels(year) {
            return $http.get('/Setup/Hotels/List/' + year);
        };

        function saveHotel(year, hotel) {
            var d1 = moment(hotel.DefaultCheckin);
            hotel.DefaultCheckin = moment.utc(d1.format('YYYY-MM-DD')).format('YYYY-MM-DD');

            var d2 = moment(hotel.DefaultCheckout);
            hotel.DefaultCheckout = moment.utc(d2.format('YYYY-MM-DD')).format('YYYY-MM-DD');

            return ngUpload.upload({
                url: '/Setup/Hotels/Save/' + year,
                fields: {
                    id: hotel.Id,
                    name: hotel.Name,
                    website: hotel.Website,
                    phonenumber: hotel.PhoneNumber,
                    checkin: hotel.DefaultCheckin,
                    checkout: hotel.DefaultCheckout,
                    roomtypes: hotel.RoomTypes
                },
                file: [hotel.Logo, hotel.Image]
            });
        };

        function deleteHotel(year, id) {
            return $http.post('/Setup/Hotels/Delete/' + year, {
                id: id
            });
        };

        function loadGuestPackages(year) {
            return $http.get('/Setup/GuestPackages/List/' + year);
        };

        function saveGuestPackages(year, guestpackages) {
            return $http.post('/Setup/GuestPackages/Save/' + year, {
                Year: year,
                GuestPackages: guestpackages
            });
        };

        function loadCentres(year) {
            return $http.get('/Setup/Centres/List/' + year);
        };

        function saveCentre(year, centre) {
            return ngUpload.upload({
                url: '/Setup/Centres/Save/' + year,
                fields: {
                    id: centre.Id,
                    name: centre.Name,
                    website: centre.Website,
                    phonenumber: centre.PhoneNumber,
                    address: centre.Address
                },
                file: [centre.Image]
            });
        };

        function deleteCentre(year, id) {
            return $http.post('/Setup/Centres/Delete/' + year, {
                id: id
            });
        };
    };

    app.factory('dataService', ['$http', 'Upload', 'moment', dataService]);
}());