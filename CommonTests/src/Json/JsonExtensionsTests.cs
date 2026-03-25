// Ignore Spelling: Json

using CommonLibTests.Models;
using DotNetExtras.Common.Json;

namespace CommonLibTests.Json;

public class JsonExtensionsTests
{
    [Fact]
    public void Object_ToJsonFromJson()
    {
        User? u1 = null;
        string json = u1.ToJson();

        Assert.Equal("null", json);

        u1 = new()
        {
            Id = "12345",
            Name = new()
            {
                GivenName = "Joe",
                Surname = "Doe"
            },
            Age = 31,
            Sponsor = new()
            {
                Id = "54321"
            },
            SocialAccounts = new()
            {
                ["Facebook"] = new()
                {
                    Account = "Joe.Doe@mail.com"
                },
            },
            Phones =
            [
                new() { Number = "123-456-7890", Type = PhoneType.Personal },
                new() { Number = "987-654-3210", Type = PhoneType.Business },
            ],

            PasswordExpirationDate = new(2031, 11, 30, 19, 15, 33),
            LastLoginDateOffset = new(new DateTime(2021, 10, 12, 20, 33, 41), new TimeSpan(3, 30, 0)),

            Password = "sensitiveValue",

            Extensions =
            [
                true,
                false,
                "stringValue",
                123,
                2147483648,
                123.456,
                123.4567f,
                new DateTime(2025, 11, 30, 19, 15, 33),
            ]
        };

        json = u1.ToJson();

        /*
        {"age":31,"id":"12345","name":{"surname":"Doe","givenName":"Joe"},"passwordExpirationDate":"2031-11-30T19:15:33","lastLoginDateOffset":"2021-10-12T20:33:41+03:30","socialAccounts":{"Facebook":{"account":"Joe.Doe@mail.com"}},"phones":[{"type":"Personal","number":"123-456-7890"},{"type":"Business","number":"987-654-3210"}],"sponsor":{"id":"54321"}}
         */
        Assert.Contains("\"age\":31", json);
        Assert.Contains("\"id\":\"12345\"", json);
        Assert.Contains("\"name\":{\"surname\":\"Doe\",\"givenName\":\"Joe\"}", json);
        Assert.Contains("\"passwordExpirationDate\":\"2031-11-30T19:15:33\"", json);
        Assert.Contains("\"lastLoginDateOffset\":\"2021-10-12T20:33:41+03:30\"", json);
        Assert.Contains("\"socialAccounts\":{\"Facebook\":{\"account\":\"Joe.Doe@mail.com\"}}", json);
        Assert.Contains("\"phones\":[{\"type\":\"Personal\",\"number\":\"123-456-7890\"},{\"type\":\"Business\",\"number\":\"987-654-3210\"}]", json);
        Assert.Contains("\"sponsor\":{\"id\":\"54321\"", json);
        Assert.Contains("\"password\":\"sensitiveValue\"", json);

        User? u2 = json.FromJson<User>();

        Assert.NotNull(u2);
        Assert.Equivalent(u1, u2);

        object? u3 = json.FromJson(typeof(User));

        Assert.IsType<User>(u3);
    }

    [Theory]
    [InlineData("O'Brien")]
    [InlineData("Joh&John")]
    [InlineData("John,John")]
    [InlineData("John.John")]
    [InlineData("John-John")]
    [InlineData("John (John)")]
    public void Object_ToJsonSpecialChars
    (
        string name
    )
    {
        User? user = new()
        {
            Name = new()
            {
                Surname = name
            }
        };
        string json = user.ToJson();

        Assert.Contains($"\"surname\":\"{name}\"", json);
    }
}
