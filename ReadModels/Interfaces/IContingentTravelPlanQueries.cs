using System.Collections.Generic;
namespace MBACNationals.ReadModels
{
    public interface IContingentTravelPlanQueries
    {
        List<ContingentTravelPlanQueries.ContingentTravelPlans> GetAllTravelPlans(string year);
        ContingentTravelPlanQueries.ContingentTravelPlans GetTravelPlans(string year, string province);

        List<ContingentTravelPlanQueries.ContingentRooms> GetAllRooms(string year);
        ContingentTravelPlanQueries.ContingentRooms GetRooms(string year, string province);
    }
}