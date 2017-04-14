using Edument.CQRS;
using Events.Tournament;
using MBACNationals.ReadModels;
using MBACNationals.Tournament.Commands;
using System;
using System.Collections;
using System.Linq;

namespace MBACNationals.Tournament
{
    public class TournamentCommandHandlers :
        IHandleCommand<CreateTournament, TournamentAggregate>,
        IHandleCommand<CreateSponsor, TournamentAggregate>,
        IHandleCommand<DeleteSponsor, TournamentAggregate>,
        IHandleCommand<CreateNews, TournamentAggregate>,
        IHandleCommand<DeleteNews, TournamentAggregate>,
        IHandleCommand<CreateHotel, TournamentAggregate>,
        IHandleCommand<DeleteHotel, TournamentAggregate>,
        IHandleCommand<SaveGuestPackages, TournamentAggregate>,
        IHandleCommand<CreateCentre, TournamentAggregate>,
        IHandleCommand<DeleteCentre, TournamentAggregate>
    {
        private ICommandQueries CommandQueries;

        public TournamentCommandHandlers(ICommandQueries commandQueries)
        {
            CommandQueries = commandQueries;
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateTournament command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new TournamentAlreadyExists();

            if (CommandQueries.GetTournaments().Any(x => x.Year.Equals(command.Year)))
                throw new TournamentAlreadyExists();

            yield return new TournamentCreated
            {
                Id = command.Id,
                Year = command.Year
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateSponsor command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new SponsorCreated
            {
                Id = tournament.Id,
                SponsorId = command.Id,
                Name = command.Name,
                Website = command.Website,
                Image = command.Image
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, DeleteSponsor command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new SponsorDeleted
            {
                Id = tournament.Id,
                SponsorId = command.Id,
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateNews command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new NewsCreated
            {
                Id = tournament.Id,
                NewsId = command.Id,
                Title = command.Title,
                Content = command.Content,
                Created = DateTime.Now
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, DeleteNews command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new NewsDeleted
            {
                Id = tournament.Id,
                NewsId = command.Id,
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateHotel command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new HotelCreated
            {
                Id = tournament.Id,
                HotelId = command.Id,
                Name = command.Name,
                Website = command.Website,
                PhoneNumber = command.PhoneNumber,
                RoomTypes = command.RoomTypes,
                Logo = command.Logo,
                Image = command.Image
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, DeleteHotel command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new HotelDeleted
            {
                Id = tournament.Id,
                HotelId = command.Id,
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, SaveGuestPackages command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            foreach (var guestpackage in command.GuestPackages)
            {
                yield return new GuestPackageSaved
                    {
                        Id = tournament.Id,
                        Code = guestpackage.Code,
                        Name = guestpackage.Name,
                        Cost = guestpackage.Cost,
                        Enabled = guestpackage.Enabled
                    };
            }
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, CreateCentre command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new CentreCreated
            {
                Id = tournament.Id,
                CentreId = command.Id,
                Name = command.Name,
                Website = command.Website,
                PhoneNumber = command.PhoneNumber,
                Address = command.Address,
                Image = command.Image
            };
        }

        public IEnumerable Handle(Func<Guid, TournamentAggregate> al, DeleteCentre command)
        {
            var tournament = CommandQueries.GetTournaments().FirstOrDefault(x => x.Year == command.Year);
            var agg = al(tournament.Id);

            yield return new CentreDeleted
            {
                Id = tournament.Id,
                CentreId = command.Id,
            };
        }
    }
}