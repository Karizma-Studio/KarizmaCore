using System.Text.Json;

namespace KarizmaPlatform.Core.Utils;

public static class JsonExtensions
{
    public static JsonDocument ToJsonDocument(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonDocument.Parse(json);
    }

    public static string ToJsonString(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonDocument.Parse(json).ToString() ?? "";
    }
}