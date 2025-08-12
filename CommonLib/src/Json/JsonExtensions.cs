using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Common.Json;
/// <summary>
/// Implements extension methods for data conversion to and from JSON strings.
/// </summary>
public static partial class JsonExtensions
{
    /// <summary>
    /// Converts an object to a JSON string.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <param name="indented">
    /// If true, serialized JSON elements will be indented.
    /// </param>
    /// <returns>
    /// JSON string.
    /// </returns>
    /// <example>
    /// <code>
    /// Sample sample = new Sample(){ Name = "John", Age = 30 };
    /// 
    /// // Prints unformatted JSON version of the object.
    /// Console.WriteLine(sample.ToJson());
    /// 
    /// // Prints formatted JSON version of the object.
    /// Console.WriteLine(sample.ToJson(true));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        bool indented = false
    )
    {
        if (source == null)
        {
            return "";
        }

        JsonSerializerOptions options = new() 
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = indented,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        return JsonSerializer.Serialize(source, options);
    }

    /// <summary>
    /// Converts a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">
    /// Target data type.
    /// </typeparam>
    /// <param name="json">
    /// Original value.
    /// </param>
    /// <returns>
    /// Converted value or default if conversion failed.
    /// </returns>
    /// <remarks>
    /// Enumerated properties in the JSON string are assumed to hold the 
    /// field names, i.e. string, not integer, values.
    /// </remarks>
    /// <example>
    /// <code>
    /// User? user = "{\"id\":123,\"name\":\"John\"}".FromJson&lt;User&gt;();
    /// </code>
    /// </example>
    public static T? FromJson<T>
    (
        this string? json
    )
    where T : class
    {
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        JsonSerializerOptions options = new() 
        {
            PropertyNameCaseInsensitive = true,  
            Converters = { new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }
}
