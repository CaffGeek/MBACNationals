using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IStandingQueries
    {
        List<StandingQueries.Team> GetDivision(string division);
    }
}
