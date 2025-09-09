using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_IsPartialEquivalentTo_Boolean()
    {
        Assert.True(true.IsPartialEquivalentTo(true));
        Assert.True(false.IsPartialEquivalentTo(false));
        Assert.False(true.IsPartialEquivalentTo(false));
        Assert.False(false.IsPartialEquivalentTo(true));
        Assert.False(true.IsPartialEquivalentTo(null));
        Assert.False(false.IsPartialEquivalentTo(null));
        Assert.True(true.IsPartialEquivalentTo("true"));
        Assert.True(false.IsPartialEquivalentTo("false"));
        Assert.False(true.IsPartialEquivalentTo("false"));
        Assert.False(false.IsPartialEquivalentTo("true"));
        Assert.True("true".IsPartialEquivalentTo(true));
        Assert.True("false".IsPartialEquivalentTo(false));
        Assert.False("true".IsPartialEquivalentTo(false));
        Assert.False("false".IsPartialEquivalentTo(true));
        Assert.True(true.IsPartialEquivalentTo(1));
        Assert.False(true.IsPartialEquivalentTo(0));
        Assert.True(false.IsPartialEquivalentTo(0));
        Assert.False(false.IsPartialEquivalentTo(1));
        Assert.False(true.IsPartialEquivalentTo(2));
        Assert.False(false.IsPartialEquivalentTo(2));
        Assert.False(2.IsPartialEquivalentTo(true));
        Assert.False(2.IsPartialEquivalentTo(false));

        bool? b1 = null;

        Assert.True(b1.IsPartialEquivalentTo(null));
        Assert.True(b1.IsPartialEquivalentTo(true));
        Assert.True(b1.IsPartialEquivalentTo(false));
        Assert.False(true.IsPartialEquivalentTo(b1));
        Assert.False(false.IsPartialEquivalentTo(b1));

        b1 = true;

        Assert.True(b1.IsPartialEquivalentTo(true));
        Assert.False(b1.IsPartialEquivalentTo(false));
        Assert.True(true.IsPartialEquivalentTo(b1));
        Assert.False(false.IsPartialEquivalentTo(b1));
        
        b1 = null;
        bool? b2 = null;

        Assert.True(b1.IsPartialEquivalentTo(b2));
        Assert.True(b2.IsPartialEquivalentTo(b1));

        b1 = true;

        Assert.False(b1.IsPartialEquivalentTo(b2));
        Assert.True(b2.IsPartialEquivalentTo(b1));

        b2 = true;

        Assert.True(b1.IsPartialEquivalentTo(b2));
        Assert.True(b2.IsPartialEquivalentTo(b1));

        b2 = false;

        Assert.False(b1.IsPartialEquivalentTo(b2));
        Assert.False(b2.IsPartialEquivalentTo(b1));

        bool b3 = true;

        Assert.True(b3.IsPartialEquivalentTo(true));
        Assert.False(b3.IsPartialEquivalentTo(false));
        Assert.False(b3.IsPartialEquivalentTo(null));
        Assert.True(b3.IsPartialEquivalentTo(b1));
        Assert.True(b1.IsPartialEquivalentTo(b3));

        Assert.False(b2.IsPartialEquivalentTo(b1));
        Assert.False(b1.IsPartialEquivalentTo(b2));

        bool b4 = true;

        Assert.True(b4.IsPartialEquivalentTo(b3));
        Assert.True(b3.IsPartialEquivalentTo(b4));

        b4 = false;

        Assert.False(b4.IsPartialEquivalentTo(b3));
        Assert.False(b3.IsPartialEquivalentTo(b4));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_Enum()
    {
        PhoneType e1 = PhoneType.Personal;
        PhoneType e2 = PhoneType.Personal;

        Assert.True(e1.IsPartialEquivalentTo(e2));
        Assert.True(e1.IsPartialEquivalentTo("personal"));
        Assert.True(e1.IsPartialEquivalentTo("Personal"));
        Assert.True(e1.IsPartialEquivalentTo(0));
        Assert.False(e1.IsPartialEquivalentTo(1));
        Assert.False(e1.IsPartialEquivalentTo(null));

        e2 = PhoneType.Business;

        Assert.False(e1.IsPartialEquivalentTo(e2));

        PhoneType? e3 = null;

        Assert.True(e3.IsPartialEquivalentTo(e1));
        Assert.False(e1.IsPartialEquivalentTo(e3));

        e3 = PhoneType.Business;

        Assert.False(e1.IsPartialEquivalentTo(e3));
        Assert.False(e3.IsPartialEquivalentTo(e1));
        Assert.True(e2.IsPartialEquivalentTo(e3));
        Assert.True(e3.IsPartialEquivalentTo(e2));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_String()
    {
        string? s0 = null;
        string s1 = "ABC";
        string s2 = "ABC";

        Assert.True(s1.IsPartialEquivalentTo(s2));
        Assert.True(s2.IsPartialEquivalentTo(s1));
        Assert.True(s1.IsPartialEquivalentTo("ABC"));
        Assert.True("ABC".IsPartialEquivalentTo(s1));

        Assert.True(s0.IsPartialEquivalentTo(s1));
        Assert.False(s1.IsPartialEquivalentTo(null));

        s2 = "";
        Assert.False(s1.IsPartialEquivalentTo(s2));
        Assert.False(s2.IsPartialEquivalentTo(s1));
        Assert.False(s2.IsPartialEquivalentTo("ABC"));
        Assert.False("ABC".IsPartialEquivalentTo(s2));

        s2 = "abc";
        Assert.False(s1.IsPartialEquivalentTo(s2));
        Assert.False(s2.IsPartialEquivalentTo(s1));
        Assert.False(s2.IsPartialEquivalentTo("ABC"));
        Assert.False("ABC".IsPartialEquivalentTo(s2));

        string? s3 = null;
        string? s4 = null;

        Assert.True(s3.IsPartialEquivalentTo(s4));
        Assert.True(s3.IsPartialEquivalentTo(null));
        Assert.True(s3.IsPartialEquivalentTo(s2));
        Assert.False(s2.IsPartialEquivalentTo(s3));

        s3 = "ABC";
        Assert.True(s3.IsPartialEquivalentTo(s1));
        Assert.True(s1.IsPartialEquivalentTo(s3));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_Integer()
    {
        short? n0 = null;

        short n1 = 1;

        Assert.True(n0.IsPartialEquivalentTo(1));
        Assert.True(n0.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n1));
        Assert.False(n1.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n1));
        Assert.False(n1.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n1));

        short n2 = 1;

        Assert.True(n1.IsPartialEquivalentTo(n2));
        Assert.True(n2.IsPartialEquivalentTo(n1));

        n2 = 2;

        Assert.False(n1.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n1));

        int n3 = 1;

        Assert.True(n3.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n3));
        Assert.False(n3.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n3));
        Assert.False(n3.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n3));

        Assert.True(n3.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(n3));
        Assert.False(n3.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n3));

        int n4 = 1;

        Assert.True(n3.IsPartialEquivalentTo(n4));
        Assert.True(n4.IsPartialEquivalentTo(n3));

        n4 = 2;

        Assert.False(n3.IsPartialEquivalentTo(n4));
        Assert.False(n4.IsPartialEquivalentTo(n3));

        long n5 = 1;

        Assert.True(n5.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n5));
        Assert.False(n5.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n5));
        Assert.False(n5.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n5));

        Assert.True(n5.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(n5));
        Assert.False(n5.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n5));

        Assert.True(n5.IsPartialEquivalentTo(n3));
        Assert.True(n3.IsPartialEquivalentTo(n5));
        Assert.False(n5.IsPartialEquivalentTo(n4));
        Assert.False(n4.IsPartialEquivalentTo(n5));

        long n6 = 1;

        Assert.True(n5.IsPartialEquivalentTo(n6));
        Assert.True(n6.IsPartialEquivalentTo(n5));

        n6 = 2;

        Assert.False(n5.IsPartialEquivalentTo(n6));
        Assert.False(n6.IsPartialEquivalentTo(n5));

        ushort n7 = 1;

        Assert.True(n7.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n7));
        Assert.False(n7.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n7));
        Assert.False(n7.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n7));

        Assert.True(n7.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(n7));
        Assert.False(n7.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n7));

        Assert.True(n7.IsPartialEquivalentTo(n3));
        Assert.True(n3.IsPartialEquivalentTo(n7));
        Assert.False(n7.IsPartialEquivalentTo(n4));
        Assert.False(n4.IsPartialEquivalentTo(n7));

        Assert.True(n7.IsPartialEquivalentTo(n5));
        Assert.True(n5.IsPartialEquivalentTo(n7));
        Assert.False(n7.IsPartialEquivalentTo(n6));
        Assert.False(n6.IsPartialEquivalentTo(n7));

        ushort n8 = 1;

        Assert.True(n7.IsPartialEquivalentTo(n8));
        Assert.True(n8.IsPartialEquivalentTo(n7));

        n8 = 2;

        Assert.False(n7.IsPartialEquivalentTo(n8));
        Assert.False(n8.IsPartialEquivalentTo(n7));

        uint n9 = 1;

        Assert.True(n9.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n9));

        Assert.True(n9.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n9));

        Assert.True(n9.IsPartialEquivalentTo(n3));
        Assert.True(n3.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(n4));
        Assert.False(n4.IsPartialEquivalentTo(n9));

        Assert.True(n9.IsPartialEquivalentTo(n5));
        Assert.True(n5.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(n6));
        Assert.False(n6.IsPartialEquivalentTo(n9));

        Assert.True(n9.IsPartialEquivalentTo(n7));
        Assert.True(n7.IsPartialEquivalentTo(n9));
        Assert.False(n9.IsPartialEquivalentTo(n8));
        Assert.False(n8.IsPartialEquivalentTo(n9));

        ushort n10 = 1;

        Assert.True(n9.IsPartialEquivalentTo(n10));
        Assert.True(n10.IsPartialEquivalentTo(n9));

        n10 = 2;

        Assert.False(n9.IsPartialEquivalentTo(n10));
        Assert.False(n10.IsPartialEquivalentTo(n9));

        uint n11 = 1;

        Assert.True(n11.IsPartialEquivalentTo(1));
        Assert.True(1.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(0));
        Assert.False(0.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(-1));
        Assert.False((-1).IsPartialEquivalentTo(n11));

        Assert.True(n11.IsPartialEquivalentTo(n1));
        Assert.True(n1.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(n2));
        Assert.False(n2.IsPartialEquivalentTo(n11));

        Assert.True(n11.IsPartialEquivalentTo(n3));
        Assert.True(n3.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(n4));
        Assert.False(n4.IsPartialEquivalentTo(n11));

        Assert.True(n11.IsPartialEquivalentTo(n5));
        Assert.True(n5.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(n6));
        Assert.False(n6.IsPartialEquivalentTo(n11));

        Assert.True(n11.IsPartialEquivalentTo(n7));
        Assert.True(n7.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(n8));
        Assert.False(n8.IsPartialEquivalentTo(n11));

        Assert.True(n11.IsPartialEquivalentTo(n9));
        Assert.True(n9.IsPartialEquivalentTo(n11));
        Assert.False(n11.IsPartialEquivalentTo(n10));
        Assert.False(n10.IsPartialEquivalentTo(n11));

        ushort n12 = 1;

        Assert.True(n11.IsPartialEquivalentTo(n12));
        Assert.True(n12.IsPartialEquivalentTo(n11));

        n12 = 2;

        Assert.False(n11.IsPartialEquivalentTo(n12));
        Assert.False(n12.IsPartialEquivalentTo(n11));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_DateTime()
    {
        string s;
        DateTime? d0 = null;

        DateTime d1 = new(2020, 1, 2, 3, 4, 5, 678);
        DateTime d2 = new(2020, 1, 2, 3, 4, 5, 678);

        Assert.True(d0.IsPartialEquivalentTo(d1));
        Assert.False(d1.IsPartialEquivalentTo(d0));
        Assert.True(d1.IsPartialEquivalentTo(d2));
        Assert.True(d2.IsPartialEquivalentTo(d1));

        DateTime d3 = new(2020, 1, 2, 3, 4, 5, 789);

        Assert.False(d1.IsPartialEquivalentTo(d3));
        Assert.False(d3.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d1.IsPartialEquivalentTo(s));
        Assert.True(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:00";

        Assert.True(d1.IsPartialEquivalentTo(s));
        Assert.True(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.False(d1.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789Z";

        Assert.False(d1.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.789+00:00";

        Assert.False(d1.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_DateTimeOffset()
    {
        string s;

        DateTimeOffset? d0 = null;
        DateTimeOffset d1 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));
        DateTimeOffset d2 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));

        Assert.True(d0.IsPartialEquivalentTo(d1));
        Assert.False(d1.IsPartialEquivalentTo(d0));
        Assert.True(d1.IsPartialEquivalentTo(d2));
        Assert.True(d2.IsPartialEquivalentTo(d1));

        DateTimeOffset d3 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(-30));

        Assert.False(d1.IsPartialEquivalentTo(d3));
        Assert.False(d3.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.True(d1.IsPartialEquivalentTo(s));
        Assert.True(s.IsPartialEquivalentTo(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d1));
        Assert.True(d3.IsPartialEquivalentTo(s));
        Assert.True(s.IsPartialEquivalentTo(d3));

        DateTimeOffset d4 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromSeconds(0));
        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d4.IsPartialEquivalentTo(s));
        Assert.True(s.IsPartialEquivalentTo(d4));

        s = "2020-01-02T03:04:05.876Z";

        Assert.False(d4.IsPartialEquivalentTo(s));
        Assert.False(s.IsPartialEquivalentTo(d4));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_ArrayNumeric()
    {
        int[] aInt1 = [1, 2, 3];
        int[] aInt2 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentTo(aInt2));
        Assert.True(aInt2.IsPartialEquivalentTo(aInt1));

        int[] aInt3 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentTo(aInt3));
        Assert.True(aInt3.IsPartialEquivalentTo(aInt1));

        int[] aInt4 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentTo(aInt4));
        Assert.False(aInt4.IsPartialEquivalentTo(aInt1));

        long[] aLong1 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentTo(aLong1));
        Assert.True(aLong1.IsPartialEquivalentTo(aInt1));

        long[] aLong2 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentTo(aLong2));
        Assert.True(aLong2.IsPartialEquivalentTo(aInt1));

        long[] aLong3 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentTo(aLong3));
        Assert.False(aLong3.IsPartialEquivalentTo(aInt1));

        short[] aShort1 = [1, 2, 3];

        Assert.True(aInt1.IsPartialEquivalentTo(aShort1));
        Assert.True(aShort1.IsPartialEquivalentTo(aInt1));

        short[] aShort2 = [1, 2];

        Assert.False(aInt1.IsPartialEquivalentTo(aShort2));
        Assert.True(aShort2.IsPartialEquivalentTo(aInt1));

        short[] aShort3 = [3, 2, 1];

        Assert.False(aInt1.IsPartialEquivalentTo(aShort3));
        Assert.False(aShort3.IsPartialEquivalentTo(aInt1));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_StringCollections()
    {
        string[] a1 = ["one", "two", "three",];
        string[] a2 = ["one", "two", "three",];

        Assert.True(a1.IsPartialEquivalentTo(a2));
        Assert.True(a2.IsPartialEquivalentTo(a1));

        string[] a3 = ["one", "two",];

        Assert.False(a1.IsPartialEquivalentTo(a3));
        Assert.True(a3.IsPartialEquivalentTo(a1));

        string[] a4 = ["three", "two", "one"];

        Assert.False(a1.IsPartialEquivalentTo(a4));
        Assert.False(a4.IsPartialEquivalentTo(a1));

        string[] a5 = ["One", "Two", "Three"];

        Assert.False(a1.IsPartialEquivalentTo(a5));
        Assert.False(a5.IsPartialEquivalentTo(a1));

        List<string> l1 = ["one", "two", "three",];
        List<string> l2 = ["one", "two", "three",];

        Assert.True(l1.IsPartialEquivalentTo(l2));
        Assert.True(l2.IsPartialEquivalentTo(l1));
        Assert.True(l1.IsPartialEquivalentTo(a1));
        Assert.True(a1.IsPartialEquivalentTo(l1));

        List<string> l3 = ["one", "two",];

        Assert.False(l1.IsPartialEquivalentTo(l3));
        Assert.True(l3.IsPartialEquivalentTo(l1));

        Assert.True(l3.IsPartialEquivalentTo(a1));
        Assert.False(a1.IsPartialEquivalentTo(l3));

        List<string> l4 = ["three", "two", "one"];

        Assert.False(l1.IsPartialEquivalentTo(l4));
        Assert.False(l4.IsPartialEquivalentTo(l1));

        Assert.False(l4.IsPartialEquivalentTo(a1));
        Assert.False(a1.IsPartialEquivalentTo(l4));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_Dictionary()
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

        Assert.True(d0.IsPartialEquivalentTo(d1));
        Assert.False(d1.IsPartialEquivalentTo(d0));
        Assert.True(d1.IsPartialEquivalentTo(d2));
        Assert.True(d2.IsPartialEquivalentTo(d1));

        Dictionary<string, string> d3 = new()
        {
            ["one"] = "two",
            ["three"] = "four"
        };

        Assert.False(d1.IsPartialEquivalentTo(d3));
        Assert.True(d3.IsPartialEquivalentTo(d1));

        Dictionary<string, string> d4 = new()
        {
            ["one"] = "two",
            ["three"] = "ten",
            ["five"] = "six"
        };

        Assert.False(d1.IsPartialEquivalentTo(d4));
        Assert.False(d4.IsPartialEquivalentTo(d1));

        Dictionary<string, string> d5 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "Six"
        };

        Assert.False(d1.IsPartialEquivalentTo(d5));
        Assert.False(d5.IsPartialEquivalentTo(d1));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_HashSet()
    {
        HashSet<string>? h0 = null;
        HashSet<string> h1 = [ "one", "two", "three" ];
        HashSet<string> h2 = [ "one", "two", "three" ];

        Assert.True(h0.IsPartialEquivalentTo(h1));
        Assert.False(h1.IsPartialEquivalentTo(h0));
        Assert.True(h1.IsPartialEquivalentTo(h2));
        Assert.True(h2.IsPartialEquivalentTo(h1));

        HashSet<string> h3 = [ "two", "three" ];

        Assert.False(h1.IsPartialEquivalentTo(h3));
        Assert.True(h3.IsPartialEquivalentTo(h1));

        HashSet<string> h4 = [ "one", "three", "two" ];

        Assert.True(h1.IsPartialEquivalentTo(h4));
        Assert.True(h4.IsPartialEquivalentTo(h1));

        HashSet<int>? i0 = null;
        HashSet<int> i1 = [ 1, 2, 3 ];
        HashSet<int> i2 = [ 1, 2, 3 ];

        Assert.True(i0.IsPartialEquivalentTo(i1));
        Assert.False(i1.IsPartialEquivalentTo(i0));
        Assert.True(i1.IsPartialEquivalentTo(i2));
        Assert.True(i2.IsPartialEquivalentTo(i1));

        HashSet<int> i3 = [ 2, 3 ];

        Assert.False(i1.IsPartialEquivalentTo(i3));
        Assert.True(i3.IsPartialEquivalentTo(i1));

        HashSet<int> i4 = [ 1, 3, 2 ];

        Assert.True(i1.IsPartialEquivalentTo(i4));
        Assert.True(i4.IsPartialEquivalentTo(i1));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_Class_Simple()
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

        Assert.True(u1.IsPartialEquivalentTo(u2, true));
        Assert.True(u2.IsPartialEquivalentTo(u1, true));

        u1.Name.Surname = "Smith";

        Assert.False(u2.IsPartialEquivalentTo(u1, true));
        Assert.False(u1.IsPartialEquivalentTo(u2, true));

        u1.Name.Surname = null;
        Assert.False(u2.IsPartialEquivalentTo(u1, true));
        Assert.True(u1.IsPartialEquivalentTo(u2, true));

        u1.Name.GivenName = null;
        Assert.False(u2.IsPartialEquivalentTo(u1, true));
        Assert.True(u1.IsPartialEquivalentTo(u2, true));
    }

    [Fact]
    public void Object_IsPartialEquivalentTo_Class()
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

        Assert.True(u1.IsPartialEquivalentTo(u2, true));
        Assert.True(u2.IsPartialEquivalentTo(u1, true));

        u2.Tags?.Remove("greeting");

        Assert.True(u2.IsPartialEquivalentTo(u1, true));
        Assert.False(u1.IsPartialEquivalentTo(u2, true));

        u2.Tags?.Clear();

        Assert.True(u2.IsPartialEquivalentTo(u1, true));
        Assert.False(u1.IsPartialEquivalentTo(u2, true));

        u2.Tags = new()
        {
            ["greeting"] = "hello",
            ["color"] = "red",
            ["shape"] = "oval"
        };

        Assert.True(u1.IsPartialEquivalentTo(u2, true));
        Assert.True(u2.IsPartialEquivalentTo(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hi";
        }

        Assert.False(u1.IsPartialEquivalentTo(u2, true));
        Assert.False(u2.IsPartialEquivalentTo(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hello";
        }

        Assert.True(u1.IsPartialEquivalentTo(u2, true));
        Assert.True(u2.IsPartialEquivalentTo(u1, true));

        u2.SocialAccounts?.Remove("Facebook");

        Assert.True(u2.IsPartialEquivalentTo(u1, true));
        Assert.False(u1.IsPartialEquivalentTo(u2, true));

        u2.SocialAccounts?.Add("Facebook", new()
        {
            Provider = "Facebook",
            Account = "jack.johnson@email.com",
            Enabled = true
        });

        Assert.True(u1.IsPartialEquivalentTo(u2, true));
        Assert.True(u2.IsPartialEquivalentTo(u1, true));

        if (u2.SocialAccounts?["Facebook"] != null)
        {
            u2.SocialAccounts["Facebook"].Enabled = false;
        }

        Assert.False(u1.IsPartialEquivalentTo(u2, true));
        Assert.False(u2.IsPartialEquivalentTo(u1, true));
    }
}
