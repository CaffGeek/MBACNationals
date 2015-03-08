using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantProfileQueries
    {
        List<ParticipantProfileQueries.Participant> GetProfiles();
        ParticipantProfileQueries.Participant GetProfile(System.Guid id);
    }
}
