using System;

namespace MBACNationals.Participant.Commands
{
    public class AddParticipantToTeam
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
    }
}
