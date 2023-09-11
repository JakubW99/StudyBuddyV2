using ApplicationCore.Inferfaces;
using ApplicationCore.Models;
using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            var team = _context.Teams.Include(x=>x.Members).FirstOrDefault(x => x.Id == teamId);
            if (team == null || team.Members == null) return;

            var memberToRemove = team.Members.FirstOrDefault(m => m.UserId == userId);
            if (memberToRemove != null)
            {
                _context.Members.Remove(memberToRemove);

                _context.SaveChanges();
            }
        }

        public void DeleteTeam(int id)
        {
            var members = _context.Members.Where(m => m.TeamEntityId == id).ToList();
            _context.RemoveRange(members);
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

        public void AddMemberToTeam(int id)
        {
            var team = _context.Teams.Include(x => x.Members).FirstOrDefault(x => x.Id == id);
            if (team == null || team.Members == null) return;

            var memberAdd = new MemberEntity
            {
                UserId = id,
                TeamEntityId = team.Id,
            };
            team.Members.Add(memberAdd);
           _context.SaveChanges();
        }
        public async Task AddRoleToUserAsync(int userId, string roleName)
        {
            var roleExists = await _context.Roles.AnyAsync(r => r.Name == "LEADER");
            if (!roleExists)
            {

                _context.Roles.Add(new UserRole { Name = "LEADER", NormalizedName = "LEADER" });
                await _context.SaveChangesAsync();
            }
            var userManager = _serviceProvider.GetRequiredService<UserManager<UserEntity>>();
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, roleName);
                await _context.SaveChangesAsync();
            }
        }
        public async Task InitializeRolesAsync()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            if (!await roleManager.RoleExistsAsync("LEADER"))
            {
                await roleManager.CreateAsync(new UserRole { Name = "LEADER" });
            }

            if (!await roleManager.RoleExistsAsync("MEMBER"))
            {
                await roleManager.CreateAsync(new UserRole { Name = "MEMBER" });
            }
        }

        private readonly IServiceProvider _serviceProvider;

        public TeamServiceEF(StudyBuddyDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
    }
}
