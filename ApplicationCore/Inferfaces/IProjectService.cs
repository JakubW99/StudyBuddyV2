using ApplicationCore.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Inferfaces
{
    public interface IProjectService
    {
        IEnumerable<Project?> FindAllTeams();
        Project? FindById(int id);
        Project AddProject(Project project);
    }
}
