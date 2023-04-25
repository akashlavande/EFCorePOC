using DbRelationshipsForDevelopersProjectsPOC.DbEntites;
using Microsoft.EntityFrameworkCore;

namespace DbRelationshipsForDevelopersProjectsPOC.Core
{
    public class DeveloperRepository : BaseRepository<ApplicationDbContext, Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async override Task<List<Developer>> GetAllAsync()
        {
            return await _context.Developers.Include(p => p.Project).AsNoTracking().ToListAsync();
        }
    }
}
