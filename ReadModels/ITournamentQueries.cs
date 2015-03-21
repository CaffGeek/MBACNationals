using MBACNationals.ReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ITournamentQueries
    {
        List<TournamentQueries.Tournament> GetTournaments();
    }
}
