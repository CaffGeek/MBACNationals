using System;
using System.Collections.Generic;

namespace Events.Contingent
{
    public class TravelPlansChanged
    {
        public Guid Id;
        public List<TravelPlan> TravelPlans;

        public class TravelPlan
        {
            public string ModeOfTransportation;
            public DateTime When;
            public string FlightNumber;
            public int NumberOfPeople;
            public int Type;
        }
    }
}
