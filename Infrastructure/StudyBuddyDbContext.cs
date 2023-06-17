using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class StudyBuddyDbContext :IdentityDbContext<UserEntity,UserRole,int>
    {
        public StudyBuddyDbContext(DbContextOptions<StudyBuddyDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<ProjectEntity> ProjectEntities { get; set; }
        public DbSet<TeamEntity> TeamEntities { get; set; }
        public DbSet<ProgrammingLanguageEntity> ProgrammingLanguages { get; set;}
     

    }
}
