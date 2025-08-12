using DotNetExtras.Common;
using System.Reflection;

namespace CommonLibTests;

public class PrimaryAssemblyTests
{
    [Fact]
    public void PrimaryAssembly_Company()
    {
        string? company = PrimaryAssembly.Company;
        Assert.Equal("Microsoft Corporation", company);
    }

    [Fact]
    public void PrimaryAssembly_Copyright()
    {
        string? copyright = PrimaryAssembly.Copyright;
        Assert.Equal("© Microsoft Corporation. All rights reserved.", copyright);
    }

    [Fact]
    public void PrimaryAssembly_Description()
    {
        string? description = PrimaryAssembly.Description;
        Assert.Null(description);
    }

    [Fact]
    public void PrimaryAssembly_Product()
    {
        string? product = PrimaryAssembly.Product;
        Assert.Equal("testhost", product);
    }

    [Fact]
    public void PrimaryAssembly_Title()
    {
        string? title = PrimaryAssembly.Title;
        Assert.Equal("testhost", title);
    }

    [Fact]
    public void PrimaryAssembly_Version()
    {
        string? version = PrimaryAssembly.Version;
        Assert.NotNull(version);
        Assert.NotEmpty(version);
    }
}
