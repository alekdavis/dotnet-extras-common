using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DotNetExtras.Common.Extensions.Specialized;
/// <summary>
/// Implements advanced extension methods for the <see cref="string"/> types.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Converts a string value to the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Target data type.
    /// </typeparam>
    /// <param name="source">
    /// Original string value.
    /// </param>
    /// <returns>
    /// Converted value or default if conversion failed.
    /// </returns>
    /// <example>
    /// <code>
    /// bool b = "true".ToType&lt;bool&gt;();
    /// int n = "123".ToType&lt;int&gt;();
    /// DateTime dt = "2021-10-11T17:54:38".ToType&lt;DateTime&gt;();
    /// DateTimeOffset dto = "2021-10-11T17:54:38-03:30".ToType&lt;DateTimeOffset&gt;();
    /// </code>
    /// </example>
    public static T ToType<T>
    (
        this string source
    )
    {
        if (typeof(T) == typeof(DateTimeOffset))
        {
            return (T)(object)DateTimeOffset.Parse(source);
        }
        // Handling Nullable types i.e, int?, double?, bool? .. etc
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        return Nullable.GetUnderlyingType(typeof(T)) != null
            ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(source)
            : (T)Convert.ChangeType(source, typeof(T));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8603 // Possible null reference return.
    }

    /// <summary>
    /// Converts a string to a <see cref="System.DateTime"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit date/time format.
    /// </param>
    /// <returns>
    /// DateTime value.
    /// </returns>
    /// <example>
    /// <code>
    /// DateTime? dt1 = "2023-11-01T11:30:00+00:30".ToDateTime();
    /// DateTime? dt2 = "01/11/2023".ToDateTime("dd/MM/yyyy");
    /// DateTime? dt3 = "2023-11-01 12:30:00".ToDateTime("yyyy-MM-dd HH:mm:ss");
    /// </code>
    /// </example>
    public static DateTime? ToDateTime
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null 
                ? DateTime.Parse(source) 
                : DateTime.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a string to a <see cref="System.DateTimeOffset"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit date/time format.
    /// </param>
    /// <returns>
    /// DateTimeOffset value.
    /// </returns>
    /// <example>
    /// <code>
    /// DateTimeOffset? dto1 = "2023-11-01T11:30:00+00:30".ToDateTimeOffset();
    /// DateTimeOffset? dto2 = "2023-11-01T11:30:00+03:30".ToDateTimeOffset();
    /// DateTimeOffset? dto3 = "2023-12-01 16:30:00 +00:00".ToDateTimeOffset("yyyy-MM-dd HH:mm:ss zzz");
    /// </code>
    /// </example>
    public static DateTimeOffset? ToDateTimeOffset
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null 
                ? DateTimeOffset.Parse(source) 
                : DateTimeOffset.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a string to a <see cref="System.DateOnly"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit date format.
    /// </param>
    /// <returns>
    /// DateTime value.
    /// </returns>
    /// <example>
    /// <code>
    /// DateOnly? d1 = "2023-11-01".ToDateOnly();
    /// DateOnly? d2 = "01/11/2023".ToDateOnly("dd/MM/yyyy");
    /// </code>
    /// </example>
    public static DateOnly? ToDate
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null 
                ? DateOnly.Parse(source) 
                : DateOnly.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a string to a <see cref="System.TimeOnly"/> value.
    /// </summary>
    /// <param name="source">
    /// Original value.
    /// </param>
    /// <param name="format">
    /// Optional explicit time format.
    /// </param>
    /// <returns>
    /// DateTime value.
    /// </returns>
    /// <example>
    /// <code>
    /// TimeOnly? t1 = "23:11:01".ToTimeOnly();
    /// TimeOnly? t2 = "11:35:48 PM".ToTimeOnly("hh:mm:ss tt");
    /// </code>
    /// </example>
    public static TimeOnly? ToTime
    (
        this string source,
        string? format = null
    )
    {
        return source == null
            ? null
            : format == null 
                ? TimeOnly.Parse(source) 
                : TimeOnly.ParseExact(source, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts string to a list.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the list elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// List item delimiter string.
    /// </param>
    /// <returns>
    /// Generic list.
    /// </returns>
    /// <example>
    /// <code>
    /// // Will hold: key1=value1, key2=value2
    /// List&lt;string&gt; result = "value1|value2|value3".ToList&lt;string&gt;();
    /// </code>
    /// </example>    
    public static List<T>? ToList<T>
    (
        this string? source,
        string delimiter = "|"
    )
    {
        if (source == null)
        {
            return null;
        }

        string[] items = source.Split(delimiter);
        List<T> list = [];

        foreach (string item in items)
        {
            if (item.Trim() != "")
            {
                T value = (T)Convert.ChangeType(item, typeof(T));
                list.Add(value);
            }
        }

        return list;
    }

    /// <summary>
    /// Converts string to array.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the array elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// Array item delimiter string.
    /// </param>
    /// <param name="options">
    /// String splitting options.
    /// </param>
    /// <returns>
    /// Generic array.
    /// </returns>
    /// <example>
    /// <code>
    /// // Will hold: key1=value1, key2=value2
    /// string[] result = "value1|value2|value3".ToArray&lt;string&gt;();
    /// </code>
    /// </example>
    public static T[]? ToArray<T>
    (
        this string? source, 
        string delimiter = "|",
        StringSplitOptions options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
    )
    {
        return source?.Split(delimiter, options).Select(n => 
            (T)Convert.ChangeType(n, typeof(T))).ToArray<T>();
    }

    /// <summary>
    /// Converts string to a dictionary.
    /// </summary>
    /// <typeparam name="TKey">
    /// Data type of the dictionary key elements.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Data type of the dictionary value elements.
    /// </typeparam>
    /// <param name="source">
    /// String value.
    /// </param>
    /// <param name="delimiter">
    /// List item delimiter string.
    /// </param>
    /// <param name="keyValueSeparator">
    /// Name value delimiter string.
    /// </param>
    /// <param name="optionsPairs">
    /// Options for splitting pairs.
    /// </param>
    /// <param name="optionsKeyValue">
    /// Options for splitting key from value.
    /// </param>
    /// <returns>
    /// Generic dictionary.
    /// </returns>
    /// <example>
    /// <code>
    /// // Will hold: key1=value1, key2=value2
    /// Dictionary&lt;string,string&gt; result = "key1=value1|key2=value2".ToDictionary&lt;string, string&gt;();
    /// </code>
    /// </example>
    public static Dictionary<TKey,TValue>? ToDictionary<TKey,TValue>
    (
        this string? source,
        string delimiter = "|",
        string keyValueSeparator = "=",
        StringSplitOptions optionsPairs = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries,
        StringSplitOptions optionsKeyValue = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
    )
    where TKey : notnull
    {
        if (source == null)
        {
            return null;
        }

        Dictionary<TKey,TValue> dictionary = [];

        if (string.IsNullOrEmpty(source))
        {
            return dictionary;
        }

        string[] pairs = source.Split(delimiter, optionsPairs);

        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(keyValueSeparator, optionsKeyValue);

            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();

                string value = keyValue[1].Trim();

                if (key != "")
                {
                    try
                    {
                        dictionary[(TKey)Convert.ChangeType(key, typeof(TKey))] = (TValue)Convert.ChangeType(value, typeof(TValue));
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException($"Cannot convert key '{key}' to '{typeof(TKey).Name}' or '{value}' to '{typeof(TValue).Name}'.", ex);
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid key{keyValueSeparator}value pair '{pair}': it is expected to be split into 2 parts, but resulted in {keyValue.Length}.");
            }
        }

        return dictionary;
    }

    /// <summary>
    /// Converts string to a hash set.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the hash set elements.
    /// </typeparam>
    /// <param name="source">
    /// Input string.
    /// </param>
    /// <param name="delimiter">
    /// Delimiter character.
    /// </param>
    /// <param name="options">
    /// String splitting options.
    /// </param>
    /// <returns>
    /// Generic hash set.
    /// </returns>
    /// <example>
    /// <code>
    /// // Will hold: [1, 2, 3]
    /// HashSet&lt;int&gt;? hashSet = "1|2|3".ToHashSet();
    /// 
    /// // Will hold: ["one", "two", "three"]
    /// HashSet&lt;string&gt;? hashSet = "one,two,three".ToHashSet(",");
    /// </code>
    /// </example>
    public static HashSet<T>? ToHashSet<T>
    (
        this string? source,
        string delimiter = "|",
        StringSplitOptions options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
    )
    {
        if (source == null)
        {
            return null;
        }

        string[] items = source.Split(delimiter, options);

        HashSet<T> hashSet = [];

        foreach (string item in items)
        {
            if (item.Trim() != "")
            {
                T value = (T)Convert.ChangeType(item, typeof(T));
                hashSet.Add(value);
            }
        }

        return hashSet;
    }

    /// <summary>
    /// Checks if the string contains valid JSON text.
    /// </summary>
    /// <param name="source">
    /// String to test.
    /// </param>
    /// <returns>
    /// True if the string is a valid JSON, otherwise false.
    /// </returns>
    public static bool IsJson
    (
        this string? source
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        try
        {
            using JsonDocument doc = JsonDocument.Parse(source);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string contains an HTML document.
    /// </summary>
    /// <remarks>
    /// A valid HTML document assumes that test starts with 
    /// either `&lt;!DOCTYPE html&gt;` or `&lt;html&gt;` tag.
    /// </remarks>
    /// <param name="source">
    /// String to test.
    /// </param>
    /// <returns>
    /// True if the string contains a valid HTML document, otherwise false.
    /// </returns>
    public static bool IsHtml
    (
        this string? source
    )
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        try
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(source, "^[\\s\\t\\r\\n]*<!DOCTYPE[\\s\\t\\r\\n]+html", RegexOptions.IgnoreCase) ||
                System.Text.RegularExpressions.Regex.IsMatch(source, "^[\\s\\t\\r\\n]*<html", RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Escapes special LDAP characters.
    /// </summary>
    /// <param name="input">
    /// String value.
    /// </param>
    /// <returns>
    /// Escaped value.
    /// </returns>
    public static string? EscapeLdapValue
    (
        this string input
    )
    {
        return string.IsNullOrEmpty(input) 
            ? input 
            : input.Contains('\\') || 
              input.Contains(',') || 
              input.Contains('#') || 
              input.Contains('+') || 
              input.Contains('<') || 
              input.Contains('>') || 
              input.Contains(';') || 
              input.Contains('"') || 
              input.Contains('=') 
                ? input.Replace("\\", $"\\\\")
                    .Replace(",", "\\,")
                    .Replace("#", "\\#")
                    .Replace("+", "\\+")
                    .Replace("<", "\\<")
                    .Replace(">", "\\>")
                    .Replace(";", "\\;")
                    .Replace("\"", "\\\"")
                    .Replace("=", "\\=")
                : input;
    }
}
