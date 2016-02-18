(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            LoadContingent: loadContingent,
            LoadLaneDraw: loadLaneDraw,
            LoadStandings: loadStandings,
            LoadMatch: loadMatch,
            LoadHighScores: loadHighScores,
            LoadParticipantScores: loadParticipantScores,
            LoadTeamScores: loadTeamScores
        };

        function loadContingent(province) {
            return $http.get('/Setup/Contingent', {
                params: { province: province }
            });
        };

        function loadLaneDraw(division) {
            return $http.get('/Setup/Scores/Schedule', {
                params: { division: division }
            });
        };

        function loadStandings(division) {
            return $http.get('/Setup/Scores/Standings', {
                params: { division: division }
            });
        };

        function loadMatch(matchId) {
            return $http.get('/Setup/Scores/Match', {
                params: { matchId: matchId }
            });
        };

        function loadHighScores(division) {
            return $http.get('/Setup/Scores/HighScores', {
                params: { division: division }
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
    };

    app.factory('dataService', ['$http', dataService]);
}());