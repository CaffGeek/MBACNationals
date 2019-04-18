using System;

namespace Events.Tournament
{
    public class ChangeNotificationEmailChanged
    {
        public Guid Id;
        public String Email { get; set; }
    }
}