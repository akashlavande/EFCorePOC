using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;

namespace DbRelationshipsForDevelopersProjectsPOC.Core
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<DeveloperProjects?> AssignDevelopersToProjectAsync(ProjectDeveloperAssignmentDto request);
    }
}
