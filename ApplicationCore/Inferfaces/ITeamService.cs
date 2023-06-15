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
        Team? FindById(int id);
        Team AddTeam(Team team);

    }
}
