using KarizmaPlatform.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace KarizmaPlatform.Core.Logic
{
    /// <summary>
    /// Base repository for entities with performing SaveChanges to the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that inherits from BaseEntity.</typeparam>
    /// <param name="baseContext">DbContext</param>
    [Obsolete]
    public abstract class BaseRepository<TEntity>(BaseContext baseContext) : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            baseContext.Add(entity);
            await baseContext.SaveChangesAsync();
            return entity;
        }

        public virtual Task Update(TEntity entity)
        {
            baseContext.Update(entity);
            return baseContext.SaveChangesAsync();
        }

        public virtual async Task DeleteById(long identifier)
        {
            var entity = await FindById(identifier);
            if (entity != null)
            {
                baseContext.Remove(entity);
                await baseContext.SaveChangesAsync();
            }
        }

        public virtual async Task SoftDelete(long identifier)
        {
            var entity = await FindById(identifier);
            if (entity != null)
            {
                entity.DeletedDate = DateTimeOffset.UtcNow;
                baseContext.Update(entity);
                await baseContext.SaveChangesAsync();
            }
        }

        public virtual Task<TEntity?> FindById(long identifier)
        {
            return baseContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == identifier);
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return baseContext.Set<TEntity>().ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAllNotDeleted()
        {
            return baseContext.Set<TEntity>().Where(entity => entity.DeletedDate == null).ToListAsync();
        }
    }
}