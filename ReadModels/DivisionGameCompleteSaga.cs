using System;
using Edument.CQRS;
using Events.Tournament;
using Events.Scores;
using System.Collections.Generic;
using System.Linq;
using MBACNationals.Tournament.Commands;

namespace MBACNationals.ReadModels
{
    public class DivisionGameCompleteSaga :
        IReadModel,
        ISetDispatcher,
        IDivisionGameCompleteSaga, 
        ISubscribeTo<TournamentCreated>, 
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public MessageDispatcher Dispatcher { get; private set; }

        public List<Tournament> Tournaments { get; set; }
        
        public class Tournament
        {
            public Guid Id { get; set; }
            public String Year { get; set; }
            public List<Match> Matches { get; set; }

        }

        public class Match
        {
            public Guid Id { get; set; }
            public string Division { get; set; }
            public bool IsPOA { get; set; }
            public int Number { get; set; }
            public Team Away { get; set; }
            public Team Home { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public bool IsComplete { get; set; }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public string Province { get; set; }
            public List<Bowler> Bowlers { get; set; }
            public int Score { get; set; }
            public int POA { get; set; }
            public decimal Points { get; set; }
            public decimal TotalPoints { get; set; }

            public Team()
            {
                Bowlers = new List<Bowler>();
            }
        }

        public class Bowler
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Position { get; set; }
            public int Score { get; set; }
            public int POA { get; set; }
            public decimal Points { get; set; }
        }

        public DivisionGameCompleteSaga()
        {
            Reset();
        }

        public void SetDispatcher(MessageDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public void Handle(TournamentCreated e)
        {
            if (Tournaments.Any(x => x.Id == e.Id)) return;

            Tournaments.Add(
                new Tournament
                {
                    Id = e.Id,
                    Year = e.Year ?? "2014",
                    Matches = new List<Match>(),
                }
            );
        }

        public void Handle(MatchCreated e)
        {
            if (e.TournamentId == Guid.Empty || e.Year == null) return;

            var tournament = Tournaments.SingleOrDefault(x => x.Id == e.TournamentId || x.Year == e.Year);
            tournament.Matches.Add(new Match
            {
                Id = e.Id,
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = new Team { Province = e.Away },
                Home = new Team { Province = e.Home },
                Lane = e.Lane,
                Centre = e.CentreName,
                IsComplete = false,
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Matches.Any(y => y.Id == e.Id));
            if (tournament == null) return;

            var match = tournament.Matches.SingleOrDefault(x => x.Id == e.Id);

            var team = (match.Home.Province == e.Contingent) ? match.Home : match.Away;

            team.Bowlers.RemoveAll(x => x.Id == e.ParticipantId);
            team.Bowlers.Add(new Bowler
            {
                Id = e.ParticipantId,
                Name = e.Name,
                Position = e.Position,
                Score = e.Score,
                POA = e.POA,
                Points = e.Points
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Matches.Any(y => y.Id == e.Id));
            if (tournament == null) return;

            var match = tournament.Matches.SingleOrDefault(x => x.Id == e.Id);
            
            var team = (match.Home.Province == e.Contingent) ? match.Home : match.Away;

            team.Id = e.TeamId;
            team.Score = e.Score;
            team.POA = e.POA;
            team.Points = e.Points;
            team.TotalPoints = e.TotalPoints;
        }

        public void Handle(MatchCompleted e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Matches.Any(y => y.Id == e.Id));
            if (tournament == null) return;

            var match = tournament.Matches.SingleOrDefault(x => x.Id == e.Id);
            match.IsComplete = true;

            var divisionMatches = tournament.Matches.Where(x => x.Division == e.Division && x.Number == e.Number);

            if (!Dispatcher.IsRegenerating && divisionMatches.All(x => x.IsComplete))
            {
                var matchesHtml = divisionMatches.Select(BuildMatchTableHtml).ToList();

                Dispatcher.SendCommand(
                    new EmailDivisionGameResult
                    {
                        Id = tournament.Id,
                        Year = tournament.Year,
                        Division = e.Division,
                        GameNumber = e.Number,
                        Title = ${e.Division} - Game {e.Number} ({tournament.Year})",
                        Message = String.Join("<hr/>", matchesHtml),
                    });
            }
        }

        public String BuildMatchTableHtml(Match match)
        {
            var scoreRows = "";
            for (var i = 0; i < match.Home.Bowlers.Count; i++)
            {
                var home = match.Home.Bowlers[i];
                var away = match.Away.Bowlers[i];

                scoreRows += $@"
                        <tr>
                            <td></td>
                            <td>{home.Position}</td>
                            <td>{home.Name}</td>
                            <td>{home.Score}</td>

                            <td></td>
                            <td>{away.Position}</td>
                            <td>{away.Name}</td>
                            <td>{away.Score}</td>
                        </tr>
                ";
            }

            return $@"
                <table style='width:800px'>
                    <tr>
                        <th style='width:50px'>{match.Home.Province}</th>
                        <th style='width:50px; text-align: left;'>Pos</th>
                        <th style='width:250px; text-align: left;'>Name</th>
                        <th style='width:50px; text-align: left;'>Score</th>

                        <th style='width:50px'>{match.Away.Province}</th>
                        <th style='width:50px; text-align: left;'>Pos</th>
                        <th style='width:250px; text-align: left;'>Name</th>
                        <th style='width:50px; text-align: left;'>Score</th>
                    </tr>
                    {scoreRows}
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th>{match.Home.Score}</th>

                        <th></th>
                        <th></th>
                        <th></th>
                        <th>{match.Away.Score}</th>
                    </tr>
                </table>
            ";
        }
    }
}
