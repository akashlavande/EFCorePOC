namespace DbRelationshipsForDevelopersProjectsPOC.DbEntites
{
    public class DeveloperProjects
    {
        public Guid DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
