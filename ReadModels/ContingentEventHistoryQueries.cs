using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentEventHistoryQueries : AReadModel,
        IContingentEventHistoryQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>
    {
        public ContingentEventHistoryQueries(string readModelFilePath)
            : base(readModelFilePath) { }

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }

            public string Province { get; internal set; }
            public List<Event> Events { get; internal set; }
        }

        public class Event
        {
            public Guid AggregateId { get; internal set; }
            public string Type { get; internal set; }
            public string Description { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }

            public Guid ContingentId { get; internal set; }

            public string Name { get; internal set; }
        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }

            public Guid ContingentId { get; internal set; }

            public string Name { get; internal set; }
        }

        public List<ContingentEventHistoryQueries.Event> GetEvents(string province)
        {
            return Read<Contingent>(x => x.Province.Equals(province)).FirstOrDefault().Events;
        }

        public void Handle(ContingentCreated e)
        {
            Create(new Contingent(e.Id)
            {
                Province = e.Province,
                Events = new List<Event>
                {
                    new Event
                    {
                        AggregateId = e.Id,
                        Type = e.GetType().Name,
                        Description = e.Province
                    }
                }
            });
        }

        public void Handle(TeamCreated e)
        {
            Create(new Team(e.TeamId)
            {
                ContingentId = e.Id,
                Name = e.Name
            });

            Update<Contingent>(e.Id, x =>
            {
                x.Events.Add(new Event
                {
                    AggregateId = e.TeamId,
                    Type = e.GetType().Name,
                    Description = e.Name
                });
            });
        }

        public void Handle(TeamRemoved e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();

            Update<Contingent>(e.Id, x =>
            {
                x.Events.Add(new Event
                {
                    AggregateId = e.TeamId,
                    Type = e.GetType().Name,
                    Description = team.Name
                });
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id)
            {
                Name = e.Name
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = Read<Participant>(x => x.Id == e.Id).FirstOrDefault();
            if (participant == null)
                return;

            Update<Contingent>(participant.ContingentId, x =>
            {
                x.Events.Add(new Event
                {
                    AggregateId = e.Id,
                    Type = e.GetType().Name,
                    Description = participant.Name + " to " + e.Name
                });
            });

            Update<Participant>(e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            if (team == null)
                return;

            Update<Participant>(e.Id, x => x.ContingentId = team.ContingentId);

            Update<Contingent>(team.ContingentId, x =>
            {
                x.Events.Add(new Event
                {
                    AggregateId = e.TeamId,
                    Type = e.GetType().Name,
                    Description = e.Name + " to " + team.Name
                });
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            if (team == null)
                return;

            Update<Participant>(e.Id, x => x.ContingentId = team.ContingentId);

            Update<Contingent>(team.ContingentId, x =>
            {
                x.Events.Add(new Event
                {
                    AggregateId = e.TeamId,
                    Type = e.GetType().Name,
                    Description = e.Name + " to " + team.Name
                });
            });
        }
    }
}
