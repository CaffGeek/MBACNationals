using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TeamScoreQueries : AzureReadModel,
        ITeamScoreQueries,
        ISubscribeTo<TeamGameCompleted>
    {
        public TeamScoreQueries(string readModelFilePath)
        {

        }
        
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
            public bool IsWin { get; internal set; }
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
            public bool IsWin { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public string Opponent { get; set; }
            public int OpponentScratch { get; set; }
            public int OpponentPOA { get; set; }
            public bool IsPOA { get; set; }
            public decimal Points { get; set; }
            public decimal OpponentPoints { get; set; }
        }


        public Team GetTeam(Guid id)
        {
            var matches = Query<TSMatch>(x => x.TeamId == id);
            var firstMatch = matches.First();
            var scores = matches.Select(x => new Score
            {
                MatchId = x.Id.ToString(),
                Number = x.Number,
                Scratch = x.Scratch,
                POA = x.POA,
                IsWin = x.IsWin,
                Lane = x.Lane,
                Centre = x.Centre,
                Opponent = x.Opponent,
                OpponentScratch = x.OpponentScratch,
                OpponentPOA = x.OpponentPOA,
                IsPOA = x.IsPOA,
                Points = x.Points,
                OpponentPoints = x.OpponentPoints,
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
        
        public void Handle(TeamGameCompleted e)
        {
            Create(e.TeamId, e.Id, new TSMatch
            {
                TeamName = e.Division,
                Province = e.Contingent,
                Number = e.Number,
                Scratch = e.Score,
                POA = e.POA,
                IsWin = e.Points > 0 || e.TotalPoints > e.OpponentPoints,
                Lane = e.Lane,
                Centre = e.Centre,
                Opponent = e.Opponent,
                OpponentScratch = e.OpponentScore,
                OpponentPOA = e.OpponentPOA,
                OpponentPoints = e.OpponentPoints,
                IsPOA = e.IsPOA,
                Points = e.TotalPoints
            });
        }
    }
}
