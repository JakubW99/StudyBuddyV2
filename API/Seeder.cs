using ApplicationCore.Models.Project;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services
{
    public static class Seeder
    {
        public static async Task Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<StudyBuddyDbContext>();
                if (dbContext is null)
                {
                    throw new ExternalException("Error occured while trying to seed data. Cannot get required services.");
                }

                await dbContext.Database.MigrateAsync();
                await dbContext.Database.EnsureCreatedAsync();

                var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
                var findUserOne = await userManager.FindByNameAsync("kamil");
                if (findUserOne == null)
                {
                    UserEntity user = new UserEntity() { Email = "kamil@wsei.edu.pl", UserName = "kamil" };

                    var saved = await userManager?.CreateAsync(user, "1234ABcd$");
                    userManager.AddToRoleAsync(user, "USER");
                }
                var findUserTwo = await userManager.FindByNameAsync("pawel");
                if (findUserTwo == null)
                {
                    UserEntity user = new UserEntity() { Email = "pawel@wsei.edu.pl", UserName = "pawel" };

                    var saved = await userManager?.CreateAsync(user, "1234ABcd$");
                    userManager.AddToRoleAsync(user, "USER");
                }
                var checkTeam = dbContext.Teams.Where(x => x.Name == "test1").FirstOrDefault();
                var chechTeam2 = dbContext.Teams.Where(x => x.Name == "TESTOWY TEAM").FirstOrDefault();
                var members = new List<MemberEntity>();
                var members2 = new List<MemberEntity>
                {
                    new MemberEntity(1, "MEMBER"),
                    new MemberEntity(2, "SUPPORT")
                   
                };

                var team = new TeamEntity("test1", 1, members, true);
                var team2 = new TeamEntity("TESTOWY TEAM", 2, members2, true);
                if(checkTeam== null)
                {
                    dbContext.Teams.Add(team);
                }
               
                if(chechTeam2 == null) 
                {
                    dbContext.Teams.Add(team2);
                }
                var checkProject = dbContext.Projects.Where(x => x.Topic == "Tworzenie Aplikacji Mobilnej");
                var project = new ProjectEntity()
                {

                    Topic = "Tworzenie Aplikacji Mobilnej",
                    Description = "Projekt polega na stworzeniu aplikacji mobilnej do zarządzania zadaniami.",
                    Team = team,

                    Languages = new List<ProgrammingLanguageEntity>
                 {
                     new ProgrammingLanguageEntity {  Name = "C#"},
                     new ProgrammingLanguageEntity {  Name = "Xamarin" }
                 },
                    PlannedEndDate = DateTime.Parse("2023-12-31"),
                    DeadlineDate = DateTime.Parse("2024-02-15"),
                    Difficulty = "Średni",
                    RepositoryLink = "https://github.com/tworzenie-aplikacji-mobilnej",
                    IsFinished = false
                };
                if(checkProject == null)
                {
                    dbContext.Projects.Add(project);
                } 
                
               

                await dbContext.SaveChangesAsync();
              
            }

        }
    }
}
