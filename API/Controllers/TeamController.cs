using API.Dto;
using ApplicationCore.Inferfaces;
using ApplicationCore.Models.Project;
using Azure;
using Azure.Identity;
using Infrastructure;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
      
        [HttpGet]
        public IEnumerable<Team?> GetAllTeams()
        {
            return _service.FindAllTeams();
        }
      
        [HttpGet]
        [Route("{id}")]
        public Team GetTeamById(int id)
        {
            return _service.FindTeamById(id);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Post(TeamDto teamDto)
        {
            var members = teamDto.Members.Select(x => new Member(x.UserId, "MEMBER")).ToList();
            members.Add(new Member(teamDto.LeaderId, "LEADER"));

            var team = new Team()
            {
                Name = teamDto.Name,
                LeaderId = teamDto.LeaderId,
                Members = members,
                IsOpenTeam = teamDto.IsOpenTeam

            };

            _service.AddTeam(team);
            return Created("", team);
        }
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(TeamDto teamDto, [FromRoute] int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findTeam = _service.FindTeamById(id);
            var team = new Team()
            {
                Name = teamDto.Name,
                LeaderId = teamDto.LeaderId,
                Members = teamDto.Members.Select(x => new Member(x.UserId,"MEMBER")),
                IsOpenTeam = teamDto.IsOpenTeam
            };
          
            
            if (findTeam.IsOpenTeam == false)
            {
                if (Convert.ToInt32(user) != teamDto.LeaderId)
                {
                    return Unauthorized("Not a team leader "); // or Forbidden()
                }
                else return Unauthorized("Not a team leader and it is not open team");
            }
            _service.UpdateTeam(team, id);
            return Created("", team);
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteTeam(int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findTeam = _service.FindTeamById(id);
            if (Convert.ToInt32(user) != findTeam.LeaderId)
            {
                return Unauthorized("Not a team leader "); // or Forbidden()
            }
            _service.DeleteTeam(id);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("/User/{teamId}/{userId}")]
        public ActionResult DeleteUserFromTeam([FromRoute] int teamId, [FromRoute] int userId)
        {
            _service.DeleteMemberFromTeam(userId, teamId);
            return Ok();
        }
        [Authorize]
        [HttpPost]
        [Route("/User/{teamId}/{userId}/{role}")]
        public async Task<ActionResult> AddMemberToTeam([FromRoute] int teamId, [FromRoute] int userId, [FromRoute] string role)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = _service.FindTeamById(teamId);
            if (team.IsOpenTeam == false)
            {
                if (Convert.ToInt32(user) != team.LeaderId)
                {
                    return Unauthorized("Not a team leader "); // or Forbidden()
                }
                else return Unauthorized("Not a team leader and it is not open team");
            }
            _service.AddMemberToTeam(teamId,userId, role);
            return Ok();
        }
     

    }
}
