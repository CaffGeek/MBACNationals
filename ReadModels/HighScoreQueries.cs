using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class HighScoreQueries : AReadModel,
        IHighScoreQueries,
        ISubscribeTo<ParticipantGameCompleted>
    {
        private readonly Guid Tournament = Guid.Parse("5673d324-2ed3-4160-9e73-0692d4ea51c4");
        private readonly Guid Teaching = Guid.Parse("ad147620-2bca-43c2-a85b-60b913e628fd");
        private readonly Guid Senior = Guid.Parse("df3e6fc7-a4bf-4716-9100-4a7d0ff0f158");

        public HighScoreQueries(string readModelFilePath)
            : base(readModelFilePath)
        {

        }

        private void Build()
        {
            Create<Division>(new Division(Tournament) { Name = "Tournament", Scores = new List<Score>() });
            Create<Division>(new Division(Teaching) { Name = "Teaching", Scores = new List<Score>() });
            Create<Division>(new Division(Senior) { Name = "Senior", Scores = new List<Score>() });
        }

        public class Division : AEntity
        {
            public Division(Guid id) : base(id) { }
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

        public HighScoreQueries.Division GetDivision(string division)
        {
            return Read<Division>(x => x.Name == division).FirstOrDefault();
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var divisions = Read<Division>().ToList();
            if (!divisions.Any())
                Build();

            foreach (var division in Read<Division>().ToList())
            {
                if (!e.Division.Contains(division.Name))
                    continue;

                if (!(e.Score < 250 || e.POA > 75))
                    continue;

                Update<Division>(division.Id, x =>
                {
                    x.Scores.RemoveAll(s => s.MatchId == e.Id.ToString() && s.ParticipantId == e.ParticipantId.ToString());

                    x.Scores.Add(new Score
                    {
                        MatchId = e.Id.ToString(),
                        ParticipantId = e.ParticipantId.ToString(),
                        Name = e.Name,
                        Gender = e.Gender,
                        Scratch = e.Score,
                        POA = e.POA
                    });
                });
            }
        }
    }
}
