namespace MBACNationals.ReadModels
{
    public interface IHighScoreQueries
    {
        HighScoreQueries.Division GetDivision(string division, int year);
    }
}
