using System;

namespace MBACNationals.Tournament.Commands
{
    public class ChangeTournamentSettings
    {
        public Guid Id;
        public string ChangeNotificationCutoff { get; set; }
        public string ChangeNotificationEmail { get; set; }
        public string ScoreNotificationEmail { get; set; }
        public string Year { get; set; }
    }
}