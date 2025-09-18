using System.Collections;

namespace DotNetExtras.Common.Extensions;

/// <summary>
/// Implements extension methods
/// applicable to the <see cref="IEnumerable"/> types or parameters,
/// such as getting the count of items in a collection,
/// converting a collection of generic elements to a comma-separated string value,
/// checking if a value is in a collection.
/// </summary>
public static partial class IEnumerableExtensions
{
    /// <summary>
    /// Returns the number of items in any collection type.
    /// </summary>
    /// <param name="source">
    /// Any type of collection.
    /// </param>
    /// <returns>
    /// Number of items.
    /// </returns>
    public static int Count
    (
        this IEnumerable source
    )
    {
        if (source == null)
        {
            return 0;
        }

        if (source is ICollection collection)
        {
            return collection.Count;
        }

        int count = 0;
        IEnumerator e = source.GetEnumerator();
        while (e.MoveNext())
        {
            count++;
        }

        if (e is IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
            }
        }

        return count;
    }

    /// <summary>
    /// Converts a collection of generic elements to a comma-separated string value.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the generic elements.
    /// </typeparam>
    /// <param name="values">
    /// Collection of generic elements.
    /// </param>
    /// <param name="separator">
    /// Value separator.
    /// </param>
    /// <param name="leftQuote">
    /// Left quote enclosing each value.
    /// </param>
    /// <param name="rightQuote">
    /// Right quote enclosing each value 
    /// (if left quote is specified and right quote is not, then left quote will be used as right quote).
    /// </param>
    /// <returns>
    /// Comma-(or whatever)-separated string value (or empty string if collection is null or empty).
    /// </returns>
    /// <example>
    /// <code>
    /// List&lt;int&gt; numbers = new List&lt;int&gt;(){ 1, 2, 3, 4, 5 };
    /// 
    /// // Output: 1, 2, 3, 4, 5
    /// Console.WriteLine(numbers.ToCsv()); 
    ///
    /// List&lt;string&gt; words = new List&lt;string&gt;(){ "apple", "banana", "cherry" };
    /// // Output: "apple", "banana", "cherry"
    /// Console.WriteLine(words.ToCsv(", ", "\"", "\"")); 
    /// </code>
    /// </example>
    public static string ToCsv<T>
    (
        this IEnumerable<T> values,
        string separator = ", ",
        string leftQuote = "",
        string rightQuote = ""
    )
    {
        if (string.IsNullOrEmpty(rightQuote))
        {
            rightQuote = leftQuote;
        }

        return values == null || !values.Any<T>() 
            ? "" 
            : string.Join(separator, values.Select(item => leftQuote + item + rightQuote));
    }

    /// <summary>
    /// Checks if a value is in the list.
    /// </summary>
    /// <typeparam name="T">
    /// Value data type.
    /// </typeparam>
    /// <param name="value">
    /// Value to be checked.
    /// </param>
    /// <param name="values">
    /// List of values.
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is found in the list; otherwise, <c>false</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// // Returns true:
    /// 5.In(1, 2, 3, 4, 5);
    /// 
    /// // Returns false:
    /// 5.In(1, 2, 3, 4);
    /// ]]>
    /// </code>
    /// </example>
    public static bool In<T>
    (
        this T value,
        params T[] values
    ) 
    {
        return values != null && values.Contains(value);
    }

    /// <summary>
    /// Checks if a string value is in the list.
    /// </summary>
    /// <param name="value">
    /// Value to be checked.
    /// </param>
    /// <param name="ignoreCase">
    /// If <c>true</c>, case sensitivity will be ignored.
    /// </param>
    /// <param name="values">
    /// List of values.
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is found in the list; otherwise, <c>false</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// // Returns true:
    /// "one".In(false, "one, "two", "three");
    /// 
    /// // Returns true:
    /// "one".In(true, "ONE, "TWO", "THREE");
    /// 
    /// // Returns false:
    /// "one".In(true, "two", "three");
    /// 
    /// // Returns false:
    /// "one".In(false, "two", "three");
    /// ]]>
    /// </code>
    /// </example>
    public static bool In
    (
        this string value,
        bool ignoreCase,
        params string[] values
    ) 
    {
        return values != null && values.Contains(value, ignoreCase ? StringComparer.OrdinalIgnoreCase  : null);
    }

    /// <summary>
    /// Checks if a case-sensitive string value is in the list.
    /// </summary>
    /// <param name="value">
    /// Value to be checked.
    /// </param>
    /// <param name="values">
    /// List of values.
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is found in the list; otherwise, <c>false</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// // Returns true:
    /// "one".In("one, "two", "three");
    /// 
    /// // Returns false:
    /// "one".In("ONE, "TWO", "THREE");
    /// 
    /// // Returns false:
    /// "one".In("two", "three");
    /// ]]>
    /// </code>
    /// </example>
    public static bool In
    (
        this string value,
        params string[] values
    ) 
    {
        return In(value, false, values);
    }
}
