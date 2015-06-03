using System;

namespace MBACNationals.Tournament.Commands
{
    public class CreateNews
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
