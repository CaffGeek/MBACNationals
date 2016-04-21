using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IContingentPracticePlanQueries
    {
        List<ContingentPracticePlanQueries.ContingentPracticePlan> GetAllSchedules(string year);
        ContingentPracticePlanQueries.ContingentPracticePlan GetSchedule(string year, string province);
    }
}
