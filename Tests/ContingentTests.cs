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
    public class ContingentTests : BDDTest<ContingentCommandHandlers, ContingentAggregate, CommandQueries>
    {
        [SetUp]
        public void Setup()
        {
            var commandQueries = new CommandQueries();
            SystemUnderTest(new ContingentCommandHandlers(commandQueries), commandQueries);
        }

        [Test]
        public void CanCreateContingent()
        {
            var tournamentId = Guid.NewGuid();
            var contingentId = Guid.NewGuid();
            var contingentProvince = "ZZ";

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
            var contingentId = Guid.NewGuid();
            var contingentProvince = "ZZ";

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
