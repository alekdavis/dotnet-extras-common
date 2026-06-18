namespace DotNetExtras.Common.Exceptions;
/// <summary>
/// Use the <see cref="SafeException"/> class as the base exception for your custom exception classes,
/// so you can easily recognize them in code.
/// This can be handy in a few cases.
/// For example, calling the <see cref="Exceptions.ExceptionExtensions.GetMessages{T}(Exception, bool, bool)"/>
/// extension method passing <see cref="SafeException"/> as the generic type
/// (or calling the <see cref="Exceptions.ExceptionExtensions.GetSafeMessages(Exception, bool, bool)"/>
/// extension method),
/// will only return messages from your custom exceptions
/// which can help you control the error details sent to the other apps
/// and make sure sensitive information is not leaked outside of your application.
/// </summary>
[Serializable]
public class SafeException: Exception
{
    /// <summary>
    /// Can be used to mark the exception message as safe, so it can be recognized in code.
    /// </summary>
    public bool IsSafe { get; init; } = false;

    /// <inheritdoc cref="SafeException(string, Exception, bool)"/>
    public SafeException() : base()
    {
    }

    /// <inheritdoc cref="SafeException(string, Exception, bool)"/>
    public SafeException
    (
        bool isSafe
    )
    : base()
    {
        IsSafe = isSafe;
    }

    /// <inheritdoc cref="SafeException(string, Exception, bool)"/>
    public SafeException
    (
        string message,
        bool isSafe = true
    )
    : base(message)
    {
        IsSafe = isSafe;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeException"/> class.
    /// </summary>
    /// <param name="message">
    /// Error message.
    /// </param>
    /// <param name="innerException">
    /// Inner exception.
    /// </param>
    /// <param name="isSafe">
    /// Indicates whether the exception is ot safe to be shown to the user.
    /// </param>
    public SafeException
    (
        string message,
        Exception innerException,
        bool isSafe = true
    )
    : base(message, innerException)
    {
        IsSafe = isSafe;
    }
}