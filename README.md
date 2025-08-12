# DotNetExtras.Common

`DotNetExtras.Common` is a general-purpose .NET Core library that simplifies common operations frequently used in .NET applications.

Use the `DotNetExtras.Common` library to:

- Generate fully qualified or *camelCase* names for variables, types, and members using the `NameOf` utility (similar to the `nameof` operator).
- Retrieve application assembly information including company, version, and product details.
- Retrieve error information from exceptions, including immediate and inner exceptions.
- Serialize objects as JSON strings and deserialize JSON strings into objects in one call.
- Serialize as collection as a comma-separated (or tokenized) string.
- Compare complex object for equivalency (property by property).
- Deep clone complex objects (including all nested properties).
- Get and set object values using compound (nested) property names (create property hierarchy if necessary).
- Convert strings to strongly-typed data types including dates, times, and collections.
- Validate content formats such as JSON, HTML, and common patterns using built-in regular expressions.
- Access enumeration metadata like descriptions, abbreviations, and custom attributes.
- Check if objects are empty or determine type characteristics (primitive vs. complex).
- Convert objects to dynamic types and perform advanced type introspection.
- Escape special characters for LDAP queries and SQL statements.

## Documentation
For complete documentation, usage details, and code samples, see:

- [Documentation](https://alekdavis.github.io/dotnet-extras-common)
- [Unit tests](https://github.com/alekdavis/dotnet-extras-common/tree/main/CommonTests)

## Package
Install the latest version of the `DotNetExtras.Common` Nuget package from:

- [https://www.nuget.org/packages/DotNetExtras.Common](https://www.nuget.org/packages/DotNetExtras.Common)