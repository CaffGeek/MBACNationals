using MBACNationalsReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IContingentEventHistoryQueries
    {
        List<ContingentEventHistoryQueries.Event> GetEvents(string province);
    }
}