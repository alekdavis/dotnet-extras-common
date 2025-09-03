# DotNetExtras.Common

`DotNetExtras.Common` is a general-purpose .NET Core library that simplifies common operations frequently used in .NET applications.

Use the `DotNetExtras.Common` library to:

- Generate fully qualified names for types, variables, and object properties in the original or _camelCase_ notation (think of the `nameof` operator on steroids).
- Retrieve application assembly information including company, version, and product details.
- Retrieve error information from exceptions, including immediate and inner exceptions.
- Convert objects to and from JSON strings via the `System.Text.Json` (STJ) serialization.
- Convert collections to comma-separated (or tokenized) string.
- Compare complex object for equivalence or partial equivalence (by property, key, or element).
- Get and set object values using compound (nested) property names (create property hierarchy if necessary).
- Access enumeration metadata like descriptions, abbreviations, and custom attributes.
- Check if objects are empty.
- Determine if an object is primitive or a complex type.

## Usage

The following examples illustrates some operations implemented by the `DotNetExtras.Common` library.

### DotNetExtras.Common.NameOf class

Generate fully qualified or partial names for types, variables, and object properties:

```cs
// Prints: Some.Type.Property.SubProperty
Console.WriteLine(NameOf.Full(nameof(Some.Type.Property.SubProperty)));

// Prints: some.type.property.subProperty
Console.WriteLine(NameOf.Full(nameof(Some.Type.Property.SubProperty), true));

// Prints: Property.SubProperty
Console.WriteLine(NameOf.Long(someObject.Property.SubProperty)));

// Prints: property.subProperty
Console.WriteLine(NameOf.Long(someObject.Property.SubProperty), true));

// Prints: SubProperty
Console.WriteLine(NameOf.Short(someObject.Property.SubProperty)));

// Prints: subProperty
Console.WriteLine(NameOf.Short(someObject.Property.SubProperty), true));
```

### DotNetExtras.Common.PrimaryAssembly class

Given the following `csproj` file settings:

```
<PropertyGroup>
  <AssemblyName>MyApp</AssemblyName>
  <Version>1.0.8</Version>
  <Title>$(AssemblyName)</Title>
  <Copyright>© 2023 MyCompany</Copyright>
</PropertyGroup>
```

Print the application assembly information as `MyApp v1.0.8 © 2023 MyCompany`:

```cs
Console.WriteLine($"{PrimaryAssembly.Title} v{PrimaryAssembly.Version} {PrimaryAssembly.Copyright}");
```

### DotNetExtras.Common.Exceptions.ExceptionExtensions class

Print exception messages from the immediate and all inner exceptions:

```cs
try
{
  try
  {
  	throw new Exception("Cannot do that");
  }
  catch (Exception ex)
  {
  	throw new Exception("Failed to do this", ex);
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex.GetMessages());
}
```

In the example above, the output will be:

```
Failed to do this. Cannot do that.
```

### DotNetExtras.Common.Extensions.IEnumerable class

Convert list or array values to delimited strings:

```cs
List<string> strings = ["one", "two", "tree"];
int[] numbers = [1, 2, 3];

Console.WriteLine("Strings: " + strings.ToCsv() + ".");
Console.WriteLine("Strings: " + strings.ToCsv(";") + ".");
Console.WriteLine("Numbers: " + numbers.ToCsv("|") + ".");
```

In the example above, the output will be:

```
Strings: one, two, three.
Strings: one;two;three.
Numbers: 1|2|3.
```

### DotNetExtras.Common.Extensions.ObjectExtensions class

Given the class definitions:

```cs
public class Name
{
    public string GivenName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}

public class User
{
    public Name Name { get; set; } = new Name();
}
```

Set and get the nested property value using the compound property name.

```cs
User user = new();

// Set the nested property value using a compound property name.
user.SetPropertyValue("Name.Surname", "Johnson");

// Get a nested property value using a compound property name.
string surname = user.GetPropertyValue<string>("Name.Surname");
```

Check if an object holds any properties with non-default values.

```cs
User user = new();
bool isEmpty = user.IsEmpty(); // true
user.Name = new()
{
    GivenName = "Alice";
}
isEmpty = user.IsEmpty(); // false
```

Compare two complex objects for equivalence or partial equivalence.

```cs
User userA = new()
{
    Name = new()
    {
        GivenName = "Alice",
        Surname = "Johnson"
    }
};

User userB = new()
{
    Name = new()
    {
        GivenName = "Alice",
    }
};

// Compare two arrays for partial equivalence.
bool almostTheSame;

almostTheSame = userB.IsPartiallyEquivalentTo(userA)); // true
almostTheSame = userA.IsPartiallyEquivalentTo(userB)); // false

userB.Name.Surname = "Johnson";

userB.IsEquivalentTo(userA)); // true
userA.IsEquivalentTo(userB)); // true
```

Keep in mind that partially equivalent objects may not be equivalent, but equivalent objects are always partially equivalent. In addition to comparing objects, you can also compare arrays, lists, dictionaries, hash sets, and primitive types. For the explanation of the equivalence and partial equivalence rules, see the class and method documentation.

### DotNetExtras.Common.Json.JsonExtensions class

Serialize and deserialize objects to and from JSON strings:

```cs
User user = new()
{
    Name = new()
    {
        GivenName = "Alice",
        Surname = "Johnson"
    }
};

string json = user.ToJson();

user = json.FromJson<User>();
```

### Enum metadata definition and access classes

Set the metadata attributes for enumeration values:

```cs
public enum Something
{
    [Description("Description 1")]
    [Abbreviation("ABBR1")]
    [ShortName("Short1")]
    Value1,

    [Description("Description 2")]
    [Abbreviation("ABBR2")]
    [ShortName("Short2")]
    Value2,
}
```

Get the metadata attributes for enumeration values:

```cs
Something something = Something.Value1;

// Prints: Description 1
Console.WriteLine(something.ToDescription());

// Prints: ABBR1
Console.WriteLine(something.ToAbbreviation());

// Prints: Short1
Console.WriteLine(something.ToShortName());
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
