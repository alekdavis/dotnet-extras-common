using DotNetExtras.Common.Extensions;

namespace CommonLibTests.Extensions;

public class DateTimeExtensionTests
{
    [Theory]
    [InlineData(null, 0, null)]
    [InlineData("2025-10-30 17:16:15.987Z", 0, "2025-10-30T17:16:15Z")]
    [InlineData("2025-10-30 17:16:15.987Z", 3, "2025-10-30T17:16:15.987Z")]
    [InlineData("2025-10-30 17:16:15.987 +1:15", 3, "2025-10-30T16:01:15.987Z")]
    [InlineData("2025-10-30 17:16:15.9876543-01:15", 7, "2025-10-30T18:31:15.9876543Z")]
    public void DateTime_ToUniversalIso8601
    (
        string? dateTime,
        int precision,
        string? expected
    )
    {
        DateTime? value = dateTime == null ? null : DateTime.Parse(dateTime);

        string? result = value.ToUniversalIso8601(precision);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, null)]
    [InlineData("2025-10-30 17:16:15.987Z", 0, "2025-10-30T17:16:15Z")]
    [InlineData("2025-10-30 17:16:15.987Z", 3, "2025-10-30T17:16:15.987Z")]
    [InlineData("2025-10-30 17:16:15.987 +1:15", 3, "2025-10-30T16:01:15.987Z")]
    [InlineData("2025-10-30 17:16:15.9876543-01:15", 7, "2025-10-30T18:31:15.9876543Z")]
    public void DateTimeOffset_ToUniversalIso8601
    (
        string? dateTime,
        int precision,
        string? expected
    )
    {
        DateTimeOffset? value = dateTime == null ? null : DateTimeOffset.Parse(dateTime);

        string? result = value.ToUniversalIso8601(precision);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("2025-10-30 17:16:15Z", 0)]
    [InlineData("2025-10-30 17:16:15", 0)]
    [InlineData("2025-10-30T17:16:15Z", 0)]
    [InlineData("2025-10-30 17:16:15.987Z", 0)]
    [InlineData("2025-10-30 17:16:15.987Z", 3)]
    [InlineData("2025-10-30 17:16:15.987 +1:15", 3)]
    [InlineData("2025-10-30 17:16:15.9876543-01:15", 7)]
    public void DateTime_ToIso8601
    (
        string? dateTime,
        int precision
    )
    {
        DateTime? dateTimeParsed;
        string? result;

        if (dateTime == null)
        {
            dateTimeParsed = null;

            result = dateTimeParsed.ToIso8601();
            Assert.Null(result);
            return;
        }

        string? expected;

        if (dateTime.EndsWith('Z'))
        {
            dateTimeParsed = DateTime.Parse(dateTime);
        }
        else if (dateTime.Contains('-') || dateTime.Contains('+'))
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(dateTime);
            dateTimeParsed = dateTimeOffset.DateTime;
        }
        else
        {
            dateTimeParsed = DateTime.Parse(dateTime);
        }

        result = dateTimeParsed.ToIso8601(precision);
        expected = dateTimeParsed!.Value.ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : ""));

        Assert.StartsWith(expected, result);
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("2025-10-30 17:16:15Z", 0)]
    [InlineData("2025-10-30 17:16:15", 0)]
    [InlineData("2025-10-30T17:16:15Z", 0)]
    [InlineData("2025-10-30 17:16:15.987Z", 0)]
    [InlineData("2025-10-30 17:16:15.987Z", 3)]
    [InlineData("2025-10-30 17:16:15.987 +1:15", 3)]
    [InlineData("2025-10-30 17:16:15.9876543-01:15", 7)]
    public void DateTimeOffset_ToIso8601
    (
        string? dateTimeOffset,
        int precision
    )
    {
        DateTimeOffset? dateTimeOffsetParsed;
        string? result;

        if (dateTimeOffset == null)
        {
            dateTimeOffsetParsed = null;

            result = dateTimeOffsetParsed.ToIso8601();
            Assert.Null(result);
            return;
        }

        string? expected;

        dateTimeOffsetParsed = DateTimeOffset.Parse(dateTimeOffset);

        result = dateTimeOffsetParsed.ToIso8601(precision);
        expected = dateTimeOffsetParsed!.Value.ToString("yyyy-MM-ddTHH:mm:ss" + (precision > 0 ? "." + new string('f', precision) : ""));

        Assert.StartsWith(expected, result);
    }
}
