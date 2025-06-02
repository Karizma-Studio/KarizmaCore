using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KarizmaPlatform.Core.JsonConverters;

public class ListJsonConverter<T> : ValueConverter<List<T>, string>
{
    /// <inheritdoc />
    public ListJsonConverter()
        : base(
            list => JsonSerializer.Serialize(list, (JsonSerializerOptions?)null), // Convert List<T> to JSON
            json => JsonSerializer.Deserialize<List<T>>(json, (JsonSerializerOptions?)null) ?? new List<T>() // Convert JSON back to List<T>
        )
    {
    }
}