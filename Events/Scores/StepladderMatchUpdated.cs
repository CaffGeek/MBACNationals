using System;

namespace Events.Scores
{
    public class StepladderMatchUpdated
    {
        public Guid Id;
        public Guid TournamentId;
        public string AwayShots;
        public string HomeShots;
        public DateTime Updated;
    }
}