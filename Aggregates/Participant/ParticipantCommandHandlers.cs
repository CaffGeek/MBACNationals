using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant.Commands;
using MBACNationals.ReadModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.Participant
{
    public class ParticipantCommandHandlers :
        IHandleCommand<CreateParticipant, ParticipantAggregate>,
        IHandleCommand<UpdateParticipant, ParticipantAggregate>,
        IHandleCommand<RenameParticipant, ParticipantAggregate>,
        IHandleCommand<AddParticipantToTeam, ParticipantAggregate>,
        IHandleCommand<AssignAlternateToTeam, ParticipantAggregate>,
        IHandleCommand<AddParticipantToContingent, ParticipantAggregate>,
        IHandleCommand<AddCoachToTeam, ParticipantAggregate>,
        IHandleCommand<AssignParticipantToRoom, ParticipantAggregate>,
        IHandleCommand<RemoveParticipantFromRoom, ParticipantAggregate>,
        IHandleCommand<UpdateParticipantProfile, ParticipantAggregate>,
        IHandleCommand<ReplaceParticipant, ParticipantAggregate>,
        IHandleCommand<ReorderParticipant, ParticipantAggregate>
    {
        private ICommandQueries CommandQueries;

        public ParticipantCommandHandlers(ICommandQueries commandQueries)
        {
            CommandQueries = commandQueries;
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, CreateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new ParticipantAlreadyExists();
            
            yield return new ParticipantCreated
            {
                Id = command.Id,
                Name = command.Name,
                Gender = command.Gender,
                IsDelegate = command.IsDelegate,
                IsManager = command.IsManager,
                YearsQualifying = command.YearsQualifying,
                IsGuest = command.IsGuest,
                Birthday = command.Birthday,
            };

            yield return new ParticipantAverageChanged
            {
                Id = command.Id,
                LeaguePinfall = command.LeaguePinfall,
                LeagueGames = command.LeagueGames,
                TournamentPinfall = command.TournamentPinfall,
                TournamentGames = command.TournamentGames,
                Average = (command.LeagueGames + command.TournamentGames) > 0
                    ? (command.LeaguePinfall + command.TournamentPinfall) / (command.LeagueGames + command.TournamentGames)
                    : 0
            };

            yield return new ParticipantShirtSizeChanged
            {
                Id = command.Id,
                ShirtSize = command.ShirtSize,
            };

            if (command.Package != null)
                yield return new ParticipantGuestPackageChanged
                {
                    Id = command.Id,
                    ManitobaDinner = command.Package.ManitobaDinner,
                    ManitobaDance = command.Package.ManitobaDance,
                    FinalBanquet = command.Package.FinalBanquet,
                    Transportation = command.Package.Transportation,
                    Option1 = command.Package.Option1,
                    Option2 = command.Package.Option2,
                    Option3 = command.Package.Option3,
                    Option4 = command.Package.Option4,
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, UpdateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.Name != command.Name)
            {
                yield return new ParticipantRenamed
                {
                    Id = command.Id,
                    Name = command.Name,
                };
                
                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Participant Renamed", $"{agg.Name} changed to {command.Name}");
            }

            if (agg.Gender != command.Gender)
            {
                yield return new ParticipantGenderReassigned
                {
                    Id = command.Id,
                    Gender = command.Gender,
                };

                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Gender Changed", $"{agg.Name}'s Gender of {agg.Gender} changed to {command.Gender}");
            }

            if (agg.IsDelegate != command.IsDelegate && command.IsDelegate)
            {
                yield return new ParticipantDelegateStatusGranted
                {
                    Id = command.Id
                };
            }

            if (agg.IsDelegate != command.IsDelegate && !command.IsDelegate)
            {
                yield return new ParticipantDelegateStatusRevoked
                {
                    Id = command.Id
                };
            }

            if (agg.IsManager != command.IsManager && command.IsManager)
            {
                yield return new ParticipantManagerStatusGranted
                {
                    Id = command.Id
                };
            }

            if (agg.IsManager != command.IsManager && !command.IsManager)
            {
                yield return new ParticipantManagerStatusRevoked
                {
                    Id = command.Id
                };
            }

            if (agg.YearsQualifying != command.YearsQualifying)
            {
                yield return new ParticipantYearsQualifyingChanged
                {
                    Id = command.Id,
                    YearsQualifying = command.YearsQualifying,
                };

                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "YearsQualifying Changed", $"{agg.Name}'s YearsQualifying of {agg.YearsQualifying} changed to {command.YearsQualifying}");
            }

            if (agg.ShirtSize != command.ShirtSize)
            {
                yield return new ParticipantShirtSizeChanged
                {
                    Id = command.Id,
                    ShirtSize = command.ShirtSize,
                };
            }

            if (agg.LeaguePinfall != command.LeaguePinfall
                || agg.LeagueGames != command.LeagueGames
                || agg.TournamentPinfall != command.TournamentPinfall
                || agg.TournamentGames != command.TournamentGames)
            {
                var newAverage = (command.LeagueGames + command.TournamentGames) > 0
                        ? (command.LeaguePinfall + command.TournamentPinfall) / (command.LeagueGames + command.TournamentGames)
                        : 0;

                yield return new ParticipantAverageChanged
                {
                    Id = command.Id,
                    LeaguePinfall = command.LeaguePinfall,
                    LeagueGames = command.LeagueGames,
                    TournamentPinfall = command.TournamentPinfall,
                    TournamentGames = command.TournamentGames,
                    Average = newAverage
                };

                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Average Changed", $"{agg.Name}'s Average of {agg.Average} changed to {newAverage}");
            }

            if (agg.Package == null
                || agg.Package.ManitobaDinner != command.Package.ManitobaDinner
                || agg.Package.ManitobaDance != command.Package.ManitobaDance
                || agg.Package.FinalBanquet != command.Package.FinalBanquet
                || agg.Package.Transportation != command.Package.Transportation
                || agg.Package.Option1 != command.Package.Option1
                || agg.Package.Option2 != command.Package.Option2
                || agg.Package.Option3 != command.Package.Option3
                || agg.Package.Option4 != command.Package.Option4)
            {
                yield return new ParticipantGuestPackageChanged
                {
                    Id = command.Id,
                    ManitobaDinner = command.Package.ManitobaDinner,
                    ManitobaDance = command.Package.ManitobaDance,
                    FinalBanquet = command.Package.FinalBanquet,
                    Transportation = command.Package.Transportation,
                    Option1 = command.Package.Option1,
                    Option2 = command.Package.Option2,
                    Option3 = command.Package.Option3,
                    Option4 = command.Package.Option4,
                };

                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Guest Package Changed", $"{agg.Name}'s Guest Package choices have changed.");
            }

            if (command.Birthday.HasValue && agg.Birthday != command.Birthday)
            {
                yield return new ParticipantBirthdayChanged
                {
                    Id = command.Id,
                    Birthday = command.Birthday.Value,
                };
            }
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, RenameParticipant command)
        {
            var agg = al(command.Id);

            if (agg.Name != command.Name)
            {
                yield return new ParticipantRenamed
                {
                    Id = command.Id,
                    Name = command.Name,
                };

                var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Participant Renamed", $"{agg.Name} changed to {command.Name}");
            }
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddParticipantToTeam command)
        {
            var agg = al(command.Id);

            yield return new ParticipantAssignedToTeam
            {
                Id = command.Id,
                TeamId = command.TeamId,
                Name = agg.Name
            };

            var participants = CommandQueries.GetTeamParticipants(command.TeamId)
                ?? new List<CommandQueries.Participant>();
            
            yield return new ParticipantQualifyingPositionChanged
            {
                Id = command.Id,
                TeamId = command.TeamId,
                QualifyingPosition = participants.Count + 1
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AssignAlternateToTeam command)
        {
            var agg = al(command.Id);

            yield return new ParticipantDesignatedAsAlternate
            {
                Id = command.Id,
                TeamId = command.TeamId,
                Name = agg.Name
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddParticipantToContingent command)
        {
            var agg = al(command.Id);

            if (agg.ContingentId != command.ContingentId)
                yield return new ParticipantAssignedToContingent
                {
                    Id = command.Id,
                    ContingentId = command.ContingentId
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddCoachToTeam command)
        {
            var agg = al(command.Id);

            if (agg.TeamId != command.TeamId)
                yield return new CoachAssignedToTeam
                {
                    Id = command.Id,
                    TeamId = command.TeamId,
                    Name = agg.Name
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AssignParticipantToRoom command)
        {
            var agg = al(command.Id);

            yield return new ParticipantAssignedToRoom
            {
                Id = command.Id,
                RoomNumber = command.RoomNumber
            };

            var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
            if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Room Change", $"{agg.Name} changed to room {command.RoomNumber}");
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, RemoveParticipantFromRoom command)
        {
            var agg = al(command.Id);

            yield return new ParticipantRemovedFromRoom
            {
                Id = command.Id
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, UpdateParticipantProfile command)
        {
            var agg = al(command.Id);

            yield return new ParticipantProfileChanged
            {
                Id = command.Id,
                Age = command.Age,
                HomeTown = command.HomeTown,
                MaritalStatus = command.MaritalStatus,
                SpouseName = command.SpouseName,
                Children = command.Children,
                Occupation = command.Occupation,

                HomeCenter = command.HomeCenter,
                YearsBowling = command.YearsBowling,
                NumberOfLeagues = command.NumberOfLeagues,
                HighestAverage = command.HighestAverage,

                YearsCoaching = command.YearsCoaching,
                YearsCoachingAdults = command.YearsCoachingAdults,
                BestFinishProvincially = command.BestFinishProvincially,
                BestFinishNationally = command.BestFinishNationally,

                MastersYears = command.MastersYears,
                MasterProvincialWins = command.MasterProvincialWins,
                MastersAchievements = command.MastersAchievements,

                OpenAchievements = command.OpenAchievements,
                OpenYears = command.OpenYears,

                OtherAchievements = command.OtherAchievements,
                Hobbies = command.Hobbies,
            };

            var tournament = CommandQueries.GetTournaments().SingleOrDefault(x => x.Year == DateTime.Now.Year.ToString());
            if (!string.IsNullOrEmpty(agg.Name)) Emailer.SendChangeNotification(tournament.ChangeNotificationCutoffChanged, tournament.ChangeNotificationEmailAddresses, "Singles Profile Changed", $"Profile for {agg.Name} has been changed.");
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, ReplaceParticipant command)
        {
            var agg = al(command.Id);
            var alternate = CommandQueries.GetParticipant(command.AlternateId);
            
            yield return new ParticipantReplacedWithAlternate
            {
                Id = command.Id,
                ContingentId = alternate.ContingentId,
                TeamId = alternate.TeamId,
                Name = agg.Name,
                AlternateId = command.AlternateId,
                AlternateName = alternate.Name,
                Average = alternate.Average,
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, ReorderParticipant command)
        {
            var agg = al(command.Id);

            yield return new ParticipantQualifyingPositionChanged
            {
                Id = command.Id,
                TeamId = command.TeamId,
                QualifyingPosition = command.QualifyingPosition
            };
        }
    }
}
