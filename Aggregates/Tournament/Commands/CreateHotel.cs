using System;

namespace MBACNationals.Tournament.Commands
{
    public class CreateHotel
    {
        public Guid Id;
        public string Year { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string DefaultCheckin { get; set; }
        public string DefaultCheckout { get; set; }
        public string[] RoomTypes { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Image { get; set; }
    }
}