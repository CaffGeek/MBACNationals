//using Edument.CQRS;
//using Events.Participant;
//using MBACNationals.Participant;
//using MBACNationals.Participant.Commands;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace MBACNationalsTests
//{
//    [TestClass]
//    public class ParticipantTests : BDDTest<ParticipantCommandHandlers, ParticipantAggregate>
//    {
//        private Guid participantId;
//        private Guid teamId;

//        private string name;
//        private string newName;

//        [TestInitialize]
//        public void Setup()
//        {
//            participantId = Guid.NewGuid();
//            teamId = Guid.NewGuid();
//            name = "John";
//            newName = "David";
//        }

//        [TestMethod]
//        public void CanCreateParticipant()
//        {
//            Test(
//                Given(),
//                When(new CreateParticipant
//                {
//                    Id = participantId,
//                    Name = name,
//                    Gender = "M",
//                    IsDelegate = true,
//                    YearsQualifying = 10,

//                }),
//                Then(new ParticipantCreated
//                {
//                    Id = participantId,
//                    Name = name,
//                    Gender = "M",
//                    IsDelegate = true,
//                    YearsQualifying = 10,
//                }));
//        }

//        [TestMethod]
//        public void CanNotDuplicateParticipant()
//        {
//            Test(
//                Given(new ParticipantCreated
//                {
//                    Id = participantId,
//                    Name = name
//                }),
//                When(new CreateParticipant
//                {
//                    Id = participantId,
//                    Name = name
//                }),
//                ThenFailWith<ParticipantAlreadyExists>());
//        }

//        [TestMethod]
//        public void CanRenameParticipant()
//        {
//            Test(
//                Given(new ParticipantCreated
//                {
//                    Id = participantId,
//                    Name = name
//                }),
//                When(new RenameParticipant
//                {
//                    Id = participantId,
//                    Name = newName
//                }),
//                Then(new ParticipantRenamed
//                {
//                    Id = participantId,
//                    Name = newName
//                }));
//        }

//        [TestMethod]
//        public void CanAssignParticipantToTeam()
//        {
//            Test(
//                Given(),
//                When(new AddParticipantToTeam
//                {
//                    Id = participantId,
//                    TeamId = teamId
//                }),
//                Then(new ParticipantAssignedToTeam
//                {
//                    Id = participantId,
//                    TeamId = teamId
//                }));
//        }
//    }
//}
