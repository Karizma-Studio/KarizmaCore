using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KarizmaPlatform.Core.JsonConverters;

public class NullableJsonConverter<T> : ValueConverter<T?, string>
{
    /// <inheritdoc />
    public NullableJsonConverter() : base(
        gameData => JsonSerializer.Serialize(gameData, JsonSerializerOptions.Default),
        json => JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default))
    {
    }
}