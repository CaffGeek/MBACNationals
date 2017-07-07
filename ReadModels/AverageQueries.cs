using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class AverageQueries : 
        IReadModel,
        IAverageQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public List<Division> Divisions { get; set; }
        public Dictionary<Guid, string> Tournaments { get; set; }
        public Dictionary<Guid, string> MatchYearLookup { get; set; }

        public class Division
        {
            public string Name { get; set; }
            public string Year { get; set; }
            public List<Participant> Participants { get; set; }
            
            public Division()
            {
                Participants = new List<Participant>();
            }
        }

        public class Participant
        {
            public Guid ParticipantId { get; set; }
            public string Gender { get; set; }
            public string Name { get; set; }
            public int Total { get; set; }
            public decimal Average { get; set; }
            public List<Score> Scores { get; set; }
        }

        public class Score
        {
            public Guid MatchId { get; set; }
            public int Scratch { get; set; }
        }

        public AverageQueries()
        {
            Reset();
        }
        
        public void Reset()
        {
            Divisions = new List<Division>();
            Tournaments = new Dictionary<Guid, string>();
            MatchYearLookup = new Dictionary<Guid, string>();
            Tournaments.Add(Guid.Empty, "2014");
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public Division GetDivision(string divisionName, int year)
        {
            var division = Divisions.FirstOrDefault(x => x.Year == year.ToString() && x.Name.Equals(divisionName, StringComparison.OrdinalIgnoreCase));
            if (division == null)
                return null;

            return division;
        }

        public void Handle(TournamentCreated e)
        {
            if (Tournaments.ContainsValue(e.Year))
            {
                var tournament = Tournaments.SingleOrDefault(x => x.Value == e.Year);
                if (tournament.Key == Guid.Empty && tournament.Value == "2014")
                {
                    Tournaments.Remove(Guid.Empty);
                    Tournaments.Add(e.Id, e.Year);
                }
            }
            else
            {
                Tournaments.Add(e.Id, e.Year);
            }            
        }

        public void Handle(MatchCreated e)
        {
            MatchYearLookup.Add(e.Id, e.Year ?? "2014");
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var divisionName = SimplifyDivision(e.Division);
            var year = "2014";
            if (MatchYearLookup.ContainsKey(e.Id))
            {
                year = MatchYearLookup[e.Id];
            }
            
            var division = Divisions.SingleOrDefault(x => x.Name == divisionName && x.Year == year);
            if (division == null)
            {
                division = new Division
                {
                    Name = divisionName,
                    Year = year
                };

                Divisions.Add(division);
            }

            var participant = division.Participants.SingleOrDefault(x => x.ParticipantId == e.ParticipantId);
            if (participant == null)
            {
                participant = new Participant
                {
                    ParticipantId = e.ParticipantId,
                    Scores = new List<Score>()
                };
                division.Participants.Add(participant);
            }

            participant.Scores.RemoveAll(x => x.MatchId == e.Id);
            participant.Name = e.Name;
            participant.Gender = e.Gender;
            participant.Scores.Add(new Score { MatchId = e.Id, Scratch = e.Score });
            participant.Total = participant.Scores.Sum(x => x.Scratch);
            participant.Average = 1.0M * participant.Total / participant.Scores.Count;
        }

        private static string SimplifyDivision(string division)
        {
            if (division.Contains("Tournament"))
                return "Tournament";

            if (division.Contains("Teaching"))
                return "Teaching";

            if (division.Contains("Senior"))
                return "Senior";

            return null;
        }
    }
}
