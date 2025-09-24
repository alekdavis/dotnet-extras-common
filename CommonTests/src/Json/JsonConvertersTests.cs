// Ignore Spelling: Json

using DotNetExtras.Common.Json.Converters;
using System.Text.Json;

namespace CommonLibTests.Json;
public class TestClass
{
    public DateTime? DateTimeValue { get; set; }

    public DateTimeOffset? DateTimeOffsetValue { get; set; }
}

public class JsonConvertersTests
{
    [Fact]
    public void DateTimeConverter_Serialize()
    {
        TestClass testObject = new()
        {
            DateTimeValue = new DateTime(2023, 10, 5, 14, 48, 30, 123, DateTimeKind.Utc),
            DateTimeOffsetValue = new DateTimeOffset(2023, 10, 5, 14, 48, 30, 123, TimeSpan.Zero)
        };

        string json = JsonSerializer.Serialize(testObject, new JsonSerializerOptions
        {
            Converters = { new JsonDateTimeConverter(), new JsonDateTimeOffsetConverter() },
            WriteIndented = false
        });

        Assert.Equal("{\"DateTimeValue\":\"2023-10-05T14:48:30.123Z\",\"DateTimeOffsetValue\":\"2023-10-05T14:48:30.123+00:00\"}", json);

        testObject = new()
        {
            DateTimeValue = null,
            DateTimeOffsetValue = null
        };

        json = JsonSerializer.Serialize(testObject, new JsonSerializerOptions
        {
            Converters = { new JsonDateTimeConverter(), new JsonDateTimeOffsetConverter() },
            WriteIndented = false
        });

        Assert.Equal("{\"DateTimeValue\":null,\"DateTimeOffsetValue\":null}", json);

        testObject = new()
        {
            DateTimeValue = new DateTime(2023, 10, 5, 0, 0, 0, 0),
            DateTimeOffsetValue = new DateTimeOffset(2023, 10, 5, 0, 0, 0, 0, TimeSpan.Zero)
        };

        json = JsonSerializer.Serialize(testObject, new JsonSerializerOptions
        {
            Converters = { new JsonDateTimeConverter(), new JsonDateTimeOffsetConverter() },
            WriteIndented = false
        });

        Assert.Equal("{\"DateTimeValue\":\"2023-10-05T00:00:00\",\"DateTimeOffsetValue\":\"2023-10-05T00:00:00+00:00\"}", json);
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
