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
            public DateTime When { get; set; }
            public string FlightNumber { get; set; }
            public int NumberOfPeople { get; set; }
            public int Type { get; set; }
        }
    }
}
