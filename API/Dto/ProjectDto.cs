using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;

namespace API.Dto
{
    public class ProjectDto
    {
        public string Topic { get; set; }

        public int TeamId { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProgrammingLanguageDto> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Difficulty { get; set; }
        public string RepositoryLink { get; set; }
        public bool IsFinished { get; set; } = false;
       
       
    }
}
