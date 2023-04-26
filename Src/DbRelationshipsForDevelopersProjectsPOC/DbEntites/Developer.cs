using DbRelationshipsForDevelopersProjectsPOC.Core.Common;

namespace DbRelationshipsForDevelopersProjectsPOC.DbEntites
{
    public class Developer : IIdEntity
    {
        public Developer()
        {
            Project = new HashSet<DeveloperProjects>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<DeveloperProjects> Project { get; set; }
    }
}
