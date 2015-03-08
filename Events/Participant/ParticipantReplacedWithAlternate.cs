using System;

namespace Events.Participant
{
    public class ParticipantReplacedWithAlternate
    {
        public Guid Id;
        public string Name;
        public Guid AlternateId;
        public string AlternateName;
        public int Average;
        public Guid TeamId;
        public Guid ContingentId;
    }
}
