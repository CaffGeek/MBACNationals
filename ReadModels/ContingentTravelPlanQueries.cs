using Edument.CQRS;
using Events.Contingent;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : 
        IReadModel,
        IContingentTravelPlanQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TravelPlansChanged>,
        ISubscribeTo<RoomTypeChanged>,
        ISubscribeTo<ReservationInstructionsChanged>
    {
        public List<Tournament> Tournaments { get; set; }
        public Dictionary<Guid, string> Contingents { get; set; }

        public class Tournament
        {
            public Guid Id { get; set; }
            public string Year { get; set; }
            public List<ContingentTravelPlans> ContingentTravelPlans { get; set; }
            public List<ContingentRooms> ContingentRooms { get; set; }

            public Tournament()
            {
                ContingentTravelPlans = new List<ContingentTravelPlans>();
                ContingentRooms = new List<ContingentRooms>();
            }
        }

        public class ContingentTravelPlans
        {
            public Guid Id { get; set; }
            public string Province { get; set; }
            public List<TravelPlan> TravelPlans { get; set; }

            public ContingentTravelPlans()
            {
                TravelPlans = new List<TravelPlan>();
            }
        }

        public class TravelPlan
        {
            public string ModeOfTransportation { get; set; }
            public string When { get; set; }
            public string FlightNumber { get; set; }
            public int NumberOfPeople { get; set; }
            public int Type { get; set; }
            public List<Occupant> Occupants { get; set; }

            public TravelPlan()
            {
                Occupants = new List<Occupant>();
            }
        }

        public class Occupant
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
        }

        public class ContingentRooms
        {
            public Guid Id { get; set; }
            public string Province { get; set; }
            public List<HotelRoom> HotelRooms { get; set; }
            public string Instructions { get; set; }

            public ContingentRooms()
            {
                HotelRooms = new List<HotelRoom>();
            }
        }

        public class HotelRoom
        {
            public int RoomNumber { get; set; }
            public string Type { get; set; }
        }

        public ContingentTravelPlanQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();
            Contingents = new Dictionary<Guid, string>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
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
            var t = Tournaments.Single(x => x.Year == year);            
            return t.ContingentRooms;
        }

        public ContingentRooms GetRooms(string year, string province)
        {
            return GetAllRooms(year).Single(x => x.Province == province);
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
            var tournament = Tournaments.SingleOrDefault(x => x.Id == e.TournamentId);
            if (tournament == null)
            {
                tournament = new Tournament { Year = "2014" };
                Tournaments.Add(tournament);
            }

            var contingent = Contingents.Single(x => x.Key == e.Id);

            tournament.ContingentTravelPlans.Add(new ContingentTravelPlans
                {
                    Id = e.Id,
                    Province = contingent.Value
                });
            tournament.ContingentRooms.Add(new ContingentRooms
                {
                    Id = e.Id,
                    Province = contingent.Value
                });
        }

        public void Handle(TravelPlansChanged e)
        {
            var tournament = GetTournamentFromContingentId(e.Id);

            var travelPlans = e.TravelPlans.Select(travelPlan => new TravelPlan
                {
                    FlightNumber = travelPlan.FlightNumber,
                    ModeOfTransportation = travelPlan.ModeOfTransportation,
                    NumberOfPeople = travelPlan.NumberOfPeople,
                    Occupants = travelPlan.Occupants.Select(occupant => new Occupant
                    {
                        Id = occupant.Id,
                        Name = occupant.Name,
                        Province = occupant.Province
                    }).ToList(),
                    Type = travelPlan.Type,
                    When = travelPlan.When,
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
            var tournament = GetTournamentFromContingentId(e.Id);

            var contingentRooms = tournament.ContingentRooms.FirstOrDefault(x => x.Id == e.Id);

            if (contingentRooms == null)
            {
                var contingent = Contingents.Single(x => x.Key == e.Id);
                contingentRooms = new ContingentRooms
                {
                    Id = contingent.Key,
                    Province = contingent.Value,
                };
                tournament.ContingentRooms.Add(contingentRooms);
            }

            var room = contingentRooms.HotelRooms.FirstOrDefault(x => x.RoomNumber == e.RoomNumber);
            if (room == null)
            {
                contingentRooms.HotelRooms.Add(new HotelRoom
                {
                    RoomNumber = e.RoomNumber,
                    Type = e.Type
                });
            }
            else
            {
                room.Type = e.Type;
            }
        }

        public void Handle(ReservationInstructionsChanged e)
        {
            if (e.Instructions == null)
                return;

            var tournament = GetTournamentFromContingentId(e.Id);
            var contingentRooms = tournament.ContingentRooms.FirstOrDefault(x => x.Id == e.Id);
            if (contingentRooms == null)
            {
                var contingent = Contingents.Single(x => x.Key == e.Id);
                contingentRooms = new ContingentRooms
                {
                    Id = contingent.Key,
                    Province = contingent.Value,
                };
                tournament.ContingentRooms.Add(contingentRooms);
            }
            contingentRooms.Instructions = e.Instructions;
        }

        private Tournament GetTournamentFromContingentId(Guid contingentId)
        {
            var tournament = (from t in Tournaments
                              where t.ContingentTravelPlans.Any(x => x.Id == contingentId)
                              select t).SingleOrDefault()
                              ?? (from t in Tournaments
                                  where t.Year == "2014"
                                  select t).SingleOrDefault();

            if (tournament == null)
            {
                tournament = new Tournament { Year = "2014" };
                Tournaments.Add(tournament);
            }

            return tournament;
        }
    }
}
