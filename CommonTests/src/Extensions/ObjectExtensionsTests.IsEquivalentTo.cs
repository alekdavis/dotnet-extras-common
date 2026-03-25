using CommonLibTests.Models;
using DotNetExtras.Common.Extensions;

namespace CommonLibTests.Extensions;

public partial class ObjectExtensionsTests
{
    [Fact]
    public void Object_IsEquivalentOf_Boolean()
    {
        Assert.True(true.IsEquivalentOf(true));
        Assert.True(false.IsEquivalentOf(false));
        Assert.False(true.IsEquivalentOf(false));
        Assert.False(false.IsEquivalentOf(true));
        Assert.False(true.IsEquivalentOf(null));
        Assert.False(false.IsEquivalentOf(null));
        Assert.True(true.IsEquivalentOf("true"));
        Assert.True(false.IsEquivalentOf("false"));
        Assert.False(true.IsEquivalentOf("false"));
        Assert.False(false.IsEquivalentOf("true"));
        Assert.True("true".IsEquivalentOf(true));
        Assert.True("false".IsEquivalentOf(false));
        Assert.False("true".IsEquivalentOf(false));
        Assert.False("false".IsEquivalentOf(true));
        Assert.True(true.IsEquivalentOf(1));
        Assert.False(true.IsEquivalentOf(0));
        Assert.True(false.IsEquivalentOf(0));
        Assert.False(false.IsEquivalentOf(1));
        Assert.False(true.IsEquivalentOf(2));
        Assert.False(false.IsEquivalentOf(2));
        Assert.False(2.IsEquivalentOf(true));
        Assert.False(2.IsEquivalentOf(false));

        bool? b1 = null;

        Assert.True(b1.IsEquivalentOf(null));
        Assert.False(b1.IsEquivalentOf(true));
        Assert.False(b1.IsEquivalentOf(false));
        Assert.False(true.IsEquivalentOf(b1));
        Assert.False(false.IsEquivalentOf(b1));

        b1 = true;

        Assert.True(b1.IsEquivalentOf(true));
        Assert.False(b1.IsEquivalentOf(false));
        Assert.True(true.IsEquivalentOf(b1));
        Assert.False(false.IsEquivalentOf(b1));

        b1 = null;
        bool? b2 = null;

        Assert.True(b1.IsEquivalentOf(b2));
        Assert.True(b2.IsEquivalentOf(b1));

        b1 = true;

        Assert.False(b1.IsEquivalentOf(b2));
        Assert.False(b2.IsEquivalentOf(b1));

        b2 = true;

        Assert.True(b1.IsEquivalentOf(b2));
        Assert.True(b2.IsEquivalentOf(b1));

        b2 = false;

        Assert.False(b1.IsEquivalentOf(b2));
        Assert.False(b2.IsEquivalentOf(b1));

        bool b3 = true;

        Assert.True(b3.IsEquivalentOf(true));
        Assert.False(b3.IsEquivalentOf(false));
        Assert.False(b3.IsEquivalentOf(null));
        Assert.True(b3.IsEquivalentOf(b1));
        Assert.True(b1.IsEquivalentOf(b3));

        Assert.False(b2.IsEquivalentOf(b1));
        Assert.False(b1.IsEquivalentOf(b2));

        bool b4 = true;

        Assert.True(b4.IsEquivalentOf(b3));
        Assert.True(b3.IsEquivalentOf(b4));

        b4 = false;

        Assert.False(b4.IsEquivalentOf(b3));
        Assert.False(b3.IsEquivalentOf(b4));
    }

    [Fact]
    public void Object_IsEquivalentOf_Enum()
    {
        PhoneType e1 = PhoneType.Personal;
        PhoneType e2 = PhoneType.Personal;

        Assert.True(e1.IsEquivalentOf(e2));
        Assert.True(e1.IsEquivalentOf("personal"));
        Assert.True(e1.IsEquivalentOf("Personal"));
        Assert.True(e1.IsEquivalentOf(0));
        Assert.False(e1.IsEquivalentOf(1));
        Assert.False(e1.IsEquivalentOf(null));

        e2 = PhoneType.Business;

        Assert.False(e1.IsEquivalentOf(e2));

        PhoneType? e3 = null;

        Assert.False(e1.IsEquivalentOf(e3));

        e3 = PhoneType.Business;

        Assert.False(e1.IsEquivalentOf(e3));
        Assert.False(e3.IsEquivalentOf(e1));
        Assert.True(e2.IsEquivalentOf(e3));
        Assert.True(e3.IsEquivalentOf(e2));
    }

