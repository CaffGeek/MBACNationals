using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantQueries
    {
        List<ParticipantQueries.Participant> GetParticipants(string year);

        ParticipantQueries.Participant GetParticipant(System.Guid id);
    }
}
