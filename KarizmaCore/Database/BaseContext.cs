using Microsoft.EntityFrameworkCore;

namespace KarizmaPlatform.Core.Database
{
    public class BaseContext : DbContext, IBaseContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            UpdateBaseEntityDefaultValues();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateBaseEntityDefaultValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateBaseEntityDefaultValues()
        {
            var insertedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
                if (insertedEntry is BaseEntity baseEntity)
                    baseEntity.CreatedDate = baseEntity.UpdatedDate = DateTimeOffset.UtcNow;

            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
                if (modifiedEntry is BaseEntity baseEntity)
                    baseEntity.UpdatedDate = DateTimeOffset.UtcNow;
        }
    }
}
