namespace MBACNationals.ReadModels
{
    public interface IContingentPracticePlanQueries
    {
        ContingentPracticePlanQueries.ContingentPracticePlan GetSchedule(string year, string province);
    }
}
