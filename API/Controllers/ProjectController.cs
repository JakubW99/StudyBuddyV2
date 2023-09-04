using API.Dto;
using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Infrastructure;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private StudyBuddyDbContext _context;
        private IProjectService _service;
        public ProjectController(StudyBuddyDbContext context, IProjectService service)
        {
            _context = context;
            _service = service;
        }
      //  [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            return Ok(_service.FindAllProjects());
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public Project GetProjectById(int id)
        {
            return _service.FindProjectById(id);
        }
       // [Authorize]
        [HttpPost]
        public void AddProject([FromBody] ProjectDto project) 
        {
            var entity = new ProjectEntity();
            var team = _context.Teams.Find(project.TeamId);
            entity.Id = 0;
           entity.Languages = project.Languages.Select(x=>Mapper.FromDtoToLanguage(x)).ToList();
            entity.PlannedEndDate = project.PlannedEndDate;
            entity.DeadlineDate = project.DeadlineDate;
            entity.Team = team;
            entity.Topic = project.Topic;
            _context.Projects.Add(entity);
            _context.SaveChanges();
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public void DeleteProject(int id) 
        {
            _service.DeleteProject(id);
        }
    }
}
