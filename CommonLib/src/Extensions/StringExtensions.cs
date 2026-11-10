using System.Text;
using System.Text.RegularExpressions;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements extension methods
/// applicable to strings,
/// such as escaping special characters, 
/// converting accented characters to their ASCII equivalents, 
/// making sure the sting ends in a punctuation character, etc.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Escapes specific characters in a string.
    /// </summary>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="escapeChar">
    /// Specifies the character that must be escaped.
    /// </param>
    /// <param name="replacementString">
    /// Specifies the replacement string for the escaped character 
    /// (may need to include the escaped character).
    /// </param>
    /// <returns>
    /// String value with properly escaped character.
    /// </returns>
    /// <example>
    /// <code>
    /// // escaped = in "It''s a test".
    /// string escaped = "It's a test".Escape();
    /// </code>
    /// </example>
    public static string? Escape
    (
        this string source,
        char escapeChar = '\'',
        string replacementString = "''"
    )
    {
        return string.IsNullOrEmpty(source)
            ? source
            : source.Replace($"{escapeChar}", $"{replacementString}");
    }

    /// <summary>
    /// Appends a period at the end of the string,
    /// unless it already ends with one of the punctuation characters.
    /// </summary>
    /// <param name="source">
    /// Input string.
    /// </param>
    /// <param name="trimStart">
    /// Indicates that white space characters must be trimmed from the string start.
    /// </param>
    /// <param name="trimEnd">
    /// Indicates that white space characters must be trimmed from the string end.
    /// </param>
    /// <param name="compact">
    /// Indicates that the multiple space and new line characters will be converted to a single space.
    /// </param>
    /// <param name="preserveQuoted">
    /// When <paramref name="compact"/> is <c>true</c>, indicates that the content inside
    /// balanced single- or double-quoted spans is preserved verbatim (whitespace and
    /// special characters intact), while text outside quotes is compacted as usual.
    /// </param>
    /// <returns>
    /// Input string that has a valid punctuation string at the end.
    /// </returns>
    /// <example>
    /// <code>
    /// // PRINTS: Hello, world.
    /// Console.WriteLine(" Hello, world  ".ToSentence());
    /// </code>
    /// </example>
    public static string ToSentence
    (
        this string source,
        bool trimStart = false,
        bool trimEnd = false,
        bool compact = false,
        bool preserveQuoted = true
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return "";
        }

        if (trimStart)
        {
            source = LeadingWhitespaceRegex().Replace(source, "");
        }

        if (trimEnd)
        {
            source = TrailingWhitespaceRegex().Replace(source, "");
        }

        if (compact)
        {
            source = preserveQuoted
                ? CompactOutsideQuotes(source)
                : WhitespaceRegex().Replace(source, " ");
        }

        return string.IsNullOrEmpty(source)
            ? ""
            : EndsWithPunctuationRegex().IsMatch(source)
                ? source
                : source + ".";
    }

    /// <summary>
    /// Collapses runs of whitespace to a single space in all text outside balanced
    /// single- or double-quoted spans, while copying quoted spans (including their
    /// whitespace and special characters) verbatim.
    /// </summary>
    /// <param name="input">
    /// String to compact.
    /// </param>
    /// <returns>
    /// Compacted string with quoted values preserved.
    /// </returns>
    /// <remarks>
    /// A quote character is recognized as an opening delimiter only when it appears
    /// at the start of the string or is preceded by whitespace, and as a closing
    /// delimiter only when it appears at the end of the string or is followed by
    /// whitespace or punctuation. This word-boundary heuristic allows embedded
    /// apostrophes (e.g. <c>'Dick's Smith's'</c>) to be treated as literal
    /// characters rather than as closing delimiters.
    /// </remarks>
    private static string CompactOutsideQuotes
    (
        string input
    )
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        StringBuilder result = new();
        StringBuilder segment = new();
        char quoteChar = '\0';
        bool insideQuotes = false;

        void FlushCompacted()
        {
            result.Append(WhitespaceRegex().Replace(segment.ToString(), " "));
            segment.Clear();
        }

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (insideQuotes)
            {
                if (c == quoteChar && IsClosingDelimiter(input, i))
                {
                    result.Append(quoteChar).Append(segment).Append(quoteChar);
                    segment.Clear();
                    insideQuotes = false;
                }
                else
                {
                    segment.Append(c);
                }
            }
            else if ((c == '\'' || c == '"') && IsOpeningDelimiter(input, i))
            {
                FlushCompacted();
                quoteChar = c;
                insideQuotes = true;
            }
            else
            {
                segment.Append(c);
            }
        }

        // An unbalanced opening quote and any text after it are normal prose.
        if (insideQuotes)
        {
            result.Append(quoteChar);
        }

        FlushCompacted();

        return result.ToString();
    }

    /// <summary>
    /// Determines whether the quote character at the specified position starts a
    /// quoted span (it is at the start of the string or preceded by whitespace).
    /// </summary>
    /// <param name="input">
    /// String being scanned.
    /// </param>
    /// <param name="index">
    /// Zero-based position of the quote character.
    /// </param>
    /// <returns>
    /// <c>true</c> if the quote character opens a span; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsOpeningDelimiter
    (
        string input,
        int index
    )
    {
        return index == 0 || char.IsWhiteSpace(input[index - 1]);
    }

    /// <summary>
    /// Determines whether the quote character at the specified position closes a
    /// quoted span (it is at the end of the string or followed by whitespace or
    /// punctuation).
    /// </summary>
    /// <param name="input">
    /// String being scanned.
    /// </param>
    /// <param name="index">
    /// Zero-based position of the quote character.
    /// </param>
    /// <returns>
    /// <c>true</c> if the quote character closes a span; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsClosingDelimiter
    (
        string input,
        int index
    )
    {
        if (index == input.Length - 1)
        {
            return true;
        }

        char next = input[index + 1];

        return char.IsWhiteSpace(next) || char.IsPunctuation(next);
    }

    /// <summary>
    /// Masks characters in the input string allowing to keep the specified number of
    /// characters in the beginning and/or end of the string unmasked.
    /// </summary>
    /// <param name="input">
    /// Input string.
    /// </param>
    /// <param name="maskChar">
    /// Mask character.
    /// </param>
    /// <param name="keepCharsStart">
    /// Number of characters at the beginning of the string to keep unmasked.
    /// </param>
    /// <param name="keepCharsEnd">
    /// Number of characters at the end of the string to keep unmasked.
    /// </param>
    /// <returns>
    /// Masked string.
    /// </returns>
    /// <remarks>
    /// Depending if the input string is shorter than the sum of the characters
    /// to be kept unmasked in the beginning and/or end of the input string
    /// then the original string will be returned as-is.
    /// </remarks>
    /// <example>
    /// <code>
    /// // PRINTS: He#########d!
    /// Console.WriteLine("Hello, world!".Mask('#', 2, 1));
    /// </code>
    /// </example>
    public static string? Mask
    (
        this string input,
        char maskChar = '*',
        int keepCharsStart = 0,
        int keepCharsEnd = 0
    )
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (keepCharsStart < 0)
        {
            keepCharsStart = 0;
        }

        if (keepCharsEnd < 0)
        {
            keepCharsEnd = 0;
        }

        if (keepCharsStart == 0 && keepCharsEnd == 0)
        {
            return new string(maskChar, input.Length);
        }

        if (keepCharsStart + keepCharsEnd >= input.Length)
        {
            return input;
        }

        string start = keepCharsStart == 0 ? "" : input[..keepCharsStart];
        string end = keepCharsEnd == 0 ? "" : input[^keepCharsEnd..];
        string middle = new(maskChar, input.Length - keepCharsStart - keepCharsEnd);

        return start + middle + end;
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"^\s+")]
    private static partial Regex LeadingWhitespaceRegex();

    [GeneratedRegex(@"\s+$")]
    private static partial Regex TrailingWhitespaceRegex();

    [GeneratedRegex(@"[\p{P}]$")]
    private static partial Regex EndsWithPunctuationRegex();
}
