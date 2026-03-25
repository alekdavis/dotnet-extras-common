using DotNetExtras.Common;

namespace CommonLibTests;

public class CodeContextTests
{
    [Fact]
    public void GetClassName_WithNull_ReturnsEmpty()
    {
        string result = CodeContext.GetClassName(null!);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetClassName_ShortName()
    {
        string result = CodeContext.GetClassName(this);
        Assert.Equal(nameof(CodeContextTests), result);
    }

    [Fact]
    public void GetClassName_FullName()
    {
        string result = CodeContext.GetClassName(this, useFullName: true);
        Assert.Equal(GetType().FullName, result);
    }

    [Fact]
    public void GetClassMethodName_WithCaller_ShortName()
    {
        string result = CodeContext.GetClassMethodName(this);
        Assert.Equal($"{nameof(CodeContextTests)}.{nameof(GetClassMethodName_WithCaller_ShortName)}", result);
    }

    [Fact]
    public void GetClassMethodName_WithCaller_FullName()
    {
        string result = CodeContext.GetClassMethodName(this, useFullName: true);
        Assert.Equal($"{GetType().FullName}.{nameof(GetClassMethodName_WithCaller_FullName)}", result);
    }

    [Fact]
    public void GetClassMethodName_WithoutCaller_ReturnsMethodNameOnly()
    {
        string result = CodeContext.GetClassMethodName();
        Assert.Equal(nameof(GetClassMethodName_WithoutCaller_ReturnsMethodNameOnly), result);
    }

    [Fact]
    public void GetMethodName_ReturnsCallerMethodName()
    {
        string result = CodeContext.GetMethodName();
        Assert.Equal(nameof(GetMethodName_ReturnsCallerMethodName), result);
    }

    [Fact]
    public void GetFilePath_ReturnsFullPathEndingWithFileName()
    {
        string path = CodeContext.GetFilePath();
        Assert.EndsWith($"{nameof(CodeContextTests)}.cs", path, StringComparison.OrdinalIgnoreCase);
        Assert.True(Path.IsPathRooted(path));
    }

    [Fact]
    public void GetFileName_WithoutExtension()
    {
        string name = CodeContext.GetFileName();
        Assert.Equal(nameof(CodeContextTests), name);
    }

    [Fact]
    public void GetFileName_WithExtension()
    {
        string name = CodeContext.GetFileName(withExtension: true);
        Assert.Equal($"{nameof(CodeContextTests)}.cs", name);
    }

    [Fact]
    public void GetLineNumber_IncrementsOnNextLine()
    {
        int line1 = CodeContext.GetLineNumber();
        int line2 = CodeContext.GetLineNumber();
        Assert.Equal(line1 + 1, line2);
    }
}
