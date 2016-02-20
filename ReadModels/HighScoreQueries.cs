using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class HighScoreQueries : BaseReadModel<HighScoreQueries>,
        IHighScoreQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        private readonly List<Division> divisions = new List<Division>() {
            new Division
            {
                Id = Guid.Parse("5673d324-2ed3-4160-9e73-0692d4ea51c4"),
                Name = "Tournament"
            },
            new Division
            {
                Id = Guid.Parse("ad147620-2bca-43c2-a85b-60b913e628fd"),
                Name = "Teaching"
            },
            new Division
            {
                Id = Guid.Parse("df3e6fc7-a4bf-4716-9100-4a7d0ff0f158"),
                Name = "Senior"
            }};

        public class Division
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public List<Score> Scores { get; internal set; }
        }

        public class Score
        {
            public string MatchId { get; internal set; }
            public string ParticipantId { get; internal set; }
            public int Year { get; set; }
            public string Name { get; internal set; }
            public string Gender { get; internal set; }
            public int Scratch { get; internal set; }
            public int POA { get; internal set; }
        }

        private class TSScore : Entity
        {
            public Guid TournamentId { get; set; }
            public Guid DivisionId { get; set; }
            public int Year { get; set; }
            public string Name { get;  set; }
            public string Gender { get;  set; }
            public int Scratch { get;  set; }
            public int POA { get;  set; }
        }

        private class TSTournament : Entity
        {
            public string Year { get; set; }
            public Guid Id { get; set; }
        }

        public Division GetDivision(string divisionName, int year)
        {
            var division = divisions.FirstOrDefault(d => d.Name.Equals(divisionName, StringComparison.OrdinalIgnoreCase));
            if (division == null)
                return null;

            var scores = Storage.Query<TSScore>(score => score.DivisionId == division.Id && score.Year == year)
                .Select(x => new Score
                {
                    ParticipantId = x.RowKey,
                    MatchId = x.PartitionKey,
                    Year = x.Year,
                    Name = x.Name,
                    Gender = x.Gender,
                    POA = x.POA,
                    Scratch = x.Scratch,
                })
                .ToList();

            return new Division
            {
                Id = division.Id,
                Name = division.Name,
                Scores = scores
            };
        }

        public void Handle(TournamentCreated e)
        {
            //HACK: Track current tournament
            Storage.Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var tournament = GetCurrentTournament();
            
            var division = divisions.FirstOrDefault(d => e.Division.Contains(d.Name));
            if (division == null)
                return;

            var year = 0;
            int.TryParse(tournament.Year, out year);
            if (year == 0) year = 2014; //FIX: Adjustment for old style of events in 2014

            if (division.Name.Contains("Tournament") && e.Score < 275) return;
            if (!division.Name.Contains("Tournament") && e.POA < 75) return;

            Storage.Create(e.Id, e.ParticipantId, new TSScore
            {
                TournamentId = tournament.Id,
                Year = year,
                DivisionId = division.Id,
                Name = e.Name,
                Gender = e.Gender,
                Scratch = e.Score,
                POA = e.POA
            });
        }

        private TSTournament GetCurrentTournament()
        {
            var tournament = Storage.Read<TSTournament>(Guid.Empty, Guid.Empty)
                ?? new TSTournament { Id = Guid.Empty, Year = "0" };
            return tournament;
        }
    }
}
