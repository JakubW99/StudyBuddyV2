namespace API.Dto
{
    public class TeamDto
    {
        public string Name { get; set; }
        public int LeaderId { get; set; }
        public IEnumerable<MemberDto> Members { get; set; }
        public bool IsOpenTeam { get; set; }
    }
}
