using System;

namespace Events.Tournament
{
    public class SponsorDeleted
    {
        public Guid Id;
        public Guid SponsorId { get; set; }
    }
}