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
                LeaderId = team.LeaderId,
                Members = team.Members.Select(x => new MemberEntity() { Id = x.Id, UserId= x.UserId , TeamId = x.TeamId})
            };
        }

     
        public static User FromEntityToUser(UserEntity entity)
        {
            var user = new User();
            user.Id = entity.Id;
            user.Username = entity.UserName;
            return user;
        }
      
        public static Team FromEntityToTeam(TeamEntity entity)
        {
            return new Team()
            {
                Id = entity.Id,
                Name = entity.Name,
                LeaderId = entity.LeaderId,
               Members = entity.Members.Select(x=> new Member() { Id = x.Id, TeamId = x.TeamId, UserId = x.UserId})
                };
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
