using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : AzureReadModel,
        IContingentTravelPlanQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TravelPlansChanged>,
        ISubscribeTo<RoomTypeChanged>,
        ISubscribeTo<ReservationInstructionsChanged>
    {
        public ContingentTravelPlanQueries(string readModelFilePath)
        {

        }

        public class ContingentTravelPlans
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<TravelPlan> TravelPlans { get; internal set; }
        }

        public class ContingentRooms
        {
            public Guid Id { get; internal set; }
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

        private class TSContingent : Entity
        {
            public string Province { get; set; }
            public string Instructions { get; set; }
        }

        private class TSTravelPlan : Entity
        {
            public string ModeOfTransportation { get; set; }
            public string When { get; set; }
            public string FlightNumber { get; set; }
            public int NumberOfPeople { get; set; }
            public int Type { get; set; }
        }

        private class TSHotelRoom : Entity
        {
            public int RoomNumber { get; set; }
            public string Type { get; set; }
        }

        public ContingentTravelPlans GetTravelPlans(string province)
        {
            throw new NotImplementedException();
            //TODO: Read<ContingentTravelPlans>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public ContingentRooms GetRooms(string province)
        {
            throw new NotImplementedException();
            //TODO: Read<ContingentRooms>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(e.Id, e.Id, new TSContingent
            {
                Province = e.Province
            });
        }

        public void Handle(TravelPlansChanged e)
        {
            //Delete old plans
            Query<TSTravelPlan>(x => x.PartitionKey == e.Id.ToString())
                .ForEach(x => Delete<TSTravelPlan>(Guid.Parse(x.PartitionKey), Guid.Parse(x.RowKey)));

            //Create new ones
            e.TravelPlans.ForEach(plan =>
            {
                Create(e.Id, Guid.NewGuid(), new TSTravelPlan
                {
                    ModeOfTransportation = plan.ModeOfTransportation,
                    When = plan.When.ToString("yyyy-MM-ddTHH:mm"),
                    FlightNumber = plan.FlightNumber,
                    NumberOfPeople = plan.NumberOfPeople,
                    Type = plan.Type
                });
            });
        }

        public void Handle(RoomTypeChanged e)
        {
            var roomBytes = BitConverter.GetBytes(e.RoomNumber);
            var eightBytes = Enumerable
                .Repeat<Byte>(0, 8 - roomBytes.Length)
                .Concat(roomBytes)
                .ToArray();
            var roomKey = new Guid(0, 0, 0, eightBytes);

            Create(e.Id, roomKey, new TSHotelRoom
            {
                RoomNumber = e.RoomNumber,
                Type = e.Type,
            });
        }

        public void Handle(ReservationInstructionsChanged e)
        {
            Update<TSContingent>(e.Id, e.Id, contingent => contingent.Instructions = e.Instructions);
        }
    }
}
