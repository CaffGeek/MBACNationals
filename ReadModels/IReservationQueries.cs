using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IReservationQueries
    {
        List<ReservationQueries.Participant> GetParticipants(string province);
    }
}
