using System;

namespace Events.Tournament
{
    public class CentreCreated
    {
        public Guid Id;
        public Guid CentreId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
    }
}
