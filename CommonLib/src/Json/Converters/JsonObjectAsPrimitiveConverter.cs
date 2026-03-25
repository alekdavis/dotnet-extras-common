// Ignore Spelling: Json

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Common.Json.Converters;

/// <summary>
/// Converts JSON objects to the corresponding primitive types when possible during JSON deserialization
/// via the <code>System.Text.Json</code> (STJ) operations.
/// </summary>
/// <remarks>
/// <para>
/// The deserialization routine will try to convert JSON objects to the following primitive types:
/// </para>
/// <list type="bullet">
/// <item><para>Boolean (true/false)</para></item>
/// <item><para>Numeric (integer, long, decimal, double; depending on the value and precision)</para></item>
/// <item><para>Date and time offset (converted from the string value)</para></item>
/// <item><para>Date and time (converted from the string value)</para></item>
/// <item><para>String</para></item>
/// <item><para>List (of objects)</para></item>
/// <item><para>Dictionary (string key, object value)</para></item>
/// </list>
/// <para>
/// Keep in mind that this converter is only applied when the target property type is <see cref="object"/>.
/// The code logic will attempt to guess what the most appropriate type for the value would be 
/// by attempting to perform conversions to the above mentioned primitive types.
/// </para>
/// <para>
/// The code was adapted from 
/// <see href="https://stackoverflow.com/a/65974452/52545"/>
/// and
/// <see href="https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to#deserialize-inferred-types-to-object-properties"/>.
/// </para>
/// </remarks>
public class JsonObjectAsPrimitiveConverter: JsonConverter<object>
{
    /// <summary>
    /// Writes the specified object value as JSON using the provided <see cref="Utf8JsonWriter"/>.
    /// </summary>
    /// <param name="writer">
    /// The <see cref="Utf8JsonWriter"/> to which the JSON representation of the value will be written.
    /// </param>
    /// <param name="value">
    /// The object to write. If <c>null</c>, a JSON null value is written.
    /// </param>
    /// <param name="options">
    /// The <see cref="JsonSerializerOptions"/> to use when serializing the value. Can be <c>null</c>.
    /// </param>
    public override void Write
    (
        Utf8JsonWriter writer,
        object value,
        JsonSerializerOptions options
    )
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else if (value.GetType() == typeof(object))
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
        }
        else
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

    /// <summary>
    /// Reads and converts JSON element to a .NET object based on the JSON token type.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="Utf8JsonReader"/> to read from.
    /// </param>
    /// <param name="typeToConvert">
    /// The target <see cref="Type"/> to convert the JSON data to
    /// (in this case, it is always <see cref="object"/>).
    /// </param>
    /// <param name="options">
    /// The <see cref="JsonSerializerOptions"/> to use during deserialization.
    /// </param>
    /// <returns>
    /// A .NET object representing the JSON data.
    /// </returns>
    public override object? Read
    (
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;

            case JsonTokenType.False:
                return false;

            case JsonTokenType.True:
                return true;

            case JsonTokenType.String:
                string? stringValue = reader.GetString();

                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return stringValue;
                }

                if (stringValue.Contains('+') || stringValue.Contains('-'))
                {
                    if (reader.TryGetDateTimeOffset(out var dtoValue))
                    {
                        return dtoValue;
                    }
                }

                if (reader.TryGetDateTime(out var dtValue))
                {
                    return dtValue;
                }

                return reader.GetString();

            case JsonTokenType.Number:
            {
                if (reader.TryGetInt32(out var intValue))
                {
                    return intValue;
                }

                if (reader.TryGetInt64(out var longValue))
                {
                    return longValue;
                }


                bool isDecimal = reader.TryGetDecimal(out decimal decimalValue);
                bool isDouble = reader.TryGetDouble(out double doubleValue);

                if (isDecimal && isDouble)
                {
                    return (double)decimalValue != doubleValue
                        ? doubleValue
                        : decimalValue;
                }

                if (isDecimal)
                {
                    return decimalValue;
                }

                if (isDouble)
                {
                    return doubleValue;
                }

                using JsonDocument doc = JsonDocument.ParseValue(ref reader);

                return (object)doc.RootElement.Clone();
            }

            case JsonTokenType.StartArray:
            {
                List<object?> list = [];

                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        default:
                            list.Add(Read(ref reader, typeof(object), options));
                            break;

                        case JsonTokenType.EndArray:
                            return list;
                    }
                }

                throw new JsonException();
            }

            case JsonTokenType.StartObject:
            {
                Dictionary<string, object?> dictionary = [];

                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.EndObject:
                            return dictionary;

                        case JsonTokenType.PropertyName:
                            string key = reader.GetString()!;
                            reader.Read();
                            dictionary.Add(key, Read(ref reader, typeof(object), options));
                            break;

                        default:
                            throw new JsonException();
                    }
                }

                throw new JsonException();
            }

            default:
            {
                throw new JsonException(string.Format("Unknown JSON token '{0}'.", reader.TokenType));
            }
        }
    }
}

