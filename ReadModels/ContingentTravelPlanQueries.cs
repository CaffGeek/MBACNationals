using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : AReadModel,
        IContingentTravelPlanQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TravelPlansChanged>,
        ISubscribeTo<RoomTypeChanged>,
        ISubscribeTo<ReservationInstructionsChanged>
    {
        public ContingentTravelPlanQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class ContingentTravelPlans : AEntity
        {
            public ContingentTravelPlans(Guid id) : base(id) { }
            public string Province { get; internal set; }
            public IList<TravelPlan> TravelPlans { get; internal set; }
        }

        public class ContingentRooms : AEntity
        {
            public ContingentRooms(Guid id) : base(id) { }
            public string Province { get; internal set; }
            public IList<HotelRoom> HotelRooms { get; internal set; }
            public string Instructions { get; internal set; }
        }

        public class TravelPlan
        {
            public string ModeOfTransportation { get; internal set; }
            public string When { get; internal set; }
            public string FlightNumber { get; internal set; }
            public int NumberOfPeople { get; internal set; }
            public int Type { get; internal set; }
        }

        public class HotelRoom
        {
            public int RoomNumber { get; internal set; }
            public string Type { get; internal set; }
        }

        public ContingentTravelPlans GetTravelPlans(string province)
        {
            return Read<ContingentTravelPlans>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public ContingentRooms GetRooms(string province)
        {
            return Read<ContingentRooms>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new ContingentTravelPlans(e.Id)
            {
                Province = e.Province,
                TravelPlans = new List<TravelPlan>()
            });

            Create(new ContingentRooms(e.Id)
            {
                Province = e.Province,
                HotelRooms = new List<HotelRoom>()
            });
        }

        public void Handle(TravelPlansChanged e)
        {
            Update<ContingentTravelPlans>(e.Id, contingent =>
            {
                contingent.TravelPlans = e.TravelPlans.Select(x =>
                {
                    return new TravelPlan
                    {
                        ModeOfTransportation = x.ModeOfTransportation,
                        When = x.When.ToString("yyyy-MM-ddTHH:mm"),
                        FlightNumber = x.FlightNumber,
                        NumberOfPeople = x.NumberOfPeople,
                        Type = x.Type
                    };
                }).ToList();
            });
        }

        public void Handle(RoomTypeChanged e)
        {
            Update<ContingentRooms>(e.Id, contingent =>
            {
                var room = contingent.HotelRooms.FirstOrDefault(x => x.RoomNumber.Equals(e.RoomNumber));
                if (room != null)
                {
                    room.Type = e.Type;
                }
                else
                {
                    contingent.HotelRooms.Add(new HotelRoom
                    {
                        RoomNumber = e.RoomNumber,
                        Type = e.Type
                    });
                }
            });         
        }

        public void Handle(ReservationInstructionsChanged e)
        {
            Update<ContingentRooms>(e.Id, contingent => { contingent.Instructions = e.Instructions; });
        }
    }
}
