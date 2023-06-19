using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProjectServiceEF : IProjectService
    {
      
        private StudyBuddyDbContext _context;
        public ProjectServiceEF(StudyBuddyDbContext context)
        {
            _context = context;
        }   
        public Project AddProject(Project project)
        {
            var entity = new ProjectEntity();
            entity.Id = project.Id;
            entity.PlannedEndDate = project.PlannedEndDate;
            entity.DeadlineDate = project.DeadlineDate;
            entity.Team = new TeamEntity() {
                Id = project.Team.Id,
                Name = project.Team.Name,
                Leader = new UserEntity()
                {
                    UserName = project.Team.Leader.Username,
                    Id = project.Team.Leader.Id,
                },
               Members = new List<UserEntity>()
              
            };
            _context.Projects.Add(entity);
            return project;
        }

        public void DeleteProject(int id)
        {
           var projectToDelete =  _context.Projects.Find(id);
            if(projectToDelete != null)
            _context.Projects.Remove(projectToDelete);
        }

        public IEnumerable<Project?> FindAllProjects()
        {
            return _context.Projects
                .AsNoTracking()
                .Include(t => t.Team)
                .Include(p => p.Languages)
                .Select(Mappers.Mapper.FromEntityToProject)
                .ToList();
           
        }

     

        public Project? FindProjectById(int id)
        {
        var project = _context.Projects
                .AsNoTracking()
                .Include(t => t.Team)
                .Include(p => p.Languages)
                .FirstOrDefault(e => e.Id == id);
            return project is null ? null : Mappers.Mapper.FromEntityToProject(project);
        }

     
    }
}
