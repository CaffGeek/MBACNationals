using System;

namespace MBACNationals.ReadModels
{
    public interface IParticipantScoreQueries
    {
        ParticipantScoreQueries.Participant GetParticipant(Guid id);
    }
}
