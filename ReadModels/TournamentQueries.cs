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
        ISubscribeTo<SponsorPositionChanged>,
        ISubscribeTo<NewsCreated>,
        ISubscribeTo<NewsDeleted>,
        ISubscribeTo<HotelCreated>,
        ISubscribeTo<HotelDeleted>,
        ISubscribeTo<GuestPackageSaved>,
        ISubscribeTo<CentreCreated>,
        ISubscribeTo<CentreDeleted>
    {
        public List<Tournament> Tournaments { get; set; }
        public List<News> NewsArticles { get; set; }
        public List<Sponsor> Sponsors { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<GuestPackage> GuestPackages { get; set; }
        public List<Centre> Centres { get; set; }

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
            public int Position { get; set; }
        }

        public class Hotel
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Name { get; set; }
            public string Website { get; set; }
            public string PhoneNumber { get; set; }
            public string DefaultCheckin { get; set; }
            public string DefaultCheckout { get; set; }
            public string[] RoomTypes { get; set; }
        }

        public class GuestPackage
        {
            public Guid TournamentId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal Cost { get; set; }
            public bool Enabled { get; set; }
        }

        public class Centre
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Name { get; set; }
            public string Website { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
        }

        public class TSSponsorLogo : Blob { }
        public class TSHotelLogo : Blob { }
        public class TSHotelImage : Blob { }
        public class TSCentreImage : Blob { }

        public TournamentQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();
            NewsArticles = new List<News>();
            Sponsors = new List<Sponsor>();
            Hotels = new List<Hotel>();
            GuestPackages = new List<GuestPackage>();
            Centres = new List<Centre>();
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
            var sponsors = Sponsors.Where(x => x.TournamentId == tournament.Id)
                    .OrderBy(x => x.Position)
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
            var tournament = Tournaments.Single(x => x.Year == year);
            var news = NewsArticles.Where(x => x.TournamentId == tournament.Id).ToList();
            return news;
        }

        public List<Hotel> GetHotels(string year)
        {
            var tournament = Tournaments.Single(x => x.Year == year);
            var hotels = Hotels.Where(x => x.TournamentId == tournament.Id).ToList();
            return hotels;
        }

        public byte[] GetHotelImage(Guid hotelId)
        {
            var image = Storage.ReadBlob<TSHotelImage>(hotelId);
            return image.Contents;
        }

        public byte[] GetHotelLogo(Guid hotelId)
        {
            var logo = Storage.ReadBlob<TSHotelLogo>(hotelId);
            return logo.Contents;
        }

        public List<TournamentQueries.GuestPackage> GetGuestPackages(string year)
        {
            var tournament = Tournaments.Single(x => x.Year == year);
            var guestPackages = GuestPackages.Where(x => x.TournamentId == tournament.Id).ToList();

            var defaultPackages = new [] {
                new TournamentQueries.GuestPackage
                {
                    TournamentId = tournament.Id,
                    Code = "Option1",
                    Name = "Meet & Greet",
                    Enabled = true
                },
                new TournamentQueries.GuestPackage
                {
                    TournamentId = tournament.Id,
                    Code = "Option2",
                    Name = "Transportation",
                    Enabled = true
                },
                new TournamentQueries.GuestPackage
                {
                    TournamentId = tournament.Id,
                    Code = "Option3",
                    Name = "Provincial Night",
                    Enabled = true
                },
                new TournamentQueries.GuestPackage
                {
                    TournamentId = tournament.Id,
                    Code = "Option4",
                    Name = "Victory Banquet",
                    Enabled = true
                }
            }.ToList();

            var mergedPackages = (
                from d in defaultPackages
                join g in guestPackages
                    on d.Code equals g.Code into joined
                from j in joined.DefaultIfEmpty()
                select j ?? d).ToList();

            return mergedPackages;
        }

        public List<TournamentQueries.Centre> GetCentres(string year)
        {
            var tournament = Tournaments.Single(x => x.Year == year);
            var centres = Centres.Where(x => x.TournamentId == tournament.Id).ToList();
            return centres;
        }

        public byte[] GetCentreImage(Guid hotelId)
        {
            var image = Storage.ReadBlob<TSCentreImage>(hotelId);
            return image.Contents;
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

        public void Handle(SponsorPositionChanged e)
        {
            var sponsor = Sponsors.SingleOrDefault(x => x.Id == e.Id);
            if (sponsor == null) return;

            sponsor.Position = e.Position;
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

        public void Handle(HotelCreated e)
        {
            Hotels.Add(new Hotel
            {
                Id = e.HotelId,
                TournamentId = e.Id,
                Name = e.Name,
                Website = e.Website,
                PhoneNumber = e.PhoneNumber,
                DefaultCheckin = e.DefaultCheckin,
                DefaultCheckout = e.DefaultCheckout,
                RoomTypes = e.RoomTypes
            });

            Storage.Create(new TSHotelLogo
            {
                Id = e.HotelId,
                Contents = e.Logo
            });

            Storage.Create(new TSHotelImage
            {
                Id = e.HotelId,
                Contents = e.Image
            });
        }

        public void Handle(HotelDeleted e)
        {
            Hotels.RemoveAll(x => x.Id == e.HotelId);
            //TODO: Delete logo and image
        }

        public void Handle(GuestPackageSaved e)
        {
            //TODO: Chad: Overwrite existing
            var package = GuestPackages.SingleOrDefault(x => x.TournamentId == e.Id && x.Code == e.Code);
            if (package == null)
            {
                GuestPackages.Add(new GuestPackage
                    {
                        TournamentId = e.Id,
                        Code = e.Code,
                        Name = e.Name,
                        Cost = e.Cost,
                        Enabled = e.Enabled
                    });
            }
            else
            {
                package.Name = e.Name;
                package.Cost = e.Cost;
                package.Enabled = e.Enabled;
            }
        }

        public void Handle(CentreCreated e)
        {
            Centres.Add(new Centre
            {
                Id = e.CentreId,
                TournamentId = e.Id,
                Name = e.Name,
                Website = e.Website,
                PhoneNumber = e.PhoneNumber,
                Address = e.Address
            });

            Storage.Create(new TSCentreImage
            {
                Id = e.CentreId,
                Contents = e.Image
            });
        }

        public void Handle(CentreDeleted e)
        {
            Centres.RemoveAll(x => x.Id == e.CentreId);
            //TODO: Delete image
        }
    }
}
