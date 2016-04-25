using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries :
        IReadModel,
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
        public List<Contingent> Contingents { get; set; }
        public Dictionary<Guid, Participant> Participants { get; set; }

        public class Contingent
        {
            public Guid Id { get; set; }
            public Guid Tournament { get; set; }
            public string Province { get; set; }
            public List<Team> Teams { get; set; }
            public List<Participant> Guests { get; internal set; }

            public Contingent()
            {
                Teams = new List<Team>();
            }
        }

        public class Team
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid ContingentId { get; set; }
            public List<Participant> Bowlers { get; internal set; }
            public Guid? CoachId { get; set; }
            public Participant Coach { get; internal set; }
            public Guid? Alternate { get; set; }
            public string Gender { get; set; }
            public int SizeLimit { get; set; }
            public bool RequiresShirtSize { get; set; }
            public bool RequiresCoach { get; set; }
            public bool RequiresAverage { get; set; }
            public bool RequiresBio { get; set; }
            public bool RequiresGender { get; set; }
            public bool IncludesSinglesRep { get; set; }
        }

        public class Participant
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public Guid TeamId { get; set; }
            public string Name { get; set; }
            public bool IsRookie { get; set; }
            public bool IsDelegate { get; set; }
            public bool IsManager { get; set; }
            public bool IsGuest { get; set; }
            public int Average { get; set; }
            public Guid? ReplacedBy { get; set; }
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
            Participants = new Dictionary<Guid, Participant>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public Contingent GetContingent(Guid tournamentId, string province)
        {
            var contingent = Contingents
                .FirstOrDefault(x => x.Tournament == tournamentId && x.Province == province);

            contingent.Teams.ForEach(team =>
            {
                if (team.CoachId.HasValue)
                    team.Coach = Participants[team.CoachId.Value];

                team.Bowlers = Participants.Where(x => x.Value.TeamId == team.Id)
                    .Select(x => x.Value)
                    .OrderBy(x => x.QualifyingPosition)
                    .ToList();
            });

            contingent.Guests = Participants.Where(x => x.Value.ContingentId == contingent.Id)
                    .Select(x => x.Value)
                    .Except(contingent.Teams.SelectMany(x => x.Bowlers.Concat<Participant>(new[] { x.Coach })))
                    .ToList();
            return contingent;
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
            var participant = Participants[e.Id];
            participant.ContingentId = e.ContingentId;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var participant = Participants[e.Id];
            participant.TeamId = e.TeamId;
            participant.QualifyingPosition = Participants.Count(x => x.Value.TeamId == e.TeamId);
        }

        public void Handle(ParticipantDesignatedAsAlternate e)
        {
            var team = Contingents.SelectMany(x => x.Teams)
                .Single(x => x.Id == e.TeamId);
            team.Alternate = e.Id;
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Contingents.SelectMany(x => x.Teams)
                .Single(x => x.Id == e.TeamId);
            team.CoachId = e.Id;
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
            if (Participants.ContainsKey(e.AlternateId)) Participants[e.AlternateId].TeamId = e.TeamId;
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
