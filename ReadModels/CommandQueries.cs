using Edument.CQRS;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class CommandQueries : AReadModel,
        ICommandQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantCreated>
    {
        public CommandQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Tournament : AEntity
        {
            public Tournament(Guid id) : base(id) { }
            public virtual string Year { get; internal set; }
        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
            public virtual Guid ContingentId { get; internal set; }
            public virtual Guid TeamId { get; internal set; }
            public virtual string Name { get; internal set; }
            public virtual int Average { get; internal set; }
        }

        public List<Tournament> GetTournaments()
        {
            return Read<Tournament>().ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            return Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }
        
        public void Handle(TournamentCreated e)
        {
            Create(new Tournament(e.Id)
            {
                Year = e.Year
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id)
            {
                Name = e.Name
            });
        }
    }
}
