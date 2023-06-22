﻿namespace Infrastructure.EF.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int LeaderId { get; set; }
        public IEnumerable<MemberEntity> Members { get; set; }
    }
}