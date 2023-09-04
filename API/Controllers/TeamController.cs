using API.Dto;
using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Azure.Identity;
using Infrastructure;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamService _service;
        private StudyBuddyDbContext _context;
        public TeamController(ITeamService service, StudyBuddyDbContext context) 
        {
            _service = service;
            _context = context;
        }
        //[Authorize]
        [HttpGet]
        public IEnumerable<Team?> GetAllTeams()
        {
            return _service.FindAllTeams();
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public Team GetTeamById(int id)
        {
            return _service.FindTeamById(id);
        }
        //[Authorize]
        [HttpPost]
        public ActionResult Post(TeamDto teamDto)
        {

            var team = new Team()
            {
                Name = teamDto.Name,
                LeaderId = teamDto.LeaderId,
                Members = new List<Member>()
            };
            _service.AddTeam(team);
            return Created("", team);
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteTeam(int id) 
        {
            _service.DeleteTeam(id);
        }
        [Authorize]
        [HttpDelete]
        [Route("{teamId}/{userId}")]
        public ActionResult DeleteUserFromTeam([FromRoute] int teamId, [FromRoute] int userId) 
        {
            _service.DeleteMemberFromTeam(userId, teamId);
            return Ok();
        }
        //[Authorize]
        [HttpPost]
        [Route("{teamId}/{userId}")]
        public ActionResult AddUserToTeam([FromRoute] int teamId, [FromRoute] int userId)
        {
            _service.AddMemberToTeam(userId, teamId);
            return Ok();
        }
    }
}
