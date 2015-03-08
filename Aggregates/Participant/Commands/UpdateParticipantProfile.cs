using System;

namespace MBACNationals.Participant.Commands
{
    public class UpdateParticipantProfile
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string HomeTown { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string Children { get; set; }
        public string Occupation { get; set; }

        public string HomeCenter { get; set; }
        public int YearsBowling { get; set; }
        public int NumberOfLeagues { get; set; }
        public int HighestAverage { get; set; }

        public int YearsCoaching { get; set; }
        public string BestFinishProvincially { get; set; }
        public string BestFinishNationally { get; set; }

        public int MasterProvincialWins { get; set; }
        public string MastersAchievements { get; set; }

        public string OpenAchievements { get; set; }
        public int OpenYears { get; set; }

        public string OtherAchievements { get; set; }
        public string Hobbies { get; set; }
    }
}
