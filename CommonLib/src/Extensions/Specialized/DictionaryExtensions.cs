using System.Dynamic;

namespace DotNetExtras.Common.Extensions.Specialized;

/// <summary>
/// Implements advanced extension methods for the <see cref="Dictionary{TKey, TValue}"/> types.
/// </summary>
public static partial class DictionaryExtensions
{
    /// <summary>
    /// Converts a string dictionary object to a dynamic object.
    /// </summary>
    /// <remarks>
    /// For non-dictionary objects,
    /// use the <see cref="ObjectExtensions.ToDynamic{T}(T, Dictionary{string,object}, bool)"/> method instead.
    /// </remarks>
    /// <param name="source">
    /// Dictionary object.
    /// </param>
    /// <returns>
    /// Expando object.
    /// </returns>
    /// <example>
    /// <code>
    /// Dictionary&lt;string, object?&gt; dictionary = new()
    /// {
    ///     { "Key1", "Value1" },
    ///     { "Key2", 123 },
    ///     { "Key3", true }
    /// };
    /// 
    /// dynamic result1 = dictionary.ToDynamic();
    /// </code>
    /// </example>
    public static dynamic ToDynamic
    (
        this Dictionary<string, object?> source
    )
    {
        dynamic target = source.Aggregate(
            new ExpandoObject() as IDictionary<string, object?>,
                (a, p) =>
                {
                    a.Add(p);
                    return a;
                });

        return target;
    }
}
