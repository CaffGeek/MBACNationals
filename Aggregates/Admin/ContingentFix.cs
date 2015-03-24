using Edument.CQRS;
using MBACNationals.Contingent.Commands;
using MBACNationals.Scores.Commands;
using System;
using System.Collections.Generic;

namespace MBACNationals
{
    public static class ContingentFix
    {
        public static void Assign2014Contingents(MessageDispatcher dispatcher, Guid tournamentId)
        {
            var commands = new List<AssignContingentToTournament>
                {
                    new AssignContingentToTournament { Id = new Guid("A6B2DDB3-5CD5-40DB-A4F9-031147CC5733"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("201CCDA2-7E24-4BEC-875E-0DDA03ADC8A7"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("9F366971-66C7-4F74-B789-1FA4D8150701"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("DE2F86AF-CF7B-4D07-A763-231CF7258F55"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("71914206-95A3-4228-9C3E-374FB4977F32"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("282E7902-2332-4F30-9DF9-37EAE97CA184"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("FDE0CE26-A143-4F27-9C9C-AA5633127C9A"), TournamentId = tournamentId },
                    new AssignContingentToTournament { Id = new Guid("275413A5-A985-4A7F-9EFA-F7DECECE1D96"), TournamentId = tournamentId }
                };

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }
    }
}