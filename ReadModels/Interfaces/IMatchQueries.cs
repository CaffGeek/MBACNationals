using System;

namespace MBACNationals.ReadModels
{
    public interface IMatchQueries
    {
        MatchQueries.Match GetMatch(Guid matchId);
    }
}
