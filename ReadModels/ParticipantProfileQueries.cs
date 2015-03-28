using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantProfileQueries : AzureReadModel,
        IParticipantProfileQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantProfileChanged>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public ParticipantProfileQueries(string readModelFilePath)
        {

        }

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

        private class TSContingent : Entity
        {
            public string Province { get; internal set; }
        }

        private class TSTeam : Entity
        {
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
        }

        public List<Participant> GetProfiles()
        {
            return null; //TODO: Read<Participant>(x => x.HasProfile).ToList();
        }

        public Participant GetProfile(Guid id)
        {
            return null; //TODO: Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(e.Id, e.Id, new TSContingent { Province = e.Province });
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Read<TSContingent>(e.Id, e.Id);
            if (contingent == null)
                return;

            Create(e.Id, e.TeamId, new TSTeam { Name = e.Name, Province = contingent.Province });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(e.Id, e.Id, new TSParticipant { Name = e.Name });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<TSParticipant>(e.Id, e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantProfileChanged e)
        {
            Update<TSParticipant>(e.Id, e.Id, x =>
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
            var team = Read<TSTeam>(e.TeamId);
            if (team == null)
                return;

            Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.Team = team.Name;
                x.Province = team.Province;
            });
        }
    }
}
