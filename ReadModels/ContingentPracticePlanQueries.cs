using Edument.CQRS;
using Events.Contingent;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentPracticePlanQueries : 
        IReadModel,
        IContingentPracticePlanQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<TeamPracticeRescheduled>
    {
        public List<ContingentPracticePlan> ContingentPracticePlans { get; set; }
        public Dictionary<Guid, string> Tournaments { get; set; }
        
        public class ContingentPracticePlan
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Year { get; set; }
            public string Province { get; set; }
            public List<Team> Teams { get; set; }

            public ContingentPracticePlan()
            {
                Teams = new List<Team>();
            }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
        }

        public ContingentPracticePlanQueries()
        {
            Reset();
        }

        public void Reset()
        {
            ContingentPracticePlans = new List<ContingentPracticePlan>();
            Tournaments = new Dictionary<Guid, string>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }
                
        public List<ContingentPracticePlan> GetAllSchedules(string year)
        {
            return ContingentPracticePlans.Where(x => x.Year == year).ToList();
        }

        public ContingentPracticePlan GetSchedule(string year, string province)
        {
            return ContingentPracticePlans.SingleOrDefault(x => x.Year == year && x.Province == province);
        }
        
        public void Handle(TournamentCreated e)
        {
            if (Tournaments.ContainsValue(e.Year))
            {
                var tournament = Tournaments.SingleOrDefault(x => x.Value == e.Year);
                if (tournament.Key == Guid.Empty && tournament.Value == "2014")
                {
                    Tournaments.Remove(Guid.Empty);
                    Tournaments.Add(e.Id, e.Year);
                }
            }
            else
            {
                Tournaments.Add(e.Id, e.Year);
            }
        }
        
        public void Handle(ContingentCreated e)
        {
            if (string.IsNullOrWhiteSpace(e.Province))
                return;

            ContingentPracticePlans.Add(
                new ContingentPracticePlan
                    {
                        Id = e.Id,
                        Province = e.Province,                        
                    });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var tournament = Tournaments.ContainsKey(e.TournamentId)
                ? Tournaments[e.TournamentId]
                : "2014";

            var contingent = ContingentPracticePlans.SingleOrDefault(x => x.Id == e.Id);

            contingent.TournamentId = e.TournamentId;
            contingent.Year = tournament;
        }

        public void Handle(TeamCreated e)
        {
            var contingent = ContingentPracticePlans.SingleOrDefault(x => x.Id == e.Id);
            contingent.Teams.Add(new Team
            {
                Id = e.TeamId,
                Name = e.Name
            });
        }

        public void Handle(TeamRemoved e)
        {            
            var contingent = ContingentPracticePlans.SingleOrDefault(x => x.Id == e.Id);
            contingent.Teams.RemoveAll(x => x.Id == e.TeamId);
        }

        public void Handle(TeamPracticeRescheduled e)
        {
            var contingent = ContingentPracticePlans.SingleOrDefault(x => x.Id == e.Id);
            var team = contingent.Teams.SingleOrDefault(x => x.Id == e.TeamId);
            team.PracticeLocation = e.PracticeLocation;
            team.PracticeTime = e.PracticeTime;
        }
    }
}