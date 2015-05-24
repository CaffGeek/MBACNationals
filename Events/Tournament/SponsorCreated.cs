using System;

namespace Events.Tournament
{
    public class SponsorCreated
    {
        public Guid Id;
        public Guid SponsorId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public byte[] Image { get; set; }
    }
}