using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Inferfaces
{
    internal class ProjectService : IProjectService
    {
        private IGenericRepository<Project,int> _repository;
        public ProjectService(IGenericRepository<Project,int> repository)
        { 
            _repository = repository;
        }
        public Project AddProject(Project project)
        {
            return _repository.Add(project);
        }

        public IEnumerable<Project?> FindAllTeams()
        {
            return _repository.FindAll();
        }

        public Project? FindById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
