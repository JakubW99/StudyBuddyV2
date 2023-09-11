using ApplicationCore.Models.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Inferfaces
{
    public interface IProjectService
    {
        IEnumerable<Project?> FindAllProjects();
   
        Project? FindProjectById(int id);
        Project AddProject(Project project);
         void DeleteProject(int id);
         Project UpdateProject(Project project, int id);
      
    }
}
