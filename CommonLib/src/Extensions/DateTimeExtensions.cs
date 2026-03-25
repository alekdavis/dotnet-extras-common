using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// Implements extension methods
/// applicable to date and time values,
/// such as formatting date and time values.
/// </summary>
public static class DateTimeExtensions
{
    private static readonly string _formatPrefix = "yyyy-MM-ddTHH:mm:ss";
    private static readonly char _formatPrecision = 'f';
    private static readonly char _formatUtc = 'Z';
    private static readonly string _formatOffset = "zzz";

    /// <summary>
    /// Formats a nullable <see cref="DateTime"/> value as an ISO 8601 string
    /// using local time and the specified precision.
    /// </summary>
    /// <param name="dateTime">
    /// The <see cref="DateTime"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the local <paramref name="dateTime"/> in the ISO 8601 format 
    /// with a "T" separator and possible time offset.
    /// </returns>
    public static string? ToIso8601
    (
        this DateTime? dateTime,
        [Range(0, 7)]
        int precision = 0
    )
    {
        if (dateTime == null)
        {
            return null;
        }

        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        if (dateTime.Value.Kind == DateTimeKind.Utc)
        {
            format.Append(_formatUtc);
        }
        else if (dateTime.Value.Kind == DateTimeKind.Local)
        {
            format.Append(dateTime.Value.ToString(_formatOffset));
        }

        return dateTime.Value.ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a non-nullable <see cref="DateTime"/> value as an ISO 8601 string
    /// using local time and the specified precision.
    /// </summary>
    /// <param name="dateTime">
    /// The <see cref="DateTime"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the local <paramref name="dateTime"/> in the ISO 8601 format 
    /// with a "T" separator and possible time offset.
    /// </returns>
    public static string ToIso8601
    (
        this DateTime dateTime,
        [Range(0, 7)]
        int precision = 0
    )
    {
        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        if (dateTime.Kind == DateTimeKind.Utc)
        {
            format.Append(_formatUtc);
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            format.Append(dateTime.ToString(_formatOffset));
        }

        return dateTime.ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a nullable <see cref="DateTimeOffset"/> value as an ISO 8601 string
    /// using local time and the specified precision.
    /// </summary>
    /// <param name="dateTimeOffset">
    /// The <see cref="DateTimeOffset"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the local <paramref name="dateTimeOffset"/> in the ISO 8601 format 
    /// with a "T" separator and possible time offset.
    /// </returns>
    public static string? ToIso8601
    (
        this DateTimeOffset? dateTimeOffset,
        [Range(0, 7)]
        int precision = 0
    )
    {
        if (dateTimeOffset == null)
        {
            return null;
        }

        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        if (dateTimeOffset.Value.Offset == TimeSpan.Zero)
        {
            format.Append(_formatUtc);
        }
        else
        {
            format.Append(dateTimeOffset.Value.ToString(_formatOffset));
        }

        return dateTimeOffset.Value.ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a non-nullable <see cref="DateTimeOffset"/> value as an ISO 8601 string
    /// using local time and the specified precision.
    /// </summary>
    /// <param name="dateTimeOffset">
    /// The <see cref="DateTimeOffset"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the local <paramref name="dateTimeOffset"/> in the ISO 8601 format 
    /// with a "T" separator and possible time offset.
    /// </returns>
    public static string ToIso8601
    (
        this DateTimeOffset dateTimeOffset,
        [Range(0, 7)]
        int precision = 0
    )
    {
        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        if (dateTimeOffset.Offset == TimeSpan.Zero)
        {
            format.Append(_formatUtc);
        }
        else
        {
            format.Append(dateTimeOffset.ToString(_formatOffset));
        }

        return dateTimeOffset.ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a nullable <see cref="DateTime"/> value as an ISO 8601 string
    /// using Universal (UTC) time and the specified precision.
    /// </summary>
    /// <param name="dateTime">
    /// The <see cref="DateTime"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the Universal <paramref name="dateTime"/> in the ISO 8601 format 
    /// with a "T" separator.
    /// </returns>
    public static string? ToUniversalIso8601
    (
        this DateTime? dateTime,
        [Range(0, 7)]
        int precision = 0
    )
    {
        if (dateTime == null)
        {
            return null;
        }

        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        format.Append(_formatUtc);

        return dateTime.Value.ToUniversalTime().ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a non-nullable <see cref="DateTime"/> value as an ISO 8601 string
    /// using Universal (UTC) time and the specified precision.
    /// </summary>
    /// <param name="dateTime">
    /// The <see cref="DateTime"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the Universal <paramref name="dateTime"/> in the ISO 8601 format 
    /// with a "T" separator.
    /// </returns>
    public static string ToUniversalIso8601
    (
        this DateTime dateTime,
        [Range(0, 7)]
        int precision = 0
    )
    {
        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        format.Append(_formatUtc);

        return dateTime.ToUniversalTime().ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a nullable <see cref="DateTimeOffset"/> value as an ISO 8601 string
    /// using Universal (UTC) time and the specified precision.
    /// </summary>
    /// <param name="dateTimeOffset">
    /// The <see cref="DateTimeOffset"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the Universal <paramref name="dateTimeOffset"/> in the ISO 8601 format 
    /// with a "T" separator.
    /// </returns>
    public static string? ToUniversalIso8601
    (
        this DateTimeOffset? dateTimeOffset,
        [Range(0, 7)]
        int precision = 0
    )
    {
        if (dateTimeOffset == null)
        {
            return null;
        }

        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        format.Append(_formatUtc);

        return dateTimeOffset.Value.ToUniversalTime().ToString(format.ToString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a non-nullable <see cref="DateTimeOffset"/> value as an ISO 8601 string
    /// using Universal (UTC) time and the specified precision.
    /// </summary>
    /// <param name="dateTimeOffset">
    /// The <see cref="DateTimeOffset"/> value to format. 
    /// </param>
    /// <param name="precision">
    /// Number of digits in the fraction of a second to be included in the output.
    /// </param>
    /// <returns>
    /// A string representing the Universal <paramref name="dateTimeOffset"/> in the ISO 8601 format 
    /// with a "T" separator.
    /// </returns>
    public static string ToUniversalIso8601
    (
        this DateTimeOffset dateTimeOffset,
        [Range(0, 7)]
        int precision = 0
    )
    {
        StringBuilder format = new(_formatPrefix);
        if (precision > 0)
        {
            format.Append('.');
            format.Append(_formatPrecision, precision);
        }

        format.Append(_formatUtc);

        return dateTimeOffset.ToUniversalTime().ToString(format.ToString(), CultureInfo.InvariantCulture);
    }
}
