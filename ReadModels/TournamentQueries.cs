using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TournamentQueries : AReadModel,
        ITournamentQueries,
        ISubscribeTo<TournamentCreated>
    {
        public TournamentQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Tournament : AEntity
        {
            public Tournament(Guid id) : base(id) { }
            public string Year { get; internal set; }
        }

        public List<Tournament> GetTournaments()
        {
            return Read<Tournament>().ToList();
        }
        
        public void Handle(TournamentCreated e)
        {
            Create(new Tournament(e.Id)
            {
                Year = e.Year
            });
        }
    }
}
