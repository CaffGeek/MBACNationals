using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using MBACNationals.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace MBACNationalsTests
{
    [TestClass]
    public class ContingentTests : BDDTest<ContingentCommandHandlers, ContingentAggregate>
    {
        public Mock<ICommandQueries> CommandQueriesMock { get; set; }

        private Guid contingentId;
        private string contingentProvince;

        [TestInitialize]
        public void Setup()
        {
            CommandQueriesMock = new Mock<ICommandQueries>();
            SystemUnderTest(new ContingentCommandHandlers(CommandQueriesMock.Object));
            contingentId = Guid.NewGuid();
            contingentProvince = "ZZ";
        }

        [TestMethod]
        public void CanCreateContingent()
        {
            Test(
                Given(),
                When(new CreateContingent
                {
                    Id = contingentId,
                    Province = contingentProvince,
                }),
                Then(new ContingentCreated
                {
                    Id = contingentId,
                    Province = contingentProvince,
                }));
        }

        [TestMethod]
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
