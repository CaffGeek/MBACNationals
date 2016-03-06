using Edument.CQRS;
using Events.Participant;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class CommandQueries : BaseReadModel<CommandQueries>,
        ICommandQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<MatchCreated>
    {
        public class Tournament
        {
            public virtual Guid Id { get; set; }
            public virtual string Year { get; set; }
        }

        public class Participant
        {
            public virtual Guid Id { get; set; }
            public virtual Guid ContingentId { get; set; }
            public virtual Guid TeamId { get; set; }
            public virtual string Name { get; set; }
            public virtual int Average { get; set; }
            public virtual string Gender { get; set; }
        }

        public class Match
        {
            public virtual Guid Id { get; set; }
            public virtual string Year { get; set; }
            public virtual string Division { get; set; }
            public virtual int Number { get; set; }
            public virtual string Slot { get; set; }
            public virtual bool IsPOA { get; set; }
            public virtual int Lane { get; set; }
            public virtual string Centre { get; set; }
            public virtual string Away { get; set; }
            public virtual string AwayId { get; set; }
            public virtual string Home { get; set; }
            public virtual string HomeId { get; set; }
            
        }

        private class TSTournament : Entity
        {
            public virtual string Year { get; set; }
        }

        private class TSParticipant : Entity
        {
            public virtual Guid ContingentId { get; set; }
            public virtual Guid TeamId { get; set; }
            public virtual string Name { get; set; }
            public virtual int Average { get; set; }
            public virtual string Gender { get; set; }
        }

        private class TSMatch : Entity
        {
            public Guid MatchId
            {
                get { return Guid.Parse(RowKey); }
                internal set { RowKey = value.ToString(); PartitionKey = value.ToString(); }
            }
            public string Year { get; set; }
            public string Division { get; set; }
            public int Number { get; set; }
            public string Slot { get; set; }
            public bool IsPOA { get; set; }
            public int Lane { get; set; }
            public string Centre { get; set; }
            public string Away { get; set; }
            public string AwayId { get; set; }
            public string Home { get; set; }
            public string HomeId { get; set; }
        }

        public List<Tournament> GetTournaments()
        {
            return Storage.Query<TSTournament>()
                .Select(x => new Tournament
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year
                })
                .ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            var participant = Storage.Read<TSParticipant>(Guid.Empty, id);
            return new Participant 
            {
                Id = Guid.Parse(participant.RowKey),
                TeamId = participant.TeamId,
                ContingentId = participant.ContingentId,
                Name = participant.Name,
                Average = participant.Average,
                Gender = participant.Gender
            };
                
        }

        public Match GetMatch(string year, string division, int game, string slot)
        {
            return Storage.Query<TSMatch>(x =>
                x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)
                && x.Division.Equals(division, StringComparison.OrdinalIgnoreCase)
                && x.Number == game
                && x.Slot.Equals(slot, StringComparison.OrdinalIgnoreCase))
                .Select(x => new Match
                {
                    Id = Guid.Parse(x.RowKey),
                    Year = x.Year,
                    Division = x.Division,
                    Number = x.Number,
                    Slot = x.Slot,
                    Lane = x.Lane,
                    Centre = x.Centre,
                    IsPOA = x.IsPOA,
                    Away = x.Away,
                    AwayId = x.AwayId,
                    Home = x.Home,
                    HomeId = x.HomeId,
                })
                .FirstOrDefault();
        }
        
        public void Handle(TournamentCreated e)
        {
            Storage.Create(Guid.Empty, e.Id, new TSTournament
            {
                Year = e.Year
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Storage.Create(Guid.Empty, e.Id, new TSParticipant
            {
                Name = e.Name,
                Gender = e.Gender
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Storage.Update<TSParticipant>(Guid.Empty, e.Id, x => x.Gender = e.Gender);
        }

        public void Handle(ParticipantRenamed e)
        {
            Storage.Update<TSParticipant>(Guid.Empty, e.Id, x => x.Name = e.Name);
        }

        public void Handle(MatchCreated e)
        {
            Storage.Create(e.Id, e.Id, new TSMatch
            {
                Division = e.Division,
                Year = e.Year,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = e.Away,
                Home = e.Home,
                Lane = e.Lane,
                Centre = e.Centre.ToString(),
                Slot = e.Slot
            });
        }
    }
}
