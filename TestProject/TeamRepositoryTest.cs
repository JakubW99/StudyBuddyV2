using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.Project;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;
using Xunit;
namespace TestProject
{
    public class TeamRepositoryTest
    {
        private readonly IGenericRepository<Team,int> _repository;
        private readonly Team team1;
        private readonly Team team2;
        private readonly Team team3;
        private readonly Team team4;
        private  Member member1;
        private  Member member2;
        public TeamRepositoryTest()
        {
            member1= new Member(1,"LEADER");
            member2 = new Member(2, "MEMBER");
            _repository = new MemoryGenericRepository<Team, int>(new IntGenerator());
            team1 = _repository.Add(new Team(
                id:1,
                name: "test1",
                leaderId: 1,
                members: new List<Member>(),
                isOpenTeam: true

                ));
            team2 = _repository.Add(new Team(
                id: 2,
                name: "test2",
                leaderId: 2,
                members: new List<Member>(),
                isOpenTeam: false

                ));
        }
        [Fact]
        public void CanAddTeamToRepository()
        {
            // SprawdŸ, czy mo¿na dodaæ zespó³ do repozytorium
            Team retrievedTeam = _repository.FindById(1);
            Assert.NotNull(retrievedTeam);
            Assert.Equal("test1", retrievedTeam.Name);
        }

        [Fact]
        public void CanUpdateTeamInRepository()
        {
            // SprawdŸ, czy mo¿na zaktualizowaæ zespó³ w repozytorium
            Team retrievedTeam = _repository.FindById(2);
            Assert.NotNull(retrievedTeam);

            retrievedTeam.Name = "Updated Team";
            _repository.Update(2,retrievedTeam);

            Team updatedTeam = _repository.FindById(2);
            Assert.Equal("Updated Team", updatedTeam.Name);
        }

        [Fact]
        public void CanRemoveTeamFromRepository()
        {
            // SprawdŸ, czy mo¿na usun¹æ zespó³ z repozytorium
            _repository.RemoveById(1);
            Team deletedTeam = _repository.FindById(1);
            Assert.Null(deletedTeam);
        }

        [Fact]
        public void CanAddMemberToTeam()
        {
            // SprawdŸ, czy mo¿na dodaæ cz³onka do zespo³u
            member1 = new Member(userId: 3, role: "Member");
            var members = team1.Members.ToList();
            members.Add(member1);

            Assert.Equal(1, members.Count());
        }
    }
}