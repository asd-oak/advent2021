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
    public async void TestDay1SonarSweepAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day1/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("7", responseContent);

        response = await client.GetAsync("/Day1/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1215", responseContent);

        response = await client.GetAsync("/Day1/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5", responseContent);

        response = await client.GetAsync("/Day1/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1150", responseContent);
    }

    [Fact]
    public async void TestDay2DiveAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day2/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("150", responseContent);

        response = await client.GetAsync("/Day2/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1693300", responseContent);

        response = await client.GetAsync("/Day2/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("900", responseContent);

        response = await client.GetAsync("/Day2/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1857958050", responseContent);
    }

    [Fact]
    public async void TestDay3BinaryDiagnosticAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day3/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("198", responseContent);

        response = await client.GetAsync("/Day3/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("3923414", responseContent);

        response = await client.GetAsync("/Day3/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("230", responseContent);

        response = await client.GetAsync("/Day3/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5852595", responseContent);
    }

    [Fact]
    public async void TestDay4GiantSquidAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day4/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("4512", responseContent);

        response = await client.GetAsync("/Day4/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("34506", responseContent);

        response = await client.GetAsync("/Day4/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1924", responseContent);

        response = await client.GetAsync("/Day4/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("7686", responseContent);
    }

    [Fact]
    public async void TestDay5HydrothermalVentureAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day5/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5", responseContent);

        response = await client.GetAsync("/Day5/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5167", responseContent);

        response = await client.GetAsync("/Day5/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("12", responseContent);

        response = await client.GetAsync("/Day5/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("17604", responseContent);
    }

    [Fact]
    public async void TestDay6LanternfishAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day6/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("5934", responseContent);

        response = await client.GetAsync("/Day6/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("360761", responseContent);

        response = await client.GetAsync("/Day6/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("26984457539", responseContent);

        response = await client.GetAsync("/Day6/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("1632779838045", responseContent);
    }

    [Fact]
    public async void TestDay7TheTreacheryOfWhalesAsync()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Day7/Part1-Sample");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("37", responseContent);

        response = await client.GetAsync("/Day7/Part1");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("345197", responseContent);

        response = await client.GetAsync("/Day7/Part2-Sample");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("168", responseContent);

        response = await client.GetAsync("/Day7/Part2");
        responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal("96361606", responseContent);
    }
}