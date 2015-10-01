using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantProfileQueries : BaseReadModel<ParticipantProfileQueries>,
        IParticipantProfileQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantProfileChanged>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public class Participant
        {
            public Guid Id { get; internal set; }
            public bool HasProfile { get; internal set; }
            public string Province { get; internal set; }
            public string Team { get; internal set; }
            public string Name { get; internal set; }
            public int Age { get; internal set; }
            public string HomeTown { get; set; }
            public string MaritalStatus { get; internal set; }
            public string SpouseName { get; internal set; }
            public string Children { get; internal set; }
            public string Occupation { get; internal set; }
            public string HomeCenter { get; internal set; }
            public int YearsBowling { get; internal set; }
            public int NumberOfLeagues { get; internal set; }
            public int HighestAverage { get; internal set; }
            public int YearsCoaching { get; internal set; }
            public string BestFinishProvincially { get; internal set; }
            public string BestFinishNationally { get; internal set; }
            public int MasterProvincialWins { get; internal set; }
            public string MastersAchievements { get; internal set; }
            public string OpenAchievements { get; internal set; }
            public int OpenYears { get; internal set; }
            public string OtherAchievements { get; internal set; }
            public string Hobbies { get; internal set; }
        }
        
        private class TSTournament : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Province { get; internal set; }
            public Guid TournamentId { get; set; }
        }

        private class TSTeam : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid ContingentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public string Province { get; set; }
        }

        private class TSParticipant : Entity
        {
            public bool HasProfile { get; set; }
            public string Province { get; set; }
            public string Team { get; set; }
            public string Name { get; set; }
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
            public Guid ContingentId { get; set; }
        }

        public List<Participant> GetProfiles(int year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year == year.ToString()).FirstOrDefault();
            var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);

            var partipants = Storage.Query<TSParticipant>(x => x.HasProfile)
                .Where(x => contingents.Any(c => c.Id == x.ContingentId))
                .Select(x => new Participant
                {
                    Id = Guid.Parse(x.RowKey),
                    HasProfile = x.HasProfile,
                    Province = x.Province,
                    Team = x.Team,
                    Name = x.Name,
                    Age = x.Age,
                    HomeTown = x.HomeTown,
                    MaritalStatus = x.MaritalStatus,
                    SpouseName = x.SpouseName,
                    Children = x.Children,
                    Occupation = x.Occupation,
                    HomeCenter = x.HomeCenter,
                    YearsBowling = x.YearsBowling,
                    NumberOfLeagues = x.NumberOfLeagues,
                    HighestAverage = x.HighestAverage,
                    YearsCoaching = x.YearsCoaching,
                    BestFinishProvincially = x.BestFinishProvincially,
                    BestFinishNationally = x.BestFinishNationally,
                    MasterProvincialWins = x.MasterProvincialWins,
                    MastersAchievements = x.MastersAchievements,
                    OpenAchievements = x.OpenAchievements,
                    OpenYears = x.OpenYears,
                    OtherAchievements = x.OtherAchievements,
                    Hobbies = x.Hobbies,
                }).ToList();
            return partipants;
        }

        public Participant GetProfile(Guid id)
        {
            var participant = Storage.Read<TSParticipant>(id, id);
            return new Participant
                {
                    Id = Guid.Parse(participant.RowKey),
                    HasProfile = participant.HasProfile,
                    Province = participant.Province,
                    Team = participant.Team,
                    Name = participant.Name,
                    Age = participant.Age,
                    HomeTown = participant.HomeTown,
                    MaritalStatus = participant.MaritalStatus,
                    SpouseName = participant.SpouseName,
                    Children = participant.Children,
                    Occupation = participant.Occupation,
                    HomeCenter = participant.HomeCenter,
                    YearsBowling = participant.YearsBowling,
                    NumberOfLeagues = participant.NumberOfLeagues,
                    HighestAverage = participant.HighestAverage,
                    YearsCoaching = participant.YearsCoaching,
                    BestFinishProvincially = participant.BestFinishProvincially,
                    BestFinishNationally = participant.BestFinishNationally,
                    MasterProvincialWins = participant.MasterProvincialWins,
                    MastersAchievements = participant.MastersAchievements,
                    OpenAchievements = participant.OpenAchievements,
                    OpenYears = participant.OpenYears,
                    OtherAchievements = participant.OtherAchievements,
                    Hobbies = participant.Hobbies,
                }; 
        }

        public void Handle(TournamentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ContingentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSContingent { Province = e.Province });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            Storage.Update<TSContingent>(e.Id, e.Id, x => x.TournamentId = e.TournamentId);
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Storage.Read<TSContingent>(e.Id, e.Id);
            if (contingent == null)
                return;

            Storage.Create(e.Id, e.TeamId, new TSTeam { Name = e.Name, Province = contingent.Province });
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSParticipant { Name = e.Name });
        }

        public void Handle(ParticipantRenamed e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantProfileChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.HasProfile = true;
                x.Age = e.Age;
                x.HomeTown = e.HomeTown;
                x.MaritalStatus = e.MaritalStatus;
                x.SpouseName = e.SpouseName;
                x.Children = e.Children;
                x.Occupation = e.Occupation;
                x.HomeCenter = e.HomeCenter;
                x.YearsBowling = e.YearsBowling;
                x.NumberOfLeagues = e.NumberOfLeagues;
                x.HighestAverage = e.HighestAverage;
                x.YearsCoaching = e.YearsCoaching;
                x.BestFinishProvincially = e.BestFinishProvincially;
                x.BestFinishNationally = e.BestFinishNationally;
                x.MasterProvincialWins = e.MasterProvincialWins;
                x.MastersAchievements = e.MastersAchievements;
                x.OpenAchievements = e.OpenAchievements;
                x.OpenYears = e.OpenYears;
                x.OtherAchievements = e.OtherAchievements;
                x.Hobbies = e.Hobbies;
            });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Storage.Read<TSTeam>(e.TeamId);
            if (team == null)
                return;

            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.Team = team.Name;
                x.Province = team.Province;
                x.ContingentId = team.ContingentId;
            });
        }
    }
}
