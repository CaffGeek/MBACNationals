using System;

namespace MBACNationals.Participant.Commands
{
    public class AssignAlternateToTeam
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
    }
}
