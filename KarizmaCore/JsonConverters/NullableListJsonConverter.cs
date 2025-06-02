using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KarizmaPlatform.Core.JsonConverters;

public class NullableListJsonConverter<T> : ValueConverter<List<T>?, string?>
{
    /// <inheritdoc />
    public NullableListJsonConverter()
        : base(
            list => list == null ? null : JsonSerializer.Serialize(list, (JsonSerializerOptions?)null), // Convert to JSON (null-safe)
            json => json == null ? null : JsonSerializer.Deserialize<List<T>>(json, (JsonSerializerOptions?)null) // Convert back (null-safe)
        )
    {
    }
}