using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class HighScoreQueries : 
        IReadModel,
        IHighScoreQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public List<Division> Divisions { get; set; }
        public Dictionary<Guid, string> Tournaments { get; set; }
        public Dictionary<Guid, string> Matches { get; set; }

        public class Division
        {
            public string Name { get; set; }
            public string Year { get; set; }
            public List<Score> Scores { get; set; }
            
            public Division()
            {
                Scores = new List<Score>();
            }
        }

        public class Score
        {
            public Guid MatchId { get; set; }
            public Guid ParticipantId { get; set; }
            public string Year { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Scratch { get; set; }
            public int POA { get; set; }
        }

        public HighScoreQueries()
        {
            Reset();
        }
        
        public void Reset()
        {
            Divisions = new List<Division>();
            Tournaments = new Dictionary<Guid, string>();
            Matches = new Dictionary<Guid, string>();
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
            Matches.Add(e.Id, e.Year ?? "2014");
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var divisionName = SimplifyDivision(e.Division);
            var year = "2014";
            if (Matches.ContainsKey(e.Id))
            {
                year = Matches[e.Id];
            }
            
            if (divisionName.Equals("Tournament", StringComparison.OrdinalIgnoreCase) && e.Score < 275) return;
            if (!divisionName.Equals("Tournament", StringComparison.OrdinalIgnoreCase) && e.POA < 75) return;

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

            division.Scores.Add(new Score{
                Gender = e.Gender,
                MatchId = e.Id,
                Name = e.Name,
                ParticipantId = e.ParticipantId,
                POA = e.POA,
                Scratch = e.Score,
                Year = year                
            });
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
