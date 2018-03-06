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
            public TravelPlan()
            {
                Occupants = new List<Occupant>();
            }

            public string ModeOfTransportation;
            public string When;
            public string FlightNumber;
            public int NumberOfPeople;
            public int Type;
            public List<Occupant> Occupants;
        }

        public class Occupant
        {
            public Guid Id;
            public string Name;
            public string Province;
        }
    }
}
