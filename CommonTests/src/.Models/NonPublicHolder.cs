namespace CommonLibTests.Models;

/// <summary>
/// Test model that exposes non-public members through a nested object so that
/// propagation of the <c>includeNonPublic</c> flag can be verified.
/// </summary>
internal class NonPublicHolder
{
    public string? PublicValue { get; set; }

    public NonPublicNested? Nested { get; set; }
}

internal class NonPublicNested
{
    public string? PublicValue { get; set; }

    internal string? NonPublicValue { get; set; }
}
