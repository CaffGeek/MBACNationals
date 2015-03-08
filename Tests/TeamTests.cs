using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent;
using MBACNationals.Contingent.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MBACNationalsTests
{
    [TestClass]
    public class TeamTests : BDDTest<ContingentCommandHandlers, ContingentAggregate>
    {
        private Guid teamId;
        private Guid contingentId;
        private string teamName;

        [TestInitialize]
        public void Setup()
        {
            teamId = Guid.NewGuid();
            contingentId = Guid.NewGuid();
            teamName = "Test Team";
        }

        [TestMethod]
        public void CanCreateTeam()
        {
            Test(
                Given(new ContingentCreated
                {
                    Id = contingentId,
                }),
                When(new CreateTeam
                {
                    ContingentId = contingentId,
                    TeamId = teamId,
                    Name = teamName,
                }),
                Then(new TeamCreated
                {
                    Id = contingentId,
                    TeamId = teamId,
                    Name = teamName,
                }));
        }

        //[TestMethod]
        //public void CanNotDuplicateTeam()
        //{
        //    Test(
        //        Given(new ContingentCreated
        //        {
        //            Id = contingentId,
        //        }, new TeamCreated
        //        {
        //            Id = contingentId,
        //            TeamId = teamId,
        //            Name = teamName,
        //        }),
        //        When(new CreateTeam
        //        {
        //            ContingentId = contingentId,
        //            TeamId = teamId,
        //            Name = teamName,
        //        }),
        //        ThenFailWith<TeamAlreadyExists>());
        //}
    }
}
