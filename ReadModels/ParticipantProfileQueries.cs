using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantProfileQueries : AReadModel,
        IParticipantProfileQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantProfileChanged>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public ParticipantProfileQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
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

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }
            public string Province { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
        }

        public List<Participant> GetProfiles()
        {
            return Read<Participant>(x => x.HasProfile).ToList();
        }

        public Participant GetProfile(Guid id)
        {
            return Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new Contingent(e.Id) { Province = e.Province });
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Read<Contingent>(x => x.Id == e.Id).FirstOrDefault();
            if (contingent == null)
                return;

            Create(new Team(e.TeamId) { Name = e.Name, Province = contingent.Province });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id) { Name = e.Name });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantProfileChanged e)
        {
            Update<Participant>(e.Id, x =>
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
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            if (team == null)
                return;

            Update<Participant>(e.Id, x => {
                x.Team = team.Name;
                x.Province = team.Province;
            });
        }
    }
}
