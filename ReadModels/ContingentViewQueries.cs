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
        ISubscribeTo<ParticipantBirthdayChanged>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>
    {
        public List<Contingent> Contingents { get; private set; }
        public Dictionary<Guid, Participant> Participants { get; private set; }

        public class Contingent
        {
            public Guid Id { get; set; }
            public Guid Tournament { get; set; }
            public string Province { get; set; }
            public List<Team> Teams { get; set; }
            public List<Participant> Guests { get; set; }

            public Contingent()
            {
                Teams = new List<Team>();
                Guests = new List<Participant>();
            }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid ContingentId { get; set; }
            public IList<Participant> Bowlers { get; set; }
            public Participant Coach { get; set; }
            public Guid Alternate { get; set; }
            public string Gender { get; set; }
            public int SizeLimit { get; set; }
            public bool RequiresShirtSize { get; set; }
            public bool RequiresCoach { get; set; }
            public bool RequiresAverage { get; set; }
            public bool RequiresBio { get; set; }
            public bool RequiresGender { get; set; }
            public bool IncludesSinglesRep { get; set; }

            public Team()
            {
                Bowlers = new List<Participant>();
            }
        }

        public class Participant
        {
            public Guid Id { get; set; }
            public Guid TeamId { get; set; }
            public string Name { get; set; }
            public bool IsRookie { get; set; }
            public bool IsDelegate { get; set; }
            public bool IsManager { get; set; }
            public bool IsGuest { get; set; }
            public int Average { get; set; }
            public Guid ReplacedBy { get; set; }
            public DateTime? Birthday { get; set; }
            public int QualifyingPosition { get; set; }
        }

        public ContingentViewQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Contingents = new List<Contingent>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public Contingent GetContingent(Guid tournamentId, string province)
        {
            return Contingents.SingleOrDefault(x => x.Tournament == tournamentId && x.Province == province);
            //TODO: .OrderBy(x => x.QualifyingPosition)
        }
        
        public void Handle(ContingentCreated e)
        {
            Contingents.Add(new Contingent
            {
                Id = e.Id,
                Province = e.Province
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Contingents.Single(x => x.Id == e.Id);
            contingent.Tournament = e.TournamentId;
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Contingents.Single(x => x.Id == e.Id);
            contingent.Teams.Add(new Team
            {
                Id = e.TeamId,
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
            var contingent = Contingents.Single(x => x.Id == e.Id);
            contingent.Teams.RemoveAll(x => x.Id == e.TeamId);
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(e.Id, new Participant
            {
                Id = e.Id,
                Name = e.Name,
                IsDelegate = e.IsDelegate,
                IsRookie = e.YearsQualifying == 1,
                IsGuest = e.IsGuest,
                Birthday = e.Birthday,
            });
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            //var contingent = Contingents.Single(x => x.Id == e.ContingentId);
            //var participant = Participants[e.Id];
            //contingent.Guests.Add(participant);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            //var team = Contingents.SelectMany(x => x.Teams)
            //    .Single(x => x.Id == e.TeamId);

            //var participant = Participants[e.Id];
            //participant.TeamId = e.TeamId;
            //participant.QualifyingPosition = team.Bowlers.Count + 1;
            //team.Bowlers.Add(participant);
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            var team = Contingents.SelectMany(x => x.Teams)
                .Single(x => x.Id == e.TeamId);
            team.Alternate = e.Id;
        }

        public void Handle(CoachAssignedToTeam e)
        {
            //var team = Contingents.SelectMany(x => x.Teams)
            //    .Single(x => x.Id == e.TeamId);
            //var coach = Participants[e.Id];
            //team.Coach = coach;
        }

        public void Handle(ParticipantRenamed e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].Name = e.Name;
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].IsDelegate = true;
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].IsDelegate = false;
        }

        public void Handle(ParticipantManagerStatusGranted e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].IsManager = true;
        }

        public void Handle(ParticipantManagerStatusRevoked e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].IsManager = false;
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].IsRookie = e.YearsQualifying == 1;
        }

        public void Handle(ParticipantAverageChanged e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].Average = e.Average;
        }

        public void Handle(ParticipantReplacedWithAlternate e)
        {
            if (Participants.ContainsKey(e.AlternateId)) Participants[e.Id].TeamId = e.TeamId;
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].ReplacedBy = e.AlternateId;
        }

        public void Handle(ParticipantBirthdayChanged e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].Birthday = e.Birthday;
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            if (Participants.ContainsKey(e.Id)) Participants[e.Id].QualifyingPosition = e.QualifyingPosition;
        }
    }
}
