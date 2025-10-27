using DotNetExtras.Common.Enums;
using System.ComponentModel;

namespace CommonLibTests.Enums;

public class EnumExtensionsTests
{
    public enum TestEnum
    {
        [Description("Description 1")]
        [Abbreviation("A1")]
        [ShortName("Short1")]
        Value1,

        [Description("Description 2")]
        [Abbreviation("A2")]
        [ShortName("Short2")]
        Value2,

        [Description("Description 1")]
        [Abbreviation("A1")]
        [ShortName("Short1")]
        Duplicate,
    }

    [Fact]
    public void Enum_ToDescription()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? description = value.ToDescription();

        // Assert
        Assert.Equal("Description 1", description);
    }

    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("test", false, null)]
    [InlineData(null, true, null)]
    [InlineData("", true, null)]
    [InlineData("test", true, null)]
    [InlineData("Description 1", false, TestEnum.Value1)]
    [InlineData("DESCRIPTION 1", true, TestEnum.Value1)]
    [InlineData("Description 2", false, TestEnum.Value2)]
    [InlineData("DESCRIPTION 2", true, TestEnum.Value2)]
    public void Enum_FromDescription
    (
        string? description,
        bool ignoreCase,
        TestEnum? expectedValue
    )
    {
        // Act
        TestEnum? actualValue = EnumExtensions.FromDescription<TestEnum>(description!, ignoreCase);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Enum_ToAbbreviation()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? abbreviation = value.ToAbbreviation();

        // Assert
        Assert.Equal("A1", abbreviation);
    }

    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("test", false, null)]
    [InlineData(null, true, null)]
    [InlineData("", true, null)]
    [InlineData("test", true, null)]
    [InlineData("A1", false, TestEnum.Value1)]
    [InlineData("a1", true, TestEnum.Value1)]
    [InlineData("A2", false, TestEnum.Value2)]
    [InlineData("a2", true, TestEnum.Value2)]
    public void Enum_FromAbbreviation
    (
        string? abbreviation,
        bool ignoreCase,
        TestEnum? expectedValue
    )
    {
        TestEnum? actualValue = EnumExtensions.FromAbbreviation<TestEnum>(abbreviation!, ignoreCase);
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Enum_ToShortName()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
        string? shortName = value.ToShortName();

        // Assert
        Assert.Equal("Short1", shortName);
    }

    // New tests based on FromDescription pattern for FromShortName
    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("test", false, null)]
    [InlineData(null, true, null)]
    [InlineData("", true, null)]
    [InlineData("test", true, null)]
    [InlineData("Short1", false, TestEnum.Value1)]
    [InlineData("SHORT1", true, TestEnum.Value1)]
    [InlineData("Short2", false, TestEnum.Value2)]
    [InlineData("SHORT2", true, TestEnum.Value2)]
    public void Enum_FromShortName
    (
        string? shortName,
        bool ignoreCase,
        TestEnum? expectedValue
    )
    {
        // Act
        TestEnum? actualValue = EnumExtensions.FromShortName<TestEnum>(shortName!, ignoreCase);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }
}
