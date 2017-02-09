using Edument.CQRS;
using Events.Tournament;
using MBACNationals.ReadModels;
using MBACNationals.Tournament;
using MBACNationals.Tournament.Commands;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MBACNationalsTests
{
    [TestFixture]
    public class TournamentTests : BDDTest<TournamentCommandHandlers, TournamentAggregate>
    {
        [SetUp]
        public void Setup()
        {
            var tournamentMock = new Mock<CommandQueries.Tournament>();
            tournamentMock.Object.Id = Guid.NewGuid();
            tournamentMock.SetupGet(x => x.Year).Returns("9999");

            var commandQueriesMock = new Mock<ICommandQueries>();
            commandQueriesMock
                    .Setup(m => m.GetTournaments())
                    .Returns(new List<CommandQueries.Tournament>() { tournamentMock.Object });
            SystemUnderTest(new TournamentCommandHandlers(commandQueriesMock.Object));
        }

        [Test]
        public void CanCreateTournament()
        {
            var newId = Guid.NewGuid();
            Test(
                Given(),
                When(new CreateTournament
                {
                    Id = newId,
                    Year = "2014",
                }),
                Then(new TournamentCreated
                {
                    Id = newId,
                    Year = "2014",
                }));
        }

        [Test]
        public void CanCreateAnotherTournament()
        {
            var newId = Guid.NewGuid();
            Test(
                Given(),
                When(new CreateTournament
                {
                    Id = newId,
                    Year = "2015",
                }),
                Then(new TournamentCreated
                {
                    Id = newId,
                    Year = "2015",
                }));
        }

        [Test]
        public void CanNotDuplicateTournament()
        {
            var newId = Guid.NewGuid();
            Test(
                Given(),
                When(new CreateTournament
                {
                    Id = newId,
                    Year = "9999",
                }),
                ThenFailWith<TournamentAlreadyExists>()
            );
        }
    }
}
