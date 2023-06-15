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
    }
}
