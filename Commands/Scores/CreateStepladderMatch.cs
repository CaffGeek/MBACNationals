using System;

namespace MBACNationals.Scores.Commands
{
    public class CreateStepladderMatch
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public Guid AwayBowlerId { get; set; }
        public Guid HomeBowlerId { get; set; }
    }
}
