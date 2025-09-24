// Ignore Spelling: Json

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Common.Json.Converters;
/// <summary>
/// Provides a more capable JSON deserialization for <see cref="DateTimeOffset"/> values 
/// than the default converter.
/// </summary>
public class JsonDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private readonly string? _format = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDateTimeConverter"/> class.
    /// </summary>
    /// <param name="format">
    /// Date and time format string to use for serialization and deserialization.
    /// </param>
    public JsonDateTimeOffsetConverter
    (
        string? format = null
    )
    {
        _format = format;
    }

    /// <summary>
    /// Reads and converts a JSON string to a <see cref="DateTimeOffset"/> object.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="Utf8JsonReader"/> to read the JSON data from.
    /// </param>
    /// <param name="typeToConvert">
    /// The type of the object to convert (not used).
    /// </param>
    /// <param name="options">
    /// The serializer options to use (not used).
    /// </param>
    /// <returns>
    /// The <see cref="DateTimeOffset"/> object parsed from the JSON string.
    /// </returns>
    public override DateTimeOffset Read
    (
        ref Utf8JsonReader reader, 
        Type typeToConvert, 
        JsonSerializerOptions options
    )
    {
        string value = reader.GetString()!;

        DateTimeOffset dateTimeOffset = string.IsNullOrEmpty(_format)
            ? DateTimeOffset.Parse(value)
            : DateTimeOffset.ParseExact(value, _format, null); 

        return dateTimeOffset;
    }

    /// <summary>
    /// Writes the specified <see cref="DateTimeOffset"/> value as JSON using the provided <see cref="Utf8JsonWriter"/>.
    /// </summary>
    /// <param name="writer">
    /// The <see cref="Utf8JsonWriter"/> to which the <see cref="DateTime"/> value will be written.
    /// </param>
    /// <param name="value">
    /// The <see cref="DateTimeOffset"/> value to write.
    /// </param>
    /// <param name="options">
    /// The <see cref="JsonSerializerOptions"/> to use when writing the value.
    /// </param>
    /// <remarks>
    /// This method serializes the <see cref="DateTimeOffset"/> value using the specified <see
    /// cref="JsonSerializerOptions"/>.
    /// </remarks>
    public override void Write
    (
        Utf8JsonWriter writer, 
        DateTimeOffset value, 
        JsonSerializerOptions options
    )
    {
        if (string.IsNullOrEmpty(_format))
        {
            JsonSerializer.Serialize(writer, value);
        }
        else
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
