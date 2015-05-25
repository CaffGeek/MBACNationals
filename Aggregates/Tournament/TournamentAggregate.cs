using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;

namespace MBACNationals.Tournament
{
    public class TournamentAggregate : Aggregate,
        IApplyEvent<TournamentCreated>,
        IApplyEvent<SponsorCreated>
    {
        public string Year { get; private set; }

        public void Apply(TournamentCreated e)
        {
            Year = e.Year;
        }

        public void Apply(SponsorCreated e)
        {
            //TODO: nothing at this time
        }
    }
}
