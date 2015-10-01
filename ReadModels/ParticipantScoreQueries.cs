using Edument.CQRS;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantScoreQueries : BaseReadModel<ParticipantScoreQueries>,
        IParticipantScoreQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public string Division { get; internal set; }
            public int Average { get; internal set; }
            public int NationalGames { get; internal set; }
            public int NationalTotal { get; internal set; }
            public int NationalAverage { get; internal set; }
            public decimal NationalWins { get; internal set; }
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
            public string WinLossTie { get; internal set; }
            public int Lane { get; internal set; }
            public string Centre { get; internal set; }
            public string OpponentName { get; internal set; }
            public string OpponentProvince { get; internal set; }
            public int OpponentScratch { get; internal set; }
            public int OpponentPOA { get; internal set; }
            public bool IsPOA { get; internal set; }
        }

        private class TSParticipant : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public string Province { get; set; }
            public string Division { get; set; }
            public int Average { get; set; }
            public int NationalGames { get; set; }
            public int NationalTotal { get; set; }
            public int NationalAverage { get; set; }
            public double NationalWins { get; set; }
            public int HighScore { get; set; }
            public int HighPOA { get; set; }
        }

        private class TSGame : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString();  }
            }
            public Guid ParticipantId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public int Number { get; set; }
            public int Scratch { get; set; }
            public int POA { get; set; }
            public string WinLossTie { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public string OpponentName { get; set; }
            public string OpponentProvince { get; set; }
            public int OpponentScratch { get; set; }
            public int OpponentPOA { get; set; }
            public bool IsPOA { get; set; }
        }

        public ParticipantScoreQueries.Participant GetParticipant(Guid id)
        {
            var scores = Storage.Query<TSGame>(x => x.ParticipantId == id)
                .Select(x => new Score
                {
                    MatchId = x.Id.ToString(),
                    Number = x.Number,
                    Scratch = x.Scratch,
                    POA = x.POA,
                    WinLossTie = x.WinLossTie,
                    Lane = x.Lane,
                    Centre = x.Centre,
                    OpponentProvince = x.OpponentProvince,
                    OpponentName = x.OpponentName,
                    OpponentScratch = x.OpponentScratch,
                    OpponentPOA = x.OpponentPOA,
                    IsPOA = x.IsPOA
                }).ToList();

            var tsp = Storage.Read<TSParticipant>(id, id);
            return new Participant
            {
                Id = tsp.Id,
                Name = tsp.Name,
                Province = tsp.Province,
                Division = tsp.Division,
                Average = tsp.Average,
                NationalGames = tsp.NationalGames,
                NationalTotal = tsp.NationalTotal,
                NationalAverage = tsp.NationalAverage,
                NationalWins = (decimal)tsp.NationalWins,
                Scores = scores,
                HighScore = scores.Max(x => x.Scratch),
                HighPOA = scores.Max(x => x.POA),
            };
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSParticipant
            {
                Name = e.Name
            });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => x.Average = e.Average);
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var game = Storage.Read<TSGame>(e.ParticipantId, e.Id);
            if (game == null)
            {
                Storage.Create<TSGame>(e.ParticipantId, e.Id, new TSGame
                   {
                       Id = e.Id,
                       ParticipantId = e.ParticipantId,
                       Number = e.Number,
                       Scratch = e.Score,
                       POA = e.POA,
                       WinLossTie = e.IsPOA 
                        ? (e.POA > e.OpponentPOA ? "W" : e.POA < e.OpponentPOA ? "L" : "T")
                        : (e.Score > e.OpponentScore ? "W" : e.Score < e.OpponentScore ? "L" : "T"),
                       Lane = e.Lane,
                       Centre = e.Centre,
                       OpponentProvince = e.Opponent,
                       OpponentName = e.OpponentName,
                       OpponentScratch = e.OpponentScore,
                       OpponentPOA = e.OpponentPOA,
                       IsPOA = e.IsPOA
                   });

                Storage.Update<TSParticipant>(e.ParticipantId, e.ParticipantId, x =>
                {
                    x.Name = e.Name;
                    x.Division = e.Division;
                    x.Province = e.Contingent;
                    x.NationalGames += 1;
                    x.NationalTotal += e.Score;
                    x.NationalAverage = x.NationalTotal / x.NationalGames;
                    x.NationalWins += (double)(e.Points > 0M ? ( e.Points % 1M == 0M ? 1M : .5M) : 0M);
                });
            }
            else
            {
                Storage.Update<TSParticipant>(e.ParticipantId, e.ParticipantId, x =>
                {
                    x.Name = e.Name;
                    x.NationalTotal = x.NationalTotal - game.Scratch + e.Score;
                    x.NationalAverage = x.NationalTotal / x.NationalGames;
                    x.NationalWins = x.NationalWins
                        - (double)(game.WinLossTie == "W" ? 1M : game.WinLossTie == "T" ? .5M : 0M)
                        + (double)(e.Points > 0 ? (e.Points % 1M == 0M ? 1M : .5M) : 0M);
                });
            }
        }
    }
}
