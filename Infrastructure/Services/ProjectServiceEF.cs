using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
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
                LeaderId= project.Team.LeaderId,
                Members = project.Team.Members.Select(m => new MemberEntity() { Id = m.Id , UserId= m.UserId}).ToList()
              
            };
            _context.Projects.Add(entity);
            _context.SaveChanges();
            return project;
        }

        public void DeleteProject(int id)
        {
           var projectToDelete =  _context.Projects.Find(id);
            if(projectToDelete != null)
            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
        }

        public IEnumerable<Project?> FindAllProjects()
        {
            return _context.Projects
                .AsNoTracking()
                .Include(t => t.Team)
                .ThenInclude(x=>x.Members)
                .Include(p => p.Languages)
                .Select(x=>Mapper.FromEntityToProject(x))
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
        public Project UpdateProject(Project project, int id)
        {
            var findProject = _context.Projects
               .AsNoTracking()
               .Include(t => t.Team)
               .Include(p => p.Languages)
               .FirstOrDefault(e => e.Id == id);
            if(findProject != null)
            {
               var prjct = Mappers.Mapper.FromEntityToProject(findProject);
                prjct.DeadlineDate = project.DeadlineDate;
                prjct.Topic = project.Topic;
                prjct.Languages = project.Languages;
                prjct.PlannedEndDate = project.PlannedEndDate;
                prjct.Team = project.Team;
                _context.SaveChanges();
                return prjct;
            }
            return null;
            
        }

     
    }
}
