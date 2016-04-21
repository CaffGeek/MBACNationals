using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : 
        IReadModel,
        IMatchQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public Dictionary<Guid, Match> Matches { get; private set; }

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
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public List<Bowler> Bowlers { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal TotalPoints { get; internal set; }
            
            public Team()
            {
                Bowlers = new List<Bowler>();
            }
        }

        public class Bowler
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public int Position { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
        }

        public MatchQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Matches = new Dictionary<Guid, Match>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }
        
        public Match GetMatch(Guid matchId)
        {
            return Matches[matchId];
        }
        
        public void Handle(MatchCreated e)
        {
            Matches.Add(e.Id, new Match
            {
                Id = e.Id,
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = new Team { Province = e.Away },
                Home = new Team { Province = e.Home },
                Lane = e.Lane,
                Centre = e.CentreName,
                IsComplete = false,
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var match = Matches[e.Id];
            var team = (match.Home.Province == e.Contingent) ? match.Home : match.Away;
            team.Bowlers.Add(new Bowler
            {
                Id = e.ParticipantId,
                Name = e.Name,
                Position = e.Position,
                Score = e.Score,
                POA = e.POA,
                Points = e.Points
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            var match = Matches[e.Id];
            var team = (match.Home.Province == e.Contingent) ? match.Home : match.Away;

            team.Id = e.TeamId;
            team.Score = e.Score;
            team.POA = e.POA;
            team.Points = e.Points;
            team.TotalPoints = e.TotalPoints;            
        }

        public void Handle(MatchCompleted e)
        {
            var match = Matches[e.Id];
            match.IsComplete = true;
        }
    }
}
