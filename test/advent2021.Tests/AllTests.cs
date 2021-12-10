using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Xunit;

namespace advent2021.Tests;

public class AllTests
{
    private readonly WebApplicationFactory<Program> _factory;

    public enum Puzzle {
        Part1Sample,
        Part1,
        Part2Sample,
        Part2
    };

    public Dictionary<Puzzle,string> _queryParts = new Dictionary<Puzzle, string>() {
        {Puzzle.Part1Sample, "Part1-Sample"},
        {Puzzle.Part1, "Part1"},
        {Puzzle.Part2Sample, "Part2-Sample"},
        {Puzzle.Part2, "Part2"},
    };

    public AllTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"7")]
    [InlineData(Puzzle.Part1,"1215")]
    [InlineData(Puzzle.Part2Sample,"5")]
    [InlineData(Puzzle.Part2,"1150")]
    public async void TestDay1SonarSweepAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day1/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"150")]
    [InlineData(Puzzle.Part1,"1693300")]
    [InlineData(Puzzle.Part2Sample,"900")]
    [InlineData(Puzzle.Part2,"1857958050")]
    public async void TestDay2DiveAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day2/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"198")]
    [InlineData(Puzzle.Part1,"3923414")]
    [InlineData(Puzzle.Part2Sample,"230")]
    [InlineData(Puzzle.Part2,"5852595")]
    public async void TestDay3BinaryDiagnosticAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day3/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"4512")]
    [InlineData(Puzzle.Part1,"34506")]
    [InlineData(Puzzle.Part2Sample,"1924")]
    [InlineData(Puzzle.Part2,"7686")]
    public async void TestDay4GiantSquidAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day4/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"5")]
    [InlineData(Puzzle.Part1,"5167")]
    [InlineData(Puzzle.Part2Sample,"12")]
    [InlineData(Puzzle.Part2,"17604")]
    public async void TestDay5HydrothermalVentureAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day5/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"5934")]
    [InlineData(Puzzle.Part1,"360761")]
    [InlineData(Puzzle.Part2Sample,"26984457539")]
    [InlineData(Puzzle.Part2,"1632779838045")]
    public async void TestDay6LanternfishAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day6/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"37")]
    [InlineData(Puzzle.Part1,"345197")]
    [InlineData(Puzzle.Part2Sample,"168")]
    [InlineData(Puzzle.Part2,"96361606")]
    public async void TestDay7TheTreacheryOfWhalesAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day7/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"26")]
    [InlineData(Puzzle.Part1,"543")]
    [InlineData(Puzzle.Part2Sample,"61229")]
    [InlineData(Puzzle.Part2,"994266")]
    public async void TestDay8SevenSegmentSearchAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day8/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"15")]
    [InlineData(Puzzle.Part1,"631")]
    [InlineData(Puzzle.Part2Sample,"1134")]
    [InlineData(Puzzle.Part2,"821560")]
    public async void TestDay9SmokeBasinAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day9/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample,"26397")]
    [InlineData(Puzzle.Part1,"389589")]
    [InlineData(Puzzle.Part2Sample,"288957")]
    [InlineData(Puzzle.Part2,"1190420163")]
    public async void TestDay10SyntaxScoringAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day10/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }
}