using Edument.CQRS;
using Events.Scores;
using System;

namespace MBACNationals.Scores
{
    public class StepladderMatchAggregate : Aggregate,
        IApplyEvent<StepladderMatchCreated>,
        IApplyEvent<StepladderMatchUpdated>,
        IApplyEvent<StepladderMatchDeleted>
    {
        public Guid TournamentId { get; private set; }
        public string Year { get; private set; }
        public Guid Away { get; private set; }
        public Guid Home { get; private set; }
        public bool IsComplete { get; private set; }

        public void Apply(StepladderMatchCreated e)
        {
            //TODO:
        }

        public void Apply(StepladderMatchUpdated e)
        {
            //TODO:
        }

        public void Apply(StepladderMatchDeleted e)
        {
            //TODO:
        }
    }
}
