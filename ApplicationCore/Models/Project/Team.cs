using ApplicationCore.Commons.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Project
{
    public class Team : IIdentity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public User Leader { get; set; }
        public IEnumerable<User> Members { get; set; }
        public Team(int id, string name, User leader, IEnumerable<User> members)
        {
            Id = id;
            Name = name;
            Leader = leader;
            Members = members;
        }
    }
}
