namespace MBACNationals.ReadModels
{
    public interface IAverageQueries
    {
        AverageQueries.Division GetDivision(string division, int year);
    }
}
