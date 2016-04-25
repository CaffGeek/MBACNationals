using Edument.CQRS;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ScheduleQueries : 
        IReadModel,
        IScheduleQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<MatchCompleted>
    {
        public List<Tournament> Tournaments { get; set; }

        public class Tournament
        {
            public Guid Id { get; set; }
            public string Year { get; set; }
            public List<Schedule> DivisionSchedules { get; set; }
            
            public Tournament()
            {
                DivisionSchedules = new List<Schedule>();
            }
        }

        public class Schedule
        {
            public string Division { get; set; }
            public List<Match> Games { get; set; }

            public Schedule()
            {
                Games = new List<Match>();
            }
        }

        public class Match
        {
            public Guid Id { get; set; }
            public string Division { get; set; }
            public bool IsPOA { get; set; }
            public int Number { get; set; }
            public string Away { get; set; }
            public string Home { get; set; }
            public int Lane { get; set; }
            public BowlingCentre Centre { get; set; }
            public string CentreName { get; set; }
            public bool IsComplete { get; set; }

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

        public ScheduleQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();            
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }
        
        public ScheduleQueries.Schedule GetSchedule(int year, string division)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Year == year.ToString());
            var schedule = tournament.DivisionSchedules.SingleOrDefault(x => x.Division == division);
            return schedule;
        }

        public void Handle(TournamentCreated e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Year == e.Year || x.Id == e.Id);
            if (tournament == null)
                Tournaments.Add(new Tournament
                {
                    Id = e.Id,
                    Year = e.Year,
                });
            else if (tournament != null && tournament.Year == "2014")
                tournament.Id = e.Id;            
        }

        public void Handle(MatchCreated e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Year == e.Year || x.Id == e.TournamentId);
            if (tournament == null) {
                tournament = new Tournament { Year = "2014" };
                Tournaments.Add(tournament);
            }

            var division = tournament.DivisionSchedules.SingleOrDefault(x => x.Division == e.Division);
            if (division == null)
            {
                division = new Schedule { Division = e.Division };
                tournament.DivisionSchedules.Add(division);
            }

            division.Games.Add(new Match(e.Id, e.Division, e.Number, e.Away, e.Home, e.Lane, e.Centre, e.IsPOA));             
        }

        public void Handle(MatchCompleted e)
        {
            var match = Tournaments
                .SelectMany(x => x.DivisionSchedules)
                .SelectMany(x => x.Games)
                .SingleOrDefault(x => x.Id == e.Id);
            match.IsComplete = true;
        }
    }
}
