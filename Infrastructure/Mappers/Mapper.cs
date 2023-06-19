using ApplicationCore.Models;
using ApplicationCore.Models.Project;
using Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class Mapper
    {
        public static TeamEntity FromTeamToEntity(Team team)
        {

            return new TeamEntity()
            {
                Id = team.Id,
                Name = team.Name,
                Leader = new UserEntity()
                {
                    Id = team.Leader.Id,
                    UserName = team.Leader.Username
                },
                Members = FromMembersToEntity(team.Members)
            };
        }

        public static IEnumerable<UserEntity> FromMembersToEntity(IEnumerable<User> members)
        {
            var mem = new List<UserEntity>();
            var usr = new UserEntity();
            foreach(var user in members)
            {
                usr.Id = user.Id;
                usr.UserName = user.Username;
                mem.Add(usr);
            }
            return mem;
        }
        public static User FromEntityToUser(UserEntity entity)
        {
            var user = new User();
            user.Id = entity.Id;
            user.Username = entity.UserName;
            return user;
        }
        public static IEnumerable<User> FromEntityToMembers(IEnumerable<UserEntity> entities)
        {
            var users = new List<User>();
            foreach (var entity in entities)
            {
                users.Add(FromEntityToUser(entity));
            }
            return users;
        }
        public static Team FromEntityToTeam(TeamEntity entity)
        {
            return new Team(
                entity.Id,
                entity.Name,
                FromEntityToUser(entity.Leader),
               FromEntityToMembers(entity.Members)
                );
        }
        public static IEnumerable<ProgrammingLanguage> FromEntityToProgrammingLanguages(IEnumerable<ProgrammingLanguageEntity> entities)
        {
           var languages = new List<ProgrammingLanguage>();
            foreach(var language in entities)
            {
                languages.Add(new ProgrammingLanguage(
                     language.Id,
                     language.Name
                     ));
            }
            return languages;
        }
        public static Project FromEntityToProject(ProjectEntity entity)
        {
            var langs = entity.Languages;
        

            return new Project(
                entity.Id,
                 entity.Topic,
                  FromEntityToTeam(entity.Team),
                  FromEntityToProgrammingLanguages(entity.Languages),
            entity.PlannedEndDate,
                entity.DeadlineDate




                );
        }
    }
}
