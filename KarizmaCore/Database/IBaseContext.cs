namespace KarizmaPlatform.Core.Database
{
    public interface IBaseContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
