namespace Infrastructure.EF.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public UserEntity Leader { get; set; }
        public IEnumerable<UserEntity> Members { get; set; }
    }
}