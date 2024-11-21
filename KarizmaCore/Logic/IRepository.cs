using KarizmaPlatform.Core.Database;

namespace KarizmaPlatform.Core.Logic
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity?> FindById(long identifier);
        Task DeleteById(long identifier);
        Task<List<TEntity>> GetAll();
    }
}
