using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ReservationQueries : AzureReadModel,
        IReservationQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToRoom>,
        ISubscribeTo<ParticipantRemovedFromRoom>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantAssignedToContingent>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>
    {
        public ReservationQueries(string readModelFilePath)
        {

        }

        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public int RoomNumber { get; internal set; }
        }

        private class TSParticipant : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Name { get; set; }
            public string Province { get; set; }
            public int RoomNumber { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Province { get; internal set; }
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
        }

        public List<ReservationQueries.Participant> GetParticipants(string province)
        {
            return null;
            //TODO: return Read<Participant>(x => x.Province == province).ToList();
        }

        public void Handle(ParticipantCreated e)
        {
            Create(e.Id, e.Id, new TSParticipant
            {
                Name = e.Name
            });
        }

        public void Handle(ContingentCreated e)
        {
            Create(e.Id, e.Id, new TSContingent
            {
                Province = e.Province
            });
        }

        public void Handle(TeamCreated e)
        {
            Create(e.Id, e.TeamId, new TSTeam());
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Read<TSContingent>(e.ContingentId, e.ContingentId);
            Update<TSParticipant>(e.Id, e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Read<TSTeam>(e.TeamId);
            var contingent = Read<TSContingent>(team.ContingentId, team.ContingentId);
            Update<TSParticipant>(e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Read<TSTeam>(e.TeamId);
            var contingent = Read<TSContingent>(team.ContingentId, team.ContingentId);
            Update<TSParticipant>(e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<TSParticipant>(e.Id, e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantAssignedToRoom e)
        {
            Update<TSParticipant>(e.Id, e.Id, x => x.RoomNumber = e.RoomNumber);
        }

        public void Handle(ParticipantRemovedFromRoom e)
        {
            Update<TSParticipant>(e.Id, e.Id, x => x.RoomNumber = 0);
        }
    }
}
