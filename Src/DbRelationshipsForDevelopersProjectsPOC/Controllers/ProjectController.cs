using DbRelationshipsForDevelopersProjectsPOC.Core.Interfaces;
using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;
using Microsoft.AspNetCore.Mvc;

namespace DbRelationshipsForDevelopersProjectsPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var result = await _projectRepository.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto request)
        {
            var newProject = new Project
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            var result = await _projectRepository.CreateAsync(newProject);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] Project request)
        {
            var result = await _projectRepository.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("ProjectDeveloperAssignment")]
        public async Task<IActionResult> ProjectDeveloperAssignment(ProjectDeveloperAssignmentDto request)
        {
            var result = await _projectRepository.AssignDevelopersToProjectAsync(request);
            return Ok(result);
        }
    }
}
