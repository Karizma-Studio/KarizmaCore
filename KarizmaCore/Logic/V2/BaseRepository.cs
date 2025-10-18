using KarizmaPlatform.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace KarizmaPlatform.Core.Logic.V2;

/// <summary>
/// Base repository for entities without performing SaveChanges to the database.
/// <para>
/// This repository only tracks changes to the entity state (Add, Update, Delete)
/// using the provided DbSet. Actual persistence to the database should be handled
/// by a UnitOfWork or external service that calls SaveChangesAsync.
/// </para>
/// </summary>
/// <typeparam name="TEntity">The type of the entity that inherits from BaseEntity.</typeparam>
/// <param name="context">The DbContext used to get the DbSet for the entity.</param>
public abstract class BaseRepository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public virtual Task AddAsync(TEntity entity)
    {
        dbSet.Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(TEntity entity, bool updateTimestamp = true)
    {
        if (updateTimestamp)
            entity.UpdatedDate = DateTimeOffset.UtcNow;
        dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteByIdAsync(long id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
            dbSet.Remove(entity);
    }

    public virtual async Task SoftDeleteByIdAsync(long id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            entity.DeletedDate = DateTimeOffset.UtcNow;
            await UpdateAsync(entity);
        }
    }

    public Task<TEntity?> FindByIdAsync(long id, bool asNoTracking = false)
    {
        return (asNoTracking ? dbSet.AsNoTracking() : dbSet).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<TEntity>> GetAllAsync(bool asNoTracking = false)
    {
        return (asNoTracking ? dbSet.AsNoTracking() : dbSet).ToListAsync();
    }

    public Task<List<TEntity>> GetAllNotDeletedAsync(bool asNoTracking = false)
    {
        return (asNoTracking ? dbSet.AsNoTracking() : dbSet).Where(entity => entity.DeletedDate == null).ToListAsync();
    }
}