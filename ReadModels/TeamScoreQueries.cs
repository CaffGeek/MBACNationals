using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TeamScoreQueries : AReadModel,
        ITeamScoreQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>
    {
        public TeamScoreQueries(string readModelFilePath)
            : base(readModelFilePath)
        {

        }

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }
            public string Province { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public List<Score> Scores { get; internal set; }
            public int HighScore { get; internal set; }
            public int HighPOA { get; internal set; }
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

        public Team GetTeam(Guid id)
        {
            return Read<Team>(x => x.Id == id).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new Contingent(e.Id)
            {
                Province = e.Province
            });
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Read<Contingent>(x => x.Id == e.Id).FirstOrDefault();

            Create(new Team(e.TeamId)
            {
                Name = e.Name,
                Province = contingent.Province,
                Scores = new List<Score>()
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            //TODO:e throw new NotImplementedException();
        }

        public void Handle(TeamGameCompleted e)
        {
            Update<Team>(e.TeamId, x =>
            {
                x.Scores.RemoveAll(m => m.MatchId == e.Id.ToString());
                x.Scores.Add(new Score
                {
                    MatchId = e.Id.ToString(),
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
            });
        }
    }
}
