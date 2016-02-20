using System;

namespace Events.Participant
{
    public class ParticipantCreated
    {
        public Guid Id;
        public string Name;
        public string Gender;
        public bool IsDelegate;
        public bool IsManager;
        public int YearsQualifying;
        public bool IsGuest;
        public DateTime? Birthday;
    }
}
