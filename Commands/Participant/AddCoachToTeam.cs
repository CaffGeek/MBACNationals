using System;

namespace MBACNationals.Participant.Commands
{
    public class AddCoachToTeam
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
    }
}
