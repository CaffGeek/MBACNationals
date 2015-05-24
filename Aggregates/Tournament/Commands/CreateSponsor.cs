using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBACNationals.Tournament.Commands
{
    public class CreateSponsor
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public byte[] Image { get; set; }
    }
}
