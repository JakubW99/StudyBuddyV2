using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Project
{
    public class Member
    {
 
        public int UserId { get; set; }

        public string Role { get; set; }

        public Member(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        public Member() { 
        }

    }
}
