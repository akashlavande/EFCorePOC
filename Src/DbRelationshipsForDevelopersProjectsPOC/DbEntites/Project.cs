using DbRelationshipsForDevelopersProjectsPOC.Core.Common;

namespace DbRelationshipsForDevelopersProjectsPOC.DbEntites
{
    public class Project : IIdEntity
    {
        public Project()
        {
            Developer = new HashSet<DeveloperProjects>();
        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<DeveloperProjects> Developer { get; set; }
    }
}
