// Ignore Spelling: Json

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetExtras.Common.Json.Converters;
/// <summary>
/// Provides a more capable JSON deserialization for <see cref="bool"/> and
/// <see cref="Nullable{T}"/> of <see cref="bool"/> values than the default converter.
/// </summary>
/// <remarks>
/// <para>
/// Supports the following JSON representations of a boolean value:
/// </para>
/// <list type="bullet">
///   <item>Native boolean tokens (<c>true</c> / <c>false</c>).</item>
///   <item>The number <c>1</c> (interpreted as <see langword="true"/>) and the number <c>0</c>
///   (interpreted as <see langword="false"/>); any other number raises a <see cref="JsonException"/>.</item>
///   <item>Case-insensitive strings: <c>"true"</c>, <c>"1"</c>, <c>"yes"</c>, <c>"y"</c>, <c>"on"</c>
///   are interpreted as <see langword="true"/>; <c>"false"</c>, <c>"0"</c>, <c>"no"</c>, <c>"n"</c>,
///   <c>"off"</c> are interpreted as <see langword="false"/>; any other string raises a
///   <see cref="JsonException"/>.</item>
///   <item>When the target property is of type <see cref="Nullable{T}"/> of <see cref="bool"/>,
///   a JSON <c>null</c> token is read as <see langword="null"/>.</item>
/// </list>
/// </remarks>
public class JsonBoolConverter: JsonConverterFactory
{
    /// <inheritdoc/>
    public override bool CanConvert
    (
        Type typeToConvert
    )
    {
        return typeToConvert == typeof(bool) || typeToConvert == typeof(bool?);
    }

    /// <inheritdoc/>
    public override JsonConverter CreateConverter
    (
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return typeToConvert == typeof(bool?)
            ? new NullableBoolConverter()
            : new BoolConverter();
    }

    /// <summary>
    /// Reads a boolean value from the current JSON token,
    /// honoring the extended set of accepted representations
    /// (booleans, the numbers <c>0</c>/<c>1</c>, and recognized strings).
    /// </summary>
    /// <param name="reader">
    /// The <see cref="Utf8JsonReader"/> positioned on the value to read.
    /// </param>
    /// <returns>
    /// The parsed <see cref="bool"/> value.
    /// </returns>
    /// <exception cref="JsonException">
    /// Thrown when the current token cannot be interpreted as a boolean
    /// (an unrecognized string, a number other than <c>0</c> or <c>1</c>,
    /// or any other unsupported token type).
    /// </exception>
    private static bool ReadBool
    (
        ref Utf8JsonReader reader
    )
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                string? value = reader.GetString()?.ToLowerInvariant();

                if (value is "true" or "1" or "yes" or "y" or "on")
                {
                    return true;
                }

                if (value is "false" or "0" or "no" or "n" or "off")
                {
                    return false;
                }

                throw new JsonException(
                    $"Cannot convert string value '{value}' to a Boolean. " +
                    "Expected one of: true/false, 1/0, yes/no, y/n, on/off.");

            case JsonTokenType.Number:
                if (reader.TryGetInt32(out int number))
                {
                    return number switch
                    {
                        1 => true,
                        0 => false,
                        _ => throw new JsonException(
                            $"Cannot convert number value '{number}' to a Boolean. Expected 0 or 1.")
                    };
                }

                throw new JsonException(
                    "Cannot convert non-integer number value to a Boolean. Expected 0 or 1.");

            default:
                // Default handling for true/false tokens; throws for any other token type.
                return reader.GetBoolean();
        }
    }

    /// <summary>
    /// Internal converter for non-nullable <see cref="bool"/> values.
    /// </summary>
    private sealed class BoolConverter: JsonConverter<bool>
    {
        public override bool Read
        (
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            return ReadBool(ref reader);
        }

        public override void Write
        (
            Utf8JsonWriter writer,
            bool value,
            JsonSerializerOptions options
        )
        {
            writer.WriteBooleanValue(value);
        }
    }

    /// <summary>
    /// Internal converter for <see cref="Nullable{T}"/> of <see cref="bool"/> values.
    /// Maps a JSON <c>null</c> token to <see langword="null"/>.
    /// </summary>
    private sealed class NullableBoolConverter: JsonConverter<bool?>
    {
        public override bool? Read
        (
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return ReadBool(ref reader);
        }

        public override void Write
        (
            Utf8JsonWriter writer,
            bool? value,
            JsonSerializerOptions options
        )
        {
            if (value.HasValue)
            {
                writer.WriteBooleanValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
