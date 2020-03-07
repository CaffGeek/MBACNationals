using System;

namespace Events.Tournament
{
    public class DivisionGameResultEmailed
    {
        public Guid Id;
        public string Year { get; set; }
        public string Division { get; set; }
        public int GameNumber { get; set; }
    }
}
