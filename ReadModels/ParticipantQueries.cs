using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantQueries : 
        IReadModel,
        IParticipantQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToContingent>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantDelegateStatusGranted>,
        ISubscribeTo<ParticipantDelegateStatusRevoked>,
        ISubscribeTo<ParticipantManagerStatusGranted>,
        ISubscribeTo<ParticipantManagerStatusRevoked>,
        ISubscribeTo<ParticipantYearsQualifyingChanged>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGuestPackageChanged>,
        ISubscribeTo<ParticipantShirtSizeChanged>,
        ISubscribeTo<ParticipantAssignedToRoom>,
        ISubscribeTo<ParticipantBirthdayChanged>,
        ISubscribeTo<ParticipantDesignatedAsAlternate>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>
    {
        public List<Participant> Participants { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public List<Contingent> Contingents { get; set; }
        public List<Team> Teams { get; set; }

        public class Participant
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public Guid ContingentId { get; set; }
            public string Province { get; set; }
            public Guid TeamId { get; set; }
            public string TeamName { get; set; }
            public bool IsDelegate { get; set; }
            public bool IsManager { get; set; }
            public bool IsCoach { get; set; }
            public int YearsQualifying { get; set; }
            public int LeaguePinfall { get; set; }
            public int LeagueGames { get; set; }
            public int TournamentPinfall { get; set; }
            public int TournamentGames { get; set; }
            public int Average { get; set; }
            public int RoomNumber { get; set; }
            public bool IsGuest { get; set; }
            public PackageInformation Package { get; set; }
            public string ShirtSize { get; set; }
            public string Birthday { get; set; }
            public bool IsAlternate { get; set; }
            public int QualifyingPosition { get; set; }
            
            public Participant()
            {
                Package = new PackageInformation();
            }
        }

        public class PackageInformation
        {
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
            public bool Option1 { get; set; }
            public bool Option2 { get; set; }
            public bool Option3 { get; set; }
            public bool Option4 { get; set; }
        }

        public class Tournament
        {
            public Guid Id { get; set; }
            public string Year { get; set; }
        }

        public class Contingent
        {
            public Guid Id { get; set; }
            public Guid TournamentId { get; set; }
            public string Year { get; set; }
            public string Province { get; set; }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public string Name { get; set; }

            public Guid Alternate { get; set; }
            public string AlternateName { get; set; }
        }

        public ParticipantQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Participants = new List<Participant>();
            Tournaments = new List<Tournament>();
            Contingents = new List<Contingent>();
            Teams = new List<Team>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public List<Participant> GetParticipants(string year)
        {
            var contingents = Contingents.Where(x => x.Year == year);
            
            var participants = Participants
                .Where(x => contingents.Any(c => c.Id == x.ContingentId))
                .ToList();

            return participants;
        }

        public Participant GetParticipant(Guid id)
        {
            var participant = Participants.Single(x => x.Id == id);

            return participant;
        }

        public List<Participant> GetAlternates(string year)
        {
            var contingents = Contingents.Where(x => x.Year == year);

            var alternates = Participants
                .Where(x => x.IsAlternate)
                .Where(x => contingents.Any(c => c.Id == x.ContingentId))
                .ToList();

            return alternates;
        }

        public void Handle(ContingentCreated e)
        {
            Contingents.Add(new Contingent
            {
                Id = e.Id,
                Province = e.Province,
            });
        }

        public void Handle(TeamCreated e)
        {
            Teams.Add(new Team
            {
                Id = e.TeamId,
                ContingentId = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(new Participant
            {
                Id = e.Id,
                Name = e.Name,
                Gender = e.Gender,
                IsDelegate = e.IsDelegate,
                YearsQualifying = e.YearsQualifying,
                IsGuest = e.IsGuest,
                ContingentId = Guid.Empty,
                TeamId = Guid.Empty
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Name = e.Name;
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Contingents.Single(x => x.Id == e.ContingentId);            
            var participant = Participants.Single(x => x.Id == e.Id);

            participant.ContingentId = contingent.Id;
            participant.Province = contingent.Province;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Teams.Single(x => x.Id == e.TeamId);
            var contingent = Contingents.Single(x => x.Id == team.ContingentId);

            var currentTeammates = Participants.Where(x => x.TeamId == e.TeamId).ToList()
                ?? new List<Participant>();

            var participant = Participants.Single(x => x.Id == e.Id);

            participant.TeamId = team.Id;
            participant.TeamName = team.Name;
            participant.ContingentId = team.ContingentId;
            participant.Province = contingent.Province;
            participant.QualifyingPosition = currentTeammates.Count + 1;
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            var team = Teams.Single(x => x.Id == e.TeamId);

            var priorAlternateId = team.Alternate;
            if (priorAlternateId != Guid.Empty)
                Participants.Single(x => x.Id == priorAlternateId).IsAlternate = false;

            var alternate = Participants.Single(x => x.Id == e.Id);
            alternate.IsAlternate = true;
            alternate.TeamName = team.Name;

            team.Alternate = e.Id;
            team.AlternateName = e.Name;
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Teams.Single(x => x.Id == e.TeamId);
            var contingent = Contingents.Single(x=> x.Id == team.ContingentId);

            var participant = Participants.Single(x => x.Id == e.Id);

            participant.IsCoach = true;
            participant.TeamId = team.Id;
            participant.TeamName = team.Name;
            participant.ContingentId = team.ContingentId;
            participant.Province = contingent.Province;
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Gender = e.Gender;
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.IsDelegate = true;
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.IsDelegate = false;
        }

        public void Handle(ParticipantManagerStatusGranted e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.IsManager = true;
        }

        public void Handle(ParticipantManagerStatusRevoked e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.IsManager = false;
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.YearsQualifying = e.YearsQualifying;
        }

        public void Handle(ParticipantAverageChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.LeagueGames = e.LeagueGames;
            participant.LeaguePinfall = e.LeaguePinfall;
            participant.TournamentGames = e.TournamentGames;
            participant.TournamentPinfall = e.TournamentPinfall;
            participant.Average = e.Average;
        }

        public void Handle(ParticipantGuestPackageChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Package.ManitobaDinner = e.ManitobaDinner;
            participant.Package.ManitobaDance = e.ManitobaDance;
            participant.Package.FinalBanquet = e.FinalBanquet;
            participant.Package.Transportation = e.Transportation;
            participant.Package.Option1 = e.Option1;
            participant.Package.Option2 = e.Option2;
            participant.Package.Option3 = e.Option3;
            participant.Package.Option4 = e.Option4;
        }

        public void Handle(ParticipantShirtSizeChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.ShirtSize = e.ShirtSize;
        }

        public void Handle(ParticipantAssignedToRoom e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.RoomNumber = e.RoomNumber;
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Contingents.Single(x => x.Id == e.Id);
            var tournament = Tournaments.SingleOrDefault(x => x.Id == e.TournamentId)
                ?? new Tournament { Id = e.TournamentId, Year = "2014" };

            contingent.TournamentId = tournament.Id;
            contingent.Year = tournament.Year;
        }

        public void Handle(TournamentCreated e)
        {
            Tournaments.Add(new Tournament
            {
                Id = e.Id,
                Year = e.Year
            });
        }

        public void Handle(ParticipantBirthdayChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Birthday = e.Birthday.ToString("yyyy-MM-ddTHH:mm");
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.QualifyingPosition = e.QualifyingPosition;
        }
    }
}