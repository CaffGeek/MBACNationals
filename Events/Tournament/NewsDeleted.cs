using System;

namespace Events.Tournament
{
    public class NewsDeleted
    {
        public Guid Id;
        public Guid NewsId { get; set; }
    }
}