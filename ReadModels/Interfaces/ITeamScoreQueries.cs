using System;

namespace MBACNationals.ReadModels
{
    public interface ITeamScoreQueries
    {
        TeamScoreQueries.Team GetTeam(Guid id);
    }
}
