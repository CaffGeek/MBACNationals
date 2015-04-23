using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StandingQueries : AzureReadModel,
        IStandingQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<TeamGameCompleted>
    {
        public StandingQueries(string readModelFilePath)
        {

        }

        public class Team
        {
            public Guid Id { get; internal set; }
            public string TeamId { get; internal set; }
            public string Division { get; internal set; }
            public string Province { get; internal set; }
            public decimal RunningPoints { get; internal set; }
            public List<Match> Matches { get; internal set; }
        }

        public class Match
        {
            public Guid Id { get; internal set; }
            public int Number { get; internal set; }
            public string Opponent { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal TotalPoints { get; internal set; }
            public bool IsPOA { get; internal set; }
        }

        private class TSMatch : Entity
        {
            public Guid TeamId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public Guid MatchId {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid TournamentId { get; set; }
            public bool IsPOA { get; set; }
            public string Division { get; set; }
            public string Province { get; set; }
            public int Number { get; set; }
            public string Opponent { get; set; }
            public int Score { get; set; }
            public int POA { get; set; }
            public double Points { get; set; }
            public double TotalPoints { get; set; }

        }

        private class TSTournament : Entity
        {
            public Guid TournamentId
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid TournamentId { get; set; }
            public string Province { get; set; }
        }

        private class TSTeam : Entity
        {
            public Guid ContingentId
            {
                get { return Guid.Parse(PartitionKey); }
            }
            public Guid TeamId
            {
                get { return Guid.Parse(RowKey); }
            }
            public string Name { get; set; }
        }

        public List<Team> GetDivision(Guid tournamentId, string division)
        {
            var matches = Query<TSMatch>(x =>
                x.TournamentId == tournamentId
                && x.Division.Equals(division, StringComparison.OrdinalIgnoreCase));

            var teams = matches
                .GroupBy(x => x.TeamId)
                .Select(g => new Team
                {
                    Id = g.First().TeamId,
                    TeamId = g.First().TeamId.ToString(),
                    Division = g.First().Division,
                    Province = g.First().Province,
                    RunningPoints = (decimal)g.Sum(x=>x.TotalPoints),
                }).ToList();

            foreach (var team in teams)
            {
                team.Matches = matches
                    .Where(m => m.TeamId.Equals(team.Id))
                    .Select(m => new Match
                    {
                        Id = m.MatchId,
                        IsPOA = m.IsPOA,
                        Number = m.Number,
                        Opponent = m.Opponent,
                        POA = m.POA,
                        Points = (decimal)m.Points,
                        Score = m.Score,
                        TotalPoints = (decimal)m.TotalPoints,
                    }).ToList();
            }

            return teams;
        }

        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ContingentCreated e)
        {
            Create(e.Id, e.Id, new TSContingent
            {
                Province = e.Province,
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            Update<TSContingent>(e.Id, e.Id, contingent =>
            {
                contingent.TournamentId = e.TournamentId;
            });
        }

        public void Handle(TeamCreated e)
        {
            Create(e.Id, e.TeamId, new TSTeam
            {
                Name = e.Name,
            });
        }

        public void Handle(TeamRemoved e)
        {
            Delete<TSTeam>(e.Id, e.TeamId);
        }

        public void Handle(TeamGameCompleted e)
        {
            var team = Read<TSTeam>(e.TeamId);
            var contingent = Read<TSContingent>(team.ContingentId, team.ContingentId);
            Create(e.TeamId, e.Id, new TSMatch
            {
                TournamentId = contingent.TournamentId,
                IsPOA = e.IsPOA,
                Division = e.Division,
                Province = e.Contingent,
                Number = e.Number,
                Opponent = e.Opponent,
                Score = e.Score,
                POA = e.POA,
                Points = (double)e.Points,
                TotalPoints = (double)e.TotalPoints,
            });
        }
    }
}
