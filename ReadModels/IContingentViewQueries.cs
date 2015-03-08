using MBACNationalsReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IContingentViewQueries
    {
        ContingentViewQueries.Contingent GetContingent(System.Guid id);

        ContingentViewQueries.Contingent GetContingent(string province);
    }
}