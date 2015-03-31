using System;

namespace MBACNationals.Contingent.Commands
{
    public class CreateContingent
    {
        public Guid Id;
        public Guid TournamentId { get; set; }
        public string Province { get; set; }
    }
}
