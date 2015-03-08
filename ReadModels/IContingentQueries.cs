using MBACNationalsReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IContingentQueries
    {
        List<ContingentQueries.Contingent> GetContingents();
    }
}