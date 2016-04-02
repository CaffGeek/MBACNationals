using System;

namespace MBACNationals.Participant.Commands
{
    public class ReorderParticipant
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public int QualifyingPosition { get; set; }
    }
}
