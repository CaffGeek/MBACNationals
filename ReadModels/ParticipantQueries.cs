using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantQueries : BaseReadModel<ParticipantQueries>,
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
        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Gender { get; internal set; }
            public string ContingentId { get; internal set; }
            public string Province { get; internal set; }
            public string TeamId { get; internal set; }
            public string TeamName { get; internal set; }
            public bool IsDelegate { get; internal set; }
            public bool IsManager { get; internal set; }
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
            public string Birthday { get; internal set; }
            public bool IsAlternate { get; set; }
            public int QualifyingPosition { get; internal set; }
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

        private class TSParticipant : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string ContingentId { get; set; }
            public string Province { get; set; }
            public string TeamId { get; set; }
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
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
            public bool Option1 { get; set; }
            public bool Option2 { get; set; }
            public bool Option3 { get; set; }
            public bool Option4 { get; set; }
            public string ShirtSize { get; set; }
            public string Birthday { get; set; }
            public int QualifyingPosition { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Province { get; set; }
            public Guid TournamentId { get; set; }
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

        private class TSTeam : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid ContingentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public Guid Alternate { get; set; }
            public string AlternateName { get; set; }
        }
        
        public List<Participant> GetParticipants(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);
            var alternates = Storage.Query<TSTeam>()
                .Select(x => x.Alternate)
                .ToList();

            return Storage.Query<TSParticipant>()
                .Where(x => contingents.Any(c => c.Id.ToString() == x.ContingentId))
                .Select(x => new Participant
                {
                    Id = x.Id,
                    TeamId = x.TeamId,
                    ContingentId = x.ContingentId,
                    Name = x.Name,
                    Gender = x.Gender,
                    Province = x.Province,
                    TeamName = x.TeamName,
                    IsDelegate = x.IsDelegate,
                    IsManager = x.IsManager,
                    IsCoach = x.IsCoach,
                    YearsQualifying = x.YearsQualifying,
                    LeaguePinfall = x.LeaguePinfall,
                    LeagueGames = x.LeagueGames,
                    TournamentPinfall = x.TournamentPinfall,
                    TournamentGames = x.TournamentGames,
                    Average = x.Average,
                    RoomNumber = x.RoomNumber,
                    IsGuest = x.IsGuest,
                    Package = new PackageInformation
                    {
                        FinalBanquet = x.FinalBanquet,
                        ManitobaDance = x.ManitobaDance,
                        ManitobaDinner = x.ManitobaDinner,
                        Transportation = x.Transportation,
                        Option1 = x.Option1,
                        Option2 = x.Option2,
                        Option3 = x.Option3,
                        Option4 = x.Option4,
                    },
                    ShirtSize = x.ShirtSize,
                    Birthday = x.Birthday,
                    IsAlternate = alternates.Any(a => a == x.Id),
                    QualifyingPosition = x.QualifyingPosition
                })
                .ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            var participant = Storage.Read<TSParticipant>(id, id);
            var alternates = Storage.Query<TSTeam>(t => t.Alternate == id);

            return new Participant
                {
                    Id = participant.Id,
                    TeamId = participant.TeamId,
                    ContingentId = participant.ContingentId,
                    Name = participant.Name,
                    Gender = participant.Gender,
                    Province = participant.Province,
                    TeamName = participant.TeamName,
                    IsDelegate = participant.IsDelegate,
                    IsManager = participant.IsManager,
                    IsCoach = participant.IsCoach,
                    YearsQualifying = participant.YearsQualifying,
                    LeaguePinfall = participant.LeaguePinfall,
                    LeagueGames = participant.LeagueGames,
                    TournamentPinfall = participant.TournamentPinfall,
                    TournamentGames = participant.TournamentGames,
                    Average = participant.Average,
                    RoomNumber = participant.RoomNumber,
                    IsGuest = participant.IsGuest,
                    Package = new PackageInformation
                    {
                        FinalBanquet = participant.FinalBanquet,
                        ManitobaDance = participant.ManitobaDance,
                        ManitobaDinner = participant.ManitobaDinner,
                        Transportation = participant.Transportation,
                        Option1 = participant.Option1,
                        Option2 = participant.Option2,
                        Option3 = participant.Option3,
                        Option4 = participant.Option4,
                    },
                    ShirtSize = participant.ShirtSize,
                    Birthday = participant.Birthday,
                    IsAlternate = alternates.Any(),
                    QualifyingPosition = participant.QualifyingPosition
                };
        }

        public List<Participant> GetAlternates(string year)
        {
            var tournament = Storage.Query<TSTournament>(x => x.Year == year).FirstOrDefault();
            var contingents = Storage.Query<TSContingent>(x => x.TournamentId == tournament.Id);
            var teams = Storage.Query<TSTeam>()
                .Where(x => contingents.Any(c => c.Id == x.ContingentId))
                .ToList();
            
            var alternates = Storage.Query<TSParticipant>()
                .Where(x => teams.Any(t => t.Alternate == x.Id))                
                .ToList();

            return (from team in teams
                    join alternate in alternates
                     on team.Alternate equals alternate.Id into a
                    from d in a.DefaultIfEmpty()
                    where d != null
                    select new Participant
                    {
                        Id = d.Id,
                        TeamId = d.TeamId,
                        ContingentId = d.ContingentId,
                        Name = d.Name,
                        Gender = d.Gender,
                        Province = d.Province,
                        TeamName = team.Name,
                        IsDelegate = d.IsDelegate,
                        IsManager = d.IsManager,
                        IsCoach = d.IsCoach,
                        YearsQualifying = d.YearsQualifying,
                        LeaguePinfall = d.LeaguePinfall,
                        LeagueGames = d.LeagueGames,
                        TournamentPinfall = d.TournamentPinfall,
                        TournamentGames = d.TournamentGames,
                        Average = d.Average,
                        RoomNumber = d.RoomNumber,
                        IsGuest = d.IsGuest,
                        Package = new PackageInformation
                        {
                            FinalBanquet = d.FinalBanquet,
                            ManitobaDance = d.ManitobaDance,
                            ManitobaDinner = d.ManitobaDinner,
                            Transportation = d.Transportation,
                            Option1 = d.Option1,
                            Option2 = d.Option2,
                            Option3 = d.Option3,
                            Option4 = d.Option4,
                        },
                        ShirtSize = d.ShirtSize,
                        Birthday = d.Birthday,
                        IsAlternate = true,
                        QualifyingPosition = d.QualifyingPosition
                    }).ToList();
        }

        public void Handle(ContingentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSContingent
            {
                Province = e.Province
            });
        }

        public void Handle(TeamCreated e)
        {
            Storage.Create(e.Id, e.TeamId, new TSTeam
            {
                Name = e.Name
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSParticipant
            {
                Name = e.Name,
                Gender = e.Gender,
                IsDelegate = e.IsDelegate,
                YearsQualifying = e.YearsQualifying,
                IsGuest = e.IsGuest,
                ContingentId = Guid.Empty.ToString(),
                TeamId = Guid.Empty.ToString()
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Storage.Read<TSContingent>(e.ContingentId, e.ContingentId);

            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            { 
                x.ContingentId = contingent.Id.ToString();
                x.Province = contingent.Province;
            });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Storage.Read<TSTeam>(e.TeamId);
            var contingent = Storage.Read<TSContingent>(team.ContingentId, team.ContingentId);

            var currentTeammates = Storage.Query<TSParticipant>(x => x.TeamId == e.TeamId.ToString())
               ?? new List<TSParticipant>();
            
            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            { 
                x.TeamId = team.Id.ToString();
                x.TeamName = team.Name;
                x.ContingentId = team.ContingentId.ToString();
                x.Province = contingent.Province;
                x.QualifyingPosition = currentTeammates.Count + 1;
            });
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            var team = Storage.Read<TSTeam>(e.TeamId);
            
            Storage.Update<TSTeam>(e.TeamId, x => { 
                x.Alternate = e.Id;
                x.AlternateName = e.Name;
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Storage.Read<TSTeam>(e.TeamId);
            var contingent = Storage.Read<TSContingent>(team.ContingentId, team.ContingentId);

            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.IsCoach = true;
                x.TeamId = team.Id.ToString();
                x.TeamName = team.Name;
                x.ContingentId = team.ContingentId.ToString();
                x.Province = contingent.Province;
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.Gender = e.Gender; });
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.IsDelegate = true; });
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.IsDelegate = false; });
        }

        public void Handle(ParticipantManagerStatusGranted e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.IsManager = true; });
        }

        public void Handle(ParticipantManagerStatusRevoked e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.IsManager = false; });
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.YearsQualifying = e.YearsQualifying; });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.LeagueGames = e.LeagueGames;
                x.LeaguePinfall = e.LeaguePinfall;
                x.TournamentGames = e.TournamentGames;
                x.TournamentPinfall = e.TournamentPinfall;
                x.Average = e.Average;
            });
        }

        public void Handle(ParticipantGuestPackageChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x =>
            { 
                x.ManitobaDinner = e.ManitobaDinner;
                x.ManitobaDance = e.ManitobaDance;
                x.FinalBanquet = e.FinalBanquet;
                x.Transportation = e.Transportation;
                x.Option1 = e.Option1;
                x.Option2 = e.Option2;
                x.Option3 = e.Option3;
                x.Option4 = e.Option4;
            });
        }

        public void Handle(ParticipantShirtSizeChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.ShirtSize = e.ShirtSize; });
        }

        public void Handle(ParticipantAssignedToRoom e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => { x.RoomNumber = e.RoomNumber; });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            Storage.Update<TSContingent>(e.Id, e.Id, x => x.TournamentId = e.TournamentId);
        }

        public void Handle(TournamentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ParticipantBirthdayChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, e.Id, x => x.Birthday = e.Birthday.ToString("yyyy-MM-ddTHH:mm"));
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.QualifyingPosition = e.QualifyingPosition; });
        }
    }
}