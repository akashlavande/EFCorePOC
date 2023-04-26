using DbRelationshipsForDevelopersProjectsPOC.Core.Common;
using DbRelationshipsForDevelopersProjectsPOC.Core.Interfaces;
using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using DbRelationshipsForDevelopersProjectsPOC.Model;
using Microsoft.EntityFrameworkCore;

namespace DbRelationshipsForDevelopersProjectsPOC.Core.Repositories
{
    public class ProjectRepository : BaseRepository<ApplicationDbContext, Project>, IProjectRepository
    {
        public const string DeveloperOverloadErrorMessage = "Developer with id {0} overloaded! Please add other developer!";
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async override Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.Include(d => d.Developer).AsNoTracking().ToListAsync();
        }

        public async Task<DeveloperProjects?> AssignDevelopersToProjectAsync(ProjectDeveloperAssignmentDto request)
        {
            var developer = _context.DeveloperProjects.AsNoTracking().Where(d => d.DeveloperId.Equals(request.DeveloperId)).ToList();
            if (developer.Count >= 2)
                throw new Exception(string.Format("Developer with id {0} overloaded! Please add other developer!", request.DeveloperId.ToString()));

            var req = new DeveloperProjects()
            {
                DeveloperId = request.DeveloperId,
                ProjectId = request.ProjectId
            };
            var result = await _context.DeveloperProjects.AddAsync(req);
            await _context.SaveChangesAsync();
            _context.Entry(result.Entity).State = EntityState.Detached;
            return await _context.DeveloperProjects.AsNoTracking()
                                    .Include(d => d.Developer)
                                    .Include(p => p.Project)
                                    .FirstOrDefaultAsync(d => d.DeveloperId.Equals(request.DeveloperId) && d.ProjectId.Equals(request.ProjectId));
        }
    }
}
