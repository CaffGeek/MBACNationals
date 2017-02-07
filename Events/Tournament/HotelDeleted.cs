using System;

namespace Events.Tournament
{
    public class HotelDeleted
    {
        public Guid Id;
        public Guid HotelId { get; set; }
    }
}