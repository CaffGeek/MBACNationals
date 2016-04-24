using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StepladderQueries :
        IReadModel,
        IStepladderQueries,
        ISubscribeTo<StepladderMatchCreated>,
        ISubscribeTo<StepladderMatchUpdated>,
        ISubscribeTo<StepladderMatchDeleted>
    {
        public List<Match> Matches { get; set; }

        public class Match
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Year { get; set; }
            public Guid HomeId { get; set; }
            public string HomeName { get; set; }
            public string HomeShots { get; set; }
            public Guid AwayId { get; set; }
            public string AwayName { get; set; }
            public string AwayShots { get; set; }
            public bool IsComplete { get; set; }
            public string Gender { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
        }

        public StepladderQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Matches = new List<Match>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public List<Match> GetMatches(string year)
        {
            return Matches.Where(x => x.Year == year).ToList();
        }
        
        public void Handle(StepladderMatchCreated e)
        {
            Matches.Add(new Match
            {
                Id = e.Id,
                TournamentId = e.TournamentId,
                Year = e.Year,
                HomeId = e.Home,
                HomeName = e.HomeBowler,
                HomeShots = string.Empty,
                AwayId = e.Away,
                AwayName = e.AwayBowler,
                AwayShots = string.Empty,
                Gender = e.Gender,
                Created = e.Created,
            });
        }

        public void Handle(StepladderMatchUpdated e)
        {
            var match = Matches.SingleOrDefault(x => x.Id == e.Id);
            match.AwayShots = e.AwayShots;
            match.HomeShots = e.HomeShots;
            match.Updated = e.Updated;
        }

        public void Handle(StepladderMatchDeleted e)
        {
            Matches.RemoveAll(x => x.Id == e.Id);
        }
    }
}
