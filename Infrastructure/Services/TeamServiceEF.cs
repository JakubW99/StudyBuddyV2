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
    internal class TeamServiceEF : ITeamService
    {

        private StudyBuddyDbContext _context;
        public TeamServiceEF(StudyBuddyDbContext context)
        {
            _context = context;
        }
        public Team AddTeam(Team team)
        {
           _context.Add(Mappers.Mapper.FromTeamToEntity(team));
            return team;
        }

        public void DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);
            if (team != null)
                _context.Remove(team);
        }

        public IEnumerable<Team?> FindAllTeams()
        {
            return _context.Teams.AsNoTracking()
                 .Include(m => m.Members)
                 .Include(l => l.Leader)
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
