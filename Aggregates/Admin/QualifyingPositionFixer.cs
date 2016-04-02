using Edument.CQRS;
using MBACNationals.Participant.Commands;
using MBACNationals.ReadModels;
using System.Collections.Generic;

namespace MBACNationals
{
    public static class QualifyingPositionFixer
    {
        public static void Execute(ICommandQueries commandQueries, IContingentViewQueries contingentViewQueries, MessageDispatcher dispatcher)
        {
            var commands = new List<ReorderParticipant>();
            var provinces = new List<string> { "BC", "AB", "SK", "MB", "NO", "SO", "QC", "NL" };

            foreach (var tournament in commandQueries.GetTournaments())
            {
                foreach (var province in provinces)
                {
                    var contingent = contingentViewQueries.GetContingent(tournament.Id, province);
                    if (contingent == null)
                        continue;

                    foreach (var team in contingent.Teams)
                    {
                        var i = 0;
                        foreach (var bowler in team.Bowlers)
                        {
                            if (++i == bowler.QualifyingPosition)
                                continue;

                            commands.Add(
                                new ReorderParticipant
                                {
                                    Id = bowler.Id,
                                    TeamId = team.Id,
                                    Name = bowler.Name,
                                    QualifyingPosition = i
                                });
                        }
                    }
                }
            }
            
            commands.ForEach(dispatcher.SendCommand);
        }
    }
}
