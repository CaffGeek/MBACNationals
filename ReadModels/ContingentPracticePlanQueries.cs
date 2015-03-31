using Edument.CQRS;
using Events.Contingent;
using Events.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentPracticePlanQueries : AzureReadModel,
        IContingentPracticePlanQueries,
        ISubscribeTo<TournamentCreated>,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<ContingentAssignedToTournament>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<TeamPracticeRescheduled>
    {
        public ContingentPracticePlanQueries(string readModelFilePath)
        {

        }

        public class ContingentPracticePlan
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
        }

        public class Team
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
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
            public string Name { get; set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
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

        public ContingentPracticePlan GetSchedule(string year, string province)
        {
            var tournament = Query<TSTournament>(x => x.Year.Equals(year, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            var contingent = Query<TSContingent>(x =>
                x.TournamentId == tournament.Id
                && x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var teams = Query<TSTeam>(x => x.ContingentId == contingent.Id)
                .Select(x => new Team {
                    Id = x.Id,
                    Name = x.Name,
                    PracticeLocation = x.PracticeLocation,
                    PracticeTime = x.PracticeTime
                })
                .ToList();

            return new ContingentPracticePlan
            {
                Id = contingent.Id,
                Province = contingent.Province,
                Teams = teams,
            };
        }

        public void Handle(TournamentCreated e)
        {
            Create(e.Id, e.Id, new TSTournament { Year = e.Year, Id = e.Id });

            //HACK: Track current tournament
            Create(Guid.Empty, Guid.Empty, new TSTournament { Year = e.Year, Id = e.Id });
        }

        public void Handle(ContingentCreated e)
        {
            if (string.IsNullOrWhiteSpace(e.Province))
                return;

            var tournamentId = GetCurrentTournamentId();

            Create(tournamentId, e.Id, new TSContingent
            {
                Province = e.Province
            });
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
            Create(e.Id, e.TeamId, new TSTeam
            {
                Name = e.Name
            });
        }

        public void Handle(TeamRemoved e)
        {
            Delete<TSTeam>(e.Id, e.TeamId);
        }

        public void Handle(TeamPracticeRescheduled e)
        {
            Update<TSTeam>(e.Id, e.TeamId, team =>
            {
                team.PracticeLocation = e.PracticeLocation;
                team.PracticeTime = e.PracticeTime;
            });
        }

        private Guid GetCurrentTournamentId()
        {
            var tournament = Read<TSTournament>(Guid.Empty, Guid.Empty)
                ?? new TSTournament { Id = Guid.Empty };
            return tournament.Id;
        }
    }
}