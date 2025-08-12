using System.Collections;
using System.Reflection;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements the most frequently used extension methods,
/// such as deep cloning, checking object equivalency, 
/// getting and setting a nested property value by a compound name, and more,
/// for all (any <see cref="object"/>) types.
/// </summary>
/// <seealso cref="Specialized"/>
public static partial class ObjectExtensions
{
    #region Private properties
        // When getting and setting properties,
        // treat names as case sensitive (we need to specify all flags to make it work).
        private static readonly BindingFlags _BINDING_FLAGS =
            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance;
    #endregion

    #region Public methods
    /// <summary>
    /// Determines whether the specified object 
    /// has no properties or fields holding non-null values or non-empty collections.
    /// </summary>
    /// <param name="source">
    /// The object to check.
    /// </param>
    /// <param name="publicOnly">
    /// If <c>true</c>, only public properties and fields will be checked.
    /// </param>
    /// <returns>
    /// <c>true</c> if the object is empty; otherwise, <c>false</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// User? u1 = new();
    /// Assert.True(u1.IsEmpty());
    ///
    /// User? u2 = new()
    /// {
    ///     Id = "123"
    /// };
    /// Assert.False(u2.IsEmpty());
    /// </code>
    /// </example>
    public static bool IsEmpty
    (
        this object? source,
        bool publicOnly = false
    )
    {
        if (source == null)
        {
            return true;
        }

        Type type = source.GetType();

        // Check all public and internal properties
        foreach (PropertyInfo propertyInfo in type.GetProperties(
            BindingFlags.Instance |
            BindingFlags.Public |
            (publicOnly ? 0 : BindingFlags.NonPublic)))
        {
            if (propertyInfo.GetIndexParameters().Length == 0) // Ignore indexers
            {
                object? value = propertyInfo.GetValue(source);

                if (value != null)
                {
                    if (!IsEmptyValue(value))
                    {
                        return false;
                    }
                }
            }
        }

        // Check all public and internal fields
        foreach (FieldInfo field in type.GetFields(
            BindingFlags.Instance |
            BindingFlags.Public  |
            (publicOnly ? 0 : BindingFlags.Public)))
        {
            object? value = field.GetValue(source);

            if (value != null)
            {
                if (!IsEmptyValue(value))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Returns the value of the immediate or nested object property.
    /// </summary>
    /// <param name="source">
    /// Object that owns the property.
    /// </param>
    /// <param name="name">
    /// Name of the property (case-insensitive; can be compound with names separated by periods).
    /// </param>
    /// <returns>
    /// Property value (or <c>null</c>, if property does not exists).
    /// </returns>
    /// <remarks>
    /// <para>
    /// The code assumes that the property exists;
    /// if it does not, the code will return <c>null</c>.
    /// </para>
    /// <para>
    /// The property can be nested.
    /// </para>
    /// <para>
    /// The code handles both class properties and fields.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// string? givenName = user.GetPropertyValue("Name.GivenName");
    /// int? age = user.GetPropertyValue("Age");
    /// </code>
    /// </example>
    public static object? GetPropertyValue
    (
        this object? source,
        string name
    )
    {
        if (source == null)
        {
            return null;
        }

        name = NormalizePropertyName(name);

        if (name.Contains('.'))
        {
            string[] names = name.Split(['.'], 2);

            return GetPropertyValue(GetPropertyValue(source, names[0]), names[1]);
        }
        else
        {
            PropertyInfo? property = source.GetType().GetProperty(name, _BINDING_FLAGS);

            if (property != null)
            {
                return property?.GetValue(source);
            }

            FieldInfo? field = source.GetType().GetField(name);

            return field?.GetValue(source);
        }
    }

    /// <summary>
    /// Sets the new value of an immediate or nested object property
    /// (creating parent properties if needed).
    /// </summary>
    /// <param name="target">
    /// Object that owns the property to be set. 
    /// </param>
    /// <param name="name">
    /// Name of the property (case-insensitive; can be compound with names separated by periods).
    /// </param>
    /// <param name="value">
    /// New property value.
    /// </param>
    /// <remarks>
    /// Adapted from <see href="https://stackoverflow.com/a/54006015/52545"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// user.SetPropertyValue("Name.GivenName", "Smith");
    /// user.GetPropertyValue("Age", 42);
    /// </code>
    /// </example>
    public static void SetPropertyValue
    (
        this object? target,
        string name,
        object? value
    )
    {
        if (target == null)
        {
            return;
        }

        name = NormalizePropertyName(name);

        string[] propertyNames = name.Split('.');

        for (int i = 0; i < propertyNames.Length - 1; i++)
        {
            PropertyInfo? propertyToGet = target?.GetType().GetProperty(propertyNames[i], _BINDING_FLAGS);

            object? propertyValue = propertyToGet?.GetValue(target, null);

            if (propertyValue == null && propertyToGet != null)
            {
                if (propertyToGet.PropertyType.IsClass)
                {
                    propertyValue = Activator.CreateInstance(propertyToGet.PropertyType);
                    propertyToGet.SetValue(target, propertyValue);
                }
            }

            target = propertyValue;
        }

        PropertyInfo? propertyToSet = target?.GetType().GetProperty(propertyNames.Last(), _BINDING_FLAGS);

        propertyToSet?.SetValue(target, value);
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Determines whether the specified value is empty.
    /// A value is considered empty if it is null, an empty string, an empty collection, or an empty enumerable.
    /// </summary>
    /// <param name="value">
    /// The value to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is empty; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsEmptyValue
    (
        object? value
    )
    {
        return value == null || (value is not string && (value is ICollection collection
            ? collection.Count == 0
            : value is IEnumerable enumerable
                ? !enumerable.Cast<object>().Any()
                : !value.GetType().IsValueType && value.IsEmpty()));
    }

    /// <summary>
    /// Converts slashes to periods in the compound property names.
    /// </summary>
    /// <param name="name">
    /// Compound property name.
    /// </param>
    /// <returns>
    /// Normalized property name with slashes replaced by periods.
    /// </returns>
    private static string NormalizePropertyName
    (
        string name
    )
    {
        return NameOf.Normalize(name.Replace('/', '.'));
    }
    #endregion
}
