﻿using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
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
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
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
            public Guid ContingentId { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
            public int RoomNumber { get; set; }
        }

        private class TSTournament : Entity
        {
            public string Year { get; set; }
            public Guid Id { get; set; }
        }

        private class TSContingent : Entity
        {
            public Guid Id
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); }
            }
            public Guid TournamentId
            {
                get { return Guid.Parse(PartitionKey); }
                internal set { PartitionKey = value.ToString(); }
            }
            public string Province { get; set; }
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

        public List<ReservationQueries.Participant> GetParticipants(string year, string province)
        {
            var tournament = Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            var contingent = Query<TSContingent>(x =>
                x.TournamentId == tournament.Id
                && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var participants = Query<TSParticipant>(x => x.ContingentId.Equals(contingent.Id))
                .Select(x => new Participant
                {
                    Id = x.Id,
                    Name = x.Name,
                    Province = x.Province,
                    RoomNumber = x.RoomNumber
                }).ToList();

            return participants;
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
            Create(Guid.Empty, e.Id, new TSContingent
            {
                Province = e.Province
            });
        }

        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament { Year = e.Year, Id = e.Id });

            //HACK: Track current tournament
            Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var contingent = Read<TSContingent>(Guid.Empty, e.Id);
            if (contingent != null)
            {
                Delete<TSContingent>(Guid.Empty, e.Id);
                contingent.TournamentId = e.TournamentId;
                Create(e.TournamentId, e.Id, contingent);
            }
        }

        public void Handle(TeamCreated e)
        {
            Create(e.Id, e.TeamId, new TSTeam());
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Read<TSContingent>(e.ContingentId);
            Update<TSParticipant>(e.Id, e.Id, x =>
            {
                x.Province = contingent.Province;
                x.ContingentId = contingent.Id;
            });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Read<TSTeam>(e.TeamId);
            var contingent = Read<TSContingent>(team.ContingentId);
            Update<TSParticipant>(e.Id, x =>
            {
                x.Province = contingent.Province;
                x.ContingentId = contingent.Id;
            });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Read<TSTeam>(e.TeamId);
            var contingent = Read<TSContingent>(team.ContingentId);
            Update<TSParticipant>(e.Id, x =>
            {
                x.Province = contingent.Province;
                x.ContingentId = contingent.Id;
            });
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
