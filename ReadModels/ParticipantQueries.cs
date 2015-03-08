using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantQueries : AReadModel,
        IParticipantQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToContingent>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantDelegateStatusGranted>,
        ISubscribeTo<ParticipantDelegateStatusRevoked>,
        ISubscribeTo<ParticipantYearsQualifyingChanged>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGuestPackageChanged>,
        ISubscribeTo<ParticipantShirtSizeChanged>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ContingentCreated>
    {
        public ParticipantQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string Gender { get; internal set; }
            public string ContingentId { get; internal set; }
            public string Province { get; internal set; }
            public string TeamId { get; internal set; }
            public string TeamName { get; internal set; }
            public bool IsDelegate { get; internal set; }
            public bool IsCoach { get; internal set; }
            public int YearsQualifying { get; internal set; }
            public int LeaguePinfall { get; internal set; }
            public int LeagueGames { get; internal set; }
            public int TournamentPinfall { get; internal set; }
            public int TournamentGames { get; internal set; }
            public int Average { get; internal set; }
            public int RoomNumber { get; internal set; }
            public bool IsGuest { get; internal set; }
            public PackageInformation Package { get; internal set; }
            public string ShirtSize { get; internal set; }
        }

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }
            public string Province { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
        }

        public class PackageInformation
        {
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
        }

        public List<Participant> GetParticipants()
        {
            return Read<Participant>().ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            return Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new Contingent(e.Id)
            {
                Province = e.Province
            });
        }

        public void Handle(TeamCreated e)
        {
            Create(new Team(e.TeamId)
            {
                ContingentId = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id)
            {
                Name = e.Name,
                Gender = e.Gender,
                IsDelegate = e.IsDelegate,
                YearsQualifying = e.YearsQualifying,
                IsGuest = e.IsGuest,
                Package = new PackageInformation(),
                ContingentId = Guid.Empty.ToString(),
                TeamId = Guid.Empty.ToString()
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Read<Contingent>(x => x.Id == e.ContingentId).FirstOrDefault();
            if (contingent == null)
                return;

            Update<Participant>(e.Id, x => { 
                x.ContingentId = contingent.Id.ToString();
                x.Province = contingent.Province;
            });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            if (team == null)
                return;

            var contingent = Read<Contingent>(x => x.Id == team.ContingentId).FirstOrDefault();
            if (contingent == null)
                return;

            Update<Participant>(e.Id, x => { 
                x.TeamId = team.Id.ToString();
                x.TeamName = team.Name;
                x.ContingentId = team.ContingentId.ToString();
                x.Province = contingent.Province;
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            if (team == null)
                return;

            var contingent = Read<Contingent>(x => x.Id == team.ContingentId).FirstOrDefault();
            if (contingent == null)
                return;

            Update<Participant>(e.Id, x => {
                x.IsCoach = true;
                x.TeamId = team.Id.ToString();
                x.TeamName = team.Name;
                x.ContingentId = team.ContingentId.ToString();
                x.Province = contingent.Province;
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Update<Participant>(e.Id, x => { x.Gender = e.Gender; });
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = true; });
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = false; });
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            Update<Participant>(e.Id, x => { x.YearsQualifying = e.YearsQualifying; });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Update<Participant>(e.Id, x => {
                x.LeagueGames = e.LeagueGames;
                x.LeaguePinfall = e.LeaguePinfall;
                x.TournamentGames = e.TournamentGames;
                x.TournamentPinfall = e.TournamentPinfall;
                x.Average = e.Average;
            });
        }

        public void Handle(ParticipantGuestPackageChanged e)
        {
            Update<Participant>(e.Id, x => { 
                x.Package.ManitobaDinner = e.ManitobaDinner;
                x.Package.ManitobaDance = e.ManitobaDance;
                x.Package.FinalBanquet = e.FinalBanquet;
                x.Package.Transportation = e.Transportation;
            });
        }

        public void Handle(ParticipantShirtSizeChanged e)
        {
            Update<Participant>(e.Id, x => { x.ShirtSize = e.ShirtSize; });
        }
    }
}