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

     
        public void AddMemberToTeam(int userId, int teamId)
        {
            var team = _repository.FindById(teamId);
                
        }

        public Team AddTeam(Team team)
        {
            return _repository.Add(team);
        }

       

        public void DeleteMemberFromTeam(int userId, int teamId)
        {
            var team = _repository.FindById(teamId);
            var userToDelete = team.Members.ElementAt(userId);
            if (userToDelete != null) 
            {
              //
            }
        }

        public void DeleteTeam(int id)
        {
           _repository.RemoveById(id);
        }

        public IEnumerable<Team?> FindAllTeams()
        {
            return _repository.FindAll();
        }

        public Team? FindTeamById(int id)
        {
            var team = _repository.FindById(id);
            if (team != null)
                return team;
            else return null;
        }

        public Team? UpdateTeam(Team Team,int id)
        {
            var findTeam = _repository.FindById(id);
            findTeam.Id = Team.Id;
            findTeam.Name = Team.Name;
            findTeam.Members = Team.Members;
            findTeam.LeaderId = Team.LeaderId;
            return findTeam;
        }
    }
}
