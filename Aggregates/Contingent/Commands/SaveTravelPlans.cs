using System;

namespace MBACNationals.Contingent.Commands
{
    public class SaveTravelPlans
    {
        public Guid Id { get; set; }
        public TravelPlan[] TravelPlans { get; set; }

        public class TravelPlan
        {
            public string ModeOfTransportation { get; set; }
            public string When { get; set; }
            public string FlightNumber { get; set; }
            public int NumberOfPeople { get; set; }
            public int Type { get; set; }
            public Occupant[] Occupants { get; set; }
        }

        public class Occupant
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
        }
    }
}
