using Edument.CQRS;
using Events.Tournament;
using MBACNationals.ReadModels;
using MBACNationals.Tournament.Commands;
using System;
using System.Collections;
using System.Linq;

namespace MBACNationals.Tournament
{
    public class TournamentCommandHandlers :
        IHandleCommand<CreateTournament, TournamentAggregate>
    {
        private ICommandQueries CommandQueries;

        public TournamentCommandHandlers(ICommandQueries commandQueries)
        {
            CommandQueries = commandQueries;
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateTournament command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new TournamentAlreadyExists();

            if (CommandQueries.GetTournaments().Any(x => x.Year.Equals(command.Year)))
                throw new TournamentAlreadyExists();

            yield return new TournamentCreated
            {
                Id = command.Id,
                Year = command.Year
            };
        }
    }
}