using System;

namespace MBACNationals.Participant.Commands
{
    public class AddParticipantToContingent
    {
        public Guid Id { get; set; }
        public Guid ContingentId { get; set; }
    }
}