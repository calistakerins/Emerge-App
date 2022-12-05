namespace emerge_testing;
using MudBlazor;
using emerge.Data;


public class AlertInfoTests
{
    [Fact]
    public void Test_GenerateZipString1()
    {
        List<String> affected = new List<String>();
        affected.Add("91770");
        affected.Add("91776");
        var payload = new NewsAlert()
        {
            Id = "3423Test",
            Time = DateTime.Now,
            Title = "Test1",
            Affected = affected,
            Author = "Megan Amber",
            Department = new Department(),
            Description = "blah blah",
            Priority = 3,
            ImageUrl = "image.url",
            Updates = new List<UpdateInfo>()
        };
        Assert.Equal("91770, 91776", payload.generateZipString());
    }

    [Fact]
    public void Test_GenerateZipString2()
    {
        List<String> affected = new List<String>();
        affected.Add("91770");
        var payload = new NewsAlert()
        {
            Id = "3423Test",
            Time = DateTime.Now,
            Title = "Test1",
            Affected = affected,
            Author = "Megan Amber",
            Department = new Department(),
            Description = "blah blah",
            Priority = 3,
            ImageUrl = "image.url",
            Updates = new List<UpdateInfo>()
        };
        Assert.Equal("91770", payload.generateZipString());
    }

    [Fact]
    public void Test_GenerateZipString3()
    {
        List<String> affected = new List<String>();
        var payload = new NewsAlert()
        {
            Id = "3423Test",
            Time = DateTime.Now,
            Title = "Test1",
            Affected = affected,
            Author = "Megan Amber",
            Department = new Department(),
            Description = "blah blah",
            Priority = 3,
            ImageUrl = "image.url",
            Updates = new List<UpdateInfo>()
        };
        Assert.Equal("", payload.generateZipString());
    }
}