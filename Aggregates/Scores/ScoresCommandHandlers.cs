using Edument.CQRS;
using Events.Scores;
using MBACNationals.Participant;
using MBACNationals.Scores.Commands;
using System;
using System.Linq;
using System.Collections;

namespace MBACNationals.Scores
{
    public class ScoresCommandHandlers :
        IHandleCommand<SaveMatchResult, MatchAggregate>,
        IHandleCommand<SaveMatch, MatchAggregate>
    {
        private MessageDispatcher _dispatcher;

        public ScoresCommandHandlers(MessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IEnumerable Handle(Func<Guid, MatchAggregate> al, SaveMatchResult command)
        {
            var agg = al(command.Id);
            
            var match = _dispatcher.Load<MatchAggregate>(command.Id);

            if (match.IsPOA)
            {
                var awayBowler = command.Away.Bowlers.First();
                var homeBowler = command.Home.Bowlers.First();

                var awayParticipant = _dispatcher.Load<ParticipantAggregate>(awayBowler.Id);
                var homeParticipant = _dispatcher.Load<ParticipantAggregate>(homeBowler.Id);

                var awayPOA = awayBowler.Score - awayParticipant.Average;
                var homePOA = homeBowler.Score - homeParticipant.Average;

                var awaySinglePoints = CalculatePoint(awayPOA, homePOA, 2);
                var homeSinglePoints = CalculatePoint(homePOA, awayPOA, 2);

                //Away
                yield return new TeamGameCompleted
                {
                    Id = command.Id,
                    Number = match.Number,
                    Division = agg.Division + " Single",
                    Contingent = match.Away,
                    Opponent = match.Home,
                    TeamId = command.Away.Id,
                    Score = awayBowler.Score,
                    POA = awayPOA,
                    Points = awaySinglePoints,
                    OpponentPoints = homeSinglePoints,
                    TotalPoints = awaySinglePoints,
                    OpponentScore = homeBowler.Score,
                    OpponentPOA = homePOA,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName,
                    IsPOA = match.IsPOA
                };

                //Home
                yield return new TeamGameCompleted
                {
                    Id = command.Id,
                    Number = match.Number,
                    Division = agg.Division + " Single",
                    Contingent = match.Home,
                    Opponent = match.Away,
                    TeamId = command.Home.Id,
                    Score = homeBowler.Score,
                    POA = homePOA,
                    Points = homeSinglePoints,
                    OpponentPoints = awaySinglePoints,
                    TotalPoints = homeSinglePoints,
                    OpponentScore = awayBowler.Score,
                    OpponentPOA = awayPOA,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName,
                    IsPOA = match.IsPOA
                };
            }

            var awayTeamScore = 0;
            var homeTeamScore = 0;
            var awayTeamPOA = 0;
            var homeTeamPOA = 0;
            var awayTeamPoints = 0m;
            var homeTeamPoints = 0m;

            for (var i = 1; i <= command.Away.Bowlers.Length; i++)
            {
                var awayBowler = command.Away.Bowlers.SingleOrDefault(x=>x.Position == i);
                if (awayBowler == null)
                    continue;
                var awayParticipant = _dispatcher.Load<ParticipantAggregate>(awayBowler.Id);

                var homeBowler = command.Home.Bowlers.SingleOrDefault(x => x.Position == i);
                if (homeBowler == null)
                    continue;
                var homeParticipant = _dispatcher.Load<ParticipantAggregate>(homeBowler.Id);

                var awayPOA = awayBowler.Score - awayParticipant.Average;
                var homePOA = homeBowler.Score - homeParticipant.Average;

                awayTeamScore += awayBowler.Score;
                homeTeamScore += homeBowler.Score;

                awayTeamPOA += awayPOA;
                homeTeamPOA += homePOA;

                var maxIndividualPoints = agg.Division.Contains("Single") ? 2 : 1;
                var awayBowlerPoint = agg.IsPOA
                    ? CalculatePoint(awayPOA, homePOA, maxIndividualPoints)
                    : CalculatePoint(awayBowler.Score, homeBowler.Score, maxIndividualPoints);

                var homeBowlerPoint = agg.IsPOA
                    ? CalculatePoint(homePOA, awayPOA, maxIndividualPoints)
                    : CalculatePoint(homeBowler.Score, awayBowler.Score, maxIndividualPoints);
                
                awayTeamPoints += awayBowlerPoint;
                homeTeamPoints += homeBowlerPoint;

                //Away
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = awayBowler.Id,
                    Number = match.Number,
                    Name = awayParticipant.Name,
                    Gender = awayParticipant.Gender,
                    Division = agg.Division,
                    Contingent = match.Away,
                    Opponent = match.Home,
                    OpponentName = homeParticipant.Name,
                    OpponentScore = homeBowler.Score,
                    OpponentPOA = homePOA,
                    Score = awayBowler.Score,
                    Position = awayBowler.Position,
                    POA = awayPOA,
                    Points = awayBowlerPoint,
                    Lane = match.Lane,
                    Centre = match.CentreName,
                    IsPOA = match.IsPOA
                };

                //Home
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = homeBowler.Id,
                    Number = match.Number,
                    Name = homeParticipant.Name,
                    Gender = homeParticipant.Gender,
                    Division = agg.Division,
                    Contingent = match.Home,
                    Opponent = match.Away,
                    OpponentName = awayParticipant.Name,
                    OpponentScore = awayBowler.Score,
                    OpponentPOA = awayPOA,
                    Score = homeBowler.Score,
                    Position = homeBowler.Position,
                    POA = homePOA,
                    Points = homeBowlerPoint,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName,
                    IsPOA = match.IsPOA
                };
            }

            var maxTeamPoints = agg.Division.Contains("Single") ? 0 : 3;
            var awayTeamPoint = agg.IsPOA
                ? CalculatePoint(awayTeamPOA, homeTeamPOA, maxTeamPoints)
                : CalculatePoint(awayTeamScore, homeTeamScore, maxTeamPoints);

            var homeTeamPoint = agg.IsPOA
                ? CalculatePoint(homeTeamPOA, awayTeamPOA, maxTeamPoints)
                : CalculatePoint(homeTeamScore, awayTeamScore, maxTeamPoints);

            //Away
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Contingent = match.Away,
                Opponent = match.Home,
                TeamId = command.Away.Id,
                Score = awayTeamScore,
                POA = awayTeamPOA,
                Points = awayTeamPoint,
                TotalPoints = awayTeamPoints + awayTeamPoint,
                OpponentPoints = homeTeamPoints + homeTeamPoint,
                OpponentScore = homeTeamScore,
                OpponentPOA = homeTeamPOA,
                Lane = match.Lane,
                Centre = match.CentreName,
                IsPOA = match.IsPOA
            };

