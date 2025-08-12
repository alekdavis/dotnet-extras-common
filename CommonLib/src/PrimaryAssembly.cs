using System.Reflection;

namespace DotNetExtras.Common;
/// <summary>
/// Returns the most frequently used attributes 
/// (company, copyright, product, version, title) 
/// of the running application's primary assembly.
/// </summary>
/// <remarks>
/// <para>
/// The primary assembly is probed in the following order:
/// </para>
/// <list type="number">
/// <item><see cref="Assembly.GetEntryAssembly"/></item>
/// <item><see cref="Assembly.GetCallingAssembly"/></item>
/// <item><see cref="Assembly.GetExecutingAssembly"/></item>
/// <item><see cref="Assembly.GetAssembly"/> implementing the <see cref="PrimaryAssembly"/> class</item>
/// </list>
/// <para>
/// Adapted from the
/// <see href="https://www.c-sharpcorner.com/UploadFile/ravesoft/access-assemblyinfo-file-and-get-product-informations">Access AssemblyInfo File and Get Product Informations</see> article by Ravee Rasaiyah.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// string company   = PrimaryAssembly.Company;
/// string copyright = PrimaryAssembly.Copyright;
/// string product   = PrimaryAssembly.Product;
/// string version   = PrimaryAssembly.Version;
/// string title     = PrimaryAssembly.Title;
/// </code>
/// </example>
public static class PrimaryAssembly
{
    #region Public properties
    /// <summary>
    /// Returns the company name defined in the
    /// <see cref="AssemblyCompanyAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    public static string? Company
    {
        get
        {
            AssemblyCompanyAttribute? company = GetCustomAttribute<AssemblyCompanyAttribute>();
            return company?.Company;
        }
    }

    /// <summary>
    /// Returns the copyright message defined in the 
    /// <see cref="AssemblyCopyrightAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    public static string? Copyright
    {
        get
        {
            AssemblyCopyrightAttribute? copyright = GetCustomAttribute<AssemblyCopyrightAttribute>();
            return copyright?.Copyright;
        }
    }

    /// <summary>
    /// Returns the assembly description defined in the 
    /// <see cref="AssemblyDescriptionAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    public static string? Description
    {
        get
        {
            AssemblyDescriptionAttribute? description = GetCustomAttribute<AssemblyDescriptionAttribute>();
            return description?.Description;
        }
    }

    /// <summary>
    /// Returns the product name defined in the 
    /// <see cref="AssemblyProductAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    public static string? Product
    {
        get
        {
            AssemblyProductAttribute? product = GetCustomAttribute<AssemblyProductAttribute>();
            return product?.Product;
        }
    }

    /// <summary>
    /// Returns the assembly title defined in the 
    /// <see cref="AssemblyTitleAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    public static string? Title
    {
        get
        {
            AssemblyTitleAttribute? title = GetCustomAttribute<AssemblyTitleAttribute>();
            return title?.Title;
        }
    }

    /// <summary>
    /// Returns the version of the assembly file defined in the 
    /// <see cref="AssemblyFileVersionAttribute"/> 
    /// of the primary assembly.
    /// </summary>
    /// <remarks>
    /// For additional information about assembly and file versions, see
    /// <see href="https://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin#answer-65062"/>.
    /// </remarks>
    public static string? Version
    {
        get
        {
            AssemblyFileVersionAttribute? version = GetCustomAttribute<AssemblyFileVersionAttribute>();
            return version?.Version;
        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Returns the main application assembly
    /// or the assembly implementing the specified type.
    /// </summary>
    /// <param name="type">
    /// Optional type of the class implemented in the assembly.
    /// </param>
    /// <returns>
    /// Current assembly or the assembly implementing the specified type.
    /// </returns>
    /// <remarks>
    /// If type is not specified, 
    /// assemblies are probed in the following order:
    /// 1. `Assembly.GetEntryAssembly()` 
    /// 2. `Assembly.GetCallingAssembly()`
    /// 3. `Assembly.GetExecutingAssembly()`,
    /// 4. `Assembly.GetAssembly(typeof(PrimaryAssembly))`
    /// </remarks>
    private static Assembly? GetAssembly
    (
        Type? type = null
    )
    {
        return type == null
            ? ((Assembly.GetEntryAssembly()
                ?? Assembly.GetCallingAssembly())
                ?? Assembly.GetExecutingAssembly())
                ?? Assembly.GetAssembly(typeof(PrimaryAssembly))
            : Assembly.GetAssembly(type);
    }

    /// <summary>
    /// Returns the custom assembly attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Attribute data type.
    /// </typeparam>
    /// <returns>
    /// Attribute for the matching type.
    /// </returns>
    private static T? GetCustomAttribute<T>()
    where T : Attribute
    {
        Assembly? assembly = GetAssembly();

        if (assembly == null)
        {
            return null;
        }

        object[] attributes = assembly.GetCustomAttributes(typeof(T), false);

        return (attributes == null) || (attributes.Length == 0)
            ? null 
            : (T)attributes[0];
    }
    #endregion
}
