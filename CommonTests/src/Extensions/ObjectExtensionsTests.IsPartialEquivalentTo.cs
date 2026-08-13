using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests.Extensions;

public partial class ObjectExtensionsTests
{
    [Fact]
    public void Object_IsPartialEquivalentOf_Boolean()
    {
        Assert.True(true.IsPartialEquivalentOf(true));
        Assert.True(false.IsPartialEquivalentOf(false));
        Assert.False(true.IsPartialEquivalentOf(false));
        Assert.False(false.IsPartialEquivalentOf(true));
        Assert.False(true.IsPartialEquivalentOf(null));
        Assert.False(false.IsPartialEquivalentOf(null));
        Assert.True(true.IsPartialEquivalentOf("true"));
        Assert.True(false.IsPartialEquivalentOf("false"));
        Assert.False(true.IsPartialEquivalentOf("false"));
        Assert.False(false.IsPartialEquivalentOf("true"));
        Assert.True("true".IsPartialEquivalentOf(true));
        Assert.True("false".IsPartialEquivalentOf(false));
        Assert.False("true".IsPartialEquivalentOf(false));
        Assert.False("false".IsPartialEquivalentOf(true));
        Assert.True(true.IsPartialEquivalentOf(1));
        Assert.False(true.IsPartialEquivalentOf(0));
        Assert.True(false.IsPartialEquivalentOf(0));
        Assert.False(false.IsPartialEquivalentOf(1));
        Assert.False(true.IsPartialEquivalentOf(2));
        Assert.False(false.IsPartialEquivalentOf(2));
        Assert.False(2.IsPartialEquivalentOf(true));
        Assert.False(2.IsPartialEquivalentOf(false));

        bool? b1 = null;

        Assert.True(b1.IsPartialEquivalentOf(null));
        Assert.True(b1.IsPartialEquivalentOf(true));
        Assert.True(b1.IsPartialEquivalentOf(false));
        Assert.False(true.IsPartialEquivalentOf(b1));
        Assert.False(false.IsPartialEquivalentOf(b1));

        b1 = true;

        Assert.True(b1.IsPartialEquivalentOf(true));
        Assert.False(b1.IsPartialEquivalentOf(false));
        Assert.True(true.IsPartialEquivalentOf(b1));
        Assert.False(false.IsPartialEquivalentOf(b1));

        b1 = null;
        bool? b2 = null;

        Assert.True(b1.IsPartialEquivalentOf(b2));
        Assert.True(b2.IsPartialEquivalentOf(b1));

        b1 = true;

        Assert.False(b1.IsPartialEquivalentOf(b2));
        Assert.True(b2.IsPartialEquivalentOf(b1));

        b2 = true;

        Assert.True(b1.IsPartialEquivalentOf(b2));
        Assert.True(b2.IsPartialEquivalentOf(b1));

        b2 = false;

        Assert.False(b1.IsPartialEquivalentOf(b2));
        Assert.False(b2.IsPartialEquivalentOf(b1));

        bool b3 = true;

        Assert.True(b3.IsPartialEquivalentOf(true));
        Assert.False(b3.IsPartialEquivalentOf(false));
        Assert.False(b3.IsPartialEquivalentOf(null));
        Assert.True(b3.IsPartialEquivalentOf(b1));
        Assert.True(b1.IsPartialEquivalentOf(b3));

        Assert.False(b2.IsPartialEquivalentOf(b1));
        Assert.False(b1.IsPartialEquivalentOf(b2));

        bool b4 = true;

        Assert.True(b4.IsPartialEquivalentOf(b3));
        Assert.True(b3.IsPartialEquivalentOf(b4));

        b4 = false;

        Assert.False(b4.IsPartialEquivalentOf(b3));
        Assert.False(b3.IsPartialEquivalentOf(b4));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_Enum()
    {
        PhoneType e1 = PhoneType.Personal;
        PhoneType e2 = PhoneType.Personal;

        Assert.True(e1.IsPartialEquivalentOf(e2));
        Assert.True(e1.IsPartialEquivalentOf("personal"));
        Assert.True(e1.IsPartialEquivalentOf("Personal"));
        Assert.True(e1.IsPartialEquivalentOf(0));
        Assert.False(e1.IsPartialEquivalentOf(1));
        Assert.False(e1.IsPartialEquivalentOf(null));

        e2 = PhoneType.Business;

        Assert.False(e1.IsPartialEquivalentOf(e2));

        PhoneType? e3 = null;

        Assert.True(e3.IsPartialEquivalentOf(e1));
        Assert.False(e1.IsPartialEquivalentOf(e3));

        e3 = PhoneType.Business;

        Assert.False(e1.IsPartialEquivalentOf(e3));
        Assert.False(e3.IsPartialEquivalentOf(e1));
        Assert.True(e2.IsPartialEquivalentOf(e3));
        Assert.True(e3.IsPartialEquivalentOf(e2));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_String()
    {
        string? s0 = null;
        string s1 = "ABC";
        string s2 = "ABC";

        Assert.True(s1.IsPartialEquivalentOf(s2));
        Assert.True(s2.IsPartialEquivalentOf(s1));
        Assert.True(s1.IsPartialEquivalentOf("ABC"));
        Assert.True("ABC".IsPartialEquivalentOf(s1));

        Assert.True(s0.IsPartialEquivalentOf(s1));
        Assert.False(s1.IsPartialEquivalentOf(null));

        s2 = "";
        Assert.False(s1.IsPartialEquivalentOf(s2));
        Assert.False(s2.IsPartialEquivalentOf(s1));
        Assert.False(s2.IsPartialEquivalentOf("ABC"));
        Assert.False("ABC".IsPartialEquivalentOf(s2));

        s2 = "abc";
        Assert.False(s1.IsPartialEquivalentOf(s2));
        Assert.False(s2.IsPartialEquivalentOf(s1));
        Assert.False(s2.IsPartialEquivalentOf("ABC"));
        Assert.False("ABC".IsPartialEquivalentOf(s2));

        string? s3 = null;
        string? s4 = null;

        Assert.True(s3.IsPartialEquivalentOf(s4));
        Assert.True(s3.IsPartialEquivalentOf(null));
        Assert.True(s3.IsPartialEquivalentOf(s2));
        Assert.False(s2.IsPartialEquivalentOf(s3));

        s3 = "ABC";
        Assert.True(s3.IsPartialEquivalentOf(s1));
        Assert.True(s1.IsPartialEquivalentOf(s3));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_Integer()
    {
        short? n0 = null;

        short n1 = 1;

        Assert.True(n0.IsPartialEquivalentOf(1));
        Assert.True(n0.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n1));
        Assert.False(n1.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n1));
        Assert.False(n1.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n1));

        short n2 = 1;

        Assert.True(n1.IsPartialEquivalentOf(n2));
        Assert.True(n2.IsPartialEquivalentOf(n1));

        n2 = 2;

        Assert.False(n1.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n1));

