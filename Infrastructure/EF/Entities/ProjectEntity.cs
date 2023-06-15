using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    internal class ProjectEntity
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public TeamEntity Team { get; set; }

        public ISet<ProgrammingLanguageEntity> Languages { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
