using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class StepladderQueries : AzureReadModel,
        IStepladderQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<StepladderMatchCreated>,
        ISubscribeTo<StepladderMatchUpdated>
    {
        public class Match
        {
            public Guid Id { get; internal set; }
            public Guid TournamentId { get; internal set; }
            public string Year { get; internal set; }
            public Guid HomeId { get; internal set; }
            public string HomeName { get; internal set; }
            public string HomeShots { get; internal set; }
            public Guid AwayId { get; internal set; }
            public string AwayName { get; internal set; }
            public string AwayShots { get; internal set; }
            public bool IsComplete { get; internal set; }
        }

        public class TSTournament : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
        }

        private class TSMatch : Entity
        {
            public Guid TournamentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public Guid MatchId
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public string Year { get; set; }
            public Guid HomeId { get; set; }
            public string HomeName { get; set; }
            public string HomeShots { get; set; }
            public Guid AwayId { get; set; }
            public string AwayName { get; set; }
            public string AwayShots { get; set; }
            public bool IsComplete { get; set; }
        }


        public List<Match> GetMatches(string year)
        {
            var tournament = Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            if (tournament == null)
                return Enumerable.Empty<Match>().ToList();

            return Query<TSMatch>(x => x.TournamentId == tournament.Id)
                .Select(x => new Match
                {
                    Id = x.MatchId,
                    Year = x.Year,
                    TournamentId = x.TournamentId,
                    HomeId = x.HomeId,
                    HomeName = x.HomeName,
                    HomeShots = x.HomeShots,
                    AwayId = x.AwayId,
                    AwayName = x.AwayName,
                    AwayShots = x.AwayShots,
                    IsComplete = x.IsComplete
                }).ToList();
        }

        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(StepladderMatchCreated e)
        {
            Create(e.TournamentId, e.Id, new TSMatch
            {
                Year = e.Year,
                HomeId = e.Home,
                HomeName = e.HomeBowler,
                HomeShots = string.Empty,
                AwayId = e.Away,
                AwayName = e.AwayBowler,
                AwayShots = string.Empty,
            });
        }

        public void Handle(StepladderMatchUpdated e)
        {
            Update<TSMatch>(e.TournamentId, e.Id, x =>
            {
                x.AwayShots = e.AwayShots;
                x.HomeShots = e.HomeShots;
            });
        }
    }
}
