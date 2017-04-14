using System;

namespace MBACNationals.Tournament.Commands
{
    public class CreateCentre
    {
        public Guid Id;
        public string Year { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
    }
}