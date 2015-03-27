using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class HighScoreQueries : AzureReadModel,
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

        public HighScoreQueries(string readModelFilePath)
        {

        }
        
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
            public string Name { get; internal set; }
            public string Gender { get; internal set; }
            public int Scratch { get; internal set; }
            public int POA { get; internal set; }
        }

        public class TSScore : Entity
        {
            public Guid TournamentId { get; set; }
            public Guid DivisionId { get; set; }
            public string Name { get;  set; }
            public string Gender { get;  set; }
            public int Scratch { get;  set; }
            public int POA { get;  set; }
        }

        public class TSTournament : Entity
        {
            public string Year { get; set; }
            public Guid Id { get; set; }
        }

        public Division GetDivision(string divisionName)
        {
            var tournamentId = GetCurrentTournamentId();

            var division = divisions.FirstOrDefault(d => d.Name.Equals(divisionName, StringComparison.OrdinalIgnoreCase));
            if (division == null)
                return null;

            var scores = Query<TSScore>(score => score.DivisionId == division.Id && score.TournamentId == tournamentId)
                .Select(x => new Score
                {
                    ParticipantId = x.RowKey,
                    MatchId = x.PartitionKey,
                    Gender = x.Gender,
                    Name = x.Name,
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
            Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var tournamentId = GetCurrentTournamentId();
            
            var division = divisions.FirstOrDefault(d => e.Division.Contains(d.Name));
            if (division == null)
                return;

            if (division.Name.Contains("Tournament") && e.Score < 275) return;
            if (!division.Name.Contains("Tournament") && e.POA < 75) return;

            Create(e.Id, e.ParticipantId, new TSScore
            {
                TournamentId = tournamentId,
                DivisionId = division.Id,
                Name = e.Name,
                Gender = e.Gender,
                Scratch = e.Score,
                POA = e.POA
            });
        }

        private Guid GetCurrentTournamentId()
        {
            var tournament = Read<TSTournament>(Guid.Empty, Guid.Empty)
                ?? new TSTournament { Id = Guid.Empty };
            return tournament.Id;
        }
    }
}
