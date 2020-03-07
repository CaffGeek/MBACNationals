using System;

namespace MBACNationals.Participant.Commands
{
    public class ReplaceParticipant
    {
        public Guid Id { get; set; }
        public Guid AlternateId { get; set; }
    }
}
