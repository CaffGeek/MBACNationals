using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant;
using MBACNationals.Participant.Commands;
using MBACNationals.ReadModels;
using Moq;
using NUnit.Framework;
using System;

namespace MBACNationalsTests
{
    [TestFixture]
    public class ParticipantTests : BDDTest<ParticipantCommandHandlers, ParticipantAggregate>
    {
        private Guid alternateId;
        private Guid participantId;
        private Guid teamId;
        private string name;
        private string newName;

        [SetUp]
        public void Setup()
        {
            alternateId = Guid.NewGuid();
            participantId = Guid.NewGuid();
            teamId = Guid.NewGuid();
            name = "John";
            newName = "David";

            var commandQueriesMock = new Mock<ICommandQueries>();
            SystemUnderTest(new ParticipantCommandHandlers(commandQueriesMock.Object));
        }

        [Test]
        public void CanCreateParticipant()
        {
            Test(
                Given(),
                When(new CreateParticipant
                {
                    Id = participantId,
                    Name = name,
                    Gender = "M",
                    IsDelegate = true,
                    YearsQualifying = 10,
                }),
                Then(
                    new ParticipantCreated
                    {
                        Id = participantId,
                        Name = name,
                        Gender = "M",
                        IsDelegate = true,
                        YearsQualifying = 10,
                    },
                    new ParticipantAverageChanged
                    {
                        Id = participantId,
                    },
                    new ParticipantShirtSizeChanged
                    {
                        Id = participantId,
                    })
                );
        }

        [Test]
        public void CanNotDuplicateParticipant()
        {
            Test(
                Given(new ParticipantCreated
                {
                    Id = participantId,
                    Name = name
                }),
                When(new CreateParticipant
                {
                    Id = participantId,
                    Name = name
                }),
                ThenFailWith<ParticipantAlreadyExists>());
        }

        [Test]
        public void CanRenameParticipant()
        {
            Test(
                Given(new ParticipantCreated
                {
                    Id = participantId,
                    Name = name
                }),
                When(new RenameParticipant
                {
                    Id = participantId,
                    Name = newName
                }),
                Then(new ParticipantRenamed
                {
                    Id = participantId,
                    Name = newName
                }));
        }

        [Test]
        public void CanAssignParticipantToTeam()
        {
            Test(
                Given(),
                When(new AddParticipantToTeam
                {
                    Id = participantId,
                    TeamId = teamId
                }),
                Then(new ParticipantAssignedToTeam
                {
                    Id = participantId,
                    TeamId = teamId
                }, new ParticipantQualifyingPositionChanged
                {
                    Id = participantId,
                    QualifyingPosition = 1,
                    TeamId = teamId
                }));
        }

        [Test]
        public void CanAssignParticipantToDifferentTeam()
        {
            Test(
                Given(new ParticipantAssignedToTeam
                {
                    Id = participantId,
                    TeamId = Guid.NewGuid(),
                }),
                When(new AddParticipantToTeam
                {
                    Id = participantId,
                    TeamId = teamId
                }),
                Then(new ParticipantAssignedToTeam
                {
                    Id = participantId,
                    TeamId = teamId
                }, new ParticipantQualifyingPositionChanged
                {
                    Id = participantId,
                    QualifyingPosition = 1,
                    TeamId = teamId
                }));
        }
    }
}
