using AzureTableHelper;
using Edument.CQRS;
using Events.Contingent;
using Events.Tournament;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : //BaseReadModel<ContingentTravelPlanQueries>,
        IReadModel,
        IContingentTravelPlanQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TravelPlansChanged>,
        ISubscribeTo<RoomTypeChanged>,
        ISubscribeTo<ReservationInstructionsChanged>
    {
        public List<Tournament> Tournaments { get; private set; }
        public Dictionary<Guid, string> Contingents { get; private set; }

        public class Tournament
        {
            public Guid Id { get; internal set; }
            public string Year { get; internal set; }
            public List<ContingentTravelPlans> ContingentTravelPlans { get; internal set; }
            public List<ContingentRooms> ContingentRooms { get; internal set; }

            public Tournament()
            {
                ContingentTravelPlans = new List<ContingentTravelPlans>();
                ContingentRooms = new List<ContingentRooms>();
            }
        }

        public class ContingentTravelPlans
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<TravelPlan> TravelPlans { get; internal set; }

            public ContingentTravelPlans()
            {
                TravelPlans = new List<TravelPlan>();
            }
        }

        public class TravelPlan
        {
            public string ModeOfTransportation { get; internal set; }
            public string When { get; internal set; }
            public string FlightNumber { get; internal set; }
            public int NumberOfPeople { get; internal set; }
            public int Type { get; internal set; }
        }

        public class ContingentRooms
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<HotelRoom> HotelRooms { get; internal set; }
            public string Instructions { get; internal set; }
        }

        public class HotelRoom
        {
            public int RoomNumber { get; internal set; }
            public string Type { get; internal set; }
        }

        public ContingentTravelPlanQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>() { 
                new Tournament {
                    Year = "2014"
                }
            };
            Contingents = new Dictionary<Guid, string>();
        }

        public void Save()
        {
            //TODO: Move to baseclass
            var jsonModel = JsonConvert.SerializeObject(this);

            var storageConnection = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(storageConnection);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var azureBlobHelper = new AzureBlobHelper(blobClient);
            var container = azureBlobHelper.GetContainerFor("ReadModels");

            var modelBlob = container.GetBlockBlobReference(this.GetType().Name);
            modelBlob.UploadText(jsonModel);
        }

        public List<ContingentTravelPlans> GetAllTravelPlans(string year)
        {
            var t = Tournaments.FirstOrDefault(x => x.Year == year)
                ?? new Tournament();

            return t.ContingentTravelPlans;
        }

        public ContingentTravelPlans GetTravelPlans(string year, string province)
        {
            return GetAllTravelPlans(year).FirstOrDefault(x => x.Province == province);
        }

        public List<ContingentRooms> GetAllRooms(string year) 
        {
            return null;
        }

        public ContingentRooms GetRooms(string year, string province)
        {
            return null;
        }

        public void Handle(TournamentCreated e)
        {
            var tournament = Tournaments.FirstOrDefault(x => x.Year == e.Year);
            if (tournament == null)
            {
                Tournaments.Add(new Tournament
                    {
                        Id = e.Id,
                        Year = e.Year
                    });
            }
            else
            {
                tournament.Id = e.Id;
            }
        }

        public void Handle(ContingentCreated e)
        {
            Contingents.Add(e.Id, e.Province);
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var tournament = Tournaments.Single(x => x.Id == e.TournamentId);
            var contingent = Contingents.Single(x => x.Key == e.Id);

            tournament.ContingentTravelPlans.Add(new ContingentTravelPlans
                {
                    Id = e.Id,
                    Province = contingent.Value
                });
        }

        public void Handle(TravelPlansChanged e)
        {
            var tournament = (from t in Tournaments
                              where t.ContingentTravelPlans.Any(x => x.Id == e.Id)
                              select t).SingleOrDefault()
                              ??
                             (from t in Tournaments
                              where t.Year == 2014.ToString()
                              select t).SingleOrDefault();

            var travelPlans = e.TravelPlans.Select(travelPlan => new TravelPlan
                {
                    FlightNumber = travelPlan.FlightNumber,
                    ModeOfTransportation = travelPlan.ModeOfTransportation,
                    NumberOfPeople = travelPlan.NumberOfPeople,
                    Type = travelPlan.Type,
                    When = travelPlan.When.ToString("yyyy-MM-ddTHH:mm"),
                }).ToList();

            var contingentPlans = tournament.ContingentTravelPlans.FirstOrDefault(x => x.Id == e.Id);
            if (contingentPlans != null)
            {
                contingentPlans.TravelPlans.Clear();
            }
            else
            {
                var contingent = Contingents.Single(x => x.Key == e.Id);
                contingentPlans = new ContingentTravelPlans
                {
                    Id = contingent.Key,
                    Province = contingent.Value,
                };
                tournament.ContingentTravelPlans.Add(contingentPlans);
            }

            travelPlans.ForEach(contingentPlans.TravelPlans.Add);
        }

        public void Handle(RoomTypeChanged e)
        {
        }

        public void Handle(ReservationInstructionsChanged e)
        {
        }

        //private class TSTournament : Entity
        //{
        //    public string Year { get; set; }
        //    public Guid Id { get; set; }
        //}

        //private class TSContingent : Entity
        //{
        //    public Guid Id
        //    {
        //        get { return Guid.Parse(RowKey); }
        //        internal set { RowKey = value.ToString(); }
        //    }
        //    public Guid TournamentId
        //    {
        //        get { return Guid.Parse(PartitionKey); }
        //        internal set { PartitionKey = value.ToString(); }
        //    }
        //    public string Province { get; set; }
        //    public string Instructions { get; set; }
        //}

        //private class TSTravelPlan : Entity
        //{
        //    public Guid ContingentId
        //    {
        //        get { return Guid.Parse(PartitionKey); }
        //        internal set { PartitionKey = value.ToString(); }
        //    }
        //    public string ModeOfTransportation { get; set; }
        //    public string When { get; set; }
        //    public string FlightNumber { get; set; }
        //    public int NumberOfPeople { get; set; }
        //    public int Type { get; set; }
        //}

        //private class TSHotelRoom : Entity
        //{
        //    public Guid ContingentId
        //    {
        //        get { return Guid.Parse(PartitionKey); }
        //        internal set { PartitionKey = value.ToString(); }
        //    }
        //    public int RoomNumber { get; set; }
        //    public string Type { get; set; }
        //}

        //public List<ContingentTravelPlans> GetAllTravelPlans(string year)
        //{
        //    var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        //    var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);

        //    var allPlans = new List<ContingentTravelPlans>();
        //    foreach (var contingent in contingents)
        //    {
        //        var travelPlans = Storage.Query<TSTravelPlan>(x => x.ContingentId == contingent.Id)
        //            .Select(x => new TravelPlan
        //            {
        //                ModeOfTransportation = x.ModeOfTransportation,
        //                When = x.When,
        //                FlightNumber = x.FlightNumber,
        //                NumberOfPeople = x.NumberOfPeople,
        //                Type = x.Type
        //            }).ToList();

        //        var contingentTravelPlan = new ContingentTravelPlans
        //        {
        //            Id = contingent.Id,
        //            Province = contingent.Province,
        //            TravelPlans = travelPlans,
        //        };
        //        allPlans.Add(contingentTravelPlan);
        //    }

        //    return allPlans;
        //}

        //public ContingentTravelPlans GetTravelPlans(string year, string province)
        //{
        //    var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        //    var contingent = Storage.Query<TSContingent>(x => 
        //        x.TournamentId == tournament.Id 
        //        && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
        //        .FirstOrDefault();

        //    var travelPlans = Storage.Query<TSTravelPlan>(x => x.ContingentId == contingent.Id)
        //        .Select(x => new TravelPlan
        //        {
        //            ModeOfTransportation = x.ModeOfTransportation,
        //            When = x.When,
        //            FlightNumber = x.FlightNumber,
        //            NumberOfPeople = x.NumberOfPeople,
        //            Type = x.Type
        //        }).ToList();

        //    var contingentTravelPlan = new ContingentTravelPlans
        //    {
        //        Id = contingent.Id,
        //        Province = contingent.Province,
        //        TravelPlans = travelPlans,
        //    };
        //    return contingentTravelPlan;
        //}

        //public List<ContingentRooms> GetAllRooms(string year)
        //{
        //    var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        //    var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);

        //    var allRooms = new List<ContingentRooms>();
        //    foreach (var contingent in contingents)
        //    {
        //        var hotelRooms = Storage.Query<TSHotelRoom>(x => x.ContingentId == contingent.Id)
        //           .Select(x => new HotelRoom
        //           {
        //               RoomNumber = x.RoomNumber,
        //               Type = x.Type
        //           }).ToList();

        //        var contingentRooms = new ContingentRooms
        //        {
        //            Id = contingent.Id,
        //            Province = contingent.Province,
        //            Instructions = contingent.Instructions,
        //            HotelRooms = hotelRooms,
        //        };
        //        allRooms.Add(contingentRooms);
        //    }

        //    return allRooms;
        //}

        //public ContingentRooms GetRooms(string year, string province)
        //{
        //    var tournament = Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        //    var contingent = Storage.Query<TSContingent>(x =>
        //        x.TournamentId == tournament.Id
        //        && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
        //        .FirstOrDefault();

        //    var hotelRooms = Storage.Query<TSHotelRoom>(x => x.ContingentId == contingent.Id)
        //        .Select(x => new HotelRoom
        //        {
        //            RoomNumber = x.RoomNumber,
        //            Type = x.Type
        //        }).ToList();

        //    var contingentRooms = new ContingentRooms
        //    {
        //        Id = contingent.Id,
        //        Province = contingent.Province,
        //        Instructions = contingent.Instructions,
        //        HotelRooms = hotelRooms,
        //    };
        //    return contingentRooms;
        //}

        //public void Handle(TournamentCreated e)
        //{
        //    Storage.Create(e.Id, e.Id, new TSTournament { Year = e.Year, Id = e.Id });

        //    //HACK: Track current tournament
        //    Storage.Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        //}

        //public void Handle(ContingentCreated e)
        //{
        //    if (string.IsNullOrWhiteSpace(e.Province))
        //        return;

        //    var tournamentId = GetCurrentTournamentId();

        //    Storage.Create(tournamentId, e.Id, new TSContingent
        //    {
        //        Province = e.Province
        //    });
        //}

        //public void Handle(ContingentAssignedToTournament e)
        //{
        //    var contingent = Storage.Read<TSContingent>(Guid.Empty, e.Id);
        //    if (contingent != null)
        //    {
        //        Storage.Delete<TSContingent>(Guid.Empty, e.Id);
        //        contingent.TournamentId = e.TournamentId;
        //        Storage.Create(e.TournamentId, e.Id, contingent);
        //    }
        //}

        //public void Handle(TravelPlansChanged e)
        //{
        //    //Storage.Delete<TS old plans
        //    Storage.Query<TSTravelPlan>(x => x.PartitionKey == e.Id.ToString())
        //        .ForEach(x => Storage.Delete<TSTravelPlan>(Guid.Parse(x.PartitionKey), Guid.Parse(x.RowKey)));

        //    //Create new ones
        //    e.TravelPlans.ForEach(plan =>
        //    {
        //        Storage.Create(e.Id, Guid.NewGuid(), new TSTravelPlan
        //        {
        //            ModeOfTransportation = plan.ModeOfTransportation,
        //            When = plan.When.ToString("yyyy-MM-ddTHH:mm"),
        //            FlightNumber = plan.FlightNumber,
        //            NumberOfPeople = plan.NumberOfPeople,
        //            Type = plan.Type
        //        });
        //    });
        //}

        //public void Handle(RoomTypeChanged e)
        //{
        //    var roomBytes = BitConverter.GetBytes(e.RoomNumber);
        //    var eightBytes = Enumerable
        //        .Repeat<Byte>(0, 8 - roomBytes.Length)
        //        .Concat(roomBytes)
        //        .ToArray();
        //    var roomKey = new Guid(0, 0, 0, eightBytes);

        //    Storage.Create(e.Id, roomKey, new TSHotelRoom
        //    {
        //        RoomNumber = e.RoomNumber,
        //        Type = e.Type,
        //    });
        //}

        //public void Handle(ReservationInstructionsChanged e)
        //{
        //    var tournamentId = GetCurrentTournamentId();
        //    Storage.Update<TSContingent>(tournamentId, e.Id, contingent => contingent.Instructions = e.Instructions);
        //}

        //private Guid GetCurrentTournamentId()
        //{
        //    var tournament = Storage.Read<TSTournament>(Guid.Empty, Guid.Empty)
        //        ?? new TSTournament { Id = Guid.Empty };
        //    return tournament.Id;
        //}
    }
}
