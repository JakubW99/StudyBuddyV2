using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.Project.Project;

namespace Infrastructure.EF.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public TeamEntity Team { get; set; }

        public IEnumerable<ProgrammingLanguageEntity> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Difficulty { get; set; }
        public string? RepositoryLink { get; set; }
        public bool IsFinished { get; set; } = false;
        public ProjectEntity()
        {

        }
        public ProjectEntity(int id, string topic, TeamEntity team, IEnumerable<ProgrammingLanguageEntity> languages, DateTime plannedEndDate, DateTime deadlineDate, string  difficulty, string? repositoryLink, bool isFinished, string description)
        {
            Id = id;
            Topic = topic;
            Team = team;
            Languages = languages;
            PlannedEndDate = plannedEndDate;
            DeadlineDate = deadlineDate;
            Difficulty = difficulty;
            RepositoryLink = repositoryLink;
            IsFinished = isFinished;
            Description = description;
        }
    }
}
