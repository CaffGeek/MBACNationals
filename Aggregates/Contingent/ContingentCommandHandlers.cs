using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent.Commands;
using System;
using System.Collections;
using System.Linq;

namespace MBACNationals.Contingent
{
    public class ContingentCommandHandlers :
        IHandleCommand<CreateContingent, ContingentAggregate>,
        IHandleCommand<CreateTeam, ContingentAggregate>,
        IHandleCommand<RemoveTeam, ContingentAggregate>,
        IHandleCommand<ChangeRoomType, ContingentAggregate>,
        IHandleCommand<SaveTravelPlans, ContingentAggregate>,
        IHandleCommand<SavePracticePlan, ContingentAggregate>,
        IHandleCommand<SaveReservationInstructions, ContingentAggregate>
    {
        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, CreateContingent command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new ContingentAlreadyExists();

            yield return new ContingentCreated
            {
                Id = command.Id,
                Province = command.Province
            };
        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, CreateTeam command)
        {
            var contingentAggregate = al(command.ContingentId);

            if (!contingentAggregate.Teams.Any(t => t.Name.Equals(command.Name)))
                yield return new TeamCreated
                {
                    Id = command.ContingentId,
                    TeamId = command.TeamId,
                    Name = command.Name,
                    Gender = command.Gender,
                    SizeLimit = command.SizeLimit,
                    RequiresShirtSize = command.RequiresShirtSize,
                    RequiresCoach = command.RequiresCoach,
                    RequiresAverage = command.RequiresAverage,
                    RequiresBio = command.RequiresBio,
                    RequiresGender = command.RequiresGender,
                    IncludesSinglesRep = command.IncludesSinglesRep
                };            
        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, RemoveTeam command)
        {
            var contingentAggregate = al(command.ContingentId);
                        
            yield return new TeamRemoved
            {
                Id = command.ContingentId,
                TeamId = command.TeamId
            };
        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, ChangeRoomType command)
        {
            var contingentAggregate = al(command.Id);
            
            yield return new RoomTypeChanged
            {
                Id = command.Id,
                RoomNumber = command.RoomNumber,
                Type = command.Type,
            };

        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, SaveTravelPlans command)
        {
            var contingentAggregate = al(command.Id);

            var travelPlans = command.TravelPlans.Select(x => new TravelPlansChanged.TravelPlan
            {
                ModeOfTransportation = x.ModeOfTransportation,
                When = x.When,
                FlightNumber = x.FlightNumber,
                NumberOfPeople = x.NumberOfPeople,
                Type = x.Type
            }).ToList();
            
            yield return new TravelPlansChanged
            {
                Id = command.Id,
                TravelPlans = travelPlans
            };
        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, SavePracticePlan command)
        {
            var contingentAggregate = al(command.Id);
            foreach (var teamCommand in command.Teams)
            {
                var team = contingentAggregate.Teams.FirstOrDefault(x => x.Id == teamCommand.Id);
                
                if (team == null)
                    continue;

                if (team.PracticePlan.PracticeLocation == teamCommand.PracticeLocation
                    && team.PracticePlan.PracticeTime == teamCommand.PracticeTime)
                    continue;

                yield return new TeamPracticeRescheduled {
                    Id = command.Id,
                    TeamId = teamCommand.Id,
                    PracticeLocation = teamCommand.PracticeLocation,
                    PracticeTime = teamCommand.PracticeTime,
                };
            }
        }

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, SaveReservationInstructions command)
        {
            var contingentAggregate = al(command.Id);

            yield return new ReservationInstructionsChanged
            {
                Id = command.Id,
                Instructions = command.Instructions,
            };
        }
    }
}