    [Fact]
    public void Object_IsEquivalentOf_String()
    {
        string s1 = "ABC";
        string s2 = "ABC";

        Assert.True(s1.IsEquivalentOf(s2));
        Assert.True(s2.IsEquivalentOf(s1));
        Assert.True(s1.IsEquivalentOf("ABC"));
        Assert.True("ABC".IsEquivalentOf(s1));

        Assert.False(s1.IsEquivalentOf(null));

        s2 = "";
        Assert.False(s1.IsEquivalentOf(s2));
        Assert.False(s2.IsEquivalentOf(s1));
        Assert.False(s2.IsEquivalentOf("ABC"));
        Assert.False("ABC".IsEquivalentOf(s2));

        s2 = "abc";
        Assert.False(s1.IsEquivalentOf(s2));
        Assert.False(s2.IsEquivalentOf(s1));
        Assert.False(s2.IsEquivalentOf("ABC"));
        Assert.False("ABC".IsEquivalentOf(s2));

        string? s3 = null;
        string? s4 = null;

        Assert.True(s3.IsEquivalentOf(s4));
        Assert.True(s3.IsEquivalentOf(null));

        s3 = "ABC";
        Assert.True(s3.IsEquivalentOf(s1));
        Assert.True(s1.IsEquivalentOf(s3));
    }

