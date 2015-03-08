using Edument.CQRS;
using Events.Tournament;

namespace MBACNationals.Tournament
{
    public class TournamentAggregate : Aggregate,
        IApplyEvent<TournamentCreated>
    {
        public void Apply(TournamentCreated e)
        {
            throw new System.NotImplementedException();
        }
    }
}
