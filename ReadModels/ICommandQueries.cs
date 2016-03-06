using System;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ICommandQueries
    {
        List<CommandQueries.Tournament> GetTournaments();
        
        CommandQueries.Participant GetParticipant(Guid guid);

        CommandQueries.Match GetMatch(string year, string division, int game, string slot);
    }
}
