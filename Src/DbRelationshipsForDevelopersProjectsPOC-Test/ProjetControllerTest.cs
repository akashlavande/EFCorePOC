using DbRelationshipsForDevelopersProjectsPOC.Controllers;
using DbRelationshipsForDevelopersProjectsPOC.Core.Interfaces;
using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace DbRelationshipsForDevelopersProjectsPOC_Test
{
    public class ProjetControllerTest
    {
        private List<Project> Projects = new();

        private readonly Mock<IProjectRepository> _mock;

        private readonly ProjectController _projectController;

        public ProjetControllerTest()
        {
            _mock = new Mock<IProjectRepository>();
            _projectController = new ProjectController(_mock.Object);
            Projects = new List<Project>()
            {
                new Project(){Id = Guid.Parse("560bb6bb-74f8-43f7-9f8b-7d62e6bf6d17"), Name = "SF Email Bot", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1)},
                new Project(){Id = Guid.Parse("660bb6bb-74f8-43f7-9f8b-7d62e6bf6d18"), Name = "SF Document Bot", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1)},
                new Project(){Id = Guid.Parse("760bb6bb-74f8-43f7-9f8b-7d62e6bf6d19"), Name = "SF Chat Bot", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1)}
            };
        }

        [Fact]
        public async void GetAllProjects_ListOfProjects()
        {
            int expectedCount = 3;
            _mock.Setup(option => option.GetAllAsync().Result).Returns(Projects);

            var result = await _projectController.GetAllProjects() as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<List<Project>>(result.Value);
            Assert.Equal(expectedCount, data.Count);
        }

        [Theory]
        [InlineData("560bb6bb-74f8-43f7-9f8b-7d62e6bf6d17")]
        public async void GetProject_SingleProject(Guid id)
        {
            var expectedBotName = "SF Email Bot";
            _mock.Setup(option => option.GetAsync(It.IsAny<Guid>()).Result).Returns(Projects.FirstOrDefault(i => i.Id == id));

            var result = await _projectController.GetProject(id) as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<Project>(result.Value);
            Assert.Equal(expectedBotName, data.Name);
        }

        [Fact]
        public async void CreateProject_NewlyCreatedProject()
        {
            var newProject = new ProjectDto("Test1", DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            _mock.Setup(option => option.CreateAsync(It.IsAny<Project>()))
                .Callback<Project>((Project req) =>
                {
                    var s = new Project()
                    {
                        Id = Guid.Parse("890bb6bb-74f8-43f7-9f8b-7d62e6bf6d10"),
                        Name = req.Name,
                        EndDate = req.EndDate,
                        StartDate = req.StartDate
                    };
                    Projects.Add(s);
                }).Returns(Task.FromResult(new Project()));

            var result = await _projectController.CreateProject(newProject) as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            var res = Assert.IsType<Project>(result.Value);
        }

        [Fact]
        public async void UpdateProject_UpdatedProject()
        {
            var updateProject = new Project()
            {
                Id = Guid.Parse("660bb6bb-74f8-43f7-9f8b-7d62e6bf6d18"),
                Name = "Simplifai Document Bot",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10)
            };

            _mock.Setup(option => option.UpdateAsync(It.IsAny<Project>()).Result)
                .Callback((Project req) =>
                {
                    var project = Projects.FirstOrDefault(o => o.Id == req.Id);
                    project.StartDate = req.StartDate;
                    project.EndDate = req.EndDate;
                    project.Name = req.Name;
                    project.Developer = req.Developer;

                }).Returns(updateProject);

            var result = await _projectController.UpdateProject(updateProject) as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Project>(result.Value);
        }

        [Theory]
        [InlineData("760bb6bb-74f8-43f7-9f8b-7d62e6bf6d19")]
        public async void DeleteProject(Guid id)
        {
            _mock.Setup(option => option.DeleteAsync(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    var project = Projects.Single(i => i.Id == id);
                    Projects.Remove(project);
                });

            var result = await _projectController.DeleteProject(id);

            Assert.IsType<OkResult>(result);
        }
    }
}