using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MBACNationalsTests
{
    [TestClass]
    public class ContingentTests : BDDTest<ContingentCommandHandlers, ContingentAggregate>
    {
        private Guid contingentId;
        private string contingentProvince;

        [TestInitialize]
        public void Setup()
        {
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
