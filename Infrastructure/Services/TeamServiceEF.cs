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
    public class TeamServiceEF : ITeamService
    {

        private StudyBuddyDbContext _context;
        public TeamServiceEF(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public void AddMemberToTeam(int userId, int teamId)
        {
            var member = new MemberEntity() {Id = 1, UserId = userId, TeamId = teamId };
            var teamMembers = _context.Teams.Where(x => x.Id == teamId);
           //_context.Member.Add(member);
            _context.SaveChangesAsync();
        }

        public Team AddTeam(Team team)
        {
            var teamEntity = new TeamEntity()
            {
                Id = team.Id,
                Name = team.Name,
                LeaderId = team.LeaderId,
                Members = team.Members.Select(m => new MemberEntity() { Id = m.Id, TeamId= m.TeamId, UserId = m.UserId}).ToList()
            };
            _context.Teams.Add(teamEntity);
            _context.SaveChanges();
            return team;
        }

        public void DeleteMemberFromTeam(int userId, int teamId)
        {
            
            var team = _context.Teams.FirstOrDefault(x => x.Id == teamId);
         var members = team.Members.ToList();
            members.RemoveAt(userId);
            _context.SaveChanges();

        }

        public void DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);
            if (team != null)
                _context.Remove(team);
            _context.SaveChanges();
        }

        public IEnumerable<Team?> FindAllTeams()
        {
            return _context.Teams.AsNoTracking()
                 .Include(m => m.Members)
               
                 .Select(Mappers.Mapper.FromEntityToTeam)
                 .ToList();
        }

        public Team? FindTeamById(int id)
        {
            var team = _context.Teams.Find(id);
            return Mappers.Mapper.FromEntityToTeam(team);
        }
    }
}
