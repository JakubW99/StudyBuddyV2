using ApplicationCore.Models;
using ApplicationCore.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Inferfaces
{
    public interface ITeamService
    {
        IEnumerable<Team?> FindAllTeams();
        Team? FindTeamById(int id);
        Team AddTeam(Team team);
        void DeleteTeam(int id);
        void AddMemberToTeam(int userId);
        void DeleteMemberFromTeam(int userId, int teamId);
        public Team? UpdateTeam(Team Team, int id);
    }
}
