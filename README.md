# DotNetExtras.Common

`DotNetExtras.Common` is a general-purpose .NET Core library that simplifies common operations frequently used in .NET applications.

Use the `DotNetExtras.Common` library to:

- Generate fully qualified names for types, variables, and object properties (think of the `nameof` operator on steroids).
- Retrieve application assembly information including company, version, and product details.
- Retrieve error information from exceptions, including immediate and inner exceptions.
- Serialize objects as JSON strings and deserialize JSON strings into objects with an option to mask or ignore specific properties.
- Convert collections to comma-separated (or tokenized) string.
- Compare complex object for equivalence or partial equivalence (by property, key, or element).
- Deep clone complex objects (including all nested properties).
- Get and set object values using compound (nested) property names (create property hierarchy if necessary).
- Access enumeration metadata like descriptions, abbreviations, and custom attributes.
- Check if objects are empty or determine type characteristics (primitive vs. complex).
- Escape special characters for LDAP queries and SQL statements.

## Usage

The following examples illustrates various operations implemented by the `DotNetExtras.Common` library.

### DotNetExtras.Common namespace

```cs
using DotNetExtras.Common;
...
// Print: Some.Type.Property.SubProperty
Console.WriteLine(NameOf.Full(nameof(Some.Type.Property.SubProperty)));

// Print: some.type.property.subProperty
Console.WriteLine(NameOf.Full(nameof(Some.Type.Property.SubProperty), true));

// Print: Property.SubProperty
Console.WriteLine(NameOf.Long(someObject.Property.Subproperty)));

// Print: property.subProperty
Console.WriteLine(NameOf.Long(someObject.Property.Subproperty), true));

// Print: SubProperty
Console.WriteLine(NameOf.Short(someObject.Property.Subproperty)));

// Print: subProperty
Console.WriteLine(NameOf.Short(someObject.Property.Subproperty), true));

// Print the version and the copyright message such as: MyApp v1.0.8 © 2023 MyCompany
Console.WriteLine($"{PrimaryAssembly.Title} v{PrimaryAssembly.Version} {PrimaryAssembly.Copyright}");
```

### DotNetExtras.Common.Exceptions namespace

```cs
using DotNetExtras.Common.Exceptions;
...
// Print exception messages from the immediate and all inner exceptions.
Console.WriteLine(ex.GetMessages());
```

### DotNetExtras.Common.Extensions namespace

```cs
using DotNetExtras.Common.Extensions;
...
// Print received tags as a comma-separated string.
List<string> tags = GetTags();
Console.WriteLine($"Got tags: {tags.ToCsv()}.");

// Set a nested property value using a compound property name.
user.SetPropertyValue("Name.Surname", "Johnson");

// Get a nested property value using a compound property name.
string surname = user.GetPropertyValue<string>("Name.Surname");

// Compare two objects for equivalence.
bool equivalent = userA.IsEquivalentTo(userB));

// Compare two arrays for partial equivalence.
bool partiallyEquivalent = arrayA.IsPartiallyEquivalentTo(arrayB, 3));
```

### DotNetExtras.Common.Json namespace

```cs
using DotNetExtras.Common.Json;
...

// Serialize an object to a JSON string.
string json = myObject.ToJson();

// Convert a JSON string to an object.
MyObjectType? myObject = json.FromJson();
```

## Documentation

For complete documentation, usage details, and code samples, see:

- [Documentation](https://alekdavis.github.io/dotnet-extras-common)
- [Unit tests](https://github.com/alekdavis/dotnet-extras-common/tree/main/CommonTests)

## Package

Install the latest version of the `DotNetExtras.Common` NuGet package from:

- [https://www.nuget.org/packages/DotNetExtras.Common](https://www.nuget.org/packages/DotNetExtras.Common)

## See also

Check out other `DotNetExtras` libraries at:

- [https://github.com/alekdavis/dotnet-extras](https://github.com/alekdavis/dotnet-extras)
