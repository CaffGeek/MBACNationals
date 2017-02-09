using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using MBACNationals.ReadModels;
using Moq;
using NUnit.Framework;
using System;

namespace MBACNationalsTests
{
    [TestFixture]
    public class ContingentTests : BDDTest<ContingentCommandHandlers, ContingentAggregate>
    {
        public Mock<ICommandQueries> CommandQueriesMock { get; set; }

        private Guid tournamentId;
        private Guid contingentId;
        private string contingentProvince;

        [SetUp]
        public void Setup()
        {
            CommandQueriesMock = new Mock<ICommandQueries>();
            SystemUnderTest(new ContingentCommandHandlers(CommandQueriesMock.Object));
            tournamentId = Guid.NewGuid();
            contingentId = Guid.NewGuid();
            contingentProvince = "ZZ";
        }

        [Test]
        public void CanCreateContingent()
        {
            Test(
                Given(),
                When(new CreateContingent
                {
                    Id = contingentId,
                    Province = contingentProvince,
                    TournamentId = tournamentId,
                }),
                Then(new ContingentCreated
                {
                    Id = contingentId,
                    Province = contingentProvince,
                },
                new ContingentAssignedToTournament
                {
                    Id = contingentId,
                    TournamentId = tournamentId,
                }));
        }

        [Test]
        public void CanNotDuplicateContingent()
        {
            Test(
                Given(new ContingentCreated
                {
                    Id = contingentId,
                    Province = contingentProvince,
                }),
                When(new CreateContingent
                {
                    Id = contingentId,
                    Province = contingentProvince,
                }),
                ThenFailWith<ContingentAlreadyExists>());
        }
    }
}
