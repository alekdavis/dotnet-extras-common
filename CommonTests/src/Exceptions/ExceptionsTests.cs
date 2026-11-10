using DotNetExtras.Common.Exceptions;

namespace CommonLibTests.Exceptions;

public partial class ExceptionsTests
{
    [Fact]
    public void Exception_GetMessages()
    {
        Exception inner1 = new("Inner exception 1");
        Exception inner2 = new("Inner exception 2");

        AggregateException ae = new("Aggregate exception", inner1, inner2);
        Exception ex = new("Outer exception", ae);

        string messages = ex.GetMessages<Exception>();

        Assert.Equal("Outer exception. Aggregate exception (Inner exception 1) (Inner exception 2) Inner exception 1.", messages);

        ex = new("OUTER EXCEPTION", new Exception("INNER EXCEPTION 1", new Exception("INNER EXCEPTION 2")));

        messages = ex.GetMessages();

        Assert.Equal("OUTER EXCEPTION. INNER EXCEPTION 1. INNER EXCEPTION 2.", messages);

        string errMsg = " This is \t an error  \r\n\rmessage\r    with   irregular \n spacing \r";
        ex = new Exception(errMsg);

        messages = ex.GetMessages();

        Assert.Equal("This is an error message with irregular spacing.", messages);
    }

    [Fact]
    public void Exception_GetSafeMessages()
    {
        SafeException ex = new("Safe Outer Exception", new SafeException("Safe Inner Exception", new Exception("Unsafe Exception")));

        string messages1 = ex.GetSafeMessages();
        string messages2 = ex.GetMessages<SafeException>();

        Assert.Equal("Safe Outer Exception. Safe Inner Exception.", messages1);
        Assert.Equal(messages2, messages1);
    }

    [Fact]
    public void Exception_GetMessages_NoDuplicates()
    {
        SafeException ex1 = new("No connection could be made because the target machine actively refused it.");
        SafeException ex2 = new("No connection could be made because the target machine actively refused it. (localhost:7000)", ex1);
        SafeException ex3 = new("Call failed. No connection could be made because the target machine actively refused it. (localhost:7000): POST https://localhost:7000/Endpoint?A=B", ex2);

        Assert.Equal("Call failed. No connection could be made because the target machine actively refused it. (localhost:7000): POST https://localhost:7000/Endpoint?A=B.", ex3.GetMessages());
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_SingleQuotes()
    {
        Exception ex = new("Workgroup 'Approver-DevTools JIRA Hades  All Roles-AWG' does not exist.");

        Assert.Equal(
            "Workgroup 'Approver-DevTools JIRA Hades  All Roles-AWG' does not exist.",
            ex.GetMessages(preserveQuoted: true));
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_DoubleQuotes()
    {
        Exception ex = new("Workgroup \"Approver-DevTools JIRA Hades  All Roles-AWG\" does not exist.");

        Assert.Equal(
            "Workgroup \"Approver-DevTools JIRA Hades  All Roles-AWG\" does not exist.",
            ex.GetMessages(preserveQuoted: true));
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_NormalizesOutsideQuotes()
    {
        Exception ex = new("Workgroup   'Keep  Me'   is \t broken.");

        Assert.Equal(
            "Workgroup 'Keep  Me' is broken.",
            ex.GetMessages(preserveQuoted: true));
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_MultipleQuotedSpans()
    {
        Exception ex = new("Value 'first  one' differs from \"second  one\" here.");

        Assert.Equal(
            "Value 'first  one' differs from \"second  one\" here.",
            ex.GetMessages(preserveQuoted: true));
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_UnbalancedQuote()
    {
        Exception ex = new("Unterminated   'value  with   spaces");

        string messages = ex.GetMessages(preserveQuoted: true);

        Assert.Equal("Unterminated 'value with spaces.", messages);
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_DefaultPreserves()
    {
        Exception ex = new("Workgroup 'Approver-DevTools JIRA Hades  All Roles-AWG' does not exist.");

        // preserveQuoted now defaults to true, so quoted content is preserved by default.
        Assert.Equal(
            "Workgroup 'Approver-DevTools JIRA Hades  All Roles-AWG' does not exist.",
            ex.GetMessages());
    }

    [Fact]
    public void Exception_GetMessages_PreserveQuoted_FalseCollapses()
    {
        Exception ex = new("Workgroup 'Approver-DevTools JIRA Hades  All Roles-AWG' does not exist.");

        // Opting out collapses all whitespace, including inside quotes.
        Assert.Equal(
            "Workgroup 'Approver-DevTools JIRA Hades All Roles-AWG' does not exist.",
            ex.GetMessages(preserveQuoted: false));
    }
}
