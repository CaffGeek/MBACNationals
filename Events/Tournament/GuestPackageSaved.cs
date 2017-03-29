using System;

namespace Events.Tournament
{
    public class GuestPackageSaved
    {
        public Guid Id;
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool Enabled { get; set; }
    }
}