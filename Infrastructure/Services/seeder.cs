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
        public static async Task Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<StudyBuddyDbContext>();

            if (dbContext is null)
            {
                throw new ExternalException("Error occured while trying to seed data. Cannot get required services.");
            }

            await dbContext.Database.MigrateAsync();
            await dbContext.Database.EnsureCreatedAsync();

            if (!await dbContext.Users.AnyAsync())
            {
             
                    var user = new UserEntity();
                user.Id = 1;
                user.UserName = "Kamil";
                user.Email = "kamil@wsei.edu.pl";
                user.PasswordHash = "AQAAAAIAAYagAAAAEGYXd0apSgc2y/HVMaUlI98IgT/ijfD6dsNZouc47OtIYxHR/mm+xFqKA/0nQrEgDw==";



                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

           
        }
    }
}
