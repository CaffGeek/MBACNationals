using Edument.CQRS;
using Events.Scores;
using System;

namespace MBACNationals.Scores
{
    public class MatchAggregate : Aggregate,
        IApplyEvent<MatchCreated>,
        IApplyEvent<MatchCompleted>,
        IApplyEvent<TeamGameCompleted>,
        IApplyEvent<ParticipantGameCompleted>
    {
        public string Division { get; private set; }
        public bool IsPOA { get; private set; }
        public int Number { get; private set; }
        public string Away { get; private set; }
        public string Home { get; private set; }
        public int Lane { get; private set; }
        public BowlingCentre Centre { get; private set; }
        public string CentreName { get; private set; }
        public bool IsComplete { get; private set; }

        public void Apply(MatchCompleted e)
        {
            IsComplete = true;
        }

        public void Apply(MatchCreated e)
        {
            Division = e.Division;
            IsPOA = e.IsPOA;
            Home = e.Home;
            Away = e.Away;
            Centre = e.Centre;
            CentreName = e.CentreName;
            Lane = e.Lane;
            Number = e.Number;
            IsComplete = false;
        }

        public void Apply(TeamGameCompleted e)
        {
            //TODO:
        }

        public void Apply(ParticipantGameCompleted e)
        {
            //TODO:
        }
    }
}
