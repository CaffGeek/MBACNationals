using System;

namespace Events.Tournament
{
    public class CentreDeleted
    {
        public Guid Id;
        public Guid CentreId { get; set; }
    }
}