    [Fact]
    public void Object_IsEquivalentOf_Integer()
    {
        short n1 = 1;

        Assert.True(n1.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n1));
        Assert.False(n1.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n1));
        Assert.False(n1.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n1));

        short n2 = 1;

        Assert.True(n1.IsEquivalentOf(n2));
        Assert.True(n2.IsEquivalentOf(n1));

        n2 = 2;

        Assert.False(n1.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n1));

        int n3 = 1;

        Assert.True(n3.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n3));
        Assert.False(n3.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n3));
        Assert.False(n3.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n3));

        Assert.True(n3.IsEquivalentOf(n1));
        Assert.True(n1.IsEquivalentOf(n3));
        Assert.False(n3.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n3));

        int n4 = 1;

        Assert.True(n3.IsEquivalentOf(n4));
        Assert.True(n4.IsEquivalentOf(n3));

        n4 = 2;

        Assert.False(n3.IsEquivalentOf(n4));
        Assert.False(n4.IsEquivalentOf(n3));

        long n5 = 1;

        Assert.True(n5.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n5));
        Assert.False(n5.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n5));
        Assert.False(n5.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n5));

        Assert.True(n5.IsEquivalentOf(n1));
        Assert.True(n1.IsEquivalentOf(n5));
        Assert.False(n5.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n5));

        Assert.True(n5.IsEquivalentOf(n3));
        Assert.True(n3.IsEquivalentOf(n5));
        Assert.False(n5.IsEquivalentOf(n4));
        Assert.False(n4.IsEquivalentOf(n5));

        long n6 = 1;

        Assert.True(n5.IsEquivalentOf(n6));
        Assert.True(n6.IsEquivalentOf(n5));

        n6 = 2;

        Assert.False(n5.IsEquivalentOf(n6));
        Assert.False(n6.IsEquivalentOf(n5));

        ushort n7 = 1;

        Assert.True(n7.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n7));
        Assert.False(n7.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n7));
        Assert.False(n7.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n7));

        Assert.True(n7.IsEquivalentOf(n1));
        Assert.True(n1.IsEquivalentOf(n7));
        Assert.False(n7.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n7));

        Assert.True(n7.IsEquivalentOf(n3));
        Assert.True(n3.IsEquivalentOf(n7));
        Assert.False(n7.IsEquivalentOf(n4));
        Assert.False(n4.IsEquivalentOf(n7));

        Assert.True(n7.IsEquivalentOf(n5));
        Assert.True(n5.IsEquivalentOf(n7));
        Assert.False(n7.IsEquivalentOf(n6));
        Assert.False(n6.IsEquivalentOf(n7));

        ushort n8 = 1;

        Assert.True(n7.IsEquivalentOf(n8));
        Assert.True(n8.IsEquivalentOf(n7));

        n8 = 2;

        Assert.False(n7.IsEquivalentOf(n8));
        Assert.False(n8.IsEquivalentOf(n7));

        uint n9 = 1;

        Assert.True(n9.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n9));

        Assert.True(n9.IsEquivalentOf(n1));
        Assert.True(n1.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n9));

        Assert.True(n9.IsEquivalentOf(n3));
        Assert.True(n3.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(n4));
        Assert.False(n4.IsEquivalentOf(n9));

        Assert.True(n9.IsEquivalentOf(n5));
        Assert.True(n5.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(n6));
        Assert.False(n6.IsEquivalentOf(n9));

        Assert.True(n9.IsEquivalentOf(n7));
        Assert.True(n7.IsEquivalentOf(n9));
        Assert.False(n9.IsEquivalentOf(n8));
        Assert.False(n8.IsEquivalentOf(n9));

        ushort n10 = 1;

        Assert.True(n9.IsEquivalentOf(n10));
        Assert.True(n10.IsEquivalentOf(n9));

        n10 = 2;

        Assert.False(n9.IsEquivalentOf(n10));
        Assert.False(n10.IsEquivalentOf(n9));

        uint n11 = 1;

        Assert.True(n11.IsEquivalentOf(1));
        Assert.True(1.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(0));
        Assert.False(0.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(-1));
        Assert.False((-1).IsEquivalentOf(n11));

        Assert.True(n11.IsEquivalentOf(n1));
        Assert.True(n1.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(n2));
        Assert.False(n2.IsEquivalentOf(n11));

        Assert.True(n11.IsEquivalentOf(n3));
        Assert.True(n3.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(n4));
        Assert.False(n4.IsEquivalentOf(n11));

        Assert.True(n11.IsEquivalentOf(n5));
        Assert.True(n5.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(n6));
        Assert.False(n6.IsEquivalentOf(n11));

        Assert.True(n11.IsEquivalentOf(n7));
        Assert.True(n7.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(n8));
        Assert.False(n8.IsEquivalentOf(n11));

        Assert.True(n11.IsEquivalentOf(n9));
        Assert.True(n9.IsEquivalentOf(n11));
        Assert.False(n11.IsEquivalentOf(n10));
        Assert.False(n10.IsEquivalentOf(n11));

        ushort n12 = 1;

        Assert.True(n11.IsEquivalentOf(n12));
        Assert.True(n12.IsEquivalentOf(n11));

        n12 = 2;

        Assert.False(n11.IsEquivalentOf(n12));
        Assert.False(n12.IsEquivalentOf(n11));
    }

    [Fact]
    public void Object_IsEquivalentOf_DateTime()
    {
        string s;

        DateTime d1 = new(2020, 1, 2, 3, 4, 5, 678);
        DateTime d2 = new(2020, 1, 2, 3, 4, 5, 678);

        Assert.True(d1.IsEquivalentOf(d2));
        Assert.True(d2.IsEquivalentOf(d1));

        DateTime d3 = new(2020, 1, 2, 3, 4, 5, 789);

        Assert.False(d1.IsEquivalentOf(d3));
        Assert.False(d3.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d1.IsEquivalentOf(s));
        Assert.True(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:00";

        Assert.True(d1.IsEquivalentOf(s));
        Assert.True(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.False(d1.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.789Z";

        Assert.False(d1.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.789+00:00";

        Assert.False(d1.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d1));
    }

    [Fact]
    public void Object_IsEquivalentOf_DateTimeOffset()
    {
        string s;

        DateTimeOffset d1 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));
        DateTimeOffset d2 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(30));

        Assert.True(d1.IsEquivalentOf(d2));
        Assert.True(d2.IsEquivalentOf(d1));

        DateTimeOffset d3 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromMinutes(-30));

        Assert.False(d1.IsEquivalentOf(d3));
        Assert.False(d3.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678+00:30";

        Assert.True(d1.IsEquivalentOf(s));
        Assert.True(s.IsEquivalentOf(d1));

        s = "2020-01-02T03:04:05.678-00:30";

        Assert.False(d1.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d1));
        Assert.True(d3.IsEquivalentOf(s));
        Assert.True(s.IsEquivalentOf(d3));

        DateTimeOffset d4 = new(2020, 1, 2, 3, 4, 5, 678, TimeSpan.FromSeconds(0));
        s = "2020-01-02T03:04:05.678Z";

        Assert.True(d4.IsEquivalentOf(s));
        Assert.True(s.IsEquivalentOf(d4));

        s = "2020-01-02T03:04:05.876Z";

        Assert.False(d4.IsEquivalentOf(s));
        Assert.False(s.IsEquivalentOf(d4));
    }

    [Fact]
    public void Object_IsEquivalentOf_ArrayNumeric()
    {
        int[] aInt1 = [1, 2, 3];
        int[] aInt2 = [1, 2, 3];

        Assert.True(aInt1.IsEquivalentOf(aInt2));
        Assert.True(aInt2.IsEquivalentOf(aInt1));

        int[] aInt3 = [1, 2];

        Assert.False(aInt1.IsEquivalentOf(aInt3));
        Assert.False(aInt3.IsEquivalentOf(aInt1));

        int[] aInt4 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentOf(aInt4));
        Assert.False(aInt4.IsEquivalentOf(aInt1));

        long[] aLong1 = [1, 2, 3];

        Assert.True(aInt1.IsEquivalentOf(aLong1));
        Assert.True(aLong1.IsEquivalentOf(aInt1));

        long[] aLong2 = [1, 2];

        Assert.False(aInt1.IsEquivalentOf(aLong2));
        Assert.False(aLong2.IsEquivalentOf(aInt1));

        long[] aLong3 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentOf(aLong3));
        Assert.False(aLong3.IsEquivalentOf(aInt1));

        short[] aShort1 = [1, 2, 3];

        Assert.True(aInt1.IsEquivalentOf(aShort1));
        Assert.True(aShort1.IsEquivalentOf(aInt1));

        short[] aShort2 = [1, 2];

        Assert.False(aInt1.IsEquivalentOf(aShort2));
        Assert.False(aShort2.IsEquivalentOf(aInt1));

        short[] aShort3 = [3, 2, 1];

        Assert.False(aInt1.IsEquivalentOf(aShort3));
        Assert.False(aShort3.IsEquivalentOf(aInt1));
    }

    [Fact]
    public void Object_IsEquivalentOf_StringCollections()
    {
        string[] a1 = ["one", "two", "three",];
        string[] a2 = ["one", "two", "three",];

        Assert.True(a1.IsEquivalentOf(a2));
        Assert.True(a2.IsEquivalentOf(a1));

        string[] a3 = ["one", "two",];

        Assert.False(a1.IsEquivalentOf(a3));
        Assert.False(a3.IsEquivalentOf(a1));

        string[] a4 = ["three", "two", "one"];

        Assert.False(a1.IsEquivalentOf(a4));
        Assert.False(a4.IsEquivalentOf(a1));

        string[] a5 = ["One", "Two", "Three"];

        Assert.False(a1.IsEquivalentOf(a5));
        Assert.False(a5.IsEquivalentOf(a1));

        List<string> l1 = ["one", "two", "three",];
        List<string> l2 = ["one", "two", "three",];

        Assert.True(l1.IsEquivalentOf(l2));
        Assert.True(l2.IsEquivalentOf(l1));
        Assert.True(l1.IsEquivalentOf(a1));
        Assert.True(a1.IsEquivalentOf(l1));

        List<string> l3 = ["one", "two",];

        Assert.False(l1.IsEquivalentOf(l3));
        Assert.False(l3.IsEquivalentOf(l1));

        Assert.False(l3.IsEquivalentOf(a1));
        Assert.False(a1.IsEquivalentOf(l3));

        List<string> l4 = ["three", "two", "one"];

        Assert.False(l1.IsEquivalentOf(l4));
        Assert.False(l4.IsEquivalentOf(l1));

        Assert.False(l4.IsEquivalentOf(a1));
        Assert.False(a1.IsEquivalentOf(l4));
    }

    [Fact]
    public void Object_IsEquivalentOf_Dictionary()
    {
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

        Assert.True(d1.IsEquivalentOf(d2));
        Assert.True(d2.IsEquivalentOf(d1));

        Dictionary<string, string> d3 = new()
        {
            ["one"] = "two",
            ["three"] = "four"
        };

        Assert.False(d1.IsEquivalentOf(d3));
        Assert.False(d3.IsEquivalentOf(d1));

        Dictionary<string, string> d4 = new()
        {
            ["one"] = "two",
            ["three"] = "ten",
            ["five"] = "six"
        };

        Assert.False(d1.IsEquivalentOf(d4));
        Assert.False(d4.IsEquivalentOf(d1));

        Dictionary<string, string> d5 = new()
        {
            ["one"] = "two",
            ["three"] = "four",
            ["five"] = "Six"
        };

        Assert.False(d1.IsEquivalentOf(d5));
        Assert.False(d5.IsEquivalentOf(d1));
    }

    [Fact]
    public void Object_IsEquivalentOf_HashSet()
    {
        HashSet<string> h1 = ["one", "two", "three"];
        HashSet<string> h2 = ["one", "two", "three"];

        Assert.True(h1.IsEquivalentOf(h2));
        Assert.True(h2.IsEquivalentOf(h1));

        HashSet<string> h3 = ["two", "three"];

        Assert.False(h1.IsEquivalentOf(h3));
        Assert.False(h3.IsEquivalentOf(h1));

        HashSet<string> h4 = ["one", "three", "two"];

        Assert.True(h1.IsEquivalentOf(h4));
        Assert.True(h4.IsEquivalentOf(h1));

        HashSet<int> i1 = [1, 2, 3];
        HashSet<int> i2 = [1, 2, 3];

        Assert.True(i1.IsEquivalentOf(i2));
        Assert.True(i2.IsEquivalentOf(i1));

        HashSet<int> i3 = [2, 3];

        Assert.False(i1.IsEquivalentOf(i3));
        Assert.False(i3.IsEquivalentOf(i1));

        HashSet<int> i4 = [1, 3, 2];

        Assert.True(i1.IsEquivalentOf(i4));
        Assert.True(i4.IsEquivalentOf(i1));
    }

    [Fact]
    public void Object_IsEquivalentOf_Class()
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

        Assert.True(u1.IsEquivalentOf(u2, true));
        Assert.True(u2.IsEquivalentOf(u1, true));

        u2.Tags?.Remove("greeting");

        Assert.False(u2.IsEquivalentOf(u1, true));
        Assert.False(u1.IsEquivalentOf(u2, true));

        u2.Tags?.Clear();

        Assert.False(u2.IsEquivalentOf(u1, true));
        Assert.False(u1.IsEquivalentOf(u2, true));

        u2.Tags = new()
        {
            ["greeting"] = "hello",
            ["color"] = "red",
            ["shape"] = "oval"
        };

        Assert.True(u1.IsEquivalentOf(u2, true));
        Assert.True(u2.IsEquivalentOf(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hi";
        }

        Assert.False(u1.IsEquivalentOf(u2, true));
        Assert.False(u2.IsEquivalentOf(u1, true));

        if (u2.Tags?["greeting"] != null)
        {
            u2.Tags["greeting"] = "hello";
        }

        Assert.True(u1.IsEquivalentOf(u2, true));
        Assert.True(u2.IsEquivalentOf(u1, true));

        u2.SocialAccounts?.Remove("Facebook");

        Assert.False(u2.IsEquivalentOf(u1, true));
        Assert.False(u1.IsEquivalentOf(u2, true));

        u2.SocialAccounts?.Add("Facebook", new()
        {
            Provider = "Facebook",
            Account = "jack.johnson@email.com",
            Enabled = true
        });

        Assert.True(u1.IsEquivalentOf(u2, true));
        Assert.True(u2.IsEquivalentOf(u1, true));

        if (u2.SocialAccounts?["Facebook"] != null)
        {
            u2.SocialAccounts["Facebook"].Enabled = false;
        }

        Assert.False(u1.IsEquivalentOf(u2, true));
        Assert.False(u2.IsEquivalentOf(u1, true));
    }
}
