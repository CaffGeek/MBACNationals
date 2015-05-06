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
            return $http.get('http://mbacnationals.com/Setup/Contingent', {
                params: { province: province, year: 2015 }
            });
        };

        function loadLaneDraw(division) {
            return $http.get('http://localhost:60827/Setup/Scores/Schedule', {
                params: { division: division, year: 2015 }
            });
        };

        function loadStandings(division) {
            return $http.get('http://mbacnationals.com/Setup/Scores/Standings', {
                params: { division: division, year: 2015 }
            });
        };

        function loadMatch(matchId) {
            return $http.get('http://mbacnationals.com/Setup/Scores/Match', {
                params: { matchId: matchId }
            });
        };

        function loadHighScores(division) {
            return $http.get('http://mbacnationals.com/Setup/Scores/HighScores', {
                params: { division: division, year: 2015 }
            });
        };

        function loadParticipantScores(participantId) {
            return $http.get('http://mbacnationals.com/Setup/Scores/Participant', {
                params: { participantId: participantId }
            });
        };

        function loadTeamScores(teamId) {
            return $http.get('http://mbacnationals.com/Setup/Scores/Team', {
                params: { teamId: teamId }
            });
        };
    };

    app.factory('dataService', ['$http', dataService]);
}());