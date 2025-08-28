using System.Text.RegularExpressions;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements the most frequently used extension methods,
/// such as escaping special characters, making sure the sting ends in a punctuation character, etc.,
/// for the <see cref="string"/> types.
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
    /// // PRINTS: "Hello, world."
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
}
