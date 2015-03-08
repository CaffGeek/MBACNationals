using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.Contingent
{
    public class ContingentAggregate : Aggregate,
        IApplyEvent<ContingentCreated>,
        IApplyEvent<TeamCreated>,
        IApplyEvent<TeamRemoved>,
        IApplyEvent<RoomTypeChanged>,
        IApplyEvent<TravelPlansChanged>,
        IApplyEvent<TeamPracticeRescheduled>,
        IApplyEvent<ReservationInstructionsChanged>,
        IApplyEvent<ParticipantDesignatedAsAlternate>
    {
        public string Province { get; private set; }
        public List<Team> Teams { get; private set; }
        public List<TravelPlan> TravelPlans { get; private set; }
        public List<HotelRoom> HotelRooms { get; private set; }
        public string Instructions { get; private set; }

        public ContingentAggregate()
        {
            Teams = new List<Team>();
            TravelPlans = new List<TravelPlan>();
            HotelRooms = new List<HotelRoom>();
        }

        public void Apply(ContingentCreated e)
        {
            Province = e.Province;
        }

        public void Apply(TeamCreated e)
        {
            var team = new Team(e, Id.Value);
            Teams.Add(team);
        }

        public void Apply(TeamRemoved e)
        {
            Teams.RemoveAll(x => x.Id.Equals(e.TeamId));
        }

        public void Apply(RoomTypeChanged e)
        {
            var room = HotelRooms.FirstOrDefault(x => x.Number == e.RoomNumber);
            if (room == null)
                HotelRooms.Add(new HotelRoom(e));
            else
                room.Type = e.Type;
        }

        public void Apply(TravelPlansChanged e)
        {
            TravelPlans = e.TravelPlans.Select(x => new TravelPlan(x)).ToList();
        }

        public void Apply(TeamPracticeRescheduled e)
        {
            var team = Teams.FirstOrDefault(x => x.Id.Equals(e.TeamId));
            if (team == null)
                return;

            team.Apply(e);
        }

        public void Apply(ReservationInstructionsChanged e)
        {
            Instructions = e.Instructions;
        }

        public void Apply(ParticipantDesignatedAsAlternate e)
        {
            var team = Teams.FirstOrDefault(x => x.Id.Equals(e.TeamId));
            if (team == null)
                return;

            team.Apply(e);
        }
    }

    public class Team
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid ContingentId { get; private set; }
        public string Gender { get; private set; }
        public int SizeLimit { get; private set; }
        public bool RequiresShirtSize { get; private set; }
        public bool RequiresCoach { get; private set; }
        public bool RequiresAverage { get; private set; }
        public bool RequiresBio { get; private set; }
        public bool RequiresGender { get; private set; }
        public bool IncludesSinglesRep { get; private set; }
        public PracticePlan PracticePlan { get; private set; }
        public Guid AlternateId { get; private set; }

        public Team(TeamCreated e, Guid contingentId)
        {
            PracticePlan = new PracticePlan();
            ContingentId = contingentId;
            Apply(e);
        }

        public void Apply(TeamCreated e)
        {
            Id = e.TeamId;
            Name = e.Name;
            Gender = e.Gender;
            SizeLimit = e.SizeLimit;
            RequiresShirtSize = e.RequiresShirtSize;
            RequiresCoach = e.RequiresCoach;
            RequiresAverage = e.RequiresAverage;
            RequiresBio = e.RequiresBio;
            RequiresGender = e.RequiresGender;
            IncludesSinglesRep = e.IncludesSinglesRep;
        }

        public void Apply(TeamPracticeRescheduled e)
        {
            PracticePlan.PracticeLocation = e.PracticeLocation;
            PracticePlan.PracticeTime = e.PracticeTime;
        }

        public void Apply(ParticipantDesignatedAsAlternate e)
        {
            AlternateId = e.Id;
        }
    }

    public class TravelPlan
    {
        public string ModeOfTransportation { get; set; }
        public DateTime When { get; set; }
        public string FlightNumber { get; set; }
        public int NumberOfPeople { get; set; }
        public int Type { get; set; }

        public TravelPlan(dynamic e)
        {            
            ModeOfTransportation = e.ModeOfTransportation;
            When = e.When;
            FlightNumber = e.FlightNumber;
            NumberOfPeople = e.NumberOfPeople;
            Type = e.Type;
        }
    }

    public class HotelRoom
    {
        public int Number { get; set; }
        public string Type { get; set; }

        public HotelRoom(dynamic e)
        {
            Number = e.RoomNumber;
            Type = e.Type;
        }
    }

    public class PracticePlan
    {
        public string PracticeLocation { get; set; }
        public int PracticeTime { get; set; }
    }
}
