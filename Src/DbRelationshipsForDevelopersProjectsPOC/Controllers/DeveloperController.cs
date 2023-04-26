using DbRelationshipsForDevelopersProjectsPOC.Core.Interfaces;
using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;
using Microsoft.AspNetCore.Mvc;

namespace DbRelationshipsForDevelopersProjectsPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperController(IDeveloperRepository developerRepo)
        {
            this._developerRepository = developerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeveloper()
        {
            var result = await _developerRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetDeveloper(Guid id)
        {
            var result = await _developerRepository.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeveloper([FromBody] DeveloperDto request)
        {
            var newDeveloper = new Developer
            {
                Name = request.Name
            };
            var result = await _developerRepository.CreateAsync(newDeveloper);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeveloper([FromBody] Developer request)
        {
            var result = await _developerRepository.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDeveloper(Guid id)
        {
            await _developerRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
