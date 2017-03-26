using System;

namespace Events.Tournament
{
    public class HotelCreated
    {
        public Guid Id;
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Image { get; set; }
    }
}
