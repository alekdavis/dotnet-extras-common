using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_IsPartiallyEquivalentTo_Boolean()
    {
        Assert.True(true.IsPartiallyEquivalentTo(true));
        Assert.True(false.IsPartiallyEquivalentTo(false));
        Assert.False(true.IsPartiallyEquivalentTo(false));
        Assert.False(false.IsPartiallyEquivalentTo(true));
        Assert.False(true.IsPartiallyEquivalentTo(null));
        Assert.False(false.IsPartiallyEquivalentTo(null));
        Assert.True(true.IsPartiallyEquivalentTo("true"));
        Assert.True(false.IsPartiallyEquivalentTo("false"));
        Assert.False(true.IsPartiallyEquivalentTo("false"));
        Assert.False(false.IsPartiallyEquivalentTo("true"));
        Assert.True("true".IsPartiallyEquivalentTo(true));
        Assert.True("false".IsPartiallyEquivalentTo(false));
        Assert.False("true".IsPartiallyEquivalentTo(false));
        Assert.False("false".IsPartiallyEquivalentTo(true));
        Assert.True(true.IsPartiallyEquivalentTo(1));
        Assert.False(true.IsPartiallyEquivalentTo(0));
        Assert.True(false.IsPartiallyEquivalentTo(0));
        Assert.False(false.IsPartiallyEquivalentTo(1));
        Assert.False(true.IsPartiallyEquivalentTo(2));
        Assert.False(false.IsPartiallyEquivalentTo(2));
        Assert.False(2.IsPartiallyEquivalentTo(true));
        Assert.False(2.IsPartiallyEquivalentTo(false));

        bool? b1 = null;

        Assert.True(b1.IsPartiallyEquivalentTo(null));
        Assert.True(b1.IsPartiallyEquivalentTo(true));
        Assert.True(b1.IsPartiallyEquivalentTo(false));
        Assert.False(true.IsPartiallyEquivalentTo(b1));
        Assert.False(false.IsPartiallyEquivalentTo(b1));

        b1 = true;

        Assert.True(b1.IsPartiallyEquivalentTo(true));
        Assert.False(b1.IsPartiallyEquivalentTo(false));
        Assert.True(true.IsPartiallyEquivalentTo(b1));
        Assert.False(false.IsPartiallyEquivalentTo(b1));
        
        b1 = null;
        bool? b2 = null;

        Assert.True(b1.IsPartiallyEquivalentTo(b2));
        Assert.True(b2.IsPartiallyEquivalentTo(b1));

        b1 = true;

        Assert.False(b1.IsPartiallyEquivalentTo(b2));
        Assert.True(b2.IsPartiallyEquivalentTo(b1));

        b2 = true;

        Assert.True(b1.IsPartiallyEquivalentTo(b2));
        Assert.True(b2.IsPartiallyEquivalentTo(b1));

        b2 = false;

        Assert.False(b1.IsPartiallyEquivalentTo(b2));
        Assert.False(b2.IsPartiallyEquivalentTo(b1));

        bool b3 = true;

        Assert.True(b3.IsPartiallyEquivalentTo(true));
        Assert.False(b3.IsPartiallyEquivalentTo(false));
        Assert.False(b3.IsPartiallyEquivalentTo(null));
        Assert.True(b3.IsPartiallyEquivalentTo(b1));
        Assert.True(b1.IsPartiallyEquivalentTo(b3));

        Assert.False(b2.IsPartiallyEquivalentTo(b1));
        Assert.False(b1.IsPartiallyEquivalentTo(b2));

        bool b4 = true;

        Assert.True(b4.IsPartiallyEquivalentTo(b3));
        Assert.True(b3.IsPartiallyEquivalentTo(b4));

        b4 = false;

        Assert.False(b4.IsPartiallyEquivalentTo(b3));
        Assert.False(b3.IsPartiallyEquivalentTo(b4));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_Enum()
    {
        PhoneType e1 = PhoneType.Personal;
        PhoneType e2 = PhoneType.Personal;

        Assert.True(e1.IsPartiallyEquivalentTo(e2));
        Assert.True(e1.IsPartiallyEquivalentTo("personal"));
        Assert.True(e1.IsPartiallyEquivalentTo("Personal"));
        Assert.True(e1.IsPartiallyEquivalentTo(0));
        Assert.False(e1.IsPartiallyEquivalentTo(1));
        Assert.False(e1.IsPartiallyEquivalentTo(null));

        e2 = PhoneType.Business;

        Assert.False(e1.IsPartiallyEquivalentTo(e2));

        PhoneType? e3 = null;

        Assert.True(e3.IsPartiallyEquivalentTo(e1));
        Assert.False(e1.IsPartiallyEquivalentTo(e3));

        e3 = PhoneType.Business;

        Assert.False(e1.IsPartiallyEquivalentTo(e3));
        Assert.False(e3.IsPartiallyEquivalentTo(e1));
        Assert.True(e2.IsPartiallyEquivalentTo(e3));
        Assert.True(e3.IsPartiallyEquivalentTo(e2));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_String()
    {
        string? s0 = null;
        string s1 = "ABC";
        string s2 = "ABC";

        Assert.True(s1.IsPartiallyEquivalentTo(s2));
        Assert.True(s2.IsPartiallyEquivalentTo(s1));
        Assert.True(s1.IsPartiallyEquivalentTo("ABC"));
        Assert.True("ABC".IsPartiallyEquivalentTo(s1));

        Assert.True(s0.IsPartiallyEquivalentTo(s1));
        Assert.False(s1.IsPartiallyEquivalentTo(null));

        s2 = "";
        Assert.False(s1.IsPartiallyEquivalentTo(s2));
        Assert.False(s2.IsPartiallyEquivalentTo(s1));
        Assert.False(s2.IsPartiallyEquivalentTo("ABC"));
        Assert.False("ABC".IsPartiallyEquivalentTo(s2));

        s2 = "abc";
        Assert.False(s1.IsPartiallyEquivalentTo(s2));
        Assert.False(s2.IsPartiallyEquivalentTo(s1));
        Assert.False(s2.IsPartiallyEquivalentTo("ABC"));
        Assert.False("ABC".IsPartiallyEquivalentTo(s2));

        string? s3 = null;
        string? s4 = null;

        Assert.True(s3.IsPartiallyEquivalentTo(s4));
        Assert.True(s3.IsPartiallyEquivalentTo(null));
        Assert.True(s3.IsPartiallyEquivalentTo(s2));
        Assert.False(s2.IsPartiallyEquivalentTo(s3));

        s3 = "ABC";
        Assert.True(s3.IsPartiallyEquivalentTo(s1));
        Assert.True(s1.IsPartiallyEquivalentTo(s3));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_Integer()
    {
        short? n0 = null;

        short n1 = 1;

        Assert.True(n0.IsPartiallyEquivalentTo(1));
        Assert.True(n0.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n1));
        Assert.False(n1.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n1));
        Assert.False(n1.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n1));

        short n2 = 1;

        Assert.True(n1.IsPartiallyEquivalentTo(n2));
        Assert.True(n2.IsPartiallyEquivalentTo(n1));

        n2 = 2;

        Assert.False(n1.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n1));

        int n3 = 1;

        Assert.True(n3.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n3));
        Assert.False(n3.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n3));
        Assert.False(n3.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n3));

        Assert.True(n3.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(n3));
        Assert.False(n3.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n3));

        int n4 = 1;

        Assert.True(n3.IsPartiallyEquivalentTo(n4));
        Assert.True(n4.IsPartiallyEquivalentTo(n3));

        n4 = 2;

        Assert.False(n3.IsPartiallyEquivalentTo(n4));
        Assert.False(n4.IsPartiallyEquivalentTo(n3));

        long n5 = 1;

        Assert.True(n5.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n5));
        Assert.False(n5.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n5));
        Assert.False(n5.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n5));

        Assert.True(n5.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(n5));
        Assert.False(n5.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n5));

        Assert.True(n5.IsPartiallyEquivalentTo(n3));
        Assert.True(n3.IsPartiallyEquivalentTo(n5));
        Assert.False(n5.IsPartiallyEquivalentTo(n4));
        Assert.False(n4.IsPartiallyEquivalentTo(n5));

        long n6 = 1;

        Assert.True(n5.IsPartiallyEquivalentTo(n6));
        Assert.True(n6.IsPartiallyEquivalentTo(n5));

        n6 = 2;

        Assert.False(n5.IsPartiallyEquivalentTo(n6));
        Assert.False(n6.IsPartiallyEquivalentTo(n5));

        ushort n7 = 1;

        Assert.True(n7.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n7));
        Assert.False(n7.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n7));
        Assert.False(n7.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n7));

        Assert.True(n7.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(n7));
        Assert.False(n7.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n7));

        Assert.True(n7.IsPartiallyEquivalentTo(n3));
        Assert.True(n3.IsPartiallyEquivalentTo(n7));
        Assert.False(n7.IsPartiallyEquivalentTo(n4));
        Assert.False(n4.IsPartiallyEquivalentTo(n7));

        Assert.True(n7.IsPartiallyEquivalentTo(n5));
        Assert.True(n5.IsPartiallyEquivalentTo(n7));
        Assert.False(n7.IsPartiallyEquivalentTo(n6));
        Assert.False(n6.IsPartiallyEquivalentTo(n7));

        ushort n8 = 1;

        Assert.True(n7.IsPartiallyEquivalentTo(n8));
        Assert.True(n8.IsPartiallyEquivalentTo(n7));

        n8 = 2;

        Assert.False(n7.IsPartiallyEquivalentTo(n8));
        Assert.False(n8.IsPartiallyEquivalentTo(n7));

        uint n9 = 1;

        Assert.True(n9.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n9));

        Assert.True(n9.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n9));

        Assert.True(n9.IsPartiallyEquivalentTo(n3));
        Assert.True(n3.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(n4));
        Assert.False(n4.IsPartiallyEquivalentTo(n9));

        Assert.True(n9.IsPartiallyEquivalentTo(n5));
        Assert.True(n5.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(n6));
        Assert.False(n6.IsPartiallyEquivalentTo(n9));

        Assert.True(n9.IsPartiallyEquivalentTo(n7));
        Assert.True(n7.IsPartiallyEquivalentTo(n9));
        Assert.False(n9.IsPartiallyEquivalentTo(n8));
        Assert.False(n8.IsPartiallyEquivalentTo(n9));

        ushort n10 = 1;

        Assert.True(n9.IsPartiallyEquivalentTo(n10));
        Assert.True(n10.IsPartiallyEquivalentTo(n9));

        n10 = 2;

        Assert.False(n9.IsPartiallyEquivalentTo(n10));
        Assert.False(n10.IsPartiallyEquivalentTo(n9));

        uint n11 = 1;

        Assert.True(n11.IsPartiallyEquivalentTo(1));
        Assert.True(1.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(0));
        Assert.False(0.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(-1));
        Assert.False((-1).IsPartiallyEquivalentTo(n11));

        Assert.True(n11.IsPartiallyEquivalentTo(n1));
        Assert.True(n1.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(n2));
        Assert.False(n2.IsPartiallyEquivalentTo(n11));

        Assert.True(n11.IsPartiallyEquivalentTo(n3));
        Assert.True(n3.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(n4));
        Assert.False(n4.IsPartiallyEquivalentTo(n11));

        Assert.True(n11.IsPartiallyEquivalentTo(n5));
        Assert.True(n5.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(n6));
        Assert.False(n6.IsPartiallyEquivalentTo(n11));

        Assert.True(n11.IsPartiallyEquivalentTo(n7));
        Assert.True(n7.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(n8));
        Assert.False(n8.IsPartiallyEquivalentTo(n11));

        Assert.True(n11.IsPartiallyEquivalentTo(n9));
        Assert.True(n9.IsPartiallyEquivalentTo(n11));
        Assert.False(n11.IsPartiallyEquivalentTo(n10));
        Assert.False(n10.IsPartiallyEquivalentTo(n11));

        ushort n12 = 1;

        Assert.True(n11.IsPartiallyEquivalentTo(n12));
        Assert.True(n12.IsPartiallyEquivalentTo(n11));

        n12 = 2;

        Assert.False(n11.IsPartiallyEquivalentTo(n12));
        Assert.False(n12.IsPartiallyEquivalentTo(n11));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_DateTime()
    {
        string s;
        DateTime? d0 = null;

        DateTime d1 = new(2020, 1, 2, 3, 4, 5, 678);
        DateTime d2 = new(2020, 1, 2, 3, 4, 5, 678);

        Assert.True(d0.IsPartiallyEquivalentTo(d1));
        Assert.False(d1.IsPartiallyEquivalentTo(d0));
        Assert.True(d1.IsPartiallyEquivalentTo(d2));
        Assert.True(d2.IsPartiallyEquivalentTo(d1));

        DateTime d3 = new(2020, 1, 2, 3, 4, 5, 789);

        Assert.False(d1.IsPartiallyEquivalentTo(d3));
        Assert.False(d3.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d1.IsPartiallyEquivalentTo(s));
        Assert.True(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:00";

        Assert.True(d1.IsPartiallyEquivalentTo(s));
        Assert.True(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.False(d1.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789Z";

        Assert.False(d1.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789+00:00";

        Assert.False(d1.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_DateTimeOffset()
    {
        string s;

        DateTimeOffset? d0 = null;
        DateTimeOffset d1 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));
        DateTimeOffset d2 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));

        Assert.True(d0.IsPartiallyEquivalentTo(d1));
        Assert.False(d1.IsPartiallyEquivalentTo(d0));
        Assert.True(d1.IsPartiallyEquivalentTo(d2));
        Assert.True(d2.IsPartiallyEquivalentTo(d1));

        DateTimeOffset d3 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(-30));

        Assert.False(d1.IsPartiallyEquivalentTo(d3));
        Assert.False(d3.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.True(d1.IsPartiallyEquivalentTo(s));
        Assert.True(s.IsPartiallyEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d1));
        Assert.True(d3.IsPartiallyEquivalentTo(s));
        Assert.True(s.IsPartiallyEquivalentTo(d3));

        DateTimeOffset d4 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromSeconds(0));
        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d4.IsPartiallyEquivalentTo(s));
        Assert.True(s.IsPartiallyEquivalentTo(d4));

        s = "2020-01-02T03:04:05.876Z";

        Assert.False(d4.IsPartiallyEquivalentTo(s));
        Assert.False(s.IsPartiallyEquivalentTo(d4));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_ArrayNumeric()
    {
        int[] aInt1 = [1, 2, 3];
        int[] aInt2 = [1, 2, 3];

        Assert.True(aInt1.IsPartiallyEquivalentTo(aInt2));
        Assert.True(aInt2.IsPartiallyEquivalentTo(aInt1));

        int[] aInt3 = [1, 2];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aInt3));
        Assert.True(aInt3.IsPartiallyEquivalentTo(aInt1));

        int[] aInt4 = [3, 2, 1];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aInt4));
        Assert.False(aInt4.IsPartiallyEquivalentTo(aInt1));

        long[] aLong1 = [1, 2, 3];

        Assert.True(aInt1.IsPartiallyEquivalentTo(aLong1));
        Assert.True(aLong1.IsPartiallyEquivalentTo(aInt1));

        long[] aLong2 = [1, 2];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aLong2));
        Assert.True(aLong2.IsPartiallyEquivalentTo(aInt1));

        long[] aLong3 = [3, 2, 1];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aLong3));
        Assert.False(aLong3.IsPartiallyEquivalentTo(aInt1));

        short[] aShort1 = [1, 2, 3];

        Assert.True(aInt1.IsPartiallyEquivalentTo(aShort1));
        Assert.True(aShort1.IsPartiallyEquivalentTo(aInt1));

        short[] aShort2 = [1, 2];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aShort2));
        Assert.True(aShort2.IsPartiallyEquivalentTo(aInt1));

        short[] aShort3 = [3, 2, 1];

        Assert.False(aInt1.IsPartiallyEquivalentTo(aShort3));
        Assert.False(aShort3.IsPartiallyEquivalentTo(aInt1));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_StringCollections()
    {
        string[] a1 = ["one", "two", "three",];
        string[] a2 = ["one", "two", "three",];

        Assert.True(a1.IsPartiallyEquivalentTo(a2));
        Assert.True(a2.IsPartiallyEquivalentTo(a1));

        string[] a3 = ["one", "two",];

        Assert.False(a1.IsPartiallyEquivalentTo(a3));
        Assert.True(a3.IsPartiallyEquivalentTo(a1));

        string[] a4 = ["three", "two", "one"];

        Assert.False(a1.IsPartiallyEquivalentTo(a4));
        Assert.False(a4.IsPartiallyEquivalentTo(a1));

        string[] a5 = ["One", "Two", "Three"];

        Assert.False(a1.IsPartiallyEquivalentTo(a5));
        Assert.False(a5.IsPartiallyEquivalentTo(a1));

        List<string> l1 = ["one", "two", "three",];
        List<string> l2 = ["one", "two", "three",];

        Assert.True(l1.IsPartiallyEquivalentTo(l2));
        Assert.True(l2.IsPartiallyEquivalentTo(l1));
        Assert.True(l1.IsPartiallyEquivalentTo(a1));
        Assert.True(a1.IsPartiallyEquivalentTo(l1));

        List<string> l3 = ["one", "two",];

        Assert.False(l1.IsPartiallyEquivalentTo(l3));
        Assert.True(l3.IsPartiallyEquivalentTo(l1));

        Assert.True(l3.IsPartiallyEquivalentTo(a1));
        Assert.False(a1.IsPartiallyEquivalentTo(l3));

        List<string> l4 = ["three", "two", "one"];

        Assert.False(l1.IsPartiallyEquivalentTo(l4));
        Assert.False(l4.IsPartiallyEquivalentTo(l1));

        Assert.False(l4.IsPartiallyEquivalentTo(a1));
        Assert.False(a1.IsPartiallyEquivalentTo(l4));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_Dictionary()
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

        Assert.True(d0.IsPartiallyEquivalentTo(d1));
        Assert.False(d1.IsPartiallyEquivalentTo(d0));
        Assert.True(d1.IsPartiallyEquivalentTo(d2));
        Assert.True(d2.IsPartiallyEquivalentTo(d1));

        Dictionary<string, string> d3 = new()
        {
            ["one"] = "two",
            ["three"] = "four"
        };

        Assert.False(d1.IsPartiallyEquivalentTo(d3));
        Assert.True(d3.IsPartiallyEquivalentTo(d1));

        Dictionary<string, string> d4 = new()
        {
            ["one"] = "two",
            ["three"] = "ten",
            ["five"] = "six"
        };

        Assert.False(d1.IsPartiallyEquivalentTo(d4));
        Assert.False(d4.IsPartiallyEquivalentTo(d1));

        Dictionary<string, string> d5 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "Six"
        };

        Assert.False(d1.IsPartiallyEquivalentTo(d5));
        Assert.False(d5.IsPartiallyEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_HashSet()
    {
        HashSet<string>? h0 = null;
        HashSet<string> h1 = [ "one", "two", "three" ];
        HashSet<string> h2 = [ "one", "two", "three" ];

        Assert.True(h0.IsPartiallyEquivalentTo(h1));
        Assert.False(h1.IsPartiallyEquivalentTo(h0));
        Assert.True(h1.IsPartiallyEquivalentTo(h2));
        Assert.True(h2.IsPartiallyEquivalentTo(h1));

        HashSet<string> h3 = [ "two", "three" ];

        Assert.False(h1.IsPartiallyEquivalentTo(h3));
        Assert.True(h3.IsPartiallyEquivalentTo(h1));

        HashSet<string> h4 = [ "one", "three", "two" ];

        Assert.True(h1.IsPartiallyEquivalentTo(h4));
        Assert.True(h4.IsPartiallyEquivalentTo(h1));

        HashSet<int>? i0 = null;
        HashSet<int> i1 = [ 1, 2, 3 ];
        HashSet<int> i2 = [ 1, 2, 3 ];

        Assert.True(i0.IsPartiallyEquivalentTo(i1));
        Assert.False(i1.IsPartiallyEquivalentTo(i0));
        Assert.True(i1.IsPartiallyEquivalentTo(i2));
        Assert.True(i2.IsPartiallyEquivalentTo(i1));

        HashSet<int> i3 = [ 2, 3 ];

        Assert.False(i1.IsPartiallyEquivalentTo(i3));
        Assert.True(i3.IsPartiallyEquivalentTo(i1));

        HashSet<int> i4 = [ 1, 3, 2 ];

        Assert.True(i1.IsPartiallyEquivalentTo(i4));
        Assert.True(i4.IsPartiallyEquivalentTo(i1));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_Class_Simple()
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

        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));

        u1.Name.Surname = "Smith";

        Assert.False(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));

        u1.Name.Surname = null;
        Assert.False(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));

        u1.Name.GivenName = null;
        Assert.False(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
    }

    [Fact]
    public void Object_IsPartiallyEquivalentTo_Class()
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

        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));

        u2.Tags?.Remove("greeting");

        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));

        u2.Tags?.Clear();

        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));

        u2.Tags = new()
        {
            ["greeting"] = "hello",
            ["color"] = "red",
            ["shape"] = "oval"
        };

        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hi";
        }

        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.False(u2.IsPartiallyEquivalentTo(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hello";
        }

        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));

        u2.SocialAccounts?.Remove("Facebook");

        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));
        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));

        u2.SocialAccounts?.Add("Facebook", new()
        {
            Provider = "Facebook",
            Account = "jack.johnson@email.com",
            Enabled = true
        });

        Assert.True(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.True(u2.IsPartiallyEquivalentTo(u1, true));

        if (u2.SocialAccounts?["Facebook"] != null)
        {
            u2.SocialAccounts["Facebook"].Enabled = false;
        }

        Assert.False(u1.IsPartiallyEquivalentTo(u2, true));
        Assert.False(u2.IsPartiallyEquivalentTo(u1, true));
    }
}
