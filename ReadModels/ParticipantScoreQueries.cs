using AzureTableHelper;
using Edument.CQRS;
using Events.Participant;
using Events.Scores;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantScoreQueries : //BaseReadModel<ParticipantScoreQueries>,
        IReadModel,
        IParticipantScoreQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public List<Participant> Participants { get; private set; }

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

            public Participant()
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
            public string OpponentName { get; internal set; }
            public string OpponentProvince { get; internal set; }
            public int OpponentScratch { get; internal set; }
            public int OpponentPOA { get; internal set; }
            public bool IsPOA { get; internal set; }
        }

        public ParticipantScoreQueries()
        {
            Reset();
        }
        
        public ParticipantScoreQueries.Participant GetParticipant(Guid id)
        {
            return Participants.SingleOrDefault(x => x.Id == id);
        }

        public void Reset()
        {
            Participants = new List<Participant>();
        }

        public void Save()
        {
            //TODO: Move to baseclass
            var jsonModel = JsonConvert.SerializeObject(this);

            var storageConnection = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(storageConnection);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var azureBlobHelper = new AzureBlobHelper(blobClient);
            var container = azureBlobHelper.GetContainerFor("ReadModels");

            var modelBlob = container.GetBlockBlobReference(this.GetType().Name);
            modelBlob.UploadText(jsonModel);
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(new Participant
            {
                Id = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Average = e.Average;
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var participant = Participants.Single(x => x.Id == e.ParticipantId);
            var game = participant.Scores.SingleOrDefault(x => x.MatchId == e.Id);
            if (game == null)
            {
                participant.Scores.Add(new Score
                {
                    MatchId = e.Id,
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

                participant.Name = e.Name;
                participant.Division = e.Division;
                participant.Province = e.Contingent;
                participant.NationalGames += 1;
                participant.NationalTotal += e.Score;
                participant.NationalAverage = participant.NationalTotal / participant.NationalGames;
                participant.NationalWins += e.Points > 0M ? (e.Points % 1M == 0M ? 1M : .5M) : 0M;
                
                participant.HighScore = Math.Max(e.Score, participant.HighScore);
                participant.HighPOA = Math.Max(e.POA, participant.HighPOA);
            }
            else
            {
                participant.Name = e.Name;
                participant.NationalTotal = participant.NationalTotal - game.Scratch + e.Score;
                participant.NationalAverage = participant.NationalTotal / participant.NationalGames;
                participant.NationalWins = participant.NationalWins
                    - (game.WinLossTie == "W" ? 1M : game.WinLossTie == "T" ? .5M : 0M)
                    + (e.Points > 0 ? (e.Points % 1M == 0M ? 1M : .5M) : 0M);

                participant.HighScore = participant.Scores.Max(x => x.Scratch);
                participant.HighPOA = participant.Scores.Max(x => x.POA);
            }
        }
    }
}
