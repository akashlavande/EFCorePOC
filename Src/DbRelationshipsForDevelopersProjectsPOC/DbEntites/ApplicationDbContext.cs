using Microsoft.EntityFrameworkCore;

namespace DbRelationshipsForDevelopersProjectsPOC.DbEntites
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeveloperProjects>()
                .HasKey(o => new { o.DeveloperId, o.ProjectId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<DeveloperProjects> DeveloperProjects { get; set; }
    }
}
