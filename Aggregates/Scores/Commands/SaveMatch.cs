using System;
using System.Collections.Generic;

namespace MBACNationals.Scores.Commands
{
    public class SaveMatch
    {
        public Guid Id { get; private set; }
        public string Division { get; private set; }
        public bool IsPOA { get; private set; }
        public int Number { get; private set; }
        public string Away { get; private set; }
        public string Home { get; private set; }
        public int Lane { get; private set; }
        public BowlingCentre Centre { get; private set; }
        public string CentreName { get; private set; }

        public SaveMatch(Guid guid, string division, int number, string away, string home, int lane, BowlingCentre centre, bool isPOA = false)
        {
            Id = guid;
            Division = division;
            IsPOA = isPOA;
            Number = number;
            Away = away;
            Home = home;
            Lane = lane;
            Centre = centre;
            CentreName = centre.ToString();
        }
    }
}
