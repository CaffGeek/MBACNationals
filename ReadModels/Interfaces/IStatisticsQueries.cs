namespace MBACNationals.ReadModels
{
    public interface IStatisticsQueries
    {
        StatisticsQueries.Division GetDivision(string division, int year);
    }
}
