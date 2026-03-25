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
        bool compact = false
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return "";
        }

        if (trimStart)
        {
            source = Regex.Replace(source, "^[\\s\n\r]+", "");
        }

        if (trimEnd)
        {
            source = Regex.Replace(source, "[\\s\n\r]+$", "");
        }

        if (compact)
        {
            source = Regex.Replace(source, "[\\s\n\r]+", " ");
        }

        return string.IsNullOrEmpty(source)
            ? ""
            : Regex.IsMatch(source, @"[\p{P}]$")
                ? source
                : source + ".";
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
}
