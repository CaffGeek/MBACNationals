using System;

namespace MBACNationals.Tournament.Commands
{
    public class EmailDivisionGameResult
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Division { get; set; }
        public int GameNumber { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}