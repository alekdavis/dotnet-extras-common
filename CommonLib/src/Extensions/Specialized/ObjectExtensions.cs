using System.Dynamic;
using System.Reflection;

namespace DotNetExtras.Common.Extensions.Specialized;
/// <summary>
/// Implements advanced extension methods for the <see cref="object"/> types.
/// </summary>
public static partial class ObjectExtensions
{
    /// <summary>
    /// Converts any object to a dynamic object.
    /// </summary>
    /// <remarks>
    /// Adapted from
    /// <see href="https://stackoverflow.com/questions/42836936/convert-class-to-dynamic-and-add-propertyNames#answer-42837044"/>.
    /// For dictionary objects, 
    /// use the <see cref="DictionaryExtensions.ToDynamic(Dictionary{string, object?})"/> method instead.
    /// </remarks>
    /// <typeparam name="T">
    /// Data type of the source object.
    /// </typeparam>
    /// <param name="source">
    /// The original object.
    /// </param>
    /// <param name="extras">
    /// Additional propertyNames to be added to the expando object.
    /// </param>
    /// <param name="publicOnly">
    /// If true, only public properties will be included.
    /// </param>
    /// <returns>
    /// Expando object.
    /// </returns>
    /// <example>
    /// <code>
    /// User user = new()
    /// {
    ///     Name = new()
    ///     {
    ///         GivenName = "John",
    ///         Surname = "Doe"
    ///     },
    ///     Age = 42,
    ///     Mail = "John.Doe@mail.com",
    /// };
    ///
    /// Dictionary&lt;string, object&gt; extras = new()
    /// {
    ///     { "ExtraProperty", "XYZ" }
    /// };
    ///
    /// dynamic? result = user.ToDynamic(extras);
    /// </code>
    /// </example>
    public static dynamic? ToDynamic<T>
    (
        this T source,
        Dictionary<string, object>? extras = null,
        bool publicOnly = false
    )
    {
        IDictionary<string, object?> expando = new ExpandoObject();

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(
            BindingFlags.Instance | 
            BindingFlags.Public | 
            (publicOnly ? 0 : BindingFlags.NonPublic)))
        {
            object? currentValue = propertyInfo.GetValue(source);

            if (currentValue != null)
            {
                expando.Add(propertyInfo.Name, currentValue);
            }
        }

        if (extras != null)
        {
            foreach (string key in extras.Keys)
            {
                if (extras[key] != null)
                {
                    expando.Add(key, extras[key]);
                }
            }
        }

        return expando as ExpandoObject;
    }
}
