using System;

namespace MBACNationals.Contingent.Commands
{
    public class CreateTeam
    {
        public Guid ContingentId { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int SizeLimit { get; set; }

        public bool RequiresShirtSize { get; set; }
        public bool RequiresCoach { get; set; }
        public bool RequiresAverage { get; set; }
        public bool RequiresBio { get; set; }
        public bool RequiresGender { get; set; }
        public bool IncludesSinglesRep { get; set; }
    }
}
