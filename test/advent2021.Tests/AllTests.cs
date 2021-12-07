using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace advent2021.Tests;

public class AllTests
{
    private readonly WebApplicationFactory<Program> _factory;
    public AllTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Fact]
    public async void TestDay1Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day1-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1215", responseContent);

        response = await client.GetAsync("/day1-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1150", responseContent);
    }

    [Fact]
    public async void TestDay2Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day2-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1693300", responseContent);

        response = await client.GetAsync("/day2-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1857958050", responseContent);
    }

    [Fact]
    public async void TestDay3Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day3-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("3923414", responseContent);

        response = await client.GetAsync("/day3-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5852595", responseContent);
    }

    [Fact]
    public async void TestDay4Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day4-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("34506", responseContent);

        response = await client.GetAsync("/day4-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("7686", responseContent);
    }

    [Fact]
    public async void TestDay5Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day5-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5167", responseContent);

        response = await client.GetAsync("/day5-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("17604", responseContent);
    }

    [Fact]
    public async void TestDay6Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day6-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("360761", responseContent);

        response = await client.GetAsync("/day6-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1632779838045", responseContent);
    }

    [Fact]
    public async void TestDay7Async()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/day7-1");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("345197", responseContent);

        response = await client.GetAsync("/day7-2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("96361606", responseContent);
    }
}