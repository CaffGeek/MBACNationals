using System;

namespace MBACNationals.Contingent.Commands
{
    public class SavePracticePlan
    {
        public Guid Id { get; set; }
        public TeamPracticePlan[] Teams { get; set; }

        public class TeamPracticePlan
        {
            public Guid Id { get; set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
        }
    }
}
