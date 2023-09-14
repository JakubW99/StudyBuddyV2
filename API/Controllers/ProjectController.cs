using API.Dto;
using ApplicationCore.Inferfaces;
using ApplicationCore.Models;
using ApplicationCore.Models.Project;
using Infrastructure;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            return Ok(_service.FindAllProjects());
        }
      
        [HttpGet]
        [Route("{id}")]
        public Project GetProjectById(int id)
        {
            return _service.FindProjectById(id);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddProject([FromBody] ProjectDto project) 
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = _context.Teams.Where(x => x.Id == project.TeamId).FirstOrDefault();
            
                if (Convert.ToInt32(user) != team.LeaderId)
                {
                    return Unauthorized("Not a team leader "); // or Forbidden()
                }
               
            var entity = new ProjectEntity();
           
            entity.Id = 0;
           entity.Languages = project.Languages.Select(x=>Mapper.FromDtoToLanguage(x)).ToList();
            entity.PlannedEndDate = project.PlannedEndDate;
            entity.DeadlineDate = project.DeadlineDate;
            entity.Team = team;
            entity.Topic = project.Topic;
            entity.Difficulty = project.Difficulty;
            entity.RepositoryLink = project.RepositoryLink;
            entity.IsFinished = false;
            entity.Description = project.Description;

            _context.Projects.Add(entity);
            _context.SaveChanges();
            return Created("", project);
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProject(int id) 
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var project = _context.Projects.Include(x=> x.Team).Where(x => x.Id == id).FirstOrDefault();

            if (Convert.ToInt32(user) != project.Team.Id)
            {
                return Unauthorized("Not a team leader "); // or Forbidden()
            }
            _service.DeleteProject(id);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateProject(Project project,[FromRoute] int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = _context.Teams.Where(x => x.Id == project.Team.Id).FirstOrDefault();

            if (Convert.ToInt32(user) != team.LeaderId)
            {
                return Unauthorized("Not a team leader "); // or Forbidden()
            }
            _service.UpdateProject(project, id);
            return Created("", project);
        }
    }
}
