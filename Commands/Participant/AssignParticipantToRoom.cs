using System;

namespace MBACNationals.Participant.Commands
{
    public class AssignParticipantToRoom
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
    }
}
