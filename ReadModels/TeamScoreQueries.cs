using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TeamScoreQueries : BaseReadModel<TeamScoreQueries>,
        ITeamScoreQueries,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public class Team 
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public List<Score> Scores { get; internal set; }
        }

        public class Score
        {
            public string MatchId { get; internal set; }
            public int Number { get; internal set; }
            public int Scratch { get; internal set; }
            public int POA { get; internal set; }
            public string WinLossTie { get; internal set; }
            public int Lane { get; internal set; }
            public string Centre { get; internal set; }
            public string Opponent { get; internal set; }
            public int OpponentScratch { get; internal set; }
            public int OpponentPOA { get; internal set; }
            public bool IsPOA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal OpponentPoints { get; internal set; }
        }

        private class TSMatch : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid TeamId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string TeamName { get; set; }
            public string Province { get; set; }
            public int Number { get; set; }
            public int Scratch { get; set; }
            public int POA { get; set; }
            public string WinLossTie { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public string Opponent { get; set; }
            public int OpponentScratch { get; set; }
            public int OpponentPOA { get; set; }
            public bool IsPOA { get; set; }
            public double Points { get; set; }
            public double OpponentPoints { get; set; }
        }

        private class TSTeamSingle : Entity
        {
            public Guid TeamId
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public Guid SingleId { get; set; }
        }
        
        public Team GetTeam(Guid id)
        {
            var matches = Storage.Query<TSMatch>(x => x.TeamId == id);
            var firstMatch = matches.First();
            var scores = matches.Select(x => new Score
            {
                MatchId = x.Id.ToString(),
                Number = x.Number,
                Scratch = x.Scratch,
                POA = x.POA,
                WinLossTie = x.WinLossTie,
                Lane = x.Lane,
                Centre = x.Centre,
                Opponent = x.Opponent,
                OpponentScratch = x.OpponentScratch,
                OpponentPOA = x.OpponentPOA,
                IsPOA = x.IsPOA,
                Points = (decimal)x.Points,
                OpponentPoints = (decimal)x.OpponentPoints,
            }).ToList();

            var team = new Team
            {
                Id = id,
                Name = firstMatch.TeamName,
                Province = firstMatch.Province,
                Scores = scores,                
            };

            return team;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var teamSingle = Storage.Read<TSTeamSingle>(e.TeamId);
            
            //First teammember becomes the single
            if (teamSingle == null)
            {
                Storage.Create(e.TeamId, e.TeamId, new TSTeamSingle{ SingleId = e.Id });
            }
        }
        
        public void Handle(TeamGameCompleted e)
        {
            var teamId = e.TeamId;

            //HACK: Old TeamGameCompleted events used teamid instead of bowlerid for POA singles 8'(
            if ((e.Division.Contains("Teaching") || e.Division.Contains("Senior")) && e.Division.Contains("Single"))
            {
                var teamSingle = Storage.Read<TSTeamSingle>(e.TeamId);
                if (teamSingle != null) teamId = teamSingle.SingleId;
            }

            Storage.Create(teamId, e.Id, new TSMatch
            {
                TeamName = e.Division,
                Province = e.Contingent,
                Number = e.Number,
                Scratch = e.Score,
                POA = e.POA,
                WinLossTie = e.TotalPoints > e.OpponentPoints
                    ? "W" : e.TotalPoints < e.OpponentPoints
                    ? "L" : "T",
                Lane = e.Lane,
                Centre = e.Centre,
                Opponent = e.Opponent,
                OpponentScratch = e.OpponentScore,
                OpponentPOA = e.OpponentPOA,
                OpponentPoints = (double)e.OpponentPoints,
                IsPOA = e.IsPOA,
                Points = (double)e.TotalPoints
            });
        }
    }
}
