using Microsoft.EntityFrameworkCore;

namespace KarizmaPlatform.Core.Database;

public abstract class SeederBase
{
    public abstract void AddData(ModelBuilder modelBuilder);
}
