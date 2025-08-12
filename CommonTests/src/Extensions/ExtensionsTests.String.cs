using DotNetExtras.Common;
using DotNetExtras.Common.Extensions;
using DotNetExtras.Common.Extensions.Specialized;

namespace CommonLibTests;
public partial class ExtensionsTests
{
    [Theory]
    [InlineData("Hello", "Hello")]
    [InlineData("It's a test", "It''s a test")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void String_Escape
    (
        string? source, 
        string? expected
    )
    {
        string? result = source?.Escape();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Hello", "Hello.")]
    [InlineData(" Hello", "Hello.")]
    [InlineData("Hello ", "Hello.")]
    [InlineData(" Hello ", "Hello.")]
    [InlineData("Hello.", "Hello.")]
    [InlineData("Hello. ", "Hello.")]
    [InlineData("Hello!", "Hello!")]
    [InlineData("Hello?", "Hello?")]
    [InlineData("Hello,", "Hello,")]
    [InlineData("Hello;", "Hello;")]
    [InlineData("Hello:", "Hello:")]
    [InlineData("Hello 123", "Hello 123.")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void String_ToSentence
    (
        string? source, 
        string expected
    )
    {
#pragma warning disable CS8604 // Possible null reference argument.
        string result = source.ToSentence();
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }

    [Fact]
    public void String_ToType()
    {
        Assert.Equal((short)123, "123".ToType<short>());
        Assert.Equal((short)123, "123".ToType<short?>());
        Assert.Equal(123, "123".ToType<int>());
        Assert.Equal(123, "123".ToType<int?>());
        Assert.Equal(123, "123".ToType<long>());
        Assert.Equal(123, "123".ToType<long?>());
        Assert.Equal((double)123.45, "123.45".ToType<double>());
        Assert.Equal((double)123.45, "123.45".ToType<double?>());
        Assert.Equal((float)123.45, "123.45".ToType<float>());
        Assert.Equal((float)123.45, "123.45".ToType<float?>());
        Assert.Equal(new DateTime(2021, 10, 11, 17, 54, 38), "2021-10-11T17:54:38".ToType<DateTime>());
        Assert.Equal(new DateTime(2021, 10, 11, 17, 54, 38), "2021-10-11T17:54:38".ToType<DateTime?>());
        Assert.Equal(new DateTimeOffset(2021, 10, 11, 17, 54, 38, new TimeSpan(-3, -30, 0)), "2021-10-11T17:54:38-03:30".ToType<DateTimeOffset>());
        Assert.Equal(new DateTimeOffset(2021, 10, 11, 17, 54, 38, new TimeSpan(-3, -30, 0)), "2021-10-11T17:54:38-03:30".ToType<DateTimeOffset?>());
        Assert.True("true".ToType<bool>());
        Assert.False("false".ToType<bool>());
    }

    [Theory]
    [InlineData("2023-11-01", null, 2023, 11, 1)]
    [InlineData("01/11/2023", "dd/MM/yyyy", 2023, 11, 1)]
    [InlineData("2023-11-01 12:30:00", "yyyy-MM-dd HH:mm:ss", 2023, 11, 1, 12, 30, 0)]
    public void String_ToDateTime
    (
        string input, 
        string? format,
        int year, 
        int month, 
        int day, 
        int hour = 0, 
        int minute = 0, 
        int second = 0
    )
    {
        DateTime? result = input.ToDateTime(format);

        Assert.NotNull(result);
        Assert.Equal(new DateTime(year, month, day, hour, minute, second), result);
    }

    [Theory]
    [InlineData("2023-11-01T11:30:00+03:30", null, "2023-11-01T11:30:00+03:30")]
    [InlineData("2023-10-01T14:30:00-03:30", null, "2023-10-01T14:30:00-03:30")]
    [InlineData("2023-12-01 16:30:00 +00:00", "yyyy-MM-dd HH:mm:ss zzz", "2023-12-01T16:30:00+00:00")]
    public void String_ToDateTimeOffset
    (
        string input, 
        string? format, 
        string expected
    )
    {
        DateTimeOffset? result = input.ToDateTimeOffset(format);

        Assert.NotNull(result);

        Assert.Equal(expected, result?.ToString("yyyy-MM-ddTHH:mm:sszzz"));
    }

    [Theory]
    [InlineData("2023-11-01", null, "2023-11-01")]
    [InlineData("2023/10/01", null, "2023-10-01")]
    [InlineData("12-01-2023", "MM-dd-yyyy", "2023-12-01")]
    public void String_ToDate
    (
        string input, 
        string? format, 
        string expected
    )
    {
        DateOnly? result = input.ToDate(format);

        Assert.NotNull(result);

        Assert.Equal(expected, result?.ToString("yyyy-MM-dd"));
    }

    [Theory]
    [InlineData("23:31:01", null, "23:31:01")]
    [InlineData("23:31:05", "HH:mm:ss", "23:31:05")]
    [InlineData("23:31", "HH:mm", "23:31:00")]
    [InlineData("11:31 pm", "hh:mm tt", "23:31:00")]
    [InlineData("11:31 AM", "hh:mm tt", "11:31:00")]
    public void String_ToTime
    (
        string input, 
        string? format, 
        string expected
    )
    {
        TimeOnly? result = input.ToTime(format);

        Assert.NotNull(result);

        Assert.Equal(expected, result?.ToString("HH:mm:ss"));
    }

    [Fact]
    public void String_ToList()
    {
        Assert.Equal([1, 2, 3], "1|2|3".ToList<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToList<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToList<long>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToList<string>());
    }

    [Fact]
    public void String_ToArray()
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0305 // Simplify collection initialization
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToArray<long>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToArray<string>());
#pragma warning restore IDE0305 // Simplify collection initialization
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void String_ToDictionary()
    {
        Assert.Equal(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        }, "key1=value1|key2=value2".ToDictionary<string, string>());

        Assert.Equal(new Dictionary<int, int>
        {
            { 12, 144 },
            { 13, 169 }
        }, "12=144|13=169".ToDictionary<int, int>());
    }

    [Fact]
    public void String_ToHashSet()
    {
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<int>());
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<short>());
        Assert.Equal([1, 2, 3], "1|2|3".ToHashSet<long>());
        Assert.Equal([(double)1.1, (double)2.2, (double)3.3], "1.1|2.2|3.3".ToHashSet<double>());
        Assert.Equal([(float)1.1, (float)2.2, (float)3.3], "1.1|2.2|3.3".ToHashSet<float>());
        Assert.Equal(["hello", "my", "world"], "hello|my|world".ToHashSet<string>());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("hello", false)]
    [InlineData("{hello}", false)]
    [InlineData("[hello]", false)]
    [InlineData("{}", true)]
    [InlineData("{\"key\": \"value\"}", true)]
    [InlineData(" {\"key\": \"value\"}", true)]
    [InlineData("{\"key\": \"value\"} ", true)]
    [InlineData("\t{\"key\": \"value\"}", true)]
    [InlineData("\t{\"key\": \"value\"}\t", true)]
    [InlineData("\r{\"key\": \"value\"}", true)]
    [InlineData("{\"key\": \"value\"}\r", true)]
    [InlineData("\n{\"key\": \"value\"}", true)]
    [InlineData("{\"key\": \"value\"}\n", true)]
    [InlineData(" \t \r\n{\"key\": \"value\"}", true)]
    [InlineData("{\"key\": \"value\"} \t \r\n", true)]
    [InlineData(" \t \r\n{\"key\": \"value\"} \t \r\n", true)]
    [InlineData("{\"key1\": \"value1\", \"key2\": \"value2\"}", true)]
    [InlineData("[]", true)]
    [InlineData("[{\"key1\": \"value1\"}, {\"key2\": \"value2\", \"key3\": 123}]", true)]
    public void String_IsJson
    (
        string? source,
        bool isJson
    )
    {
        Assert.Equal(isJson, source.IsJson());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("hello", false)]
    [InlineData("<p>hello</p>", false)]
    [InlineData("<xml>hello</xml>", false)]
    [InlineData("<html>hello</html>", true)]
    [InlineData(" \t\r\n <html>hello</html> \t\r\n ", true)]
    [InlineData("<!DOCTYPE html>hello</html>", true)]
    [InlineData(" \t\r\n <!DOCTYPE html>hello</html> \t\r\n ", true)]
    [InlineData("<!DOCTYPE  \t\r\n html>hello</html>", true)]
    [InlineData(" \t\r\n <!DOCTYPE  \t\r\n html>hello</html> \t\r\n ", true)]
    [InlineData("<!DOCTYPE html PUBLIC>hello</html>", true)]
    [InlineData(" \t\r\n <!DOCTYPE html PUBLIC>hello</html> \t\r\n ", true)]
    public void String_IsHtml
    (
        string? source,
        bool isHtml
    )
    {
        Assert.Equal(isHtml, source.IsHtml());
    }
}
