using Edument.CQRS;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class CommandQueries : AzureReadModel,
        ICommandQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantCreated>
    {
        public CommandQueries(string readModelFilePath)
        {

        }

        public class Tournament
        {
            public Guid Id { get; set; }
            public virtual string Year { get; set; }
        }

        public class Participant
        {
            public Guid Id { get; set; }
            public virtual Guid ContingentId { get; set; }
            public virtual Guid TeamId { get; set; }
            public virtual string Name { get; set; }
            public virtual int Average { get; set; }
        }

        private class TSTournament : Entity
        {
            public virtual string Year { get; set; }
        }

        private class TSParticipant : Entity
        {
            public virtual Guid ContingentId { get; set; }
            public virtual Guid TeamId { get; set; }
            public virtual string Name { get; set; }
            public virtual int Average { get; set; }
        }

        public List<Tournament> GetTournaments()
        {
            return Query<TSTournament>()
                .Select(x => new Tournament
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year
                })
                .ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            var participant = Read<TSParticipant>(Guid.Empty, id);
            return new Participant 
            {
                Id = Guid.Parse(participant.RowKey),
                TeamId = participant.TeamId,
                ContingentId = participant.ContingentId,
                Name = participant.Name,
                Average = participant.Average
            };
                
        }
        
        public void Handle(TournamentCreated e)
        {
            Create(Guid.Empty, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(Guid.Empty, e.Id, new TSParticipant
            {
                Name = e.Name
            });
        }
    }
}
