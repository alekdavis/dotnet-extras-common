using System.Runtime.CompilerServices;

namespace DotNetExtras.Common;
/// <summary>
/// Implements methods used to identify the caller context, 
/// such as current class, method, line of code, etc.
/// </summary>
/// <remarks>
/// This class methods could be helpful for logging, tracing, error handling, etc.
/// </remarks>
/// <example>
/// <code>
/// <![CDATA[
/// using Serilog.Context;
/// using SerilogTimings;
/// ...
/// public static class LogProperty
/// {
///     public const string MethodName = "MethodName";
/// }
/// ...
/// public class SomeClass
/// {
///     public void SomeMethod()
///     {
///         // If the Serilog template contains {MethodName} property,
///         // all log entries created within this scope will contain the method name.
///         using (LogContext.PushProperty(LogProperty.MethodName, CodeContext.GetClassMethodName(this)))
///         // The Operation timer will log the method duration automatically when disposed.
///         using (Operation timer = Operation.Begin(CodeContext.GetClassMethodName(this)))
///         {
///             // Implement logic here.
///             ...
///             timer.Complete();
///         }
///     }
/// }
/// ]]>
/// </code>
/// </example>
public static class CodeContext
{
    /// <summary>
    /// Returns the name of the specified class.
    /// </summary>
    /// <param name="caller">
    /// The caller class.
    /// </param>
    /// <param name="useFullName">
    /// Set to <code>true</code> to return the full class name;
    /// otherwise, the short name will be returned.
    /// </param>
    /// <returns>
    /// The short name of the class.
    /// </returns>
    public static string GetClassName
    (
        object caller,
        bool useFullName = false
    )
    {
        return caller == null
            ? string.Empty
            : useFullName
                ? caller.GetType().FullName ?? string.Empty
                : caller.GetType().Name;
    }

    /// <summary>
    /// Returns the name of the class and method that invoked this method.
    /// </summary>
    /// <param name="caller">
    /// Pass <code>this</code> in the instance methods to get the name of the caller class.
    /// </param>
    /// <param name="useFullName">
    /// If set to <code>true</code> the full class name (including the namespace) will be included.
    /// </param>
    /// <param name="method">
    /// Do not pass anything here and the name of the caller class and method will be determined automatically.
    /// </param>
    /// <returns>
    /// Name of the calling class and method calling this method.
    /// </returns>
    public static string GetClassMethodName
    (
        object? caller = null,
        bool useFullName = false,
        [CallerMemberName] string? method = null
    )
    {
        return caller == null
            ? method ?? string.Empty
            : $"{GetClassName(caller, useFullName)}.{method}";
    }

    /// <summary>
    /// Returns the name of the method that called this method.
    /// </summary>
    /// <param name="method">
    /// Do not pass anything here and the name of the caller method will be determined automatically.
    /// </param>
    /// <returns>
    /// Name of the method calling this method.
    /// </returns>
    public static string GetMethodName
    (
        [CallerMemberName] string? method = null
    )
    {
        return method ?? string.Empty;
    }

    /// <summary>
    /// Returns the path of the source code file calling this method.
    /// </summary>
    /// <param name="filePath">
    /// Do not pass anything here and the source code path will be determined automatically.
    /// </param>
    /// <returns>
    /// Path to the source code file calling this method.
    /// </returns>
    public static string GetFilePath
    (
        [CallerFilePath] string? filePath = null
    )
    {
        return filePath ?? string.Empty;
    }

    /// <summary>
    /// Returns the name of the source code file calling this method.
    /// </summary>
    /// <param name="filePath">
    /// Do not pass anything here and the name of the source code file will be determined automatically.
    /// </param>
    /// <param name="withExtension">
    /// Set to <code>true</code> to return the file name with the extension.
    /// </param>
    /// <returns>
    /// Name of the source code file calling this method.
    /// </returns>
    public static string GetFileName
    (
        bool withExtension = false,
        [CallerFilePath] string? filePath = null
    )
    {
        return string.IsNullOrWhiteSpace(filePath)
            ? string.Empty
            : withExtension
                ? Path.GetFileName(filePath)
                : Path.GetFileNameWithoutExtension(filePath);
    }

    /// <summary>
    /// Returns the line number in the source code file calling this method.
    /// </summary>
    /// <param name="lineNumber">
    /// Line number on which this method is called.
    /// </param>
    /// <returns>
    /// The line number in the source code file calling this method.
    /// </returns>
    public static int GetLineNumber
    (
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return lineNumber;
    }
}
