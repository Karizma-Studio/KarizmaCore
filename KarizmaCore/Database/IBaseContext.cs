using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KarizmaPlatform.Core.Database
{
    public interface IBaseContext : IAsyncDisposable
    {
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}