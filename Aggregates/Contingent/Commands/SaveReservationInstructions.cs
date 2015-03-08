using System;

namespace MBACNationals.Contingent.Commands
{
    public class SaveReservationInstructions
    {
        public Guid Id;
        public string Province { get; set; }
        public string Instructions { get; set; }
    }
}
