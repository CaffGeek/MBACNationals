using System;

namespace MBACNationals.Scores.Commands
{
    public class SaveMatchResult
    {
        public Guid Id { get; set; }
        public Team Home { get; set; }
        public Team Away { get; set; }

        public class Team
        {
            public Guid Id { get; set; }
            public Bowler[] Bowlers { get; set; }
        }

        public class Bowler
        {
            public Guid Id { get; set; }
            public int Position { get; set; }
            public int Score { get; set; }
        }
    }
}