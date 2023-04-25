namespace DbRelationshipsForDevelopersProjectsPOC.Model
{
    public record DeveloperDto(
        string Name
        );

    public record ProjectDto(
        string Name,
        DateTime StartDate,
        DateTime EndDate 
        );

    public record ProjectDeveloperAssignmentDto(
        Guid ProjectId,
        Guid DeveloperId
        );

}
