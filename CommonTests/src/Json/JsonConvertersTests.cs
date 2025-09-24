// Ignore Spelling: Json

using DotNetExtras.Common.Json.Converters;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonLibTests.Json;
public class TestClass
{
    public DateTime? DateTimeValue { get; set; }

    public DateTimeOffset? DateTimeOffsetValue { get; set; }
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
    [InlineData("2023-10-05",               "2023-10-05T00:00:00.0")]
    [InlineData("2023-10-05T14:48:30",      "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.0",    "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.10",   "2023-10-05T14:48:30.10")]
    [InlineData("2023-10-05T14:48:30.123Z", "2023-10-05T14:48:30.123Z")]
    [InlineData("2023-10-05 14:48:30",      "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 2:48:30 PM",    "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 14:48:30-3:30", "2023-10-05T14:48:30-03:30")]
    [InlineData("2023-10-05 14:48:30 +3:30","2023-10-05T14:48:30+03:30")]
    public void DateTimeConverter_Deserialize
    (
        string? jsonValue,
        string? expectedValue
    )
    {
        string json = jsonValue ==  null ? $"{{\"DateTimeValue\":null}}" : $"{{\"DateTimeValue\":\"{jsonValue}\"}}";

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
    [InlineData("2023-10-05",               "2023-10-05T00:00:00.0")]
    [InlineData("2023-10-05T14:48:30",      "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.0",    "2023-10-05T14:48:30.0")]
    [InlineData("2023-10-05T14:48:30.10",   "2023-10-05T14:48:30.10")]
    [InlineData("2023-10-05T14:48:30.123Z", "2023-10-05T14:48:30.123Z")]
    [InlineData("2023-10-05 14:48:30",      "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 2:48:30 PM",    "2023-10-05 14:48:30")]
    [InlineData("2023-10-05 14:48:30-3:30", "2023-10-05T14:48:30-03:30")]
    [InlineData("2023-10-05 14:48:30 +3:30","2023-10-05T14:48:30+03:30")]
    public void DateTimeOffsetConverter_Deserialize
    (
        string? jsonValue,
        string? expectedValue
    )
    {
        string json = jsonValue ==  null ? $"{{\"DateTimeOffsetValue\":null}}" : $"{{\"DateTimeOffsetValue\":\"{jsonValue}\"}}";

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
}
