namespace Infrastructure.EF.Entities
{
    public class ProgrammingLanguageEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProgrammingLanguageEntity(string name)
        {
           Name = name;
        }
        public ProgrammingLanguageEntity()
        {

        }
    }
}