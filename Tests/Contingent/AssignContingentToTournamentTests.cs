using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using MBACNationals.ReadModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MBACNationalsTests
{
    [TestFixture]
    public class AssignContingentToTournamentTests
        : BDDTest<ContingentCommandHandlers, ContingentAggregate, CommandQueries>
    {
        private Mock<ICommandQueries> CommandQueriesMock;

        [SetUp]
        public void Setup()
        {
            CommandQueriesMock = new Mock<ICommandQueries>();
            SystemUnderTest(new ContingentCommandHandlers(CommandQueriesMock.Object));
        }

        [Test]
        public void WhenTournamnetExists()
        {
            var contingentId = Guid.NewGuid();
            var tournamentId = Guid.NewGuid();

            CommandQueriesMock
                .Setup(x => x.GetTournaments())
                .Returns(new List<CommandQueries.Tournament> { new CommandQueries.Tournament{ Id = tournamentId} });

            Test(
                Given(new ContingentCreated
                {
                    Id = contingentId,
                    Province = "MB",
                }),
                When(new AssignContingentToTournament
                {
                    Id = contingentId,
                    TournamentId = tournamentId
                }),
                Then(new ContingentAssignedToTournament
                {
                    Id = contingentId,
                    TournamentId = tournamentId
                }));
        }

        [Test]
        public void WhenTournamnetIsMissing()
        {
            var contingentId = Guid.NewGuid();
            var tournamentId = Guid.NewGuid();

            CommandQueriesMock
                .Setup(x => x.GetTournaments())
                .Returns(new List<CommandQueries.Tournament> {  });

            Test(
                Given(new ContingentCreated
                {
                    Id = contingentId,
                    Province = "MB",
                }),
                When(new AssignContingentToTournament
                {
                    Id = contingentId,
                    TournamentId = tournamentId
                }),
                ThenFailWith<TournamentNotFound>());
        }
    }
}
