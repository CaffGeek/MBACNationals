using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantQueries
    {
        List<ParticipantQueries.Participant> GetParticipants();

        ParticipantQueries.Participant GetParticipant(System.Guid id);
    }
}
