using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ScheduleQueries : AzureReadModel,
        IScheduleQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<MatchCompleted>
    {
        public ScheduleQueries(string readModelFilePath)
        { }

        public class Schedule
        {
            public string Division { get; internal set; }
            public List<Match> Games { get; internal set; }
        }

        public class Match
        {
            public Guid Id { get; internal set; }
            public string Division { get; internal set; }
            public bool IsPOA { get; internal set; }
            public int Number { get; internal set; }
            public string Away { get; internal set; }
            public string Home { get; internal set; }
            public int Lane { get; internal set; }
            public BowlingCentre Centre { get; internal set; }
            public string CentreName { get; internal set; }
            public bool IsComplete { get; internal set; }

            public Match(Guid guid, string division, int number, string away, string home, int lane, BowlingCentre centre, bool isPOA = false)
            {
                Id = guid;
                Division = division;
                IsPOA = isPOA;
                Number = number;
                Away = away;
                Home = home;
                Lane = lane;
                Centre = centre;
                CentreName = centre.ToString();
                IsComplete = false;
            }
        }

        private class TSMatch : Entity
        {
            public Guid MatchId
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Division { get; set; }
            public bool IsPOA { get; set; }
            public int Number { get; set; }
            public string Away { get; set; }
            public string Home { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public bool IsComplete { get; set; }
        }

        public ScheduleQueries.Schedule GetSchedule(string division)
        {
            var matches = Query<TSMatch>(x => x.Division.Equals(division, StringComparison.OrdinalIgnoreCase))
                .Select(x => new Match(x.MatchId, x.Division, x.Number, x.Away, x.Home, x.Lane, (BowlingCentre)Enum.Parse(typeof(BowlingCentre), x.Centre), x.IsPOA))
                .ToList();

            return new Schedule
            {
                Division = division,
                Games = matches,
            };
        }

        public void Handle(MatchCreated e)
        {
            Create(e.Id, e.Id, new TSMatch {
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = e.Away,
                Home = e.Home,
                Lane = e.Lane,
                Centre = e.Centre.ToString(),
                IsComplete = false,
            });
        }

        public void Handle(MatchCompleted e)
        {
            Update<TSMatch>(e.Id, e.Id, x => x.IsComplete = true);
        }
    }
}
