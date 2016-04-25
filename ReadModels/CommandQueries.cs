using Edument.CQRS;
using Events.Participant;
using Events.Scores;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class CommandQueries : 
        IReadModel,
        ICommandQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantQualifyingPositionChanged>,
        ISubscribeTo<ParticipantReplacedWithAlternate>,
        ISubscribeTo<MatchCreated>
    {
        public List<Tournament> Tournaments { get; set; }
        public Dictionary<Guid, Participant> Participants { get; set; }
        public List<Match> Matches { get; set; }

        public class Tournament
        {
            public Guid Id { get; set; }
            public string Year { get; set; }
        }

        public class Participant
        {
            public Guid Id { get; set; }
            public Guid ContingentId { get; set; }
            public Guid TeamId { get; set; }
            public string Name { get; set; }
            public int Average { get; set; }
            public string Gender { get; set; }
            public int QualifyingPosition { get; set; }
            public bool IsReplaced { get; set; }
        }

        public class Match
        {
            public Guid Id { get; set; }
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

        public CommandQueries()
        {
            Reset();
        }

        public void Reset()
        {
            Tournaments = new List<Tournament>();
            Participants = new Dictionary<Guid, Participant>();
            Matches = new List<Match>();
        }

        public void Save()
        {
            ReadModelPersister.Save(this);
        }

        public List<Tournament> GetTournaments()
        {
            return Tournaments;
        }

        public Participant GetParticipant(Guid id)
        {
            return Participants[id];
        }

        public List<Participant> GetTeamParticipants(Guid teamId)
        {
            return Participants.Where(x => x.Value.TeamId == teamId)
                .Select(x => x.Value)
                .ToList();
        }

        public Match GetMatch(string year, string division, int game, string slot)
        {
            return Matches.FirstOrDefault(x =>
                x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)
                && x.Division.Equals(division, StringComparison.OrdinalIgnoreCase)
                && x.Number == game
                && x.Slot.Equals(slot, StringComparison.OrdinalIgnoreCase));
        }
        
        public void Handle(TournamentCreated e)
        {
            Tournaments.Add(new Tournament{
                Id = e.Id,
                Year = e.Year
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Participants.Add(e.Id, new Participant{
                Id = e.Id,
                Name = e.Name,
                Gender = e.Gender
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Participants[e.Id].Gender = e.Gender;
        }

        public void Handle(ParticipantRenamed e)
        {
            Participants[e.Id].Name = e.Name;
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Participants[e.Id].Average = e.Average;
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            Participants[e.Id].TeamId = e.TeamId;
        }

        public void Handle(ParticipantQualifyingPositionChanged e)
        {
            Participants[e.Id].QualifyingPosition = e.QualifyingPosition;
        }

        public void Handle(ParticipantReplacedWithAlternate e)
        {
            Participants[e.Id].IsReplaced = true;
        }

        public void Handle(MatchCreated e)
        {
            Matches.Add(new Match
            {
                Id = e.Id,
                Division = e.Division,
                Year = e.Year ?? "2014",
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
