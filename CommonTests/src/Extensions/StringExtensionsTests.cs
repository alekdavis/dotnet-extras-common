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
