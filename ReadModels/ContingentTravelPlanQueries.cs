using Edument.CQRS;
using Events.Contingent;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : BaseReadModel<ContingentTravelPlanQueries>,
        IContingentTravelPlanQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TravelPlansChanged>,
        ISubscribeTo<RoomTypeChanged>,
        ISubscribeTo<ReservationInstructionsChanged>
    {
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

        private class TSTournament : Entity
        {
            public string Year { get; set; }
            public Guid Id { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid TournamentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string Province { get; set; }
            public string Instructions { get; set; }
        }

        private class TSTravelPlan : Entity
        {
            public Guid ContingentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string ModeOfTransportation { get; set; }
            public string When { get; set; }
            public string FlightNumber { get; set; }
            public int NumberOfPeople { get; set; }
            public int Type { get; set; }
        }

        private class TSHotelRoom : Entity
        {
            public Guid ContingentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public int RoomNumber { get; set; }
            public string Type { get; set; }
        }

        public List<ContingentTravelPlans> GetAllTravelPlans(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);

            var allPlans = new List<ContingentTravelPlans>();
            foreach (var contingent in contingents)
            {
                var travelPlans = Storage.Query<TSTravelPlan>(x => x.ContingentId == contingent.Id)
                    .Select(x => new TravelPlan
                    {
                        ModeOfTransportation = x.ModeOfTransportation,
                        When = x.When,
                        FlightNumber = x.FlightNumber,
                        NumberOfPeople = x.NumberOfPeople,
                        Type = x.Type
                    }).ToList();

                var contingentTravelPlan = new ContingentTravelPlans
                {
                    Id = contingent.Id,
                    Province = contingent.Province,
                    TravelPlans = travelPlans,
                };
                allPlans.Add(contingentTravelPlan);
            }

            return allPlans;
        }

        public ContingentTravelPlans GetTravelPlans(string year, string province)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            var contingent = Storage.Query<TSContingent>(x => 
                x.TournamentId == tournament.Id 
                && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var travelPlans = Storage.Query<TSTravelPlan>(x => x.ContingentId == contingent.Id)
                .Select(x => new TravelPlan
                {
                    ModeOfTransportation = x.ModeOfTransportation,
                    When = x.When,
                    FlightNumber = x.FlightNumber,
                    NumberOfPeople = x.NumberOfPeople,
                    Type = x.Type
                }).ToList();

            var contingentTravelPlan = new ContingentTravelPlans
            {
                Id = contingent.Id,
                Province = contingent.Province,
                TravelPlans = travelPlans,
            };
            return contingentTravelPlan;
        }

        public List<ContingentRooms> GetAllRooms(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);

            var allRooms = new List<ContingentRooms>();
            foreach (var contingent in contingents)
            {
                var hotelRooms = Storage.Query<TSHotelRoom>(x => x.ContingentId == contingent.Id)
                   .Select(x => new HotelRoom
                   {
                       RoomNumber = x.RoomNumber,
                       Type = x.Type
                   }).ToList();

                var contingentRooms = new ContingentRooms
                {
                    Id = contingent.Id,
                    Province = contingent.Province,
                    Instructions = contingent.Instructions,
                    HotelRooms = hotelRooms,
                };
                allRooms.Add(contingentRooms);
            }

            return allRooms;
        }

        public ContingentRooms GetRooms(string year, string province)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            var contingent = Storage.Query<TSContingent>(x =>
                x.TournamentId == tournament.Id
                && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var hotelRooms = Storage.Query<TSHotelRoom>(x => x.ContingentId == contingent.Id)
                .Select(x => new HotelRoom
                {
                    RoomNumber = x.RoomNumber,
                    Type = x.Type
                }).ToList();

            var contingentRooms = new ContingentRooms
            {
                Id = contingent.Id,
                Province = contingent.Province,
                Instructions = contingent.Instructions,
                HotelRooms = hotelRooms,
            };
            return contingentRooms;
        }

        public void Handle(TournamentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSTournament { Year = e.Year, Id = e.Id });

            //HACK: Track current tournament
            Storage.Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        }

        public void Handle(ContingentCreated e)
        {
            if (string.IsNullOrWhiteSpace(e.Province))
                return;

            var tournamentId = GetCurrentTournamentId();

            Storage.Create(tournamentId, e.Id, new TSContingent
            {
                Province = e.Province
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Storage.Read<TSContingent>(Guid.Empty, e.Id);
            if (contingent != null)
            {
                Storage.Delete<TSContingent>(Guid.Empty, e.Id);
                contingent.TournamentId = e.TournamentId;
                Storage.Create(e.TournamentId, e.Id, contingent);
            }
        }

        public void Handle(TravelPlansChanged e)
        {
            //Storage.Delete<TS old plans
            Storage.Query<TSTravelPlan>(x => x.PartitionKey == e.Id.ToString())
                .ForEach(x => Storage.Delete<TSTravelPlan>(Guid.Parse(x.PartitionKey), Guid.Parse(x.RowKey)));

            //Create new ones
            e.TravelPlans.ForEach(plan =>
            {
                Storage.Create(e.Id, Guid.NewGuid(), new TSTravelPlan
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

            Storage.Create(e.Id, roomKey, new TSHotelRoom
            {
                RoomNumber = e.RoomNumber,
                Type = e.Type,
            });
        }

        public void Handle(ReservationInstructionsChanged e)
        {
            var tournamentId = GetCurrentTournamentId();
            Storage.Update<TSContingent>(tournamentId, e.Id, contingent => contingent.Instructions = e.Instructions);
        }

        private Guid GetCurrentTournamentId()
        {
            var tournament = Storage.Read<TSTournament>(Guid.Empty, Guid.Empty)
                ?? new TSTournament { Id = Guid.Empty };
            return tournament.Id;
        }
    }
}
