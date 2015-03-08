using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : AReadModel,
        IMatchQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public MatchQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }
        
        public class Match : AEntity
        {
            public Match(Guid id) : base(id) { }
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
        
        public Match GetMatch(Guid matchId)
        {
            return Read<Match>(x => x.Id == matchId).FirstOrDefault();
        }
        
        public void Handle(MatchCreated e)
        {
            var match = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            if (match != null)
                return;

            Create(new Match(e.Id)
            {
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = new Team { Province = e.Away, Bowlers = new List<Bowler>() },
                Home = new Team { Province = e.Home, Bowlers = new List<Bowler>() },
                Lane = e.Lane,
                Centre = e.CentreName,
                IsComplete = false,
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            Update<Match>(e.Id, x =>
            {
                var team = x.Away.Province == e.Contingent ? x.Away : x.Home;
                team.Bowlers.RemoveAll(b => b.Id == e.ParticipantId.ToString());

                team.Bowlers.Add(new Bowler
                {
                    Id = e.ParticipantId.ToString(),
                    Name = e.Name,
                    Position = e.Position,
                    Score = e.Score,
                    POA = e.POA,
                    Points = e.Points
                });
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            Update<Match>(e.Id, x =>
            {
                if (x.Away.Province == e.Contingent){
                    x.Away.Id = e.TeamId.ToString();
                    x.Away.Score = e.Score;
                    x.Away.POA = e.POA;
                    x.Away.Points = e.Points;
                    x.Away.TotalPoints = e.TotalPoints;
                } else {
                    x.Home.Id = e.TeamId.ToString();
                    x.Home.Score = e.Score;
                    x.Home.POA = e.POA;
                    x.Home.Points = e.Points;
                    x.Home.TotalPoints = e.TotalPoints;
                }
            });
        }

        public void Handle(MatchCompleted e)
        {
            Update<Match>(e.Id, x => x.IsComplete = true);
        }
    }
}
