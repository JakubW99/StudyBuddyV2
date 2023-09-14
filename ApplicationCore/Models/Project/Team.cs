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
        public int LeaderId { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public bool IsOpenTeam { get; set; } = true;
    }
}
