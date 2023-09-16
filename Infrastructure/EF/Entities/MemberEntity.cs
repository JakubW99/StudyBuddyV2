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
        public int TeamEntityId { get; internal set; }
        public string Role { get; set; }
        public MemberEntity(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }
        public MemberEntity()
        {

        }
    }
}
