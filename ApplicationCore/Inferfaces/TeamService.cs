using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Inferfaces
{
    public class TeamService : ITeamService
    {
        private IGenericRepository<Team,int> _repository;

        public TeamService(IGenericRepository<Team,int> repository)
        {
            _repository = repository;
        }
        public Team AddTeam(Team team)
        {
            return _repository.Add(team);
        }

        public IEnumerable<Team?> FindAllTeams()
        {
            return _repository.FindAll();
        }

        public Team? FindById(int id)
        {
          return _repository.FindById(id);
        }
    }
}
