using System;

namespace Events.Tournament
{
    public class ScoreNotificationEmailChanged
    {
        public Guid Id;
        public String Email { get; set; }
    }
}