using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;

namespace API.Dto
{
    public class ProjectDto
    {
        public string Topic { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<ProgrammingLanguageDto> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
