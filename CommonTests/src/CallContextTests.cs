using DotNetExtras.Common;

namespace CommonLibTests;
public class CallContextTests
{
    [Fact]
    public void GetClassName_WithNull_ReturnsEmpty()
    {
        string result = CallContext.GetClassName(null!);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetClassName_ShortName()
    {
        string result = CallContext.GetClassName(this);
        Assert.Equal(nameof(CallContextTests), result);
    }

    [Fact]
    public void GetClassName_FullName()
    {
        string result = CallContext.GetClassName(this, useFullName: true);
        Assert.Equal(GetType().FullName, result);
    }

    [Fact]
    public void GetClassMethodName_WithCaller_ShortName()
    {
        string result = CallContext.GetClassMethodName(this);
        Assert.Equal($"{nameof(CallContextTests)}.{nameof(GetClassMethodName_WithCaller_ShortName)}", result);
    }

    [Fact]
    public void GetClassMethodName_WithCaller_FullName()
    {
        string result = CallContext.GetClassMethodName(this, useFullName: true);
        Assert.Equal($"{GetType().FullName}.{nameof(GetClassMethodName_WithCaller_FullName)}", result);
    }

    [Fact]
    public void GetClassMethodName_WithoutCaller_ReturnsMethodNameOnly()
    {
        string result = CallContext.GetClassMethodName();
        Assert.Equal(nameof(GetClassMethodName_WithoutCaller_ReturnsMethodNameOnly), result);
    }

    [Fact]
    public void GetMethodName_ReturnsCallerMethodName()
    {
        string result = CallContext.GetMethodName();
        Assert.Equal(nameof(GetMethodName_ReturnsCallerMethodName), result);
    }

    [Fact]
    public void GetFilePath_ReturnsFullPathEndingWithFileName()
    {
        string path = CallContext.GetFilePath();
        Assert.EndsWith($"{nameof(CallContextTests)}.cs", path, StringComparison.OrdinalIgnoreCase);
        Assert.True(Path.IsPathRooted(path));
    }

    [Fact]
    public void GetFileName_WithoutExtension()
    {
        string name = CallContext.GetFileName();
        Assert.Equal(nameof(CallContextTests), name);
    }

    [Fact]
    public void GetFileName_WithExtension()
    {
        string name = CallContext.GetFileName(withExtension: true);
        Assert.Equal($"{nameof(CallContextTests)}.cs", name);
    }

    [Fact]
    public void GetLineNumber_IncrementsOnNextLine()
    {
        int line1 = CallContext.GetLineNumber();
        int line2 = CallContext.GetLineNumber();
        Assert.Equal(line1 + 1, line2);
    }
}
