using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantProfileQueries
    {
        List<ParticipantProfileQueries.Participant> GetProfiles(int year);
        ParticipantProfileQueries.Participant GetProfile(System.Guid id);
    }
}
