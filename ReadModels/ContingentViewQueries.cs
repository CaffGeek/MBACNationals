using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries : BaseReadModel<ContingentViewQueries>,
        IContingentViewQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantAssignedToContingent>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantDesignatedAsAlternate>,
        ISubscribeTo<CoachAssignedToTeam>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantDelegateStatusGranted>,
        ISubscribeTo<ParticipantDelegateStatusRevoked>,
        ISubscribeTo<ParticipantManagerStatusGranted>,
        ISubscribeTo<ParticipantManagerStatusRevoked>,
        ISubscribeTo<ParticipantYearsQualifyingChanged>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantReplacedWithAlternate>,
        ISubscribeTo<ParticipantBirthdayChanged>
    {
        public class Contingent
        {
            public Guid Id { get; internal set; }
            public Guid Tournament { get; internal set; }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
            public IList<Participant> Guests { get; internal set; }
        }

        public class Team
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
            public IList<Participant> Bowlers { get; internal set; }
            public Participant Coach { get; internal set; }
            public Guid Alternate { get; internal set; }
            public string Gender { get; internal set; }
            public int SizeLimit { get; internal set; }
            public bool RequiresShirtSize { get; internal set; }
            public bool RequiresCoach { get; internal set; }
            public bool RequiresAverage { get; internal set; }
            public bool RequiresBio { get; internal set; }
            public bool RequiresGender { get; internal set; }
            public bool IncludesSinglesRep { get; internal set; }
        }

        public class Participant
        {
            public Guid Id { get; internal set; }
            public Guid TeamId { get; set; }
            public string Name { get; internal set; }
            public bool IsRookie { get; internal set; }
            public bool IsDelegate { get; internal set; }
            public bool IsManager { get; internal set; }
            public bool IsGuest { get; internal set; }
            public int Average { get; internal set; }
            public string ReplacedBy { get; internal set; }
            public DateTime? Birthday { get; internal set; }
        }

        private class TSContingent : Entity
        {
            public Guid Tournament { get; set; }
            public string Province { get; set; }
        }

        private class TSTeam : Entity
        {
            public string Name { get; set; }
            public Guid Coach { get; set; }
            public Guid Alternate { get; set; }
            public string Gender { get; set; }
            public int SizeLimit { get; set; }
            public bool RequiresShirtSize { get; set; }
            public bool RequiresCoach { get; set; }
            public bool RequiresAverage { get; set; }
            public bool RequiresBio { get; set; }
            public bool RequiresGender { get; set; }
            public bool IncludesSinglesRep { get; set; }
        }

        private class TSParticipant : Entity
        {
            public string Name { get; set; }
            public Guid TeamId { get; set; }
            public bool IsRookie { get; set; }
            public bool IsDelegate { get; set; }
            public bool IsManager { get; set; }
            public bool IsGuest { get; set; }
            public int Average { get; set; }
            public string ReplacedBy { get; set; }
            public DateTime? Birthday { get; set; }
        }
        
        public Contingent GetContingent(Guid tournamentId, string province)
        {
            var contingent = Storage.Query<TSContingent>(x => x.Tournament == tournamentId && x.Province == province).FirstOrDefault();
            if (contingent == null)
                return null;

            var bowlers = Storage.Query<TSParticipant>(x => x.PartitionKey == contingent.PartitionKey)
                .Select(x =>
                {
                    return new Participant
                    {
                        Id = Guid.Parse(x.RowKey),
                        TeamId = x.TeamId,
                        Name = x.Name,
                        Average = x.Average,
                        IsDelegate = x.IsDelegate,
                        IsManager = x.IsManager,
                        IsRookie = x.IsRookie,
                        IsGuest = x.IsGuest,
                        ReplacedBy = x.ReplacedBy,
                        Birthday = x.Birthday,
                    };
                }).ToList();

            var teams = Storage.Query<TSTeam>(x => x.PartitionKey == contingent.PartitionKey)
                .Select(x =>
                {
                    var coach = bowlers.FirstOrDefault(b => b.Id == x.Coach);

                    return new Team
                    {
                        Id = Guid.Parse(x.RowKey),
                        ContingentId = Guid.Parse(x.PartitionKey),
                        Name = x.Name,
                        Gender = x.Gender,
                        IncludesSinglesRep = x.IncludesSinglesRep,
                        SizeLimit = x.SizeLimit,
                        RequiresAverage = x.RequiresAverage,
                        RequiresBio = x.RequiresBio,
                        RequiresCoach = x.RequiresCoach,
                        RequiresGender = x.RequiresGender,
                        RequiresShirtSize = x.RequiresShirtSize,
                        Coach = coach,
                        Bowlers = bowlers.Where(b => b.TeamId == Guid.Parse(x.RowKey)).ToList(),
                        Alternate = x.Alternate
                    };
                }).ToList();

            var guests = bowlers.Where(x => x.IsGuest).ToList();

            return new Contingent
            {
                Id = Guid.Parse(contingent.PartitionKey),
                Province = contingent.Province,
                Tournament = contingent.Tournament,
                Teams = teams,
                Guests = guests
            };
        }
        
        public void Handle(ContingentCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSContingent//(e.Id)
            {
                Province = e.Province,
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            Storage.Update<TSContingent>(e.Id, e.Id, contingent =>
            {
                contingent.Tournament = e.TournamentId;
            });
        }

        public void Handle(TeamCreated e)
        {
            Storage.Create(e.Id, e.TeamId, new TSTeam
            {
                Name = e.Name,
                SizeLimit = e.SizeLimit,
                Gender = e.Gender,
                RequiresShirtSize = e.RequiresShirtSize,
                RequiresCoach = e.RequiresCoach,
                RequiresAverage = e.RequiresAverage,
                RequiresBio = e.RequiresBio,
                RequiresGender = e.RequiresGender,
                IncludesSinglesRep = e.IncludesSinglesRep,
            });
        }

        public void Handle(TeamRemoved e)
        {
            Storage.Delete<TSTeam>(e.Id, e.TeamId);
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(Guid.Empty, e.Id, new TSParticipant
            {
                Name = e.Name,
                IsDelegate = e.IsDelegate,
                IsRookie = e.YearsQualifying == 1,
                IsGuest = e.IsGuest,
                Birthday = e.Birthday,
            });
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var participant = Storage.Read<TSParticipant>(Guid.Empty, e.Id);
            Storage.Delete<TSParticipant>(Guid.Empty, e.Id);
            Storage.Create(e.ContingentId, e.Id, participant);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Storage.Query<TSTeam>(x => { return x.RowKey == e.TeamId.ToString(); }).FirstOrDefault();
            var participant = Storage.Read<TSParticipant>(Guid.Empty, e.Id);
            Storage.Delete<TSParticipant>(Guid.Empty, e.Id);
            participant.TeamId = e.TeamId;
            Storage.Create(Guid.Parse(team.PartitionKey), e.Id, participant);
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            Storage.Update<TSTeam>(e.TeamId, team =>
            {
                team.Alternate = e.Id;
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Storage.Read<TSTeam>(e.TeamId);
            
            //FIX: If coach wasn't assigned to Contingent yet...
            var coach = Storage.Read<TSParticipant>(Guid.Empty, e.Id);
            if (coach != null)
            {
                Storage.Delete<TSParticipant>(Guid.Empty, e.Id);
                Storage.Create(Guid.Parse(team.PartitionKey), e.Id, coach);
            }

            Storage.Update<TSTeam>(Guid.Parse(team.PartitionKey), Guid.Parse(team.RowKey), x => x.Coach = e.Id);
        }

        public void Handle(ParticipantRenamed e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.IsDelegate = true; });
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.IsDelegate = false; });
        }

        public void Handle(ParticipantManagerStatusGranted e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.IsManager = true; });
        }

        public void Handle(ParticipantManagerStatusRevoked e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.IsManager = false; });
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.IsRookie = e.YearsQualifying == 1; });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.Average = e.Average; });
        }

        public void Handle(ParticipantReplacedWithAlternate e)
        {
            Storage.Update<TSParticipant>(e.AlternateId, x => x.TeamId = e.TeamId); // Assign Alternate to team
            Storage.Update<TSParticipant>(e.Id, x=> x.ReplacedBy = e.AlternateId.ToString()); // Mark bowler as replaced
        }

        public void Handle(ParticipantBirthdayChanged e)
        {
            Storage.Update<TSParticipant>(e.Id, x => { x.Birthday = e.Birthday; });
        }
    }
}
