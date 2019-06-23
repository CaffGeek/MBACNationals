using System;

namespace MBACNationals.Contingent.Commands
{
    public class RemoveTeam
    {
        public Guid ContingentId { get; set; }
        public Guid TeamId { get; set; }
    }
}
