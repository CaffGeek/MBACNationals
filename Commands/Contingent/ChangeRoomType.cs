using System;

namespace MBACNationals.Contingent.Commands
{
    public class ChangeRoomType
    {
        public Guid Id { get; set; }
        public string Province { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; }
    }
}
