using System.Collections;
using System.Reflection;
using System.Text;

namespace KarizmaPlatform.Core.Utils;

public static class ObjectExtensions
{
    /// <summary>
    /// Returns a string containing all public property names and values of an object,
    /// recursively including nested objects and collections, up to a specified depth.
    /// </summary>
    /// <param name="obj">The object to convert to string.</param>
    /// <param name="depth">Recursion depth for nested objects (default 1).</param>
    /// <returns>A readable string representation of the object.</returns>
    public static string ToPropertyString(this object? obj, int depth = 1)
    {
        if (obj == null)
            return "null";

        if (depth < 0)
            return "...";

        var type = obj.GetType();

        // Handle primitive types and string directly
        if (type.IsPrimitive || obj is string || obj is decimal || obj is DateTime || obj is DateTimeOffset)
            return obj.ToString() ?? string.Empty;

        // Handle collections
        if (obj is IEnumerable enumerable && !(obj is string))
        {
            var items = enumerable.Cast<object?>()
                .Select(o => o.ToPropertyString(depth - 1));
            return $"[{string.Join(", ", items)}]";
        }

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (!props.Any())
            return obj.ToString() ?? string.Empty;

        var sb = new StringBuilder();
        sb.AppendLine($"{type.Name} {{");

        foreach (var p in props)
        {
            object? value;
            try
            {
                value = p.GetValue(obj);
            }
            catch
            {
                value = "unavailable";
            }

            string valueString = value.ToPropertyString(depth - 1);

            sb.AppendLine($"  {p.Name}: {valueString}");
        }

        sb.Append("}");
        return sb.ToString();
    }
}