// Ignore Spelling: Json

using DotNetExtras.Common.Json.Converters;
using System.Text.Encodings.Web;
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
    /// <param name="useOriginalCase">
    /// If <c>true</c>, the original property names will be used
    /// (unless they are overwritten by the JSON serialization attributes);
    /// otherwise, the <c>camelCase</c> notation will be used.
    /// </param>
    /// <param name="includeNullValues">
    /// If <c>true</c>, properties with null values will be included;
    /// otherwise, they will be ignored.
    /// </param>
    /// <returns>
    /// JSON string.
    /// </returns>
    /// <example>
    /// <code>
    /// User user = new User(){ Name = "John", Age = 30 };
    /// 
    /// // Prints unformatted JSON version of the object.
    /// Console.WriteLine(user.ToJson());
    /// 
    /// // Prints formatted JSON version of the object.
    /// Console.WriteLine(user.ToJson(true));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        bool indented = false,
        bool useOriginalCase = false,
        bool includeNullValues = false
    )
    {
        JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = indented,
            Converters = { new JsonStringEnumConverter() }
        };

        if (!useOriginalCase)
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        if (!includeNullValues)
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }

        return JsonSerializer.Serialize(source, options);
    }

    /// <summary>
    /// Converts a JSON string to a strongly typed object.
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
    /// <para>
    /// Enumerated properties in the JSON string are assumed to hold the 
    /// field names, i.e. string, not integer, values.
    /// </para>
    /// <para>
    /// If the property being set is of type <see cref="object"/>,
    /// this method will attempt to convert the JSON value to the most appropriate
    /// primitive type, list, or collection type using <see cref="JsonObjectAsPrimitiveConverter"/>.
    /// </para>
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
            AllowTrailingCommas = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonDateTimeConverter(),
                new JsonDateTimeOffsetConverter(),
                new JsonObjectAsPrimitiveConverter()
            }
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <inheritdoc cref="FromJson{T}(string?)" />
    /// <param name="type">
    /// Data type of the deserialized object to be returned
    /// </param>
    /// <param name="json">
    /// JSON string.
    /// </param>
    public static object? FromJson
    (
        this string? json,
        Type type
    )
    {
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonDateTimeConverter(),
                new JsonDateTimeOffsetConverter(),
                new JsonObjectAsPrimitiveConverter()
            }
        };

        return JsonSerializer.Deserialize(json, type, options);
    }
}
