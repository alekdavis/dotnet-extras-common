using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DotNetExtras.Common;

/// <summary>
/// Think of the <see cref="NameOf"/> class as the 
/// <c><see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof">nameof</see></c> 
/// expression on steroids that can generate fully qualified or partial names of
/// variables, types, or members in the original case or <c>camelCase</c> notation.
/// </summary>
/// <remarks>
/// <para>
/// When applying the <see cref="NameOf"/> methods to types (vs. object properties),
/// use it with the <c>nameof</c> expression.
/// </para>
/// <para>
/// Special characters (<c>?</c>, <c>!</c>, <c>@</c>) will be removed from the object property names.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// // PRINT: Class.Parent.ChildProp
/// Console.WriteLine(NameOf.Full(nameof(Class.Parent.ChildProp)));
/// 
/// // PRINT: class.parent.childProp
/// Console.WriteLine(NameOf.Full(nameof(Class.Parent.ChildProp), true));
/// 
/// // PRINT: object.Parent.ChildProp
/// Console.WriteLine(NameOf.Full(myObject?.Parent?.ChildProp));
/// 
/// // PRINT: object.parent.childProp
/// Console.WriteLine(NameOf.Full(myObject?.Parent?.ChildProp, true));
/// 
/// // PRINT: Parent.ChildProp
/// Console.WriteLine(NameOf.Long(nameof(Class.Parent.ChildProp)));
/// 
/// // PRINT: parent.childProp
/// Console.WriteLine(NameOf.Long(nameof(Class.Parent.ChildProp), true));
/// 
/// // PRINT: Parent.ChildProp
/// Console.WriteLine(NameOf.Long(myObject?.Parent?.ChildProp));
/// 
/// // PRINT: parent.childProp
/// Console.WriteLine(NameOf.Long(myObject?.Parent?.ChildProp, true));
/// 
/// // PRINT: ChildProp
/// Console.WriteLine(NameOf.Short(nameof(Class.Parent.ChildProp)));
/// 
/// // PRINT: childProp
/// Console.WriteLine(NameOf.Short(nameof(Class.Parent.ChildProp), true));
/// 
/// // PRINT: ChildProp
/// Console.WriteLine(NameOf.Short(myObject?.Parent?.ChildProp));
/// 
/// // PRINT: childProp
/// Console.WriteLine(NameOf.Short(myObject?.Parent?.ChildProp, true));
/// </code>
/// </example>
public static class NameOf
{
    #region Public methods
    /// <summary>
    /// Returns full name of the object, class, type, or property.
    /// </summary>
    /// <param name="caller">
    /// Object, class, type, or property.
    /// </param>
    /// <param name="camelCase">
    /// Indicates whether to return the name in <c>camelCase</c>.
    /// </param>
    /// <param name="name">
    /// Must be omitted.
    /// </param>
    /// <returns>
    /// Full name including all compound parts.
    /// </returns>
    /// <example>
    /// <code>
    /// // PRINT: Class.Parent.ChildProp
    /// Console.WriteLine(NameOf.Full(nameof(Class.Parent.ChildProp)));
    /// 
    /// // PRINT: class.parent.childProp
    /// Console.WriteLine(NameOf.Full(nameof(Class.Parent.ChildProp), true));
    /// 
    /// // PRINT: object.Parent.ChildProp
    /// Console.WriteLine(NameOf.Full(myObject?.Parent?.ChildProp));
    /// 
    /// // PRINT: object.parent.childProp
    /// Console.WriteLine(NameOf.Full(myObject?.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Full
    (
        object? caller,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = RemoveNameOf(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = Normalize(name, camelCase).Split('.');

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = parts.Select(JsonNamingPolicy.CamelCase.ConvertName);
            result = string.Join('.', camelCaseParts);
        }
        else
        {
            result = string.Join('.', parts);
        }

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns the partial name of the object, class, type, or property
    /// omitting the entry before the first period (counting from left to right).
    /// </summary>
    /// <returns>
    /// Name without the first (left) compound prefix.
    /// </returns>
    /// <remarks>
    /// If the name does not include any compound parts, it will be returned as-is.
    /// </remarks>
    /// <example>
    /// <code>
    /// // PRINT: Parent.ChildProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent.ChildProp)));
    /// 
    /// // PRINT: parent.childProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent.ChildProp), true));
    /// 
    /// // PRINT: Parent.ChildProp
    /// Console.WriteLine(NameOf.Long(myObject?.Parent?.ChildProp));
    /// 
    /// // PRINT: parent.childProp
    /// Console.WriteLine(NameOf.Long(myObject?.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Long
    (
        object? caller,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        string result = Skip(caller, 1, camelCase, name);

        return string.IsNullOrEmpty(result)
            ? camelCase
                ? JsonNamingPolicy.CamelCase.ConvertName(RemoveNameOf(name))
                : RemoveNameOf(name)
            : result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns the short name of the immediate object property (same as nameof()).
    /// </summary>
    /// <returns>
    /// Short (immediate) name (without compound prefix).
    /// </returns>
    /// <example>
    /// <code>
    /// // PRINT: ChildProp
    /// Console.WriteLine(NameOf.Short(nameof(Class.Parent.ChildProp)));
    /// 
    /// // PRINT: childProp
    /// Console.WriteLine(NameOf.Short(nameof(Class.Parent.ChildProp), true));
    /// 
    /// // PRINT: ChildProp
    /// Console.WriteLine(NameOf.Short(myObject?.Parent?.ChildProp));
    /// 
    /// // PRINT: childProp
    /// Console.WriteLine(NameOf.Short(myObject?.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Short
    (
#pragma warning disable IDE0060 // Remove unused parameter
        object? caller,
#pragma warning restore IDE0060 // Remove unused parameter
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = RemoveNameOf(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = Normalize(name, camelCase).Split('.');
        string part = parts[^1];

        result = camelCase ? JsonNamingPolicy.CamelCase.ConvertName(part) : part;

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns a shortened name of the object, class, type, or property
    /// after removing the specified number of compound prefixes or suffixes.
    /// </summary>
    /// <param name="caller">
    /// Full or partial property name (can also be referenced using <c>nameof</c>).
    /// </param>
    /// <param name="count">
    /// Number of parts to be excluded from the result.
    /// A positive number indicates skipping from the left,
    /// a negative number indicates skipping from the right.
    /// If the skipped number is bigger than the number of parts,
    /// an empty string will be returned.
    /// </param>
    /// <param name="camelCase">
    /// If true, parameter names will be converted to <c>camelCase</c>.
    /// </param>
    /// <param name="name">
    /// Must be omitted.
    /// </param>
    /// <returns>
    /// Shortened name.
    /// </returns>
    /// <example>
    /// <code>
    /// // PRINT: Parent.ChildProp
    /// Console.WriteLine(NameOf.Skip(nameof(Class.Parent.ChildProp), 1));
    /// 
    /// // PRINT: Class.Parent
    /// Console.WriteLine(NameOf.Skip(nameof(Class.Parent.ChildProp), -1));
    /// 
    /// // PRINT: childProp
    /// Console.WriteLine(NameOf.Skip(nameof(Class.Parent.ChildProp), 2, true));
    /// 
    /// // PRINT: class
    /// Console.WriteLine(NameOf.Skip(nameof(Class.Parent.ChildProp), -2, true));
    /// 
    /// // PRINT: Parent.ChildProp
    /// Console.WriteLine(NameOf.Skip(object.Parent?.ChildProp), 1);
    /// 
    /// // PRINT: object.Parent
    /// Console.WriteLine(NameOf.Skip(object.Parent?.ChildProp), -1);
    /// 
    /// // PRINT: childProp
    /// Console.WriteLine(NameOf.Skip(object.Parent?.ChildProp, 2, true));
    /// 
    /// // PRINT: object
    /// Console.WriteLine(NameOf.Skip(object.Parent?.ChildProp, -2, true));
    /// </code>
    /// </example>
    public static string Skip
    (
        object? caller,
        [Range(1, int.MaxValue)]
        int count = 1,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = RemoveNameOf(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = Normalize(name, camelCase).Split('.');

        count = Math.Min(count, parts.Length);

        string[] resultParts = [.. Enumerable.Skip(parts, count)];

        if (resultParts.Length == 0)
        {
            return "";
        }

        string[] results;

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = resultParts.Select(JsonNamingPolicy.CamelCase.ConvertName);

            results = [.. camelCaseParts];
        }
        else
        {
            results = resultParts;
        }

        result = string.Join('.', results);

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns a shortened name of the object, class, type, or property
    /// keeping the specified number of compound prefixes or suffixes.
    /// </summary>
    /// <param name="caller">
    /// Full or partial property name (can also be referenced using <c>nameof</c>).
    /// </param>
    /// <param name="count">
    /// Number of parts to be included in the result.
    /// A positive number indicates including from the left,
    /// a negative number indicates including from the right.
    /// If the kept number is bigger than the number of parts,
    /// an empty string will be returned.
    /// </param>
    /// <param name="camelCase">
    /// If true, parameter names will be converted to <c>camelCase</c>.
    /// </param>
    /// <param name="name">
    /// Must be omitted.
    /// </param>
    /// <returns>
    /// Shortened name.
    /// </returns>
    /// <example>
    /// <code>
    /// // PRINT: Class
    /// Console.WriteLine(NameOf.Keep(nameof(Class.Parent.ChildProp), 1));
    /// 
    /// // PRINT: ChildProp
    /// Console.WriteLine(NameOf.Keep(nameof(Class.Parent.ChildProp), -1));
    /// 
    /// // PRINT: class.parent
    /// Console.WriteLine(NameOf.Keep(nameof(Class.Parent.ChildProp), 2, true));
    /// 
    /// // PRINT: parent.childProp
    /// Console.WriteLine(NameOf.Keep(nameof(Class.Parent.ChildProp), -2, true));
    /// 
    /// // PRINT: object
    /// Console.WriteLine(NameOf.Keep(object.Parent?.ChildProp), 1);
    /// 
    /// // PRINT: ChildProp
    /// Console.WriteLine(NameOf.Keep(object.Parent?.ChildProp), -1);
    /// 
    /// // PRINT: object.parent
    /// Console.WriteLine(NameOf.Keep(object.Parent?.ChildProp, 2, true));
    /// 
    /// // PRINT: parent.childProp
    /// Console.WriteLine(NameOf.Keep(object.Parent?.ChildProp, -2, true));
    /// </code>
    /// </example>
    public static string Keep
    (
        object? caller,
        [Range(1, int.MaxValue)]
        int count = 1,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = RemoveNameOf(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = Normalize(name, camelCase).Split('.');

        int skip = Math.Max(0, parts.Length - count);

        string[] resultParts = [.. Enumerable.Skip(parts, skip)];

        if (resultParts.Length == 0)
        {
            return "";
        }

        string[] results;

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = resultParts.Select(JsonNamingPolicy.CamelCase.ConvertName);

            results = [.. camelCaseParts];
        }
        else
        {
            results = resultParts;
        }

        result = string.Join('.', results);

        return result;
    }

    /// <summary>
    /// Removes special characters (<c>?</c>, <c>!</c>, <c>@</c>) from a compound object property name.
    /// </summary>
    /// <param name="name">
    /// Compound property name, such as <c>myObject?.PropertyX!.Value</c>.
    /// </param>
    /// <param name="camelCase">
    /// If true, the normalized name will be converted to camelCase.
    /// </param>
    /// <returns>
    /// Normalized compound property name.
    /// </returns>
    public static string Normalize
    (
        string name,
        bool camelCase = false
    )
    {
        string result = name?.Replace("?", "")?.Replace("!", "")?.Replace("@", "") ?? "";

        return camelCase 
            ? string.Join('.', result.Split('.').Select(JsonNamingPolicy.CamelCase.ConvertName))
            : result;
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Removes <c>nameof</c> from the name.
    /// </summary>
    /// <param name="name">
    /// Name that may the leading <c>nameof</c> prefix.
    /// </param>
    /// <returns>
    /// Normalized name.
    /// </returns>
    private static string RemoveNameOf
    (
        string? name
    )
    {
        if (string.IsNullOrEmpty(name))
        {
            return "";
        }

        if (name.StartsWith("nameof("))
        {
            name = name[7..^1];
        }

        return name;
    } 
    #endregion
}
