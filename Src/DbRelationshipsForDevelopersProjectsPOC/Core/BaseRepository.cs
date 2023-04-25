using Microsoft.EntityFrameworkCore;

namespace DbRelationshipsForDevelopersProjectsPOC.Core
{
    public abstract class BaseRepository<TDbContext, TDbEntity> 
        : IBaseRepository<TDbEntity>, IDisposable
        where TDbContext : DbContext
        where TDbEntity: class, IIdEntity
    {
        private const string InvalidIdErrorMessage = "Id not found in database!";
        protected readonly TDbContext _context;

        public BaseRepository(TDbContext context)
        {
            this._context = context;
        }

        public async virtual Task<List<TDbEntity>> GetAllAsync()
        {
            return await _context.Set<TDbEntity>().AsNoTracking().ToListAsync();
        }

        public async virtual Task<TDbEntity?> GetAsync(Guid id)
        {
            var records = _context.Set<TDbEntity>().AsNoTracking();
            return await records.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task<TDbEntity> CreateAsync(TDbEntity entity)
        {
            if (entity.Id.Equals(Guid.Empty))
                entity.Id = Guid.NewGuid();

            var newEntity = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(newEntity.Entity).State = EntityState.Detached;
            return newEntity.Entity;
        }

        public async virtual Task<TDbEntity> UpdateAsync(TDbEntity entity)
        {
            var updatedEntity = _context.Update(entity);
            await _context.SaveChangesAsync();
            _context.Entry(updatedEntity.Entity).State = EntityState.Deleted;
            return updatedEntity.Entity;
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            var existingEntity = await _context.Set<TDbEntity>().FindAsync(id);
            if (existingEntity is null)
                throw new NullReferenceException(InvalidIdErrorMessage);
            _context.Set<TDbEntity>().Remove(existingEntity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

    }
}
