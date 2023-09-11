using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
    public class JwtSettings
    {
        private static readonly string _section = "JwtSettings";

        private readonly IConfiguration _configuration;

        public string? Issuer => _configuration.GetSection(_section).GetSection("ValidIssuer").Value;

        public string? Audience => _configuration.GetSection(_section).GetSection("ValidAudience").Value;

        public string? Secret => _configuration.GetSection(_section).GetSection("Secret").Value;

        public JwtSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            if (!await roleManager.RoleExistsAsync("Leader"))
            {
                await roleManager.CreateAsync(new UserRole { Name = "Leader" });
            }

            if (!await roleManager.RoleExistsAsync("Member"))
            {
                await roleManager.CreateAsync(new UserRole { Name = "Member" });
            }
        }
    }
}
