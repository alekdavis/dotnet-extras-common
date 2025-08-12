namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements the most frequently used extension methods,
/// such as escaping special characters, making sure the sting ends ina punctuation character, etc.,
/// for the <see cref="string"/> types.
/// </summary>
/// <seealso cref="Specialized"/>
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
    /// unless it already ends with one of the punctuation characters:
    /// ,.!?;:
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
        bool trimStart = true,
        bool trimEnd = true
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return "";
        }

        if (trimStart)
        {
            source = source.TrimStart();
        }

        if (trimEnd)
        {
            source = source.TrimEnd();
        }

        return string.IsNullOrEmpty(source)
            ? ""
            : System.Text.RegularExpressions.Regex.IsMatch(source, @"[\p{P}]$")
                ? source
                : source + ".";
    }
}
