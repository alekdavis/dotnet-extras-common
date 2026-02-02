using DotNetExtras.Common;

namespace CommonLibTests;

public class PrimaryAssemblyTests
{
    [Fact]
    public void PrimaryAssembly_Company()
    {
        string? company = PrimaryAssembly.Company;
        Assert.Equal("Alek Davis", company);
    }

    [Fact]
    public void PrimaryAssembly_Copyright()
    {
        string? copyright = PrimaryAssembly.Copyright;
        Assert.Equal("© 2026 Alek Davis", copyright);
    }

    [Fact]
    public void PrimaryAssembly_Description()
    {
        string? description = PrimaryAssembly.Description;
        Assert.Equal("Unit tests.", description);
    }

    [Fact]
    public void PrimaryAssembly_Product()
    {
        string? product = PrimaryAssembly.Product;
        Assert.Equal("CommonTests", product);
    }

    [Fact]
    public void PrimaryAssembly_Title()
    {
        string? title = PrimaryAssembly.Title;
        Assert.Equal("CommonTests", title);
    }

    [Fact]
    public void PrimaryAssembly_Version()
    {
        string? version = PrimaryAssembly.Version;
        Assert.NotNull(version);
        Assert.NotEmpty(version);
    }
}
