using System;

namespace MBACNationals.Contingent.Commands
{
    public class ChangeRoomCheckin
    {
        public Guid Id { get; set; }
        public string Province { get; set; }
        public int RoomNumber { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
    }
}
