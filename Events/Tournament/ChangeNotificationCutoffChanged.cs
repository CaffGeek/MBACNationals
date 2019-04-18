using System;

namespace Events.Tournament
{
    public class ChangeNotificationCutoffChanged
    {
        public Guid Id;
        public String CutoffDate { get; set; }
    }
}