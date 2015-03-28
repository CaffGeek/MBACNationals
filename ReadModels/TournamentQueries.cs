using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class TournamentQueries : AzureReadModel,
        ITournamentQueries,
        ISubscribeTo<TournamentCreated>
    {
        public TournamentQueries(string readModelFilePath)
        {

        }

        public class Tournament
        {
            public Guid Id { get; internal set; }
            public string Year { get; internal set; }
        }

        public class TSTournament : Entity
        {
            public string Year { get; set; }
        }

        public Tournament GetTournament(string year)
        {
            return Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase))
                .Select(x => new Tournament
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year
                })
                .FirstOrDefault();
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
        
        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }
    }
}
