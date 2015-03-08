using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries : AReadModel,
        IContingentViewQueries,
        ISubscribeTo<ContingentCreated>,
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
        ISubscribeTo<ParticipantYearsQualifyingChanged>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantReplacedWithAlternate>
    {
        public ContingentViewQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
            public IList<Participant> Guests { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
            public IList<Participant> Bowlers { get; internal set; }
            public Participant Coach { get; internal set; }
            public string Alternate { get; internal set; }
            public string Gender { get; internal set; }
            public int SizeLimit { get; internal set; }
            public bool RequiresShirtSize { get; internal set; }
            public bool RequiresCoach { get; internal set; }
            public bool RequiresAverage { get; internal set; }
            public bool RequiresBio { get; internal set; }
            public bool RequiresGender { get; internal set; }
            public bool IncludesSinglesRep { get; internal set; }
        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public bool IsRookie { get; internal set; }
            public bool IsDelegate { get; internal set; }
            public bool IsGuest { get; internal set; }
            public int Average { get; internal set; }
            public string ReplacedBy { get; internal set; }
        }

        public Contingent GetContingent(Guid id)
        {
            return Read<Contingent>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Contingent GetContingent(string province)
        {
            return Read<Contingent>(x => x.Province.Equals(province)).FirstOrDefault();            
        }
        
        public void Handle(ContingentCreated e)
        {
            Create(new Contingent(e.Id)
            {
                Province = e.Province,
                Teams = new List<Team>(),
                Guests = new List<Participant>(),
            });
        }

        public void Handle(TeamCreated e)
        {
            Update<Contingent>(e.Id, contingent =>
            {
                contingent.Teams.Add(
                    new Team(e.TeamId)
                    {
                        Name = e.Name,
                        ContingentId = e.Id,
                        SizeLimit = e.SizeLimit,
                        Bowlers = new List<Participant>(),
                        Gender = e.Gender,
                        RequiresShirtSize = e.RequiresShirtSize,
                        RequiresCoach = e.RequiresCoach,
                        RequiresAverage = e.RequiresAverage,
                        RequiresBio = e.RequiresBio,
                        RequiresGender = e.RequiresGender,
                        IncludesSinglesRep = e.IncludesSinglesRep,
                    });
            });
        }

        public void Handle(TeamRemoved e)
        {
            Update<Contingent>(e.Id, contingent =>
            {
                contingent.Teams.Remove(contingent.Teams.SingleOrDefault(x => x.Id.Equals(e.TeamId)));
            });
        }
		
        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id)
            {
                Name = e.Name,
                IsDelegate = e.IsDelegate,
                IsRookie = e.YearsQualifying == 1,
                IsGuest = e.IsGuest
            });
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            Update<Contingent>(e.ContingentId, (contingent, odb) =>
            {
                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null 
                    || participant.IsGuest == false 
                    || contingent.Guests.Any(x => x.Id == e.Id))
                    return;

                contingent.Guests.Add(participant);
            });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var cntgt = Read<Contingent>(c => c.Teams.Any(t => t.Id.Equals(e.TeamId))).FirstOrDefault();
            if (cntgt == null)
                return;

            Update<Contingent>(cntgt.Id, (contingent, odb) => {
                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null)
                    return;

                team.Bowlers.Add(participant);
            });
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            var cntgt = Read<Contingent>(c => c.Teams.Any(t => t.Id.Equals(e.TeamId))).FirstOrDefault();
            if (cntgt == null)
                return;

            Update<Contingent>(cntgt.Id, (contingent, odb) =>
            {
                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null)
                    return;

                team.Alternate = participant.Id.ToString("D");
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var cntgt = Read<Contingent>(c => c.Teams.Any(t => t.Id.Equals(e.TeamId))).FirstOrDefault();
            if (cntgt == null)
                return;

            Update<Contingent>(cntgt.Id, (contingent, odb) =>
            {
                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null)
                    return;

                team.Coach = participant;
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
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
            Update<Participant>(e.Id, x => { x.IsRookie = e.YearsQualifying == 1; });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Update<Participant>(e.Id, x => { x.Average = e.Average; });
        }

        public void Handle(ParticipantReplacedWithAlternate e)
        {
            var contingentId = e.ContingentId;
            if (contingentId == Guid.Empty)
            {
                var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
                contingentId = team.ContingentId;
            }

            Update<Contingent>(contingentId, (contingent, odb) =>
            {
                var team = contingent.Teams.FirstOrDefault(t => t.Alternate == e.AlternateId.ToString());
                if (team == null)
                    return;

                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null)
                    return;

                var alternate = Read<Participant>(x => x.Id.Equals(e.AlternateId), odb).FirstOrDefault();
                if (alternate == null)
                    return;

                team.Bowlers.Add(alternate);
                participant.ReplacedBy = e.AlternateId.ToString();
            });
        }
    }
}
