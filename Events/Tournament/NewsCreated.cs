using System;

namespace Events.Tournament
{
    public class NewsCreated
    {
        public Guid Id;
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}