using System.Collections.Generic;
namespace MBACNationals.ReadModels
{
    public interface IContingentTravelPlanQueries
    {
        ContingentTravelPlanQueries.ContingentTravelPlans GetTravelPlans(string year, string province);

        List<ContingentTravelPlanQueries.ContingentRooms> GetAllRooms(string year);
        ContingentTravelPlanQueries.ContingentRooms GetRooms(string year, string province);
    }
}