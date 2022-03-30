(function () {
    "use strict";
    
    var dataService = function ($http, $location) {
        var url = $location.absUrl();
        var host = $location.host();
        var firstSlash = url.indexOf('/', url.indexOf(host)) + 1;
        var currentYear = url.slice(firstSlash, firstSlash + 4);

        return {
            LoadContingent: loadContingent,
            LoadLaneDraw: loadLaneDraw,
            LoadStandings: loadStandings,
            LoadStepladder: loadStepladder,
            LoadMatch: loadMatch,
            LoadHighScores: loadHighScores,
            LoadHighAverages: loadHighAverages,
            LoadParticipantScores: loadParticipantScores,
            LoadTeamScores: loadTeamScores,
            LoadSponsors: loadSponsors,
            LoadHotels: loadHotels,
            LoadNews: loadNews,
            LoadCentres: loadCentres,
            LoadSchedule: loadSchedule
        };

        function loadContingent(province) {
            return $http.get('/Setup/Contingent', {
                params: { province: province, year: currentYear }
            });
        };

        function loadLaneDraw(division) {
            return $http.get('/Setup/Scores/Schedule', {
                params: { division: division, year: currentYear }
            });
        };

        function loadStandings(division) {
            return $http.get('/Setup/Scores/Standings', {
                params: { division: division, year: currentYear }
            });
        };

        function loadStepladder() {
            return $http.get('/Setup/Scores/StepladderMatches', {
                params: { year: currentYear }
            });
        }

        function loadMatch(matchId) {
            return $http.get('/Setup/Scores/Match', {
                params: { matchId: matchId }
            });
        };

        function loadHighScores(division) {
            return $http.get('/Setup/Scores/HighScores', {
                params: { division: division, year: currentYear }
            });
        };

        function loadHighAverages(division) {
            return $http.get('/Setup/Scores/Averages', {
                params: { division: division, year: currentYear }
            });
        };

        function loadParticipantScores(participantId) {
            return $http.get('/Setup/Scores/Participant', {
                params: { participantId: participantId }
            });
        };

        function loadTeamScores(teamId) {
            return $http.get('/Setup/Scores/Team', {
                params: { teamId: teamId }
            });
        };

        function loadSponsors() {
            return $http.get('/Setup/Sponsors/List/' + currentYear);
        };

        function loadHotels() {
            return $http.get('/Setup/Hotels/List/' + currentYear);
        };

        function loadNews() {
            return $http.get('/Setup/News/List/' + currentYear);
        };

        function loadCentres() {
            return $http.get('/Setup/Centres/List/' + currentYear);
        };

        function loadSchedule() {
            switch (currentYear) {
                case "2017":
                    return $http.get('https://www.googleapis.com/calendar/v3/calendars/smk8ud28p3nbej0iebr5dum4vc@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY');
                case "2018":
                    return $http.get('https://www.googleapis.com/calendar/v3/calendars/01bjjf3a3beq7h3ol9rlu4b648@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY');
                case "2019":
                    return $http.get('https://www.googleapis.com/calendar/v3/calendars/p7vg564jka9r90qrb4n6unp928@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY');
                case "2022":
                    return $http.get('https://www.googleapis.com/calendar/v3/calendars/g5eelbnljr56imc0vkhah828lo@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY');                    
            }            
        };
    };
    
    app.factory('dataService', ['$http', '$location', dataService]);
}());
