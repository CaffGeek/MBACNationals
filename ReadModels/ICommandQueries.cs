using System;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ICommandQueries
    {
        List<CommandQueries.Tournament> GetTournaments();
        
        CommandQueries.Participant GetParticipant(Guid guid);
    }
}
