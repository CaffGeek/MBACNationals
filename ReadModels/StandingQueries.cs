using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StandingQueries : AReadModel,
        IStandingQueries,
        ISubscribeTo<TeamGameCompleted>
    {
        public StandingQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
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

        public List<Team> GetDivision(string division)
        {
            return Read<Team>(x => x.Division == division && !string.IsNullOrWhiteSpace(x.Province)).ToList();
        }
        
        public void Handle(TeamGameCompleted e)
        {
            var team = Read<Team>(x => x.TeamId == e.TeamId.ToString() && x.Division == e.Division).FirstOrDefault();

            if (team == null)
                Create(new Team(Guid.NewGuid())
                {
                    TeamId = e.TeamId.ToString(),
                    Division = e.Division,
                    Province = e.Contingent,
                    RunningPoints = e.TotalPoints,
                    Matches = new List<Match>
                    {
                        new Match
                        {
                            Id = e.Id,
                            Number = e.Number,
                            Opponent = e.Opponent,
                            Score = e.Score,
                            POA = e.POA,
                            Points = e.Points,
                            TotalPoints = e.TotalPoints,
                            IsPOA = e.IsPOA,
                        }
                    }
                });
            else
                Update<Team>(team.Id, x =>
                {
                    x.Province = e.Contingent;
                    x.Matches.RemoveAll(m => m.Id == e.Id);
                    x.Matches.Add(new Match
                    {
                        Id = e.Id,
                        Number = e.Number,
                        Opponent = e.Opponent,
                        Score = e.Score,
                        POA = e.POA,
                        Points = e.Points,
                        TotalPoints = e.TotalPoints,
                        IsPOA = e.IsPOA,
                    });
                    x.RunningPoints = x.Matches.Sum(m => m.TotalPoints);
                });
        }
    }
}
