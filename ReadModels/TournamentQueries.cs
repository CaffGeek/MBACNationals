using Edument.CQRS;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBACNationals.ReadModels
{
    public class TournamentQueries : BaseReadModel<TournamentQueries>,
        IReadModel,
        ITournamentQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<SponsorCreated>,
        ISubscribeTo<SponsorDeleted>,
        ISubscribeTo<NewsCreated>,
        ISubscribeTo<NewsDeleted>
    {
        public List<Tournament> Tournaments { get; set; }
        public List<News> NewsArticles { get; set; }
        public List<Sponsor> Sponsors { get; set; }

        public class Tournament
        {
            public Guid Id { get; set; }
            public string Year { get; set; }
        }

        public class News
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Created { get; set; }
        }

        public class Sponsor
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Name { get; set; }
            public string Website { get; set; }
        }
        
        public class TSSponsorLogo : Blob { }

        public TournamentQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();
            NewsArticles = new List<News>();
            Sponsors = new List<Sponsor>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public Tournament GetTournament(string year)
        {
            return Tournaments.SingleOrDefault(x => x.Year == year);
        }

        public List<Tournament> GetTournaments()
        {
            return Tournaments;
        }

        public List<Sponsor> GetSponsors(string year)
        {
            var tournament = Tournaments.Single(x => x.Year == year);
            var sponsors = Sponsors.Where(x => x.TournamentId == tournament.Id).ToList();
            return sponsors;
        }

        public byte[] GetSponsorImage(Guid sponsorId)
        {
            var image = Storage.ReadBlob<TSSponsorLogo>(sponsorId);
            return image.Contents;
        }

        public List<News> GetNews(string year)
        {
            var tournament = Tournaments.Single(x => x.Year == year);
            var news = NewsArticles.Where(x => x.TournamentId == tournament.Id).ToList();
            return news;
        }

        public void Handle(TournamentCreated e)
        {
            Tournaments.Add(new Tournament
            {
                Id = e.Id,
                Year = e.Year
            });
        }

        public void Handle(SponsorCreated e)
        {
            Sponsors.Add(new Sponsor
            {
                Id = e.SponsorId,
                TournamentId = e.Id,
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
            Sponsors.RemoveAll(x => x.Id == e.SponsorId);
            //TODO: Delete logo
        }

        public void Handle(NewsCreated e)
        {
            NewsArticles.Add(new News
            {
                Id = e.NewsId,
                TournamentId = e.Id,
                Title = e.Title,
                Content = e.Content,
                Created = e.Created
            });
        }

        public void Handle(NewsDeleted e)
        {
            NewsArticles.RemoveAll(x => x.Id == e.NewsId);
        }
    }
}
