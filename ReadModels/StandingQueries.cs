using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StandingQueries : 
        IReadModel,
        IStandingQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>,
        ISubscribeTo<TeamGameCompleted>
    {
        public Dictionary<Guid, string> Tournaments { get; set; }
        public List<Contingent> Contingents { get; set; }
        public List<Team> Teams { get; set; }
        public Dictionary<Guid, Guid> Singles { get; set; }

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
            public string TeamId { get; set; }
            public string Division { get; set; }
            public string Province { get; set; }
            public decimal RunningPoints { get { return Matches.Sum(x => x.TotalPoints); } }
            public List<Match> Matches { get; set; }
            
            public Team()
            {
                Matches = new List<Match>();
            }
        }

        public class Match
        {
            public Guid Id { get; set; }
            public int Number { get; set; }
            public string Opponent { get; set; }
            public int Score { get; set; }
            public int POA { get; set; }
            public decimal Points { get; set; }
            public decimal TotalPoints { get; set; }
            public bool IsPOA { get; set; }
        }

        public StandingQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new Dictionary<Guid, string>();
            Contingents = new List<Contingent>();
            Teams = new List<Team>();
            Tournaments.Add(Guid.Empty, "2014");
            Singles = new Dictionary<Guid, Guid>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public List<Team> GetDivision(Guid tournamentId, string division)
        {
            var contingents = Contingents.Where(x => x.TournamentId == tournamentId);
            var teams = Teams
                .Where(x => contingents.Any(c => c.Id == x.ContingentId) && x.Division == division)
                .ToList();

            return teams;
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
            Contingents.Add(new Contingent
            {
                Id = e.Id,
                Province = e.Province
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Contingents.Single(x => x.Id == e.Id);
            contingent.TournamentId = e.TournamentId;
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Contingents.Single(x => x.Id == e.Id);

            Teams.Add(new Team
            {
                Id = e.TeamId,
                TeamId = e.TeamId.ToString(),
                ContingentId = e.Id,
                Province = contingent.Province,
                Division = e.Name,
            });
        }

        public void Handle(TeamRemoved e)
        {
            Teams.RemoveAll(x => x.Id == e.TeamId);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            if (!Singles.ContainsKey(e.TeamId))
                Singles.Add(e.TeamId, e.Id); //First bowler is single by default
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            if (e.QualifyingPosition != 1)
                return;
            
            if (Singles.ContainsKey(e.TeamId))
                Singles[e.TeamId] = e.Id;
            else
                Singles.Add(e.TeamId, e.Id);
        }

        public void Handle(TeamGameCompleted e)
        {
            var isPOASingles = e.IsPOA && e.Division.Contains("Single");
            Guid actualTeamId = isPOASingles ? Singles[e.TeamId] : e.TeamId;

            var team = Teams.SingleOrDefault(x => x.Id == actualTeamId);
            if (team == null)
            {
                var t = Teams.Single(x => x.Id == e.TeamId);
                var contingent = Contingents.Single(x => x.Id == t.ContingentId);
                
                team = new Team
                {
                    Id = actualTeamId,
                    TeamId = actualTeamId.ToString(),
                    ContingentId = contingent.Id,
                    Province = contingent.Province,
                    Division = e.Division,
                };

                Teams.Add(team);
            }

            //Remove any previous entries as they could re-enter the scores
            team.Matches.RemoveAll(x => x.Id == e.Id);

            team.Matches.Add(new Match {
                Id = e.Id,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Opponent = e.Opponent,
                POA = e.POA,
                Points = e.Points,
                Score = e.Score,
                TotalPoints = e.TotalPoints,                
            });
        }
    }
}
