using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StandingQueries : AzureReadModel,
        IStandingQueries,
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

        public List<Team> GetDivision(string division)
        {
            var matches = Query<TSMatch>(x => x.Division.Equals(division, StringComparison.OrdinalIgnoreCase));

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
        
        public void Handle(TeamGameCompleted e)
        {
            Create(e.TeamId, e.Id, new TSMatch
            {
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
