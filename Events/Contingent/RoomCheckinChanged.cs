using System;

namespace Events.Contingent
{
    public class RoomCheckinChanged
    {
        public Guid Id;
        public int RoomNumber;
        public string Checkin;
        public string Checkout;
    }
}
