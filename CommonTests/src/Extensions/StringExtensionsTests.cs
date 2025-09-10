// Ignore Spelling: Json

using DotNetExtras.Common.Extensions;

namespace CommonLibTests.Extensions;
public partial class StringExtensionsTests
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
    [InlineData(" Hello", "Hello.", true)]
    [InlineData("Hello ", "Hello.", true, true)]
    [InlineData(" Hello ", "Hello.", true, true)]
    [InlineData("Hello.", "Hello.")]
    [InlineData("Hello. ", "Hello.", true, true)]
    [InlineData("Hello!", "Hello!")]
    [InlineData("Hello?", "Hello?")]
    [InlineData("Hello,", "Hello,")]
    [InlineData("Hello;", "Hello;")]
    [InlineData("Hello:", "Hello:")]
    [InlineData("Hello 123", "Hello 123.")]
    [InlineData("  \t\r\r\nHello\t \n\r\t  123  \r\n", "Hello 123.", true, true, true)]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void String_ToSentence
    (
        string? source, 
        string expected,
        bool trimStart = false,
        bool trimEnd = false,
        bool compact = false
    )
    {
#pragma warning disable CS8604 // Possible null reference argument.
        string result = source.ToSentence(trimStart, trimEnd, compact);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }
}
