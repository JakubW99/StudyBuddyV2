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
    public class TeamServiceEF : ITeamService
    {

        private StudyBuddyDbContext _context;
        public TeamServiceEF(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public void AddMemberToTeam(int userId, int teamId)
        {
            var member = new MemberEntity() {Id = 0, UserId = userId };
            //var team = _context.Teams.Where(x => x.Id == teamId).FirstOrDefault();
            _context.Members.Add(member);
            _context.SaveChanges();
           
             //_context.Entry(team).State = EntityState.Modified;
            
           
        }

        public Team AddTeam(Team team)
        {
            var teamEntity = new TeamEntity()
            {
                Id = team.Id,
                Name = team.Name,
                LeaderId = team.LeaderId,
                Members = team.Members.Select(m => new MemberEntity() { Id = m.Id, UserId = m.UserId}).ToList()
            };
            _context.Teams.Add(teamEntity);
            _context.SaveChanges();
            return team;
        }
        public Team UpdateTeam(Team team, int id)
        {
            var findTeam= _context.Teams.FirstOrDefault(m => m.Id == id);

       
            findTeam.Name = team.Name;
            findTeam.LeaderId = team.LeaderId;
            findTeam.Members = team.Members.Select(m => new MemberEntity() { Id = m.Id, UserId = m.UserId }).ToList();
           
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
