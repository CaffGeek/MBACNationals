using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using MBACNationals.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MBACNationalsTests
{
    [TestClass]
    public class AssignContingentToTournamentTests 
        : BDDTest<ContingentCommandHandlers, ContingentAggregate>
    {
        private Mock<ICommandQueries> CommandQueriesMock;

        [TestInitialize]
        public void Setup()
        {
            CommandQueriesMock = new Mock<ICommandQueries>();
            SystemUnderTest(new ContingentCommandHandlers(CommandQueriesMock.Object));
        }

        [TestMethod]
        public void WhenTournamnetExists()
        {
            var contingentId = Guid.NewGuid();
            var tournamentId = Guid.NewGuid();

            CommandQueriesMock
                .Setup(x => x.GetTournaments())
                .Returns(new List<CommandQueries.Tournament> { new CommandQueries.Tournament(tournamentId) });

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

        [TestMethod]
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
