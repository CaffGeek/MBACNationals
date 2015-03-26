using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentPracticePlanQueries : AzureReadModel,
        IContingentPracticePlanQueries,
        ISubscribeTo<ContingentCreated>,
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

        private class TSContingentPracticePlan : Entity
        {
            public string Province { get; set; }
        }

        private class TSTeam : Entity
        {
            public string Name { get; set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
        }

        public ContingentPracticePlan GetSchedule(string province)
        {
            var contingentPracticePlan = Query<TSContingentPracticePlan>(x => x.Province.Equals(province, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var teams = Query<TSTeam>(x => x.PartitionKey == contingentPracticePlan.PartitionKey)
                .Select(x => new Team {
                    Id = Guid.Parse(x.PartitionKey),
                    Name = x.Name,
                    PracticeLocation = x.PracticeLocation,
                    PracticeTime = x.PracticeTime
                })
                .ToList();

            return new ContingentPracticePlan
            {
                Id = Guid.Parse(contingentPracticePlan.PartitionKey),
                Province = contingentPracticePlan.Province,
                Teams = teams,
            };
            //TODO return Read<ContingentPracticePlan>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(e.Id, e.Id, new TSContingentPracticePlan
            {
                Province = e.Province
            });
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
    }
}