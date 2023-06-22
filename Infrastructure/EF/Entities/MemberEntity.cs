using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    public class MemberEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int TeamId { get; set; }
    }
}
