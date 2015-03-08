using System;

namespace Events.Contingent
{
    public class TeamCreated
    {
        public Guid Id;
        public Guid TeamId;
        public string Name;
        public string Gender;
        public int SizeLimit;
        public bool RequiresShirtSize;
        public bool RequiresCoach;
        public bool RequiresAverage;
        public bool RequiresBio;
        public bool RequiresGender;
        public bool IncludesSinglesRep;
    }
}
