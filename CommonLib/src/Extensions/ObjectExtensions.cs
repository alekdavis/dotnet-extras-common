using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements frequently used extension methods
/// applicable to all data types,
/// such as deep cloning, checking object equivalence, 
/// getting and setting a nested property value by a compound name, and more.
/// </summary>
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
            BindingFlags.Public |
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
    /// <typeparam name="T">
    /// Data type of the property value.
    /// </typeparam>
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
    public static T? GetPropertyValue<T>
    (
        this object? source,
        string name
    )
    {
        object? value = GetPropertyValue(source, name);

        return value is null or not T ? default : (T?)value;
    }

    ///  <inheritdoc cref="GetPropertyValue{T}(object?, string)" path="summary|param|returns|remarks"/>
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

    /// <inheritdoc cref="IsEquivalentOf(object?, object?, bool)" />
    [Obsolete("Use IsEquivalentOf instead.")]
    public static bool IsEquivalentTo
    (
        this object? source,
        object? target,
        bool includeNonPublic = false
    )
    {
        return IsEquivalentOf(source, target, includeNonPublic);
    }

    /// <summary>
    /// Checks if the source object is identical to the target (comparing all instance properties and fields). 
    /// </summary>
    /// <param name="source">
    /// Object we are comparing.
    /// </param>
    /// <param name="target">
    /// Object we're comparing to.
    /// </param>
    /// <param name="includeNonPublic">
    /// If <c>true</c>, non-public properties and fields will be checked along with the public properties and fields.
    /// </param>
    /// <returns>
    /// True if objects are equivalent, otherwise, false.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The idea was adapted from
    /// <see href="https://stackoverflow.com/questions/10454519/best-way-to-compare-two-complex-objects"/>.
    /// </para>
    /// <para>
    /// The source and target objects are considered equivalent if their data types are compatible (e.g. <c>int</c> and <c>long</c> are compatible) and they meet the following criteria:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <para>For simple types, such as <c>string</c>, <c>int</c>, <c>DateTime</c>, etc., both the source and target values can be typecast to the same type and found equal.</para>
    /// </item>
    /// <item>
    /// <para>For arrays, list, and collection types, both the source and target must contain the same number of elements, and each element of the source must be equivalent to the corresponding item of the target.</para>
    /// </item>
    /// <item>
    /// <para>For dictionaries, both the source and target must contain the same number of elements, and each element of the source must be equivalent to the corresponding item of the target found under the same key.</para>
    /// </item>
    /// <item>
    /// <para>For hash sets, both the source and target must contain the same number of elements, and the values in both hash sets must match.</para>
    /// </item>
    /// <item>
    /// <para>Comparison of string values is case-sensitive.</para>
    /// </item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// User source = new()
    /// {
    ///     Id = 1,
    ///     Email = "joe@mail.com",
    ///     Name = new()
    ///     {
    ///         GivenName = "Joe",
    ///     },
    /// };
    /// 
    /// User target = new()
    /// {
    ///     Id = 1,
    ///     Email = "joe@mail.com",
    ///     Name = new()
    ///     {
    ///         GivenName = "Joe",
    ///     },
    /// };
    /// 
    /// Assert.True(source.IsEquivalentOf(target));
    /// Assert.True(target.IsEquivalentOf(source));
    /// </code>
    /// </example>
    /// <seealso cref="IsPartialEquivalentOf(object?, object?, bool)"/>
    public static bool IsEquivalentOf
    (
        this object? source,
        object? target,
        bool includeNonPublic = false
    )
    {
        if (source == null && target == null)
        {
            return true;
        }

        if (source == null || target == null)
        {
            return false;
        }

        if (ReferenceEquals(source, target))
        {
            return true;
        }

        Type sourceType = source.GetType();
        Type targetType = target.GetType();

        if (source is bool && target is bool)
        {
            return source.Equals(target);
        }
        else if (source is bool && target is string tBool)
        {
            tBool = tBool.ToLower();

            bool? b = tBool == "true" ? true : tBool == "false" ? false : null;

            return b.HasValue ? source.Equals(b.Value) : source.Equals(target);
        }
        else if (target is bool && source is string sBool)
        {
            sBool = sBool.ToLower();

            bool? b = sBool == "true" ? true : sBool == "false" ? false : null;

            return b.HasValue ? target.Equals(b.Value) : target.Equals(source);
        }
        else if (source is bool && (target is short || target is int || target is long || target is ushort || target is uint || target is ulong))
        {
            long tLong = Convert.ToInt64(target);

            bool? b = tLong == 1 ? true : tLong == 0 ? false : null;

            return b.HasValue ? source.Equals(b.Value) : source.Equals(target);
        }
        else if (target is bool && (source is short || source is int || source is long || source is ushort || source is uint || source is ulong))
        {
            long sLong = Convert.ToInt64(source);

            bool? b = sLong == 1 ? true : sLong == 0 ? false : null;

            return b.HasValue ? target.Equals(b.Value) : target.Equals(source);
        }
        else if (source is string && targetType == typeof(string))
        {
            return source.Equals(target);
        }
        else if (source is StringBuilder && target is StringBuilder)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is StringBuilder && target is string)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is string && target is StringBuilder)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is DateTime && target is DateTime)
        {
            return source.Equals(target);
        }
        else if (source is DateTime && target is string tStringDt)
        {
            DateTime dt;
            if (tStringDt.Contains('+') || tStringDt.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(tStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTimeOffset dto))
                {
                    return false;
                }

                dt = dto.UtcDateTime;
            }
            else
            {
                dt = DateTime.Parse(tStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            return source.Equals(dt);
        }
        else if (target is DateTime && source is string sStringDt)
        {
            DateTime dt;
            if (sStringDt.Contains('+') || sStringDt.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(sStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTimeOffset dto))
                {
                    return false;
                }

                dt = dto.UtcDateTime;
            }
            else
            {
                dt = DateTime.Parse(sStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            return target.Equals(dt);
        }
        else if (source is DateTime sDt && target is DateTimeOffset tDto)
        {
            DateTimeOffset dto = new(sDt);

            return tDto.Equals(dto);
        }
        else if (source is DateTimeOffset && target is DateTimeOffset)
        {
            return source.Equals(target);
        }
        else if (source is DateTimeOffset && target is string tString)
        {
            DateTimeOffset dto;
            if (tString.Contains('+') || tString.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(tString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dto))
                {
                    return false;
                }
            }
            else
            {
                if (!DateTime.TryParse(tString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime dt))
                {
                    return false;
                }

                dto = new(dt);
            }

            return source.Equals(dto);
        }
        else if (target is DateTimeOffset && source is string sString)
        {
            DateTimeOffset dto;
            if (sString.Contains('+') || sString.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(sString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dto))
                {
                    return false;
                }
            }
            else
            {
                if (!DateTime.TryParse(sString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime dt))
                {
                    return false;
                }

                dto = new(dt);
            }

            return target.Equals(dto);
        }
        else if (source is DateTimeOffset sDto && target is DateTime tDt)
        {
            DateTimeOffset dto = new(tDt);

            return sDto.Equals(dto);
        }
        else if (sourceType.IsEnum && targetType.IsEnum)
        {
            return source.Equals(target);
        }
        else if (sourceType.IsEnum && (target is short || target is int || target is long || target is ushort || target is uint || target is ulong))
        {
            return Convert.ToInt64(source) == Convert.ToInt64(target);
        }
        else if (sourceType.IsEnum && target is string)
        {
            return (source?.ToString() ?? "").ToUpper().Equals(target.ToString()?.ToUpper());
        }
        else if (targetType.IsEnum && (source is short || source is int || source is long || source is ushort || source is uint || source is ulong))
        {
            return Convert.ToInt64(target) == Convert.ToInt64(source);
        }
        else if (targetType.IsEnum && source is string)
        {
            return (target?.ToString() ?? "").ToUpper().Equals(source.ToString()?.ToUpper());
        }
        else if (sourceType.IsSimple() && targetType.IsSimple())
        {
            return source.ToString() == target.ToString();
        }
        else if (sourceType.IsArray && targetType.IsArray)
        {
            Array sourceArray = (Array)source;
            Array targetArray = (Array)target;

            if (sourceArray.Length != targetArray.Length)
            {
                return false;
            }

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (!IsEquivalentOf(sourceArray.GetValue(i), targetArray.GetValue(i), includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IList).IsAssignableFrom(sourceType) && typeof(IList).IsAssignableFrom(targetType))
        {
            IList sourceList = (IList)source;
            IList targetList = (IList)target;

            if (sourceList.Count != targetList.Count)
            {
                return false;
            }

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (!IsEquivalentOf(sourceList[i], targetList[i], includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IDictionary).IsAssignableFrom(sourceType) && typeof(IDictionary).IsAssignableFrom(targetType))
        {
            IDictionary sourceDictionary = (IDictionary)source;
            IDictionary targetDictionary = (IDictionary)target;

            if (sourceDictionary.Count != targetDictionary.Count)
            {
                return false;
            }

            foreach (object? key in sourceDictionary.Keys)
            {
                if (!targetDictionary.Contains(key) ||
                    !IsEquivalentOf(sourceDictionary[key], targetDictionary[key], includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(HashSet<>) &&
            targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(HashSet<>))
        {
            IEnumerable sourceEnumerable = (IEnumerable)source;
            IEnumerable targetEnumerable = (IEnumerable)target;

            if (sourceEnumerable.Count() != targetEnumerable.Count())
            {
                return false;
            }

            foreach (object? sourceItem in sourceEnumerable.Cast<object?>())
            {
                foreach (object? targetItem in targetEnumerable.Cast<object?>())
                {
                    if (IsEquivalentOf(sourceItem, targetItem, includeNonPublic))
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
        else if (sourceType.IsClass && targetType.IsClass)
        {
            HashSet<string> skip = [];

            foreach (PropertyInfo property in sourceType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                if (property.CanRead)
                {
                    skip.Add(property.Name);
                    object? sourceValue = property.GetValue(source);
                    object? targetValue = property.GetValue(target);

                    if (!IsEquivalentOf(sourceValue, targetValue, includeNonPublic))
                    {
                        return false;
                    }
                }
            }

            foreach (PropertyInfo property in targetType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                if (property.CanRead)
                {
                    if (skip.Contains(property.Name))
                    {
                        continue;
                    }

                    object? targetValue = property.GetValue(target);

                    if (!IsEquivalentOf(null, targetValue, includeNonPublic))
                    {
                        return false;
                    }
                }
            }

            skip.Clear();

            foreach (FieldInfo field in sourceType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                skip.Add(field.Name);

                object? sourceValue = field.GetValue(source);
                object? targetValue = field.GetValue(target);

                if (!IsEquivalentOf(sourceValue, targetValue, includeNonPublic))
                {
                    return false;
                }
            }

            foreach (FieldInfo field in targetType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                if (skip.Contains(field.Name))
                {
                    continue;
                }

                object? targetValue = field.GetValue(target);

                if (!IsEquivalentOf(null, targetValue, includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            return source.Equals(target);
        }
    }

    /// <inheritdoc cref="IsPartialEquivalentOf(object?, object?, bool)" />
    [Obsolete("Use IsPartialEquivalentOf instead.")]
    public static bool IsPartiallyEquivalentTo
    (
        this object? source,
        object? target,
        bool includeNonPublic = false
    )
    {
        return IsPartialEquivalentOf(source, target, includeNonPublic);
    }

    /// <inheritdoc cref="IsPartialEquivalentOf(object?, object?, bool)" />
    [Obsolete("Use IsPartialEquivalentOf instead.")]
    public static bool IsPartialEquivalentTo
    (
        this object? source,
        object? target,
        bool includeNonPublic = false
    )
    {
        return IsPartialEquivalentOf(source, target, includeNonPublic);
    }

    /// <summary>
    /// Checks if the source value is <c>null</c> or an equivalent of a target value;
    /// or if every element of a source array, list or dictionary is a subset of the corresponding target array, list or dictionary,
    /// or if every property and field of a source object is a subset of the corresponding property or field of the target object.
    /// </summary>
    /// <param name="source">
    /// Object we are comparing (if <c>null</c>, it is treated as a partially equivalent).
    /// </param>
    /// <param name="target">
    /// Object we're comparing to.
    /// </param>
    /// <param name="includeNonPublic">
    /// If <c>true</c>, non-public properties and fields will be checked along with the public properties and fields.
    /// </param>
    /// <returns>
    /// True if the source objects is <c>null</c>, equivalent or a subset of the target; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The source and target objects are considered partially equivalent if their data types are compatible (e.g. <c>int</c> and <c>long</c> are compatible) and they meet the following criteria:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <para>For all data types, the <c>null</c>value of the source object is considered a partial match of the target.</para>
    /// </item>
    /// <item>
    /// <para>For simple types, such as <c>string</c>, <c>int</c>, <c>DateTime</c>, etc., both the source and target values can be typecast to the same type and found equal.</para>
    /// </item>
    /// <item>
    /// <para>For arrays, list, and collection types, the source must contain the same or a lesser number of elements than the target, and each element of the source must be equivalent to the corresponding item of the target.</para>
    /// </item>
    /// <item>
    /// <para>For dictionaries, the source must contain the same or a lesser number of elements, and each element of the source must be equivalent to the corresponding item of the target found under the same key.</para>
    /// </item>
    /// <item>
    /// <para>For hash sets, the source must contain the same number or a lesser number of elements, and the values in both hash sets must match.</para>
    /// </item>
    /// <item>
    /// <para>Comparison of string values is case-sensitive.</para>
    /// </item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// User source = new()
    /// {
    ///     Id = 1,
    ///     Email = "joe@mail.com",
    ///     Name = new()
    ///     {
    ///         GivenName = "Joe"
    ///     }
    /// };
    /// 
    /// User target1 = new()
    /// {
    ///     Id = 1,
    ///     Email = "joe@mail.com"
    /// };
    /// 
    /// User target2 = new()
    /// {
    ///     Id = 2,
    ///     Email = "joe@mail.com",
    ///     Name = new()
    ///     {
    ///         GivenName = "Joe",
    ///     },
    /// };
    /// 
    /// Assert.True(source.IsPartialEquivalentOf(target1));
    /// Assert.False(target1.IsPartialEquivalentOf(source));
    /// Assert.False(source.IsPartialEquivalentOf(target2));
    /// </code>
    /// </example>
    /// <seealso cref="IsEquivalentOf(object?, object?, bool)"/>"/>
    public static bool IsPartialEquivalentOf
    (
        this object? source,
        object? target,
        bool includeNonPublic = false
    )
    {
        if (source == null)
        {
            return true;
        }
        else if (target == null)
        {
            return false;
        }

        if (ReferenceEquals(source, target))
        {
            return true;
        }

        Type sourceType = source.GetType();
        Type targetType = target.GetType();

        if ((sourceType.IsSimple() && !targetType.IsSimple()) ||
            (targetType.IsSimple() && !sourceType.IsSimple()))
        {
            return false;
        }

        if ((sourceType.IsSimple() && targetType.IsSimple()) ||
            (!(sourceType.IsArray && targetType.IsArray) &&
            !(typeof(IList).IsAssignableFrom(sourceType) && typeof(IList).IsAssignableFrom(targetType)) &&
            !(typeof(IDictionary).IsAssignableFrom(sourceType) && typeof(IDictionary).IsAssignableFrom(targetType)) &&
            !(sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(HashSet<>) && targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(HashSet<>)) &&
            !(sourceType.IsClass && targetType.IsClass)))
        {
            return source.IsEquivalentOf(target, includeNonPublic);
        }
        else if (sourceType.IsArray && targetType.IsArray)
        {
            Array sourceArray = (Array)source;
            Array targetArray = (Array)target;

            if (sourceArray.Length > targetArray.Length)
            {
                return false;
            }

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (!IsPartialEquivalentOf(sourceArray.GetValue(i), targetArray.GetValue(i), includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IList).IsAssignableFrom(sourceType) && typeof(IList).IsAssignableFrom(targetType))
        {
            IList sourceList = (IList)source;
            IList targetList = (IList)target;

            if (sourceList.Count > targetList.Count)
            {
                return false;
            }

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (!IsPartialEquivalentOf(sourceList[i], targetList[i], includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IDictionary).IsAssignableFrom(sourceType) && typeof(IDictionary).IsAssignableFrom(targetType))
        {
            IDictionary sourceDictionary = (IDictionary)source;
            IDictionary targetDictionary = (IDictionary)target;

            if (sourceDictionary.Count > targetDictionary.Count)
            {
                return false;
            }

            foreach (object? key in sourceDictionary.Keys)
            {
                if (!targetDictionary.Contains(key) || !IsPartialEquivalentOf(sourceDictionary[key], targetDictionary[key], includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(HashSet<>) &&
            targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(HashSet<>))
        {
            IEnumerable sourceEnumerable = (IEnumerable)source;
            IEnumerable targetEnumerable = (IEnumerable)target;

            if (sourceEnumerable.Count() > sourceEnumerable.Count())
            {
                return false;
            }

            foreach (object? sourceItem in sourceEnumerable.Cast<object?>())
            {
                foreach (object? targetItem in targetEnumerable.Cast<object?>())
                {
                    if (IsEquivalentOf(sourceItem, targetItem, includeNonPublic))
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
        else if (sourceType.IsClass && targetType.IsClass)
        {
            HashSet<string> skip = [];

            foreach (PropertyInfo property in sourceType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                if (property.CanRead)
                {
                    skip.Add(property.Name);

                    object? sourceValue = property.GetValue(source);
                    object? targetValue = property.GetValue(target);

                    if (!IsPartialEquivalentOf(sourceValue, targetValue))
                    {
                        return false;
                    }
                }
            }

            skip.Clear();

            foreach (FieldInfo field in sourceType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                (includeNonPublic ? BindingFlags.NonPublic : 0)))
            {
                skip.Add(field.Name);

                object? sourceValue = field.GetValue(source);
                object? targetValue = field.GetValue(target);

                if (!IsPartialEquivalentOf(sourceValue, targetValue, includeNonPublic))
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            return source.Equals(target);
        }
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
