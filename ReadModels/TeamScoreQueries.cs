using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TeamScoreQueries : 
        IReadModel,
        ITeamScoreQueries,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>
    {
        public Dictionary<Guid, Team> Teams { get; set; }
        public Dictionary<Guid, Guid> PoaSingles { get; set; }

        public class Team 
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public List<Score> Scores { get; internal set; }

            public Team()
            {
                Scores = new List<Score>();
            }
        }

        public class Score
        {
            public Guid MatchId { get; internal set; }
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

        public TeamScoreQueries()
        {
            Reset();
        }
        
        public void Reset()
        {
            Teams = new Dictionary<Guid, Team>();
            PoaSingles = new Dictionary<Guid, Guid>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public Team GetTeam(Guid id)
        {
            return Teams[id];
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            if (PoaSingles.ContainsKey(e.TeamId))
                return;
            
            //First teammember becomes the single by default
            PoaSingles.Add(e.TeamId, e.Id);
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            if (e.QualifyingPosition != 1)
                return;

            if (PoaSingles.ContainsKey(e.TeamId))
                PoaSingles[e.TeamId] = e.Id;                
            else
                PoaSingles.Add(e.TeamId, e.Id);
        }
        
        public void Handle(TeamGameCompleted e)
        {
            var teamId = e.TeamId;

            //HACK: Old TeamGameCompleted events used teamid instead of bowlerid for POA singles 8'(
            if ((e.Division.Contains("Teaching") || e.Division.Contains("Senior")) && e.Division.Contains("Single"))
                teamId = PoaSingles[e.TeamId];
            
            if (!Teams.ContainsKey(teamId))
            {
                Teams.Add(teamId, new Team
                {
                    Id = teamId,
                    Name = e.Division,
                    Province = e.Contingent
                });
            }

            var team = Teams[teamId];
            team.Scores.Add(new Score
            {
                MatchId = e.Id,
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
                OpponentPoints = e.OpponentPoints,
                IsPOA = e.IsPOA,
                Points = e.TotalPoints
            });
        }
    }
}
