using System;

namespace Events.Participant
{
    public class ParticipantAverageChanged
    {
        public Guid Id;
        public int LeaguePinfall;
        public int LeagueGames;
        public int TournamentPinfall;
        public int TournamentGames;
        public int Average;
    }
}
