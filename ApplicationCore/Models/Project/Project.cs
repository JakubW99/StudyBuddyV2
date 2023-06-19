using ApplicationCore.Commons.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Project
{
    public class Project : IIdentity<int>
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public Team Team { get; set; }

        public IEnumerable<ProgrammingLanguage> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public Project(int id, string topic, Team team, IEnumerable<ProgrammingLanguage> languages, DateTime plannedEndDate, DateTime deadlineDate)
        {
            Id = id;
            Topic = topic;
            Team = team;
            Languages = languages;
            PlannedEndDate = plannedEndDate;
            DeadlineDate = deadlineDate;
        }
    }
}
