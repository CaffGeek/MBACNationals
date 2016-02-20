using Edument.CQRS;
using Events.Participant;
using System;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>,
        IApplyEvent<ParticipantRenamed>,
        IApplyEvent<ParticipantAssignedToContingent>,
        IApplyEvent<ParticipantAssignedToTeam>,
        IApplyEvent<ParticipantDesignatedAsAlternate>,
        IApplyEvent<CoachAssignedToTeam>,
        IApplyEvent<ParticipantGenderReassigned>,
        IApplyEvent<ParticipantDelegateStatusGranted>,
        IApplyEvent<ParticipantDelegateStatusRevoked>,
        IApplyEvent<ParticipantManagerStatusGranted>,
        IApplyEvent<ParticipantManagerStatusRevoked>,
        IApplyEvent<ParticipantYearsQualifyingChanged>,
        IApplyEvent<ParticipantAverageChanged>,
        IApplyEvent<ParticipantAssignedToRoom>,
        IApplyEvent<ParticipantRemovedFromRoom>,
        IApplyEvent<ParticipantGuestPackageChanged>,
        IApplyEvent<ParticipantShirtSizeChanged>,
        IApplyEvent<ParticipantProfileChanged>,
        IApplyEvent<ParticipantReplacedWithAlternate>,
        IApplyEvent<ParticipantBirthdayChanged>
    {
        public Guid TeamId { get; private set; }
        public Guid ContingentId { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public bool IsDelegate { get; private set; }
        public bool IsManager { get; private set; }
        public bool IsGuest { get; private set; }
        public bool IsCoach { get; private set; }
        public bool IsAlternate { get; private set; }
        public int YearsQualifying { get; private set; }
        public int LeaguePinfall { get; private set; }
        public int LeagueGames { get; private set; }
        public int TournamentPinfall { get; private set; }
        public int TournamentGames { get; private set; }
        public int Average { get; private set; }
        public int RoomNumber { get; private set; }
        public bool IsGuestPackageRequired { get; private set; }
        public PackageInformation Package { get; private set; }
        public string ShirtSize { get; private set; }
        public ProfileDetails Profile { get; private set; }
        public Guid ReplacedBy { get; private set; }
        public DateTime? Birthday { get; private set; }

        public class PackageInformation
        {
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
            public bool Option1 { get; set; }
            public bool Option2 { get; set; }
            public bool Option3 { get; set; }
            public bool Option4 { get; set; }
        }

        public class ProfileDetails
        {
            public int Age{ get; set; }
            public string HomeTown{ get; set; }
            public string MaritalStatus{ get; set; }
            public string SpouseName{ get; set; }
            public string Children{ get; set; }
            public string Occupation{ get; set; }
            public string HomeCenter{ get; set; }
            public int YearsBowling{ get; set; }
            public int NumberOfLeagues{ get; set; }
            public int HighestAverage{ get; set; }
            public int YearsCoaching{ get; set; }
            public string BestFinishProvincially{ get; set; }
            public string BestFinishNationally{ get; set; }
            public int MasterProvincialWins{ get; set; }
            public string MastersAchievements{ get; set; }
            public string OpenAchievements{ get; set; }
            public int OpenYears{ get; set; }
            public string OtherAchievements{ get; set; }
            public string Hobbies{ get; set; }
        }

        public void Apply(ParticipantCreated e)
        {
            Name = e.Name;
            Gender = e.Gender;
            IsDelegate = e.IsDelegate;
            IsGuest = e.IsGuest;
            YearsQualifying = e.YearsQualifying;
            Birthday = e.Birthday;
            Package = new PackageInformation();
            Profile = new ProfileDetails();
        }

        public void Apply(ParticipantRenamed e)
        {
            Name = e.Name;
        }

        public void Apply(CoachAssignedToTeam e)
        {
            IsCoach = true;
            TeamId = e.TeamId;
        }

        public void Apply(ParticipantAssignedToContingent e)
        {
            ContingentId = e.ContingentId;
        }

        public void Apply(ParticipantAssignedToTeam e)
        {
            TeamId = e.TeamId;
        }

        public void Apply(ParticipantDesignatedAsAlternate e)
        {
            TeamId = e.TeamId;
            IsAlternate = true; 
        }

        public void Apply(ParticipantGenderReassigned e)
        {
            Gender = e.Gender;
        }

        public void Apply(ParticipantDelegateStatusGranted e)
        {
            IsDelegate = true;
        }

        public void Apply(ParticipantDelegateStatusRevoked e)
        {
            IsDelegate = false;
        }

        public void Apply(ParticipantManagerStatusGranted e)
        {
            IsManager = true;
        }

        public void Apply(ParticipantManagerStatusRevoked e)
        {
            IsManager = false;
        }

        public void Apply(ParticipantYearsQualifyingChanged e)
        {
            YearsQualifying = e.YearsQualifying;
        }

        public void Apply(ParticipantAverageChanged e)
        {
            LeaguePinfall = e.LeaguePinfall;
            LeagueGames = e.LeagueGames;
            TournamentPinfall = e.TournamentPinfall;
            TournamentGames = e.TournamentGames;
            var pinfall = LeaguePinfall + TournamentPinfall;
            var games = LeagueGames + TournamentGames;
            
            Average = games > 0 
                ? pinfall / games
                : 0;
        }

        public void Apply(ParticipantAssignedToRoom e)
        {
            RoomNumber = e.RoomNumber;
        }

        public void Apply(ParticipantRemovedFromRoom e)
        {
            RoomNumber = 0;
        }

        public void Apply(ParticipantGuestPackageChanged e)
        {
            Package.ManitobaDinner = e.ManitobaDinner;
            Package.ManitobaDance = e.ManitobaDance;
            Package.FinalBanquet = e.FinalBanquet;
            Package.Transportation = e.Transportation;
            Package.Option1 = e.Option1;
            Package.Option2 = e.Option2;
            Package.Option3 = e.Option3;
            Package.Option4 = e.Option4;
        }

        public void Apply(ParticipantShirtSizeChanged e)
        {
            ShirtSize = e.ShirtSize;
        }

        public void Apply(ParticipantProfileChanged e)
        {
            Profile.Age = e.Age;
            Profile.HomeTown = e.HomeTown;
            Profile.MaritalStatus = e.MaritalStatus;
            Profile.SpouseName = e.SpouseName;
            Profile.Children = e.Children;
            Profile.Occupation = e.Occupation;
            Profile.HomeCenter = e.HomeCenter;
            Profile.YearsBowling = e.YearsBowling;
            Profile.NumberOfLeagues = e.NumberOfLeagues;
            Profile.HighestAverage = e.HighestAverage;
            Profile.YearsCoaching = e.YearsCoaching;
            Profile.BestFinishProvincially = e.BestFinishProvincially;
            Profile.BestFinishNationally = e.BestFinishNationally;
            Profile.MasterProvincialWins = e.MasterProvincialWins;
            Profile.MastersAchievements = e.MastersAchievements;
            Profile.OpenAchievements = e.OpenAchievements;
            Profile.OpenYears = e.OpenYears;
            Profile.OtherAchievements = e.OtherAchievements;
            Profile.Hobbies = e.Hobbies;
        }

        public void Apply(ParticipantReplacedWithAlternate e)
        {
            ReplacedBy = e.AlternateId;
        }

        public void Apply(ParticipantBirthdayChanged e)
        {
            Birthday = e.Birthday;
        }
    }
}
