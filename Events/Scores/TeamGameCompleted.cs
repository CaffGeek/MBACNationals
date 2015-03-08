namespace Events.Scores
{
    public class TeamGameCompleted
    {
        public System.Guid Id;
        public System.Guid TeamId;
        public string Division;
        public string Contingent;
        public string Opponent;
        public int Number;
        public int Score;
        public int POA;
        public decimal Points;
        public decimal TotalPoints;
        public int Lane;
        public string Centre;
        public bool IsPOA;
        public int OpponentScore;
        public int OpponentPOA;
        public decimal OpponentPoints;
    }
}