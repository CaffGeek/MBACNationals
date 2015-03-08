using System;

namespace Events.Contingent
{
    public class TeamPracticeRescheduled
    {
        public Guid Id;
        public Guid TeamId;
        public string PracticeLocation;
        public int PracticeTime;        
    }
}