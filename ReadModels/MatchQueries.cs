using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : BaseReadModel<MatchQueries>,
        IMatchQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public class Match
        {
            public Guid Id { get; internal set; }
            public string Division { get; internal set; }
            public bool IsPOA { get; internal set; }
            public int Number { get; internal set; }
            public Team Away { get; internal set; }
            public Team Home { get; internal set; }
            public int Lane { get; internal set; }
            public string Centre { get; internal set; }
            public bool IsComplete { get; internal set; }
        }

        public class Team
        {
            public string Id { get; internal set; }
            public string Province { get; internal set; }
            public List<Bowler> Bowlers { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal TotalPoints { get; internal set; }
        }

        public class Bowler
        {
            public string Id { get; internal set; }
            public string Name { get; internal set; }
            public int Position { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
        }

        private class TSMatch : Entity
        {
            public string Division { get; set; }
            public bool IsPOA { get; set; }
            public int Number { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public bool IsComplete { get; set; }

            public string Away { get; set; }
            public string AwayId { get; set; }
            public int AwayScore { get; set; }
            public int AwayPOA { get; set; }
            public double AwayPoints { get; set; }
            public double AwayTotalPoints { get; set; }

            public string Home { get; set; }
            public string HomeId { get; set; }
            public int HomeScore { get; set; }
            public int HomePOA { get; set; }
            public double HomePoints { get; set; }
            public double HomeTotalPoints { get; set; }
        }

        private class TSBowler : Entity
        {
            public string Name { get; set; }
            public string Province { get; set; }
            public int Position { get; set; }
            public int Score { get; set; }
            public int POA { get; set; }
            public double Points { get; set; }
        }

        public Match GetMatch(Guid matchId)
        {
            var match = Storage.Read<TSMatch>(matchId);
            var bowlers = Storage.Query<TSBowler>(x => x.PartitionKey == matchId.ToString());

            var awayBowlers = bowlers
                .Where(x => x.Province == match.Away)
                .Select(x => new Bowler
                {
                    Id = x.RowKey,
                    Name = x.Name,
                    POA = x.POA,
                    Points = (decimal)x.Points,
                    Position = x.Position,
                    Score = x.Score
                }).ToList();

            var homeBowlers = bowlers
                .Where(x => x.Province == match.Home)
                .Select(x => new Bowler
                {
                    Id = x.RowKey,
                    Name = x.Name,
                    POA = x.POA,
                    Points = (decimal)x.Points,
                    Position = x.Position,
                    Score = x.Score
                }).ToList();

            var awayTeam = new Team
            {
                Id = match.AwayId,
                POA = match.AwayPOA,
                Points = (decimal)match.AwayPoints,
                Province = match.Away,
                Score = match.AwayScore,
                TotalPoints = (decimal)match.AwayTotalPoints,
                Bowlers = awayBowlers
            };
            var homeTeam = new Team
            {
                Id = match.HomeId,
                POA = match.HomePOA,
                Points = (decimal)match.HomePoints,
                Province = match.Home,
                Score = match.HomeScore,
                TotalPoints = (decimal)match.HomeTotalPoints,
                Bowlers = homeBowlers
            };

            return new Match
            {
                Id = matchId,
                Division = match.Division,
                IsPOA = match.IsPOA,
                Number = match.Number,
                Away = awayTeam,
                Home = homeTeam,
                Lane = match.Lane,
                Centre = match.Centre,
                IsComplete = match.IsComplete
            };
        }
        
        public void Handle(MatchCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSMatch
            {
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = e.Away,
                Home = e.Home,
                Lane = e.Lane,
                Centre = e.CentreName,
                IsComplete = false,
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            Storage.Create(e.Id, e.ParticipantId, new TSBowler
            {
                Name = e.Name,
                Province = e.Contingent,
                Position = e.Position,
                Score = e.Score,
                POA = e.POA,
                Points = (double)e.Points
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            Storage.Update<TSMatch>(e.Id, e.Id, x =>
            {
                if (x.Away == e.Contingent)
                {
                    x.AwayId = e.TeamId.ToString();
                    x.AwayScore = e.Score;
                    x.AwayPOA = e.POA;
                    x.AwayPoints = (double)e.Points;
                    x.AwayTotalPoints = (double)e.TotalPoints;
                }
                else
                {
                    x.HomeId = e.TeamId.ToString();
                    x.HomeScore = e.Score;
                    x.HomePOA = e.POA;
                    x.HomePoints = (double)e.Points;
                    x.HomeTotalPoints = (double)e.TotalPoints;
                }
            });
        }

        public void Handle(MatchCompleted e)
        {
            Storage.Update<TSMatch>(e.Id, e.Id, x => x.IsComplete = true);
        }
    }
}
