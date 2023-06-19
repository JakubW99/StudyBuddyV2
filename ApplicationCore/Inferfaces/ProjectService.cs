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
      
        //private IGenericRepository<ProgrammingLanguage, int> _languageRepository;
        public ProjectService(IGenericRepository<Project,int> repository)
        { 
            _repository = repository;
         
        }
        public Project AddProject(Project project)
        {
            return _repository.Add(project);
        }

        public void DeleteProject(int id)
        {
            _repository.RemoveById(id);
        }

        public IEnumerable<Project?> FindAllProjects()
        {
            return _repository.FindAll().ToList();
        }

        public Project? FindProjectById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
