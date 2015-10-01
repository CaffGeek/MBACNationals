using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBACNationals.ReadModels
{
    public class TournamentQueries : BaseReadModel<TournamentQueries>,
        ITournamentQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<SponsorCreated>,
        ISubscribeTo<SponsorDeleted>,
        ISubscribeTo<NewsCreated>,
        ISubscribeTo<NewsDeleted>
    {
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

        public class News
        {
            public Guid Id { get; internal set; }
            public string Title { get; internal set; }
            public string Content { get; internal set; }
            public DateTime Created { get; internal set; }
        }

        private class TSTournament : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
        }

        private class TSSponsor : Entity
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

        private class TSNews : Entity
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
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Created { get; set; }
        }

        public class TSSponsorLogo : Blob { }

        public Tournament GetTournament(string year)
        {
            return Storage.Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase))
                .Select(x => new Tournament
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year
                })
                .FirstOrDefault();
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

        public List<Sponsor> GetSponsors(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            if (tournament == null)
                return Enumerable.Empty<Sponsor>().ToList();

            var sponsors = Storage.Query<TSSponsor>(x => x.TournamentId == tournament.Id)
                .Select(x => new Sponsor
                {
                    Id = x.Id,
                    Name = x.Name,
                    Website = x.Website,
                })
                .ToList();

            return sponsors;
        }

        public byte[] GetSponsorImage(Guid sponsorId)
        {
            var image = Storage.ReadBlob<TSSponsorLogo>(sponsorId);
            return image.Contents;
        }

        public List<News> GetNews(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            if (tournament == null)
                return Enumerable.Empty<News>().ToList();

            var news = Storage.Query<TSNews>(x => x.TournamentId == tournament.Id)
                .Select(x => new News
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Created = x.Created
                })
                .ToList();

            return news;
        }

        public void Handle(TournamentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(SponsorCreated e)
        {
            Storage.Create(e.Id, e.SponsorId, new TSSponsor
            {
                Name = e.Name,
                Website = e.Website
            });

            Storage.Create(new TSSponsorLogo 
            { 
                Id = e.SponsorId,
                Contents = e.Image
            });
        }

        public void Handle(SponsorDeleted e)
        {
            Storage.Delete<TSSponsor>(e.Id, e.SponsorId);
        }

        public void Handle(NewsCreated e)
        {
            Storage.Create(e.Id, e.NewsId, new TSNews
            {
                Title = e.Title,
                Content = e.Content,
                Created = e.Created
            });
        }

        public void Handle(NewsDeleted e)
        {
            Storage.Delete<TSNews>(e.Id, e.NewsId);
        }
    }
}
