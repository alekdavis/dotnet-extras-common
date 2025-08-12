using DotNetExtras.Common.Extensions.Specialized;
using DotNetExtras.Common.Utilities;
using System.Reflection;

namespace DotNetExtras.Common.Extensions;
public static partial class ObjectExtensions
{
    #region Private members
    private static readonly MethodInfo? _cloneMethodNonPublic = typeof(object).GetMethod("MemberwiseClone",
        BindingFlags.Instance |
        BindingFlags.NonPublic);
    #endregion

    #region Public methods
    /// <summary>
    /// Returns a deep copy of a strongly typed object (does not require type casting).
    /// </summary>
    /// <typeparam name="T">
    /// Object type.
    /// </typeparam>
    /// <param name="original">
    /// Original object.
    /// </param>
    /// <returns>
    /// Cloned object.
    /// </returns>
    /// <remarks>
    /// Adapted from 
    /// <see href="https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// MyObject? cloneObject = originalObject.Clone();
    /// </code>
    /// </example>
    public static T? Clone<T>
    (
        this T original
    )
    {
        return original == null 
            ? default 
            : (T)Clone((object?)original!)!;
    }

    /// <inheritdoc cref="Clone{T}(T)" path="param|returns|remarks"/>
    /// <summary>
    /// Returns a deep copy of an object (requires type casting).
    /// </summary>
    /// <example>
    /// <code>
    /// MyObject? cloneObject = (MyObject?)originalObject.Clone();
    /// </code>
    /// </example>
    public static object? Clone
    (
        this object original
    )
    {
        return Copy(original, new Dictionary<object, object>(new Utilities.ReferenceEqualityComparer()));
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Creates a deep copy of the specified object.
    /// </summary>
    /// <param name="original">
    /// The original object to copy.
    /// </param>
    /// <param name="visited">
    /// A dictionary to keep track of visited objects to handle circular references.
    /// </param>
    /// <returns>
    /// A deep copy of the original object.
    /// </returns>
    private static object? Copy
    (
        object? original,
        IDictionary<object, object> visited
    )
    {
        if (original == null)
        {
            return null;
        }

        Type type = original.GetType();

        if (type.IsPrimitive())
        {
            return original;
        }

        if (visited.ContainsKey(original))
        {
            return visited[original];
        }

        if (typeof(Delegate).IsAssignableFrom(type))
        {
            return null;
        }

        object? clone = _cloneMethodNonPublic?.Invoke(original, null);

        if (type.IsArray)
        {
            Type? elementType = type.GetElementType();

            if (elementType != null && !elementType.IsPrimitive())
            {
#pragma warning disable CS8604
#pragma warning disable CS8602
#pragma warning disable CS8600
                Array clonedArray = (Array)clone;
                clonedArray.ForEach((array, indices) =>
                    array.SetValue(Copy(clonedArray.GetValue(indices), visited), indices));
#pragma warning restore CS8600
#pragma warning restore CS8602
#pragma warning restore CS8604
            }
        }

#pragma warning disable CS8604
        visited.Add(original, clone);
        CopyFields(original, visited, clone, type);
        CopyBaseTypePrivateFields(original, visited, clone, type);
#pragma warning restore CS8604

        return clone;
    }

    /// <summary>
    /// Recursively copies the fields of the specified object.
    /// </summary>
    /// <param name="original">
    /// The original object to copy.
    /// </param>
    /// <param name="visited">
    /// A dictionary to keep track of visited objects to handle circular references.
    /// </param>
    /// <param name="clone">
    /// The clone object to populate with copied fields.
    /// </param>
    /// <param name="type">
    /// The type of the object to reflect on.
    /// </param>
    private static void CopyBaseTypePrivateFields
    (
        object original,
        IDictionary<object, object> visited,
        object clone,
        Type type
    )
    {
        if (type.BaseType != null)
        {
            CopyBaseTypePrivateFields(original, visited, clone, type.BaseType);

            CopyFields(original, visited, clone, type.BaseType,
                BindingFlags.Instance | BindingFlags.NonPublic,
                info => info.IsPrivate);
        }
    }

    /// <summary>
    /// Copies the fields of the specified object.
    /// </summary>
    /// <param name="original">
    /// The original object to copy.
    /// </param>
    /// <param name="visited">
    /// A dictionary to keep track of visited objects to handle circular references.
    /// </param>
    /// <param name="clone">
    /// The clone object to populate with copied fields.
    /// </param>
    /// <param name="type">
    /// The type of the object to reflect on.
    /// </param>
    /// <param name="bindingFlags">
    /// The binding flags to use for reflection.
    /// </param>
    /// <param name="filter">
    /// An optional filter to apply to the fields.
    /// </param>
    private static void CopyFields
    (
        object original,
        IDictionary<object, object> visited,
        object clone,
        Type type,
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy,
        Func<FieldInfo, bool>? filter = null
    )
    {
        foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
        {
            if (filter != null && filter(fieldInfo) == false)
            {
                continue;
            }

            if (fieldInfo.FieldType.IsPrimitive())
            {
                continue;
            }

            object? originalFieldValue = fieldInfo.GetValue(original);
            object? clonedFieldValue = Copy(originalFieldValue, visited);

            fieldInfo.SetValue(clone, clonedFieldValue);
        }
    } 
    #endregion
}

