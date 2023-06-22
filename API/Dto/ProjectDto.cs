using ApplicationCore.Models.Project;

namespace API.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<ProgrammingLanguage> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
