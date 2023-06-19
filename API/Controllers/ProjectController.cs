using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Infrastructure;
using Infrastructure.Services;
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
        [HttpPost]
        public void AddProject([FromBody] Project project) 
        {
            _service.AddProject(project);
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteProject(int id) 
        {
            _service.DeleteProject(id);
        }
    }
}
