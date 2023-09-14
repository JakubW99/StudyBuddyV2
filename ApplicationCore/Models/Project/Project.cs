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
        public string Description { get; set; }

        public Team Team { get; set; }

        public IEnumerable<ProgrammingLanguage> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Difficulty { get; set; }
        public string RepositoryLink { get; set; }
        public bool IsFinished { get; set; } = false;
        
        public Project(int id, string topic, Team team, IEnumerable<ProgrammingLanguage> languages, DateTime plannedEndDate, DateTime deadlineDate,string difficulty, string repositoryLink, bool isFinished, string description )
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