            //Home
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Contingent = match.Home,
                Opponent = match.Away,
                TeamId = command.Home.Id,
                Score = homeTeamScore,
                POA = homeTeamPOA,
                Points = homeTeamPoint,
                TotalPoints = homeTeamPoints + homeTeamPoint,
                OpponentPoints = awayTeamPoints + awayTeamPoint,
                OpponentScore = awayTeamScore,
                OpponentPOA = awayTeamPOA,
                Lane = match.Lane + 1,
                Centre = match.CentreName,
                IsPOA = match.IsPOA
            };

            yield return new MatchCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Home = match.Home,
                Away = match.Away
            };
        }

        public IEnumerable Handle(Func<Guid, MatchAggregate> al, SaveMatch command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new MatchAlreadyCreated();

            yield return new MatchCreated
            {
                Id = command.Id,
                Division = command.Division,
                IsPOA = command.IsPOA,
                Home = command.Home,
                Away = command.Away,
                Centre = command.Centre,
                CentreName = command.CentreName,
                Lane = command.Lane,
                Number = command.Number
            };            
        }

        public decimal CalculatePoint(int score, int opponentScore, decimal maxPoint)
        {
            return score > opponentScore ? maxPoint
                        : score == opponentScore ? maxPoint / 2m
                        : 0m;
        }
    }
}
