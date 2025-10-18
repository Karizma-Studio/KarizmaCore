using KarizmaPlatform.Core.Database;

namespace KarizmaPlatform.Core.Logic.V2;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Adds the specified entity to the DbSet for tracking.
    /// Does NOT save changes to the database; SaveChangesAsync should be called externally.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Marks the entity as modified in the DbSet and optionally updates its UpdatedDate.
    /// Does NOT save changes to the database; SaveChangesAsync should be called externally.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="updateTimestamp">If true, updates the UpdatedDate to UtcNow.</param>
    Task UpdateAsync(TEntity entity, bool updateTimestamp = true);

    /// <summary>
    /// Deletes the entity with the specified Id from the DbSet.
    /// Does NOT save changes to the database; SaveChangesAsync should be called externally.
    /// </summary>
    /// <param name="id">The Id of the entity to delete.</param>
    Task DeleteByIdAsync(long id);

    /// <summary>
    /// Performs a soft delete on the entity with the specified Id by setting DeletedDate.
    /// Does NOT save changes to the database; SaveChangesAsync should be called externally.
    /// </summary>
    /// <param name="id">The Id of the entity to soft delete.</param>
    Task SoftDeleteByIdAsync(long id);

    /// <summary>
    /// Finds and returns the entity with the specified Id, or null if not found.
    /// </summary>
    /// <param name="id">The Id of the entity to find.</param>
    /// <param name="asNoTracking">A new query where the result set will not be tracked by the context</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> FindByIdAsync(long id, bool asNoTracking = false);

    /// <summary>
    /// Retrieves all entities from the DbSet, including those marked as deleted.
    /// </summary>
    /// <param name="asNoTracking">A new query where the result set will not be tracked by the context</param>
    /// <returns>List of all entities.</returns>
    Task<List<TEntity>> GetAllAsync(bool asNoTracking = false);

    /// <summary>
    /// Retrieves all entities that are not soft-deleted (DeletedDate is null).
    /// </summary>
    /// <param name="asNoTracking">A new query where the result set will not be tracked by the context</param>
    /// <returns>List of all non-deleted entities.</returns>
    Task<List<TEntity>> GetAllNotDeletedAsync(bool asNoTracking = false);
}