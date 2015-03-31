namespace MBACNationals.ReadModels
{
    public interface IContingentTravelPlanQueries
    {
        ContingentTravelPlanQueries.ContingentTravelPlans GetTravelPlans(string year, string province);

        ContingentTravelPlanQueries.ContingentRooms GetRooms(string year, string province);
    }
}