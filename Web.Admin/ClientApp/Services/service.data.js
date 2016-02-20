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
            LoadParticipantScores: loadParticipantScores,
            LoadTeamScores: loadTeamScores,
            LoadSponsors: loadSponsors,
            LoadNews: loadNews
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

        function loadNews() {
            return $http.get('/Setup/News/List/' + currentYear);
        };
    };
    
    app.factory('dataService', ['$http', '$location', dataService]);
}());
