using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    public class UserRole :IdentityRole<int>
    {
        public UserRole()
        {

        }
        public UserRole(string role)
        {

        }
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
