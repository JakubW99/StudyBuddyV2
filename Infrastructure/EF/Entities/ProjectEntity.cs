using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public TeamEntity Team { get; set; }

        public IEnumerable<ProgrammingLanguageEntity> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public ProjectEntity()
        {

        }
        public ProjectEntity(int id, string topic, TeamEntity team, IEnumerable<ProgrammingLanguageEntity> languages, DateTime plannedEndDate, DateTime deadlineDate)
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
