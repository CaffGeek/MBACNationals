using Edument.CQRS;
using Events.Tournament;
using System;

namespace MBACNationals.Tournament
{
    public class TournamentAggregate : Aggregate,
        IApplyEvent<TournamentCreated>
    {
        public string Year { get; private set; }

        public void Apply(TournamentCreated e)
        {
            Year = e.Year;
        }
    }
}
