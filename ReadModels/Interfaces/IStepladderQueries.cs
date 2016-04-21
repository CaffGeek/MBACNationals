using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IStepladderQueries
    {
        List<StepladderQueries.Match> GetMatches(string year);
    }
}
