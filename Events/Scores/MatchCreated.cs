using MBACNationals;

namespace Events.Scores
{
    public class MatchCreated
    {
        public System.Guid Id;
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
