using System;
using System.Collections.Generic;

namespace Events.Participant
{
    public class ParticipantAssignedToTeam
    {
        public Guid Id;
        public Guid TeamId;
        public string Name;
    }
}
