using DbRelationshipsForDevelopersProjectsPOC.Core.Common;
using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;

namespace DbRelationshipsForDevelopersProjectsPOC.Core.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<DeveloperProjects?> AssignDevelopersToProjectAsync(ProjectDeveloperAssignmentDto request);
    }
}
