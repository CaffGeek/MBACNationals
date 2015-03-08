using Edument.CQRS;
using Events.Tournament;
using MBACNationals.Tournament.Commands;
using System;
using System.Collections;

namespace MBACNationals.Tournament
{
    public class TournamentCommandHandlers :
        IHandleCommand<CreateTournament, TournamentAggregate>
    {
        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateTournament command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new TournamentAlreadyExists();

            //TODO: Create simple ReadModel to check if a tournament with this year already exists

            yield return new TournamentCreated
            {
                Id = command.Id,
                Year = command.Year
            };
        }
    }
}