using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KarizmaPlatform.Core.JsonComparer;

public class JsonListComparer<T> : ValueComparer<List<T>>
{
    /// <inheritdoc />
    public JsonListComparer()
        : base(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        )
    {
    }
}