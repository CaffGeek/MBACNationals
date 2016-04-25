using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ReservationQueries : 
        IReadModel,
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
        public List<Participant> Participants { get; set; }
        public List<Contingent> Contingents { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public Dictionary<Guid, Guid> TeamContingents { get; set; }

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

        public class Participant
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public string Name { get; set; }
            public string Province { get; set; }
            public int RoomNumber { get; set; }
        }

        public ReservationQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Participants = new List<Participant>();
            Contingents = new List<Contingent>();
            Tournaments = new List<Tournament>();
            TeamContingents = new Dictionary<Guid, Guid>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public List<ReservationQueries.Participant> GetParticipants(string year, string province)
        {
            var contingents = Contingents.Where(x => x.Year == year);

            var participants = Participants
                .Where(x => contingents.Any(c => c.Id == x.ContingentId) && x.Province == province)
                .ToList();

            return participants;
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(new Participant{
                Id = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ContingentCreated e)
        {
            Contingents.Add(new Contingent
            {
                Id = e.Id,
                Province = e.Province
            });
        }

        public void Handle(TournamentCreated e)
        {
            Tournaments.Add(new Tournament
            {
                Id = e.Id,
                Year = e.Year
            });
        }

        public void Handle(ContingentAssignedToTournament e)
        {
            var tournament = Tournaments.SingleOrDefault(x => x.Id == e.TournamentId)
                ?? new Tournament { Id = e.TournamentId, Year = "2014" };

            var contingent = Contingents.Single(x => x.Id == e.Id);
            contingent.TournamentId = tournament.Id;
            contingent.Year = tournament.Year;
        }

        public void Handle(TeamCreated e)
        {
            TeamContingents.Add(e.TeamId, e.Id);
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            var contingent = Contingents.Single(x => x.Id == e.ContingentId);
            participant.ContingentId = contingent.Id;
            participant.Province = contingent.Province;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var contingentId = TeamContingents[e.TeamId];
            var contingent = Contingents.Single(x => x.Id == contingentId);
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.ContingentId = contingent.Id;
            participant.Province = contingent.Province;
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var contingentId = TeamContingents[e.TeamId];
            var contingent = Contingents.Single(x => x.Id == contingentId);
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.ContingentId = contingent.Id;
            participant.Province = contingent.Province;
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.Name = e.Name;
        }

        public void Handle(ParticipantAssignedToRoom e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.RoomNumber = e.RoomNumber;
        }

        public void Handle(ParticipantRemovedFromRoom e)
        {
            var participant = Participants.Single(x => x.Id == e.Id);
            participant.RoomNumber = 0;
        }
    }
}
