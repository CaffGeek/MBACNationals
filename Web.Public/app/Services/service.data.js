(function () {
    "use strict";
    
    var dataService = function ($http, providerUrl) {
        return {
            LoadContingent: loadContingent,
            LoadLaneDraw: loadLaneDraw,
            LoadStandings: loadStandings,
            LoadMatch: loadMatch,
            LoadHighScores: loadHighScores,
            LoadParticipantScores: loadParticipantScores,
            LoadTeamScores: loadTeamScores,
            LoadSponsors: loadSponsors
        };

        function loadContingent(province) {
            return $http.get(providerUrl + '/Setup/Contingent', {
                params: { province: province, year: 2015 }
            });
        };

        function loadLaneDraw(division) {
            return $http.get(providerUrl + '/Setup/Scores/Schedule', {
                params: { division: division, year: 2015 }
            });
        };

        function loadStandings(division) {
            return $http.get(providerUrl + '/Setup/Scores/Standings', {
                params: { division: division, year: 2015 }
            });
        };

        function loadMatch(matchId) {
            return $http.get(providerUrl + '/Setup/Scores/Match', {
                params: { matchId: matchId }
            });
        };

        function loadHighScores(division) {
            return $http.get(providerUrl + '/Setup/Scores/HighScores', {
                params: { division: division, year: 2015 }
            });
        };

        function loadParticipantScores(participantId) {
            return $http.get(providerUrl + '/Setup/Scores/Participant', {
                params: { participantId: participantId }
            });
        };

        function loadTeamScores(teamId) {
            return $http.get(providerUrl + '/Setup/Scores/Team', {
                params: { teamId: teamId }
            });
        };

        function loadSponsors() {
            return $http.get(providerUrl + '/Setup/Sponsors/List/2015');
        };
    };
    
    app.factory('providerUrl', ['$location', function providerUrlFactory(location) {
        var absUrl = location.absUrl();

        if (absUrl.indexOf('localhost') > 0)
            return 'http://localhost:60827';
        else
            return 'http://mbacnationals.com';
    }]);

    app.factory('dataService', ['$http', 'providerUrl', dataService]);
}());
