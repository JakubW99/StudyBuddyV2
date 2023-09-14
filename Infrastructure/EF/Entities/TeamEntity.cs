namespace Infrastructure.EF.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int LeaderId { get; set; }
        public List<MemberEntity> Members { get; set; }
        public bool IsOpenTeam { get; set; } = true;
    }
}