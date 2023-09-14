using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
    public class StudyBuddyDbContext : IdentityDbContext<UserEntity, UserRole, int>
    {
    
        public StudyBuddyDbContext(DbContextOptions<StudyBuddyDbContext> options ) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<ProgrammingLanguageEntity> ProgrammingLanguages { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                 "Server=(localdb)\\mssqllocaldb;Database=Std;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

    }
}