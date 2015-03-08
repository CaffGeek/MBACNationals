using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ScheduleQueries : AReadModel,
        IScheduleQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<MatchCompleted>
    {
        public ScheduleQueries(string readModelFilePath)
            : base(readModelFilePath)
        { }

        public class Schedule
        {
            public string Division { get; internal set; }
            public List<Match> Games { get; internal set; }
        }

        public class Match : AEntity
        {
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
                : base(guid)
            {
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

        public ScheduleQueries.Schedule GetSchedule(string division)
        {
            return new Schedule
            {
                Division = division,
                Games = Read<Match>(x => x.Division.Equals(division, StringComparison.OrdinalIgnoreCase)).ToList()
            };
        }

        public void Handle(MatchCreated e)
        {
            var schedule = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            if (schedule != null)
                return;

            Create(new Match(e.Id, e.Division, e.Number, e.Away, e.Home, e.Lane, e.Centre, e.IsPOA));
        }

        public void Handle(MatchCompleted e)
        {
            Update<Match>(e.Id, x => x.IsComplete = true );
        }
    }
}
