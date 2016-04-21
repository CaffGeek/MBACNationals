using System;

namespace MBACNationals.ReadModels
{
    public interface IContingentViewQueries
    {
        ContingentViewQueries.Contingent GetContingent(Guid tournamentId, string province);
    }
}