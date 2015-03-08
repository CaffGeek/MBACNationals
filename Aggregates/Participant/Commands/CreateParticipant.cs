using System;

namespace MBACNationals.Participant.Commands
{
    public class CreateParticipant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool IsDelegate { get; set; }
        public int YearsQualifying { get; set; }
        public int LeagueGames { get; set; }
        public int LeaguePinfall { get; set; }
        public int TournamentGames { get; set; }
        public int TournamentPinfall { get; set; }
        public bool IsGuest { get; set; }
        public PackageInformation Package { get; set; }
        public string ShirtSize { get; set; }

        public class PackageInformation
        {
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
        }
    }
}
