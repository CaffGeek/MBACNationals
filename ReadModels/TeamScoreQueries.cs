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
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
            public List<Score> Scores { get; set; }
            public int TotalGames { get; set; }
            public decimal TotalWins { get; set; }
            public decimal TotalLoss { get; set; }
            public int TotalScratch { get; set; }
            public int TotalOpponentScratch { get; set; }
            public int TotalPOA { get; set; }
            public int TotalOpponentPOA { get; set; }
            public decimal AverageWinsPerGame { get; set; }
            public decimal AverageLossPerGame { get; set; }
            public decimal AverageScratch { get; set; }
            public decimal AveragePOA { get; set; }
            public decimal AverageOpponentScratch { get; set; }
            public decimal AverageOpponentPOA { get; set; }

            public Team()
            {
                Scores = new List<Score>();
            }

        }

        public class Score
        {
            public Guid MatchId { get; set; }
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
            public decimal Points { get; set; }
            public decimal OpponentPoints { get; set; }
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


            //Remove any previous entries as they could re-enter the scores
            team.Scores.RemoveAll(x => x.MatchId == e.Id);

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

            team.TotalGames = team.Scores.Count;
            team.TotalWins = team.Scores.Sum(x => x.Points);
            team.TotalLoss = team.Scores.Sum(x => x.OpponentPoints);
            team.TotalScratch = team.Scores.Sum(x => x.Scratch);
            team.TotalOpponentScratch = team.Scores.Sum(x => x.OpponentScratch);
            team.TotalPOA = team.Scores.Sum(x => x.POA);
            team.TotalOpponentPOA = team.Scores.Sum(x => x.OpponentPOA);
            team.AverageWinsPerGame = (decimal)team.TotalWins / team.TotalGames;
            team.AverageLossPerGame = (decimal)team.TotalLoss / team.TotalGames;
            team.AverageScratch = (decimal)team.TotalScratch / team.TotalGames;
            team.AveragePOA = (decimal)team.TotalPOA / team.TotalGames;
            team.AverageOpponentScratch = (decimal)team.TotalOpponentScratch / team.TotalGames;
            team.AverageOpponentPOA = (decimal)team.TotalOpponentPOA / team.TotalGames;
        }
    }
}
