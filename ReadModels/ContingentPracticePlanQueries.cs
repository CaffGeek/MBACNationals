using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentPracticePlanQueries : AReadModel,
        IContingentPracticePlanQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<TeamPracticeRescheduled>
    {
        public ContingentPracticePlanQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class ContingentPracticePlan : AEntity
        {
            public ContingentPracticePlan(Guid id) : base(id) { }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string PracticeLocation { get; set; }
            public int PracticeTime { get; set; }
        }

        public ContingentPracticePlan GetSchedule(string province)
        {
            return Read<ContingentPracticePlan>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new ContingentPracticePlan(e.Id)
            {
                Province = e.Province,
                Teams = new List<Team>()
            });
        }

        public void Handle(TeamCreated e)
        {
            Update<ContingentPracticePlan>(e.Id, x =>
            {
                var team = new Team(e.TeamId)
                {
                    Name = e.Name
                };
                x.Teams.Add(team);
            });
        }

        public void Handle(TeamRemoved e)
        {
            Update<ContingentPracticePlan>(e.Id, (x, odb) =>
            {
                var team = Read<Team>(t => t.Id.Equals(e.TeamId), odb).FirstOrDefault();
                if (team == null)
                    return;

                x.Teams.Remove(team);
            });
        }

        public void Handle(TeamPracticeRescheduled e)
        {
            Update<ContingentPracticePlan>(e.Id, (x, odb) =>
            {
                var team = Read<Team>(t => t.Id.Equals(e.TeamId), odb).FirstOrDefault();
                if (team == null)
                    return;

                team.PracticeLocation = e.PracticeLocation;
                team.PracticeTime = e.PracticeTime;
            });
        }
    }
}