        int n3 = 1;

        Assert.True(n3.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n3));
        Assert.False(n3.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n3));
        Assert.False(n3.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n3));

        Assert.True(n3.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(n3));
        Assert.False(n3.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n3));

        int n4 = 1;

        Assert.True(n3.IsPartialEquivalentOf(n4));
        Assert.True(n4.IsPartialEquivalentOf(n3));

        n4 = 2;

        Assert.False(n3.IsPartialEquivalentOf(n4));
        Assert.False(n4.IsPartialEquivalentOf(n3));

        long n5 = 1;

        Assert.True(n5.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n5));
        Assert.False(n5.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n5));
        Assert.False(n5.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n5));

        Assert.True(n5.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(n5));
        Assert.False(n5.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n5));

        Assert.True(n5.IsPartialEquivalentOf(n3));
        Assert.True(n3.IsPartialEquivalentOf(n5));
        Assert.False(n5.IsPartialEquivalentOf(n4));
        Assert.False(n4.IsPartialEquivalentOf(n5));

        long n6 = 1;

        Assert.True(n5.IsPartialEquivalentOf(n6));
        Assert.True(n6.IsPartialEquivalentOf(n5));

        n6 = 2;

        Assert.False(n5.IsPartialEquivalentOf(n6));
        Assert.False(n6.IsPartialEquivalentOf(n5));

        ushort n7 = 1;

        Assert.True(n7.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n7));
        Assert.False(n7.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n7));
        Assert.False(n7.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n7));

        Assert.True(n7.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(n7));
        Assert.False(n7.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n7));

        Assert.True(n7.IsPartialEquivalentOf(n3));
        Assert.True(n3.IsPartialEquivalentOf(n7));
        Assert.False(n7.IsPartialEquivalentOf(n4));
        Assert.False(n4.IsPartialEquivalentOf(n7));

        Assert.True(n7.IsPartialEquivalentOf(n5));
        Assert.True(n5.IsPartialEquivalentOf(n7));
        Assert.False(n7.IsPartialEquivalentOf(n6));
        Assert.False(n6.IsPartialEquivalentOf(n7));

        ushort n8 = 1;

        Assert.True(n7.IsPartialEquivalentOf(n8));
        Assert.True(n8.IsPartialEquivalentOf(n7));

        n8 = 2;

        Assert.False(n7.IsPartialEquivalentOf(n8));
        Assert.False(n8.IsPartialEquivalentOf(n7));

        uint n9 = 1;

        Assert.True(n9.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n9));

        Assert.True(n9.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n9));

        Assert.True(n9.IsPartialEquivalentOf(n3));
        Assert.True(n3.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(n4));
        Assert.False(n4.IsPartialEquivalentOf(n9));

        Assert.True(n9.IsPartialEquivalentOf(n5));
        Assert.True(n5.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(n6));
        Assert.False(n6.IsPartialEquivalentOf(n9));

        Assert.True(n9.IsPartialEquivalentOf(n7));
        Assert.True(n7.IsPartialEquivalentOf(n9));
        Assert.False(n9.IsPartialEquivalentOf(n8));
        Assert.False(n8.IsPartialEquivalentOf(n9));

        ushort n10 = 1;

        Assert.True(n9.IsPartialEquivalentOf(n10));
        Assert.True(n10.IsPartialEquivalentOf(n9));

        n10 = 2;

        Assert.False(n9.IsPartialEquivalentOf(n10));
        Assert.False(n10.IsPartialEquivalentOf(n9));

        uint n11 = 1;

        Assert.True(n11.IsPartialEquivalentOf(1));
        Assert.True(1.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(0));
        Assert.False(0.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(-1));
        Assert.False((-1).IsPartialEquivalentOf(n11));

        Assert.True(n11.IsPartialEquivalentOf(n1));
        Assert.True(n1.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(n2));
        Assert.False(n2.IsPartialEquivalentOf(n11));

        Assert.True(n11.IsPartialEquivalentOf(n3));
        Assert.True(n3.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(n4));
        Assert.False(n4.IsPartialEquivalentOf(n11));

        Assert.True(n11.IsPartialEquivalentOf(n5));
        Assert.True(n5.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(n6));
        Assert.False(n6.IsPartialEquivalentOf(n11));

        Assert.True(n11.IsPartialEquivalentOf(n7));
        Assert.True(n7.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(n8));
        Assert.False(n8.IsPartialEquivalentOf(n11));

        Assert.True(n11.IsPartialEquivalentOf(n9));
        Assert.True(n9.IsPartialEquivalentOf(n11));
        Assert.False(n11.IsPartialEquivalentOf(n10));
        Assert.False(n10.IsPartialEquivalentOf(n11));

        ushort n12 = 1;

        Assert.True(n11.IsPartialEquivalentOf(n12));
        Assert.True(n12.IsPartialEquivalentOf(n11));

        n12 = 2;

        Assert.False(n11.IsPartialEquivalentOf(n12));
        Assert.False(n12.IsPartialEquivalentOf(n11));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_DateTime()
    {
        string s;
        DateTime? d0 = null;

        DateTime d1 = new(2020, 1, 2, 3, 4, 5, 678);
        DateTime d2 = new(2020, 1, 2, 3, 4, 5, 678);

        Assert.True(d0.IsPartialEquivalentOf(d1));
        Assert.False(d1.IsPartialEquivalentOf(d0));
        Assert.True(d1.IsPartialEquivalentOf(d2));
        Assert.True(d2.IsPartialEquivalentOf(d1));

        DateTime d3 = new(2020, 1, 2, 3, 4, 5, 789);

        Assert.False(d1.IsPartialEquivalentOf(d3));
        Assert.False(d3.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d1.IsPartialEquivalentOf(s));
        Assert.True(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:00";

        Assert.True(d1.IsPartialEquivalentOf(s));
        Assert.True(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.False(d1.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.789Z";

        Assert.False(d1.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.789+00:00";

        Assert.False(d1.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d1));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_DateTimeOffset()
    {
        string s;

        DateTimeOffset? d0 = null;
        DateTimeOffset d1 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));
        DateTimeOffset d2 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));

        Assert.True(d0.IsPartialEquivalentOf(d1));
        Assert.False(d1.IsPartialEquivalentOf(d0));
        Assert.True(d1.IsPartialEquivalentOf(d2));
        Assert.True(d2.IsPartialEquivalentOf(d1));

        DateTimeOffset d3 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(-30));

        Assert.False(d1.IsPartialEquivalentOf(d3));
        Assert.False(d3.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.True(d1.IsPartialEquivalentOf(s));
        Assert.True(s.IsPartialEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d1));
        Assert.True(d3.IsPartialEquivalentOf(s));
        Assert.True(s.IsPartialEquivalentOf(d3));

        DateTimeOffset d4 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromSeconds(0));
        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d4.IsPartialEquivalentOf(s));
        Assert.True(s.IsPartialEquivalentOf(d4));

        s = "2020-01-02T03:04:05.876Z";

        Assert.False(d4.IsPartialEquivalentOf(s));
        Assert.False(s.IsPartialEquivalentOf(d4));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_ArrayNumeric()
    {
        int[] aInt1 = [1, 2, 3];
        int[] aInt2 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentOf(aInt2));
        Assert.True(aInt2.IsPartialEquivalentOf(aInt1));

        int[] aInt3 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentOf(aInt3));
        Assert.True(aInt3.IsPartialEquivalentOf(aInt1));

        int[] aInt4 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentOf(aInt4));
        Assert.False(aInt4.IsPartialEquivalentOf(aInt1));

        long[] aLong1 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentOf(aLong1));
        Assert.True(aLong1.IsPartialEquivalentOf(aInt1));

        long[] aLong2 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentOf(aLong2));
        Assert.True(aLong2.IsPartialEquivalentOf(aInt1));

        long[] aLong3 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentOf(aLong3));
        Assert.False(aLong3.IsPartialEquivalentOf(aInt1));

        short[] aShort1 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentOf(aShort1));
        Assert.True(aShort1.IsPartialEquivalentOf(aInt1));

        short[] aShort2 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentOf(aShort2));
        Assert.True(aShort2.IsPartialEquivalentOf(aInt1));

        short[] aShort3 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentOf(aShort3));
        Assert.False(aShort3.IsPartialEquivalentOf(aInt1));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_StringCollections()
    {
        string[] a1 = ["one", "two", "three",];
        string[] a2 = ["one", "two", "three",];

        Assert.True(a1.IsPartialEquivalentOf(a2));
        Assert.True(a2.IsPartialEquivalentOf(a1));

        string[] a3 = ["one", "two",];

        Assert.False(a1.IsPartialEquivalentOf(a3));
        Assert.True(a3.IsPartialEquivalentOf(a1));

        string[] a4 = ["three", "two", "one"];

        Assert.False(a1.IsPartialEquivalentOf(a4));
        Assert.False(a4.IsPartialEquivalentOf(a1));

        string[] a5 = ["One", "Two", "Three"];

        Assert.False(a1.IsPartialEquivalentOf(a5));
        Assert.False(a5.IsPartialEquivalentOf(a1));

        List<string> l1 = ["one", "two", "three",];
        List<string> l2 = ["one", "two", "three",];

        Assert.True(l1.IsPartialEquivalentOf(l2));
        Assert.True(l2.IsPartialEquivalentOf(l1));
        Assert.True(l1.IsPartialEquivalentOf(a1));
        Assert.True(a1.IsPartialEquivalentOf(l1));

        List<string> l3 = ["one", "two",];

        Assert.False(l1.IsPartialEquivalentOf(l3));
        Assert.True(l3.IsPartialEquivalentOf(l1));

        Assert.True(l3.IsPartialEquivalentOf(a1));
        Assert.False(a1.IsPartialEquivalentOf(l3));

        List<string> l4 = ["three", "two", "one"];

        Assert.False(l1.IsPartialEquivalentOf(l4));
        Assert.False(l4.IsPartialEquivalentOf(l1));

        Assert.False(l4.IsPartialEquivalentOf(a1));
        Assert.False(a1.IsPartialEquivalentOf(l4));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_Dictionary()
    {
        Dictionary<string, string> d0 = new()
        {
            ["one"] = "two",
        };
        Dictionary<string, string> d1 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "six"
        };
        Dictionary<string, string> d2 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "six"
        };

        Assert.True(d0.IsPartialEquivalentOf(d1));
        Assert.False(d1.IsPartialEquivalentOf(d0));
        Assert.True(d1.IsPartialEquivalentOf(d2));
        Assert.True(d2.IsPartialEquivalentOf(d1));

        Dictionary<string, string> d3 = new()
        {
            ["one"] = "two",
            ["three"] = "four"
        };

        Assert.False(d1.IsPartialEquivalentOf(d3));
        Assert.True(d3.IsPartialEquivalentOf(d1));

        Dictionary<string, string> d4 = new()
        {
            ["one"] = "two",
            ["three"] = "ten",
            ["five"] = "six"
        };

        Assert.False(d1.IsPartialEquivalentOf(d4));
        Assert.False(d4.IsPartialEquivalentOf(d1));

        Dictionary<string, string> d5 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "Six"
        };

        Assert.False(d1.IsPartialEquivalentOf(d5));
        Assert.False(d5.IsPartialEquivalentOf(d1));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_HashSet()
    {
        HashSet<string>? h0 = null;
        HashSet<string> h1 = ["one", "two", "three"];
        HashSet<string> h2 = ["one", "two", "three"];

        Assert.True(h0.IsPartialEquivalentOf(h1));
        Assert.False(h1.IsPartialEquivalentOf(h0));
        Assert.True(h1.IsPartialEquivalentOf(h2));
        Assert.True(h2.IsPartialEquivalentOf(h1));

        HashSet<string> h3 = ["two", "three"];

        Assert.False(h1.IsPartialEquivalentOf(h3));
        Assert.True(h3.IsPartialEquivalentOf(h1));

        HashSet<string> h4 = ["one", "three", "two"];

        Assert.True(h1.IsPartialEquivalentOf(h4));
        Assert.True(h4.IsPartialEquivalentOf(h1));

        HashSet<int>? i0 = null;
        HashSet<int> i1 = [1, 2, 3];
        HashSet<int> i2 = [1, 2, 3];

        Assert.True(i0.IsPartialEquivalentOf(i1));
        Assert.False(i1.IsPartialEquivalentOf(i0));
        Assert.True(i1.IsPartialEquivalentOf(i2));
        Assert.True(i2.IsPartialEquivalentOf(i1));

        HashSet<int> i3 = [2, 3];

        Assert.False(i1.IsPartialEquivalentOf(i3));
        Assert.True(i3.IsPartialEquivalentOf(i1));

        HashSet<int> i4 = [1, 3, 2];

        Assert.True(i1.IsPartialEquivalentOf(i4));
        Assert.True(i4.IsPartialEquivalentOf(i1));

        HashSet<string> e1 = [];
        HashSet<string> e2 = [];

        Assert.True(e1.IsPartialEquivalentOf(e2));
        Assert.True(e2.IsPartialEquivalentOf(e1));

        // An empty source is a partial equivalent of a non-empty target,
        // but a larger source cannot be a partial equivalent of a smaller target.
        Assert.True(e1.IsPartialEquivalentOf(h1));
        Assert.False(h1.IsPartialEquivalentOf(e1));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_IncludeNonPublic_Nested()
    {
        NonPublicHolder source = new()
        {
            PublicValue = "same",
            Nested = new()
            {
                PublicValue = "same",
                NonPublicValue = "sourceValue"
            }
        };

        NonPublicHolder target = new()
        {
            PublicValue = "same",
            Nested = new()
            {
                PublicValue = "same",
                NonPublicValue = "targetValue"
            }
        };

        // Non-public members differ, but they are ignored by default.
        Assert.True(source.IsPartialEquivalentOf(target));

        // When non-public members are included, the differing non-public value on
        // the nested object must be detected, proving the flag is propagated
        // through the property-comparison path.
        Assert.False(source.IsPartialEquivalentOf(target, includeNonPublic: true));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_Class_Simple()
    {
        User u1 = new()
        {
            Name = new()
            {
                Surname = "Johnson",
                MiddleName = "Jack",
                GivenName = "John"
            },
        };

        User u2 = new()
        {
            Name = new()
            {
                Surname = "Johnson",
                MiddleName = "Jack",
                GivenName = "John"
            },
        };

        Assert.True(u1.IsPartialEquivalentOf(u2, true));
        Assert.True(u2.IsPartialEquivalentOf(u1, true));

        u1.Name.Surname = "Smith";

        Assert.False(u2.IsPartialEquivalentOf(u1, true));
        Assert.False(u1.IsPartialEquivalentOf(u2, true));

        u1.Name.Surname = null;
        Assert.False(u2.IsPartialEquivalentOf(u1, true));
        Assert.True(u1.IsPartialEquivalentOf(u2, true));

        u1.Name.GivenName = null;
        Assert.False(u2.IsPartialEquivalentOf(u1, true));
        Assert.True(u1.IsPartialEquivalentOf(u2, true));
    }

    [Fact]
    public void Object_IsPartialEquivalentOf_Class()
    {
        User u1 = new()
        {
            Name = new()
            {
                Surname = "Johnson",
                MiddleName = "Jack",
                GivenName = "John"
            },

            Mail = "john.johnson@email.com",

            OtherMail = ["jack.johnson@email.com", "jjohnson@email.com", "jj@email.com"],

            LuckyNumbers = [13, 57, 95, 38],

            PasswordExpirationDate = DateTime.Parse("2012/12/31 23:59:59.999"),

            SocialAccounts = new()
            {
                { "Facebook", new SocialAccount() { Provider = "Facebook", Account = "jack.johnson@email.com", Enabled = true} },
                { "Microsoft", new SocialAccount() { Provider = "Microsoft", Account = "jack.johnson@email.com", Enabled = true} },
                { "Google", new SocialAccount() { Provider = "Google", Account = "jack.johnson@email.com", Enabled = true} },
            },

            Phones =
            [
                new Phone { IsMobile = true , IsPrimary = true, Number = "+13334445566", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445577", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445588", Type = PhoneType.Business },
                new Phone { IsMobile = false, IsPrimary = false, Number = "+13334445599", Type = PhoneType.Other },
                new Phone { IsMobile = true, IsPrimary = false, Number = "+13334445500", Type = PhoneType.Other },
            ],

            Tags = new()
            {
                ["greeting"] = "hello",
                ["color"] = "red",
                ["shape"] = "oval"
            }
        };

        User u2 = new()
        {
            Name = new()
            {
                Surname = "Johnson",
                MiddleName = "Jack",
                GivenName = "John"
            },

            Mail = "john.johnson@email.com",

            OtherMail = ["jack.johnson@email.com", "jjohnson@email.com", "jj@email.com"],

            LuckyNumbers = [13, 57, 95, 38],

            PasswordExpirationDate = DateTime.Parse("2012/12/31 23:59:59.999"),

            SocialAccounts = new()
            {
                { "Facebook", new SocialAccount() { Provider = "Facebook", Account = "jack.johnson@email.com", Enabled = true} },
                { "Microsoft", new SocialAccount() { Provider = "Microsoft", Account = "jack.johnson@email.com", Enabled = true} },
                { "Google", new SocialAccount() { Provider = "Google", Account = "jack.johnson@email.com", Enabled = true} },
            },

            Phones =
            [
                new Phone { IsMobile = true , IsPrimary = true, Number = "+13334445566", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445577", Type = PhoneType.Personal },
                new Phone { IsMobile = false , IsPrimary = false, Number = "+13334445588", Type = PhoneType.Business },
                new Phone { IsMobile = false, IsPrimary = false, Number = "+13334445599", Type = PhoneType.Other },
                new Phone { IsMobile = true, IsPrimary = false, Number = "+13334445500", Type = PhoneType.Other },
            ],

            Tags = new()
            {
                ["greeting"] = "hello",
                ["color"] = "red",
                ["shape"] = "oval"
            }
        };

        Assert.True(u1.IsPartialEquivalentOf(u2, true));
        Assert.True(u2.IsPartialEquivalentOf(u1, true));

        u2.Tags?.Remove("greeting");

        Assert.True(u2.IsPartialEquivalentOf(u1, true));
        Assert.False(u1.IsPartialEquivalentOf(u2, true));

        u2.Tags?.Clear();

        Assert.True(u2.IsPartialEquivalentOf(u1, true));
        Assert.False(u1.IsPartialEquivalentOf(u2, true));

        u2.Tags = new()
        {
            ["greeting"] = "hello",
            ["color"] = "red",
            ["shape"] = "oval"
        };

        Assert.True(u1.IsPartialEquivalentOf(u2, true));
        Assert.True(u2.IsPartialEquivalentOf(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hi";
        }

        Assert.False(u1.IsPartialEquivalentOf(u2, true));
        Assert.False(u2.IsPartialEquivalentOf(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hello";
        }

        Assert.True(u1.IsPartialEquivalentOf(u2, true));
        Assert.True(u2.IsPartialEquivalentOf(u1, true));

        u2.SocialAccounts?.Remove("Facebook");

        Assert.True(u2.IsPartialEquivalentOf(u1, true));
        Assert.False(u1.IsPartialEquivalentOf(u2, true));

        u2.SocialAccounts?.Add("Facebook", new()
        {
            Provider = "Facebook",
            Account = "jack.johnson@email.com",
            Enabled = true
        });

        Assert.True(u1.IsPartialEquivalentOf(u2, true));
        Assert.True(u2.IsPartialEquivalentOf(u1, true));

        if (u2.SocialAccounts?["Facebook"] != null)
        {
            u2.SocialAccounts["Facebook"].Enabled = false;
        }

        Assert.False(u1.IsPartialEquivalentOf(u2, true));
        Assert.False(u2.IsPartialEquivalentOf(u1, true));
    }
}
