// Ignore Spelling: Json

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetExtras.Common.Extensions;

namespace DotNetExtras.Common.Json.Converters;
/// <summary>
/// Provides a more capable JSON deserialization for <see cref="DateTimeOffset"/> values 
/// than the default converter.
/// </summary>
public class JsonDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private readonly string? _format = null;
    private readonly int _precision = 0;
    private readonly bool _serializeAsUtc = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class 
    /// with the specified precision for formatting DateTimeOffset values.
    /// </summary>
    /// <param name="precision">
    /// The number of fractions of a second to include when formatting DateTimeOffset values.
    /// Must be between 0 and 7, inclusive.
    /// </param>
    public JsonDateTimeOffsetConverter
    (
        [Range(0, 7)]
        int precision = 0
    )
    {
        _precision = precision;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class
    /// with the format string.
    /// </summary>
    /// <param name="format">
    /// Date and time format string to use for serialization and deserialization.
    /// </param>
    public JsonDateTimeOffsetConverter
    (
        string? format
    )
    {
        _format = format;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class 
    /// with the flag indicating whether to use Universal (UTC) time during serialization
    /// and the specified precision for formatting DateTimeOffset values.
    /// </summary>
    /// <param name="serializeAsUtc">
    /// Indicates whether the converter should prefer UTC when handling <see cref="DateTimeOffset"/> values. 
    /// </param>
    /// <param name="precision">
    /// The number of fractions of a second to include when formatting DateTimeOffset values.
    /// Must be between 0 and 7, inclusive.
    /// </param>
    public JsonDateTimeOffsetConverter
    (
        bool serializeAsUtc,
        [Range(0, 7)]
        int precision = 0
    ) 
    {
        _serializeAsUtc = serializeAsUtc;
        _precision = precision;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class
    /// with the flag indicating whether to use Universal (UTC) time during serialization
    /// and the format string.
    /// </summary>
    /// <param name="serializeAsUtc">
    /// Indicates whether the converter should prefer UTC when handling <see cref="DateTimeOffset"/> values. 
    /// </param>
    /// <param name="format">
    /// Date and time format string to use for serialization and deserialization.
    /// </param>
    public JsonDateTimeOffsetConverter
    (
        bool serializeAsUtc,
        string? format
    ) 
    : this(format)
    {
        _serializeAsUtc = serializeAsUtc;
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
    /// The <see cref="Utf8JsonWriter"/> to which the <see cref="DateTimeOffset"/> value will be written.
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
            writer.WriteStringValue(_serializeAsUtc  
                ? value.ToUniversalIso8601(_precision) 
                : value.ToIso8601(_precision));
        }
        else
        {
            writer.WriteStringValue(_serializeAsUtc
                ? value.ToUniversalTime().ToString(_format, CultureInfo.InvariantCulture)
                : value.ToString(_format, CultureInfo.InvariantCulture));
        }
    }
}
