namespace MBACNationals.ReadModels
{
    public interface IContingentTravelPlanQueries
    {
        ContingentTravelPlanQueries.ContingentTravelPlans GetTravelPlans(string province);

        ContingentTravelPlanQueries.ContingentRooms GetRooms(string province);
    }
}