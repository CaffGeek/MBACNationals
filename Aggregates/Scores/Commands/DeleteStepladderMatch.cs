using System;

namespace MBACNationals.Scores.Commands
{
    public class DeleteStepladderMatch
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
    }
}
