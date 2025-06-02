using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KarizmaPlatform.Core.JsonConverters;

public class JsonConverter<T> : ValueConverter<T, string>
{
    /// <inheritdoc />
    public JsonConverter() : base(
        gameData => JsonSerializer.Serialize(gameData, JsonSerializerOptions.Default),
        json => JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default)!)
    {
    }
}