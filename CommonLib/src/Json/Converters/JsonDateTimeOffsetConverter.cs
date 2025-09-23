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

        DateTime dateTime = DateTime.Parse(value);

        return dateTime;
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
        JsonSerializer.Serialize(writer, value, options);
    }
}
