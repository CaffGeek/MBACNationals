using MBACNationals.ReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ITournamentQueries
    {
        TournamentQueries.Tournament GetTournament(string year);
        List<TournamentQueries.Tournament> GetTournaments();
    }
}
