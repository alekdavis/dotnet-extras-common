# DotNetExtras
`DotNetExtras` is a collection of .NET Core libraries intended to make the lives of application developers easier. The libraries are organized into several projects, each serving a specific purpose.

## DotNetExtras.Common library
Implements general-purpose classes for such common tasks as retrieving error information from the immediate and inner exceptions, simple JSON serialization and deserialization, string functions making it easier to generate tokenized strings from collections, deep object cloning and comparison, setting object properties and getting property values using compound (nested) property names, and a lot more.

## DotNetExtras.Configuration library
Offers an easy way to read and transform application settings.

## DotNetExtras.Mail library
Allows applications to easily find supported translations of localized email templates and merge them with the message-specific data.

## DotNetExtras.OData library
Implements capabilities to parse and validate OData filters expressions.

## DotNetExtras.Retry
Provides a simple way of retrying failed operations.

## DotNetExtras.Security
Simplifies security operations such as hashing, encryption, random password generation, masking sensitive properties, and so on.

## DotNetExtras.Testing
Extends common assertion operations used by unit tests. 

## DotNetExtras.WebClient
Defines helper methods for automatic refresh of the access tokens, parsing of OAuth errors, etc. 

# DotNetExtras documentation
For additional details about each library, see the library project folder. You can find the complete API documentation [https://alekdavis.github.io/dotnet-extras](https://alekdavis.github.io/dotnet-extras).