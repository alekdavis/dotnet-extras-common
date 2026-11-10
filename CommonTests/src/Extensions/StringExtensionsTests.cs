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

    [Fact]
    public void String_ToSentence_PreserveQuoted()
    {
        // preserveQuoted defaults to true: whitespace inside quotes is kept,
        // while whitespace outside quotes is compacted.
        Assert.Equal(
            "Workgroup 'Approver  All Roles' is broken.",
            "Workgroup   'Approver  All Roles'   is \t broken.".ToSentence(true, true, true));

        // Double quotes behave the same way.
        Assert.Equal(
            "Value \"first  one\" and \"second  two\"",
            "Value   \"first  one\"   and   \"second  two\"".ToSentence(true, true, true));

        // Opting out collapses whitespace everywhere, including inside quotes.
        Assert.Equal(
            "Workgroup 'Approver All Roles' is broken.",
            "Workgroup   'Approver  All Roles'   is \t broken.".ToSentence(true, true, true, false));

        // An unbalanced quote does not throw; trailing text is compacted.
        Assert.Equal(
            "Unterminated 'value with spaces.",
            "Unterminated   'value  with   spaces".ToSentence(true, true, true));
    }

    [Fact]
    public void String_ToSentence_PreserveQuoted_EmbeddedApostrophes()
    {
        // Apostrophes inside a single-quoted span are treated as literal characters,
        // not as closing delimiters, so the whole value is preserved verbatim.
        Assert.Equal(
            "Change name from 'John's' to 'Jane's'.",
            "Change name from   'John's'   to   'Jane's'.".ToSentence(true, true, true));

        // Multiple embedded apostrophes with inner double spaces are preserved.
        Assert.Equal(
            "The value 'Dick's  Smith's' is invalid.",
            "The value   'Dick's  Smith's'   is \t invalid.".ToSentence(true, true, true));

        // A double-quoted span containing single quotes is preserved as-is.
        Assert.Equal(
            "Use \"it's  a  test\" here.",
            "Use   \"it's  a  test\"   here.".ToSentence(true, true, true));

        // A leading-letter apostrophe name (O'Brien) outside quotes never opens a
        // span, so the text is simply compacted.
        Assert.Equal(
            "Error for O'Brien here.",
            "Error   for   O'Brien   here.".ToSentence(true, true, true));

        // O'Brien inside a quoted span is preserved verbatim, including inner spacing.
        Assert.Equal(
            "User 'O'Brien  Jr.' not found.",
            "User   'O'Brien  Jr.'   not \t found.".ToSentence(true, true, true));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("Hello, world!", "Hello, world!")]
    [InlineData("Café", "Cafe")]
    [InlineData("naïve", "naive")]
    [InlineData("résumé", "resume")]
    [InlineData("coöperate", "cooperate")]
    [InlineData("São Paulo", "Sao Paulo")]
    [InlineData("El Niño", "El Nino")]
    [InlineData("français", "francais")]
    [InlineData("Über", "Uber")]
    [InlineData("façade", "facade")]
    [InlineData("crème brûlée", "creme brulee")]
    [InlineData("ÀÁÂÃÄÅĀĂĄ", "AAAAAAAAA")]
    [InlineData("àáâãäåāăą", "aaaaaaaaa")]
    [InlineData("Čč Ďď Ěě Šš Ťť Žž", "Cc Dd Ee Ss Tt Zz")]
    [InlineData("Æther", "AEther")]
    [InlineData("encyclopædia", "encyclopaedia")]
    [InlineData("Œuvre", "OEuvre")]
    [InlineData("cœur", "coeur")]
    [InlineData("ﬃciency", "fficiency")]   // ﬃ -> ffi
    [InlineData("Aﬂoat", "Afloat")]        // ﬂ -> fl
    [InlineData("Straße", "Strasse")]
    [InlineData("ẞ in caps", "SS in caps")]
    [InlineData("“Hello”", "\"Hello\"")]
    [InlineData("‘single’", "'single'")]
    [InlineData("l’âme", "l'ame")]
    [InlineData("one–two—three‑four", "one-two-three-four")]
    [InlineData("Ｔｅｓｔ", "Test")]
    [InlineData("Ｆｕｌｌｗｉｄｔｈ：！", "Fullwidth:!")]
    [InlineData("“Quote”, ‘single’, 50％", "\"Quote\", 'single', 50%")]
    [InlineData("Number①②③", "Number123")]
    [InlineData("⒈ ⑴ ⒜", "1. (1) (a)")]
    [InlineData("⑩ ⑳", "10 20")]
    [InlineData("｛Test｝［A］（B）❮C❯", "{Test}[A](B)\"C\"")]
    [InlineData("faßŒ", "fassOE")]
    [InlineData("Łukasz Rafał", "Lukasz Rafal")]
    [InlineData("Привет", "Привет")]
    [InlineData("Привет", "", true)]
    [InlineData("Привет", "??????", true, '?')]
    [InlineData("タクミたくみ", "タクミたくみ")]
    [InlineData("タクミ", "", true)]
    [InlineData("タクミ", "???", true, '?')]
    [InlineData("גדעון", "גדעון")]
    public void String_ToAscii
    (
        string? input,
        string? expected,
        bool strictAscii = false,
        char? asciiDefault = null
    )
    {
        string? output = input?.ToAscii(strictAscii, asciiDefault);

        Assert.Equal(expected, output);
    }
}
