using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBACNationals.ReadModels
{
    public class TournamentQueries : AzureReadModel,
        ITournamentQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<SponsorCreated>,
        ISubscribeTo<SponsorDeleted>
    {
        public TournamentQueries(string readModelFilePath)
        {

        }

        public class Tournament
        {
            public Guid Id { get; internal set; }
            public string Year { get; internal set; }
        }

        public class Sponsor
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Website { get; internal set; }
        }

        public class TSTournament : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
        }

        public class TSSponsor : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid TournamentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public string Website { get; set; }
        }

        public class TSSponsorLogo : Blob { }

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

        public List<Sponsor> GetSponsors(string year)
        {
            var tournament = Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            if (tournament == null)
                return Enumerable.Empty<Sponsor>().ToList();

            var sponsors = Query<TSSponsor>(x => x.TournamentId == tournament.Id)
                .Select(x => new Sponsor
                {
                    Id = x.Id,
                    Name = x.Name,
                    Website = x.Website
                })
                .ToList();

            return sponsors;
        }

        public byte[] GetSponsorImage(Guid sponsorId)
        {
            var image = ReadBlob<TSSponsorLogo>(sponsorId);
            return image.Contents;
        }

        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(SponsorCreated e)
        {
            Create(e.Id, e.SponsorId, new TSSponsor
            {
                Name = e.Name,
                Website = e.Website
            });

            Create(new TSSponsorLogo 
            { 
                Id = e.SponsorId,
                Contents = e.Image
            });
        }

        public void Handle(SponsorDeleted e)
        {
            Delete<TSSponsor>(e.Id, e.SponsorId);
        }
    }
}
