using System.ComponentModel;
using System.Reflection;

namespace DotNetExtras.Common.Enums;

/// <summary>
/// Implements extension methods for the <see cref="Enum"/> types.
/// </summary>
public static partial class EnumExtensions
{
    /// <summary>
    /// Gets the value of an attribute applied to an enumerated data type.
    /// </summary>
    /// <typeparam name="T">
    /// Data type.
    /// </typeparam>
    /// <param name="value">
    /// Enumerated value.
    /// </param>
    /// <returns>
    /// Attribute value.
    /// </returns>
    /// <example>
    /// <code>
    /// DescriptionAttribute? attribute = enumValue.GetAttribute&lt;DescriptionAttribute&gt;();
    /// </code>
    /// </example>
    public static T? GetAttribute<T>
    (
        this Enum value
    ) 
    where T : Attribute
    {
        Type            type        = value.GetType();
        MemberInfo[]    memberInfo  = type.GetMember(value.ToString());
        object[]        attributes  = memberInfo[0].GetCustomAttributes(typeof(T), false);

        return attributes == null
            ? null
            : attributes.Length > 0
                ? attributes[0] as T
                : null;
    }

    /// <summary>
    /// Returns the value of the <see cref="DescriptionAttribute"/> applied to an enumerated field.
    /// </summary>
    /// <param name="value">
    /// Enumerated field value.
    /// </param>
    /// <returns>
    /// <see cref="DescriptionAttribute"/> value (or null, if the attribute is not applied).
    /// </returns>
    /// <seealso cref="DescriptionAttribute"/>
    /// <example>
    /// <code>
    /// private enum TestEnum
    /// {
    ///     [Description("Description of value 1.")]
    ///     Value1,
    ///   
    ///     [Description("Description of value 2.")]
    ///     Value2,
    /// } 
    /// 
    /// public void Enum_ToAbbreviation()
    /// {
    ///     Assert.Equal("Description of value 1.", TestEnum.Value1.ToDescription());
    /// }
    /// </code>
    /// </example>
    public static string? ToDescription
    (
        this Enum value
    )
    {
        DescriptionAttribute? attribute = value.GetAttribute<DescriptionAttribute>();
        return attribute?.Description;
    }

    /// <summary>
    /// Returns the value of the <see cref="AbbreviationAttribute"/> applied to an enumerated field.
    /// </summary>
    /// <param name="value">
    /// Enumerated field value.
    /// </param>
    /// <returns>
    /// <see cref="AbbreviationAttribute"/> value (or null, if the attribute is not applied).
    /// </returns>
    /// <seealso cref="AbbreviationAttribute"/>
    /// <example>
    /// <code>
    /// private enum TestEnum
    /// {
    ///     [Abbreviation("VAL1")]
    ///     Value1,
    ///   
    ///     [Abbreviation("VAL2")]
    ///     Value2,
    /// } 
    /// 
    /// public void Enum_ToAbbreviation()
    /// {
    ///     TestEnum value = TestEnum.Value1;
    /// 
    ///     string? abbreviation = value.ToAbbreviation();
    /// 
    ///     Assert.Equal("VAL1", abbreviation);
    /// }
    /// </code>
    /// </example>
    public static string? ToAbbreviation
    (
        this Enum value
    )
    {
        AbbreviationAttribute? attribute = value.GetAttribute<AbbreviationAttribute>();
        return attribute?.Abbreviation;
    }

    /// <summary>
    /// Returns the value of the <see cref="ShortNameAttribute"/> applied to an enumerated field.
    /// </summary>
    /// <param name="value">
    /// Enumerated field value.
    /// </param>
    /// <returns>
    /// <see cref="ShortNameAttribute"/> value (or null, if the attribute is not applied).
    /// </returns>
    /// <seealso cref="ShortNameAttribute"/>
    /// <example>
    /// <code>
    /// private enum TestEnum
    /// {
    ///     [ShortName("V1")]
    ///     Value1,
    ///
    ///     [ShortName("V2")]
    ///     Value2,
    /// }
    /// 
    /// public void Enum_ToShortName()
    /// {
    ///     TestEnum value = TestEnum.Value1;
    /// 
    ///     string? shortName = value.ToShortName();
    /// 
    ///     Assert.Equal("V1", abbreviation);
    /// }
    /// </code>
    /// </example>
    public static string? ToShortName
    (
        this Enum value
    )
    {
        ShortNameAttribute? attribute = value.GetAttribute<ShortNameAttribute>();
        return attribute?.ShortName;
    }
}
