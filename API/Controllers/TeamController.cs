using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamService _service;
        public TeamController(ITeamService service) 
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            return _service.FindAllTeams();
        }
        [HttpGet]
        [Route("{id}")]
        public Team GetTeamById(int id)
        {
            return _service.FindTeamById(id);
        }
        [HttpPost]
        public void Post(Team team) 
        {
            _service.AddTeam(team);
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteTeam(int id) 
        {
            _service.DeleteTeam(id);
        }
    }
}
