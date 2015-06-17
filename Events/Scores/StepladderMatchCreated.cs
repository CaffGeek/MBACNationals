using MBACNationals;
using System;

namespace Events.Scores
{
    public class StepladderMatchCreated
    {
        public Guid Id;
        public Guid TournamentId;
        public string Year;
        public Guid Away;
        public Guid Home;
        public string AwayBowler;
        public string HomeBowler;
        public string Gender;
        public DateTime Created;
    }
}