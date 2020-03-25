﻿using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantScoreQueries : 
        IReadModel,
        IParticipantScoreQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<TeamCreated>
    {
        public List<Participant> Participants { get; set; }
        public Dictionary<Guid, string> Contingents { get; set; }
        public Dictionary<Guid, string> Teams { get; set; }

        public class Participant
        {
         

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
            public string Division { get; set; }
            public int Average { get; set; }
            public int NationalGames { get; set; }
            public int NationalTotal { get; set; }
            public int NationalTotalPOA { get; set; }
            public int NationalAverage { get; set; }
            public int NationalAveragePOA { get; set; }
            public int OpponentTotal { get; set; }
            public int OpponentTotalPOA { get; set; }
            public int OpponentAverage { get; set; }
            public int OpponentAveragePOA { get; set; }
            public decimal NationalWins { get; set; }
            public List<Score> Scores { get; set; }
            public int HighScore { get; set; }
            public int HighPOA { get; set; }

            public Participant()
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
            public Guid OpponentId { get; set; }
            public string OpponentName { get; set; }
            public string OpponentProvince { get; set; }
            public int OpponentScratch { get; set; }
            public int OpponentPOA { get; set; }
            public bool IsPOA { get; set; }
        }

        public ParticipantScoreQueries()
        {
            Reset();
        }
        
        public Participant GetParticipant(Guid id)
        {
            return Participants.SingleOrDefault(x => x.Id == id);
        }

        public void Reset()
        {
            Participants = new List<Participant>();
            Contingents = new Dictionary<Guid, string>();
            Teams = new Dictionary<Guid, string>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(new Participant
            {
                Id = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Name = e.Name;
        }

        public void Handle(ContingentCreated e)
        {
            Contingents.Add(e.Id, e.Province);
        }

        public void Handle(TeamCreated e)
        {
            Teams.Add(e.TeamId, Contingents[e.Id]);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Province = Teams[e.TeamId];
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

            if (game != null)
            {
                //Adjust Totals
                participant.NationalTotal = participant.NationalTotal - game.Scratch;
                participant.NationalTotalPOA -= game.POA;
                participant.NationalGames -= 1;
                participant.NationalWins -= (game.WinLossTie == "W" ? 1M : game.WinLossTie == "T" ? .5M : 0M);
                participant.OpponentTotal -= game.OpponentScratch;
                participant.OpponentTotalPOA -= game.OpponentPOA;

                //Remove old game
                participant.Scores.Remove(game);
            }

            participant.Scores.Add(new Score
            {
                MatchId = e.Id,
                Number = e.Number,
                Scratch = e.Score,
                POA = e.POA,
                WinLossTie = e.Points == 1 ? "W" : e.Points == 0 ? "L" : "T",
                //WinLossTie = e.IsPOA 
                //    ? (e.POA > e.OpponentPOA ? "W" : e.POA < e.OpponentPOA ? "L" : "T")
                //    : (e.Score > e.OpponentScore ? "W" : e.Score < e.OpponentScore ? "L" : "T"),
                Lane = e.Lane,
                Centre = e.Centre,
                OpponentId = e.OpponentId,
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
            participant.NationalTotalPOA += e.POA;
            participant.NationalAveragePOA = participant.NationalTotalPOA / participant.NationalGames;
            participant.OpponentTotal += e.OpponentScore;
            participant.OpponentTotalPOA += e.OpponentPOA;
            participant.OpponentAverage = participant.OpponentTotal / participant.NationalGames;
            participant.OpponentAveragePOA = participant.OpponentTotalPOA / participant.NationalGames;

            participant.HighScore = participant.Scores.Max(x => x.Scratch);
            participant.HighPOA = participant.Scores.Max(x => x.POA);
        }
    }
}
