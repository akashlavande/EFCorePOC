namespace DbRelationshipsForDevelopersProjectsPOC.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class, IIdEntity
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetAsync(Guid id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(Guid id);
    }
}
