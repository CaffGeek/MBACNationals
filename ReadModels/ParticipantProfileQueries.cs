using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantProfileQueries : 
        IReadModel,
        IParticipantProfileQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantProfileChanged>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>
    {
        public List<Participant> Participants { get; set; }
        public Dictionary<Guid, string> Tournaments { get; set; }
        public Dictionary<Guid, Contingent> Contingents { get; set; }
        public List<Team> Teams { get; set; }

        public class Participant
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
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
            public bool IsSingle { get; set; }
        }

        public class Contingent
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Province { get; set; }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
        }

        public ParticipantProfileQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Participants = new List<Participant>();
            Tournaments = new Dictionary<Guid, string>();
            Contingents = new Dictionary<Guid, Contingent>();
            Teams = new List<Team>();
            Tournaments.Add(Guid.Empty, "2014");
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }
        
        public List<Participant> GetProfiles(int year)
        {
            if (!Tournaments.Any(x => x.Value == year.ToString()))
                return null;

            var tournament = Tournaments.Single(x => x.Value == year.ToString());
            var contingents = Contingents.Where(x => x.Value.TournamentId == tournament.Key);
            var participants = Participants.Where(x => x.IsSingle && contingents.Any(c => c.Key == x.ContingentId))
                .ToList();

            return participants;
        }

        public Participant GetProfile(Guid id)
        {
            return Participants.SingleOrDefault(x => x.Id == id);
        }

        public void Handle(TournamentCreated e)
        {
            if (Tournaments.ContainsKey(e.Id))
                Tournaments.Remove(e.Id);
            
            Tournaments.Add(e.Id, e.Year);
        }

        public void Handle(ContingentCreated e)
        {
            Contingents.Add(e.Id, new Contingent
            {
                Id = e.Id,
                Province = e.Province
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Contingents[e.Id];
            contingent.TournamentId = e.TournamentId;
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Contingents[e.Id];

            Teams.Add(new Team
            {
                Id = e.TeamId,
                ContingentId = e.Id,
                Name = e.Name,
                Province = contingent.Province
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(new Participant
            {
                Id = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Name = e.Name;
        }

        public void Handle(ParticipantProfileChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);

            participant.HasProfile = true;
            participant.Age = e.Age;
            participant.HomeTown = e.HomeTown;
            participant.MaritalStatus = e.MaritalStatus;
            participant.SpouseName = e.SpouseName;
            participant.Children = e.Children;
            participant.Occupation = e.Occupation;
            participant.HomeCenter = e.HomeCenter;
            participant.YearsBowling = e.YearsBowling;
            participant.NumberOfLeagues = e.NumberOfLeagues;
            participant.HighestAverage = e.HighestAverage;
            participant.YearsCoaching = e.YearsCoaching;
            participant.BestFinishProvincially = e.BestFinishProvincially;
            participant.BestFinishNationally = e.BestFinishNationally;
            participant.MasterProvincialWins = e.MasterProvincialWins;
            participant.MastersAchievements = e.MastersAchievements;
            participant.OpenAchievements = e.OpenAchievements;
            participant.OpenYears = e.OpenYears;
            participant.OtherAchievements = e.OtherAchievements;
            participant.Hobbies = e.Hobbies;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            if (!Teams.Any(x => x.Id == e.TeamId))
                return;
            
            var participant = Participants.Single(x => x.Id == e.Id);
            var team = Teams.Single(x => x.Id == e.TeamId);

            participant.Team = team.Name;
            participant.Province = team.Province;
            participant.ContingentId = team.ContingentId;
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.IsSingle = e.QualifyingPosition == 1;
        }
    }
}
