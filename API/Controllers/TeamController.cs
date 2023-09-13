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
            var members = teamDto.Members.Select(x => new Member(x.UserId, "MEMBER")).ToList();
            members.Add(new Member(teamDto.LeaderId, "LEADER"));

            var team = new Team()
            {
                Name = teamDto.Name,
                LeaderId = teamDto.LeaderId,
                Members = members


            };

            _service.AddTeam(team);
            return Created("", team);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(TeamDto teamDto, [FromRoute] int id)
        {
            var team = new Team()
            {
                Name = teamDto.Name,
                LeaderId = teamDto.LeaderId,
                Members = teamDto.Members.Select(x => new Member(x.UserId,"MEMBER"))
            };
            _service.UpdateTeam(team, id);
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

        [HttpPost]
        [Route("{teamId}/{userId}/{role}")]
        public ActionResult AddMemberToTeam([FromRoute] int teamId, [FromRoute] int userId, [FromRoute] string role)
        {
            _service.AddMemberToTeam(teamId,userId, role);
            return Ok();
        }

        [HttpPost]
        [Route("AddRole/{userId}/{roleName}")]
        public async Task<ActionResult> AddRoleToUser([FromRoute] int userId, [FromRoute] string roleName)
        {
            await _service.AddRoleToUserAsync(userId, roleName);
            return Ok();
        }

    }
}
