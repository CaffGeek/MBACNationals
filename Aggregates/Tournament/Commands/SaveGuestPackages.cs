using System;

namespace MBACNationals.Tournament.Commands
{
    public class SaveGuestPackages
    {
        public Guid Id;
        public string Year { get; set; }
        public GuestPackage[] GuestPackages { get; set; }

        public class GuestPackage
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal Cost { get; set; }
            public bool Enabled { get; set; }
        }
    }
}