using System;
namespace MBACNationals.Scores.Commands
{
    public class UpdateStepladderMatch
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public string HomeShots { get; set; }
        public string AwayShots { get; set; }
    }
}
