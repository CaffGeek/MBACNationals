using System;

namespace Events.Tournament
{
    public class TournamentCreated
    {
        public Guid Id;
        public string Year { get; set; }
    }
}
