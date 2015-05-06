using MBACNationals;
using System;

namespace Events.Scores
{
    public class MatchCreated
    {
        public Guid Id;
        public Guid TournamentId;
        public string Year;
        public string Division;
        public bool IsPOA;
        public int Number;
        public string Away;
        public string Home;
        public int Lane;
        public BowlingCentre Centre;
        public string CentreName;
    }
}