using Edument.CQRS;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class CommandQueries : BaseReadModel<CommandQueries>,
        ICommandQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantRenamed>
    {
        public class Tournament
        {
            public virtual Guid Id { get; set; }
            public virtual string Year { get; set; }
        }

        public class Participant
        {
            public virtual Guid Id { get; set; }
            public virtual Guid ContingentId { get; set; }
            public virtual Guid TeamId { get; set; }
            public virtual string Name { get; set; }
            public virtual int Average { get; set; }
            public virtual string Gender { get; set; }
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
            public virtual string Gender { get; set; }
        }

        public List<Tournament> GetTournaments()
        {
            return Storage.Query<TSTournament>()
                .Select(x => new Tournament
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year
                })
                .ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            var participant = Storage.Read<TSParticipant>(Guid.Empty, id);
            return new Participant 
            {
                Id = Guid.Parse(participant.RowKey),
                TeamId = participant.TeamId,
                ContingentId = participant.ContingentId,
                Name = participant.Name,
                Average = participant.Average,
                Gender = participant.Gender
            };
                
        }
        
        public void Handle(TournamentCreated e)
        {
            Storage.Create(Guid.Empty, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(Guid.Empty, e.Id, new TSParticipant
            {
                Name = e.Name,
                Gender = e.Gender
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Storage.Update<TSParticipant>(Guid.Empty, e.Id, x => x.Gender = e.Gender);
        }

        public void Handle(ParticipantRenamed e)
        {
            Storage.Update<TSParticipant>(Guid.Empty, e.Id, x => x.Name = e.Name);
        }
    }
}
