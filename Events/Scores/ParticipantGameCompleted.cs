 namespace Events.Scores
{
    public class ParticipantGameCompleted
    {
        public System.Guid Id;
        public System.Guid ParticipantId;
        public string Division;
        public string Contingent;
        public int Number;
        public string Name;
        public string Gender;
        public int Score;
        public int Position;
        public int POA;
        public decimal Points;
        public int Lane;
        public string Centre;
        public string Opponent;
        public string OpponentName;
        public int OpponentScore;
        public int OpponentPOA;
        public bool IsPOA;
    }
}
