// Ignore Spelling: Json

using DotNetExtras.Common.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonLibTests.Json;

public class TestClass
{
    public DateTime? DateTimeValue
    {
        get; set;
    }

    public DateTimeOffset? DateTimeOffsetValue
    {
        get; set;
    }

    public bool BoolValue
    {
        get; set;
    }

    public bool? NullableBoolValue
    {
        get; set;
    }
}

public class TestClassWithBoolAttribute
{
    [JsonConverter(typeof(JsonBoolConverter))]
    public bool BoolValue
    {
        get; set;
    }

    [JsonConverter(typeof(JsonBoolConverter))]
    public bool? NullableBoolValue
    {
        get; set;
    }
}

public class JsonConvertersTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("2025-10-29T10:20:30Z")]
    [InlineData("2025-10-29T10:20:30.123Z")]
    [InlineData("2025-10-29T10:20:30.123Z", 3)]
    [InlineData("2025-10-29T10:20:30")]
    [InlineData("2025-10-29T10:20:30.123", 3)]
    [InlineData("2025-10-29T10:20:30+1:30")]
    [InlineData("2025-10-29T10:20:30.123-1:30", 3)]
    public void DateTimeConverter_SerializeDateTimeUtc
    (
        string? dateTime,
        int precision = 0
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonDateTimeConverter(true, precision), new JsonDateTimeOffsetConverter(true, precision) }
        };

        TestClass testObject = new()
        {
            DateTimeValue = dateTime == null ? null : DateTime.Parse(dateTime)
        };

        string json = JsonSerializer.Serialize(testObject, options);

        DateTime? expected = dateTime == null
            ? null
            : DateTime.Parse(dateTime);

        string expectedValue = expected == null
            ? "null"
            : "\"" + expected!.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : "")) + "Z\"";

        Assert.Contains($"\"DateTimeValue\":{expectedValue}", json);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("2025-10-29T10:20:30Z")]
    [InlineData("2025-10-29T10:20:30.123Z")]
    [InlineData("2025-10-29T10:20:30.123Z", 3)]
    [InlineData("2025-10-29T10:20:30")]
    [InlineData("2025-10-29T10:20:30.123", 3)]
    [InlineData("2025-10-29T10:20:30+1:30")]
    [InlineData("2025-10-29T10:20:30.123-1:30", 3)]
    public void DateTimeConverter_SerializeDateTime
    (
        string? dateTime,
        int precision = 0
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonDateTimeConverter(false, precision), new JsonDateTimeOffsetConverter(false, precision) }
        };

        TestClass testObject = new()
        {
            DateTimeValue = dateTime == null ? null : DateTime.Parse(dateTime)
        };

        string json = JsonSerializer.Serialize(testObject, options);

        DateTime? expected = dateTime == null
            ? null
            : DateTime.Parse(dateTime);

        string expectedValue = expected == null
            ? "null"
            : "\"" + expected!.Value.ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : ""));

        Assert.Contains($"\"DateTimeValue\":{expectedValue}", json);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("2025-10-29T10:20:30Z")]
    [InlineData("2025-10-29T10:20:30.123Z")]
    [InlineData("2025-10-29T10:20:30.123Z", 3)]
    [InlineData("2025-10-29T10:20:30")]
    [InlineData("2025-10-29T10:20:30.123", 3)]
    [InlineData("2025-10-29T10:20:30+1:30")]
    [InlineData("2025-10-29T10:20:30.123-1:30", 3)]
    public void DateTimeOffsetConverter_SerializeDateTimeUtc
    (
        string? dateTimeOffset,
        int precision = 0
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonDateTimeConverter(true, precision), new JsonDateTimeOffsetConverter(true, precision) }
        };

        TestClass testObject = new()
        {
            DateTimeOffsetValue = dateTimeOffset == null ? null : DateTimeOffset.Parse(dateTimeOffset)
        };

        string json = JsonSerializer.Serialize(testObject, options);

        DateTimeOffset? expected = dateTimeOffset == null
            ? null
            : DateTimeOffset.Parse(dateTimeOffset);

        string expectedValue = expected == null
            ? "null"
            : "\"" + expected!.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : "")) + "Z\"";

        Assert.Contains($"\"DateTimeOffsetValue\":{expectedValue}", json);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("2025-10-29T10:20:30Z")]
    [InlineData("2025-10-29T10:20:30.123Z")]
    [InlineData("2025-10-29T10:20:30.123Z", 3)]
    [InlineData("2025-10-29T10:20:30")]
    [InlineData("2025-10-29T10:20:30.123", 3)]
    [InlineData("2025-10-29T10:20:30+1:30")]
    [InlineData("2025-10-29T10:20:30.123-1:30", 3)]
    public void DateTimeOffsetConverter_SerializeDateTime
    (
        string? dateTimeOffset,
        int precision = 0
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonDateTimeConverter(false, precision), new JsonDateTimeOffsetConverter(false, precision) }
        };

        TestClass testObject = new()
        {
            DateTimeOffsetValue = dateTimeOffset == null ? null : DateTimeOffset.Parse(dateTimeOffset)
        };

        string json = JsonSerializer.Serialize(testObject, options);

        DateTimeOffset? expected = dateTimeOffset == null
            ? null
            : DateTimeOffset.Parse(dateTimeOffset);

        string expectedValue = expected == null
            ? "null"
            : "\"" + expected!.Value.ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : ""));

        Assert.Contains($"\"DateTimeOffsetValue\":{expectedValue}", json);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("2023-10-05", "2023-10-05T00:00:00.0")]
    [InlineData("2023-10-05T14:48:30", "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.0", "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.10", "2023-10-05T14:48:30.10")]
    [InlineData("2023-10-05T14:48:30.123Z", "2023-10-05T14:48:30.123Z")]
    [InlineData("2023-10-05 14:48:30", "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 2:48:30 PM", "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 14:48:30-3:30", "2023-10-05T14:48:30-03:30")]
    [InlineData("2023-10-05 14:48:30 +3:30", "2023-10-05T14:48:30+03:30")]
    public void DateTimeConverter_Deserialize
    (
        string? jsonValue,
        string? expectedValue
    )
    {
        string json = jsonValue == null ? $"{{\"DateTimeValue\":null}}" : $"{{\"DateTimeValue\":\"{jsonValue}\"}}";

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonDateTimeConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);

        if (expectedValue == null)
        {
            Assert.Null(deserialized.DateTimeValue);
        }
        else
        {
            DateTime expected = DateTime.Parse(expectedValue);

            Assert.Equal(expected, deserialized.DateTimeValue!.Value);
        }
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("2023-10-05", "2023-10-05T00:00:00.0")]
    [InlineData("2023-10-05T14:48:30", "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.0", "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.10", "2023-10-05T14:48:30.10")]
    [InlineData("2023-10-05T14:48:30.123Z", "2023-10-05T14:48:30.123Z")]
    [InlineData("2023-10-05 14:48:30", "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 2:48:30 PM", "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 14:48:30-3:30", "2023-10-05T14:48:30-03:30")]
    [InlineData("2023-10-05 14:48:30 +3:30", "2023-10-05T14:48:30+03:30")]
    public void DateTimeOffsetConverter_Deserialize
    (
        string? jsonValue,
        string? expectedValue
    )
    {
        string json = jsonValue == null ? $"{{\"DateTimeOffsetValue\":null}}" : $"{{\"DateTimeOffsetValue\":\"{jsonValue}\"}}";

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonDateTimeOffsetConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);

        if (expectedValue == null)
        {
            Assert.Null(deserialized.DateTimeOffsetValue);
        }
        else
        {
            DateTimeOffset expected = DateTime.Parse(expectedValue);

            Assert.Equal(expected, deserialized.DateTimeOffsetValue!.Value);
        }
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void BoolConverter_SerializeBool
    (
        bool value,
        string expected
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonBoolConverter() }
        };

        TestClass testObject = new()
        {
            BoolValue = value
        };

        string json = JsonSerializer.Serialize(testObject, options);

        Assert.Contains($"\"BoolValue\":{expected}", json);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    [InlineData(null, "null")]
    public void BoolConverter_SerializeNullableBool
    (
        bool? value,
        string expected
    )
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = false,
            Converters = { new JsonBoolConverter() }
        };

        TestClass testObject = new()
        {
            NullableBoolValue = value
        };

        string json = JsonSerializer.Serialize(testObject, options);

        Assert.Contains($"\"NullableBoolValue\":{expected}", json);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void BoolConverter_DeserializeNativeBoolean
    (
        string jsonValue,
        bool expected
    )
    {
        string json = $"{{\"BoolValue\":{jsonValue}}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.BoolValue);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public void BoolConverter_DeserializeNumber
    (
        int jsonValue,
        bool expected
    )
    {
        string json = $"{{\"BoolValue\":{jsonValue}}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.BoolValue);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(-1)]
    [InlineData(100)]
    public void BoolConverter_DeserializeInvalidNumber_Throws
    (
        int jsonValue
    )
    {
        string json = $"{{\"BoolValue\":{jsonValue}}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClass>(json, options));
    }

    [Fact]
    public void BoolConverter_DeserializeNonIntegerNumber_Throws()
    {
        string json = "{\"BoolValue\":1.5}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClass>(json, options));
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("True", true)]
    [InlineData("TRUE", true)]
    [InlineData("1", true)]
    [InlineData("yes", true)]
    [InlineData("YES", true)]
    [InlineData("y", true)]
    [InlineData("Y", true)]
    [InlineData("on", true)]
    [InlineData("ON", true)]
    [InlineData("false", false)]
    [InlineData("False", false)]
    [InlineData("FALSE", false)]
    [InlineData("0", false)]
    [InlineData("no", false)]
    [InlineData("NO", false)]
    [InlineData("n", false)]
    [InlineData("N", false)]
    [InlineData("off", false)]
    [InlineData("OFF", false)]
    public void BoolConverter_DeserializeString
    (
        string jsonValue,
        bool expected
    )
    {
        string json = $"{{\"BoolValue\":\"{jsonValue}\"}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.BoolValue);
    }

    [Theory]
    [InlineData("maybe")]
    [InlineData("")]
    [InlineData("truthy")]
    [InlineData("falsy")]
    [InlineData("2")]
    public void BoolConverter_DeserializeInvalidString_Throws
    (
        string jsonValue
    )
    {
        string json = $"{{\"BoolValue\":\"{jsonValue}\"}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClass>(json, options));
    }

    [Fact]
    public void BoolConverter_DeserializeNullableBool_NullReturnsNull()
    {
        string json = "{\"NullableBoolValue\":null}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Null(deserialized.NullableBoolValue);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("yes", true)]
    [InlineData("no", false)]
    public void BoolConverter_DeserializeNullableBool_AcceptsValueFormats
    (
        string jsonValue,
        bool expected
    )
    {
        string json = $"{{\"NullableBoolValue\":\"{jsonValue}\"}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.NullableBoolValue);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public void BoolConverter_DeserializeNullableBool_AcceptsNumbers
    (
        int jsonValue,
        bool expected
    )
    {
        string json = $"{{\"NullableBoolValue\":{jsonValue}}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json, options);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.NullableBoolValue);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(-1)]
    public void BoolConverter_DeserializeNullableBool_InvalidNumberThrows
    (
        int jsonValue
    )
    {
        string json = $"{{\"NullableBoolValue\":{jsonValue}}}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClass>(json, options));
    }

    [Fact]
    public void BoolConverter_DeserializeNullableBool_InvalidStringThrows()
    {
        string json = "{\"NullableBoolValue\":\"maybe\"}";

        JsonSerializerOptions options = new()
        {
            Converters = { new JsonBoolConverter() }
        };

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClass>(json, options));
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("yes", true)]
    [InlineData("YES", true)]
    [InlineData("no", false)]
    [InlineData("off", false)]
    public void BoolConverter_WithAttribute_DeserializeString
    (
        string jsonValue,
        bool expected
    )
    {
        string json = $"{{\"BoolValue\":\"{jsonValue}\"}}";

        // No converters registered in options — the attribute applies the converter automatically.
        TestClassWithBoolAttribute? deserialized = JsonSerializer.Deserialize<TestClassWithBoolAttribute>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.BoolValue);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public void BoolConverter_WithAttribute_DeserializeNumber
    (
        int jsonValue,
        bool expected
    )
    {
        string json = $"{{\"BoolValue\":{jsonValue}}}";

        // No converters registered in options — the attribute applies the converter automatically.
        TestClassWithBoolAttribute? deserialized = JsonSerializer.Deserialize<TestClassWithBoolAttribute>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(expected, deserialized.BoolValue);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(-1)]
    public void BoolConverter_WithAttribute_DeserializeInvalidNumberThrows
    (
        int jsonValue
    )
    {
        string json = $"{{\"BoolValue\":{jsonValue}}}";

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestClassWithBoolAttribute>(json));
    }

    [Fact]
    public void BoolConverter_WithAttribute_DeserializeNullableNull()
    {
        string json = "{\"NullableBoolValue\":null}";

        // No converters registered in options — the attribute applies the converter automatically.
        TestClassWithBoolAttribute? deserialized = JsonSerializer.Deserialize<TestClassWithBoolAttribute>(json);

        Assert.NotNull(deserialized);
        Assert.Null(deserialized.NullableBoolValue);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void BoolConverter_WithAttribute_SerializeBool
    (
        bool value,
        string expected
    )
    {
        TestClassWithBoolAttribute testObject = new()
        {
            BoolValue = value
        };

        // No converters registered in options — the attribute applies the converter automatically.
        string json = JsonSerializer.Serialize(testObject);

        Assert.Contains($"\"BoolValue\":{expected}", json);
    }
}
