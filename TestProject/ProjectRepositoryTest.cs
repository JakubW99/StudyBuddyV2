using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.Project;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProjectRepositoryTest
    {
        private readonly IGenericRepository<Project, int> _repository;
        private readonly IGenericRepository<Team, int> _teamRepository;
        private readonly Team team1;
        private readonly Team team2;
        private readonly Project project1;
        private readonly Project project2;
        public ProjectRepositoryTest()
        {
            _repository = new MemoryGenericRepository<Project, int>(new IntGenerator());
            _teamRepository = new MemoryGenericRepository<Team, int>(new IntGenerator());

            team1 = _teamRepository.Add(new Team(
               id: 1,
               name: "test1",
               leaderId: 1,
               members: new List<Member>(),
               isOpenTeam: true

               ));
            team2 = _teamRepository.Add(new Team(
                id: 2,
                name: "test2",
                leaderId: 2,
                members: new List<Member>(),
                isOpenTeam: false

                ));
            project1 = _repository.Add(new Project(1, "projekt", team1, new List<ProgrammingLanguage>(), DateTime.Now, DateTime.Now, "easy", "github.com", false, "fjfsjfsjfsjfsjfsjj"));
        }
        [Fact]
        public void CanAddProjectToRepository()
        {
            // Sprawdź, czy można dodać projekt do repozytorium
            Project retrievedProject = _repository.FindById(1);
            Assert.NotNull(retrievedProject);
            Assert.Equal("projekt", retrievedProject.Topic);
        }

        [Fact]
        public void CanUpdateProjectInRepository()
        {
            // Sprawdź, czy można zaktualizować projekt w repozytorium
            Project retrievedProject = _repository.FindById(1);
            Assert.NotNull(retrievedProject);

            retrievedProject.Topic = "Updated Project";
            _repository.Update(1,retrievedProject);

            Project updatedProject = _repository.FindById(1);
            Assert.Equal("Updated Project", updatedProject.Topic);
        }

        [Fact]
        public void CanRemoveProjectFromRepository()
        {
            // Sprawdź, czy można usunąć projekt z repozytorium
            _repository.RemoveById(1);
            Project deletedProject = _repository.FindById(1);
            Assert.Null(deletedProject);
        }

        [Fact]
        public void CanAssignTeamToProject()
        {
            // Sprawdź, czy można przypisać zespół do projektu
            Project retrievedProject = _repository.FindById(1);
            Assert.NotNull(retrievedProject);

            retrievedProject.Team = team2;
            _repository.Update(1,retrievedProject);

            Project updatedProject = _repository.FindById(1);
            Assert.Equal(team2, updatedProject.Team);
        }
    }
}

