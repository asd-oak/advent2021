using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Xunit;

namespace advent2021.Tests;

public class AllTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public enum Puzzle
    {
        Part1Sample,
        Part1Sample2,
        Part1Sample3,
        Part1,
        Part2Sample,
        Part2Sample2,
        Part2Sample3,
        Part2
    };

    public Dictionary<Puzzle, string> _queryParts = new Dictionary<Puzzle, string>() {
        {Puzzle.Part1Sample, "Part1-Sample"},
        {Puzzle.Part1Sample2, "Part1-Sample2"},
        {Puzzle.Part1Sample3, "Part1-Sample3"},
        {Puzzle.Part1, "Part1"},
        {Puzzle.Part2Sample, "Part2-Sample"},
        {Puzzle.Part2Sample2, "Part2-Sample2"},
        {Puzzle.Part2Sample3, "Part2-Sample3"},
        {Puzzle.Part2, "Part2"},
    };

    public AllTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "7")]
    [InlineData(Puzzle.Part1, "1215")]
    [InlineData(Puzzle.Part2Sample, "5")]
    [InlineData(Puzzle.Part2, "1150")]
    public async void TestDay1SonarSweepAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day1/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "150")]
    [InlineData(Puzzle.Part1, "1693300")]
    [InlineData(Puzzle.Part2Sample, "900")]
    [InlineData(Puzzle.Part2, "1857958050")]
    public async void TestDay2DiveAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day2/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "198")]
    [InlineData(Puzzle.Part1, "3923414")]
    [InlineData(Puzzle.Part2Sample, "230")]
    [InlineData(Puzzle.Part2, "5852595")]
    public async void TestDay3BinaryDiagnosticAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day3/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "4512")]
    [InlineData(Puzzle.Part1, "34506")]
    [InlineData(Puzzle.Part2Sample, "1924")]
    [InlineData(Puzzle.Part2, "7686")]
    public async void TestDay4GiantSquidAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day4/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "5")]
    [InlineData(Puzzle.Part1, "5167")]
    [InlineData(Puzzle.Part2Sample, "12")]
    [InlineData(Puzzle.Part2, "17604")]
    public async void TestDay5HydrothermalVentureAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day5/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "5934")]
    [InlineData(Puzzle.Part1, "360761")]
    [InlineData(Puzzle.Part2Sample, "26984457539")]
    [InlineData(Puzzle.Part2, "1632779838045")]
    public async void TestDay6LanternfishAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day6/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "37")]
    [InlineData(Puzzle.Part1, "345197")]
    [InlineData(Puzzle.Part2Sample, "168")]
    [InlineData(Puzzle.Part2, "96361606")]
    public async void TestDay7TheTreacheryOfWhalesAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day7/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "26")]
    [InlineData(Puzzle.Part1, "543")]
    [InlineData(Puzzle.Part2Sample, "61229")]
    [InlineData(Puzzle.Part2, "994266")]
    public async void TestDay8SevenSegmentSearchAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day8/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "15")]
    [InlineData(Puzzle.Part1, "631")]
    [InlineData(Puzzle.Part2Sample, "1134")]
    [InlineData(Puzzle.Part2, "821560")]
    public async void TestDay9SmokeBasinAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day9/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "26397")]
    [InlineData(Puzzle.Part1, "389589")]
    [InlineData(Puzzle.Part2Sample, "288957")]
    [InlineData(Puzzle.Part2, "1190420163")]
    public async void TestDay10SyntaxScoringAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day10/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "1656")]
    [InlineData(Puzzle.Part1, "1642")]
    [InlineData(Puzzle.Part2Sample, "195")]
    [InlineData(Puzzle.Part2, "320")]
    public async void TestDay11DumboOctopusAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day11/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "10")]
    [InlineData(Puzzle.Part1Sample2, "19")]
    [InlineData(Puzzle.Part1Sample3, "226")]
    [InlineData(Puzzle.Part1, "5756")]
    [InlineData(Puzzle.Part2Sample, "36")]
    [InlineData(Puzzle.Part2Sample2, "103")]
    [InlineData(Puzzle.Part2Sample3, "3509")]
    [InlineData(Puzzle.Part2, "144603")]
    public async void TestDay12PassagePathingAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day12/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "17")]
    [InlineData(Puzzle.Part1, "666")]
    // No part 2 tests - return bitmap for user to view
    public async void TestDay13TransparentOrigamiAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day13/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "1588")]
    [InlineData(Puzzle.Part1, "2375")]
    [InlineData(Puzzle.Part2Sample, "2188189693529")]
    [InlineData(Puzzle.Part2, "1976896901756")]
    public async void TestDay14ExtendedPolymerizationAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day14/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "40")]
    [InlineData(Puzzle.Part1, "717")]
    [InlineData(Puzzle.Part2Sample, "315")]
    [InlineData(Puzzle.Part2, "2993")]
    public async void TestDay15ChitonAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day15/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "35")]
    [InlineData(Puzzle.Part1, "5597")]
    [InlineData(Puzzle.Part2Sample, "3351")]
    [InlineData(Puzzle.Part2, "18723")]
    public async void TestDay20TrenchMapAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day20/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(Puzzle.Part1Sample, "739785")]
    [InlineData(Puzzle.Part1, "797160")]
    [InlineData(Puzzle.Part2Sample, "444356092776315")]
    [InlineData(Puzzle.Part2, "27464148626406")]
    public async void TestDay21DiracDiceAsync(Puzzle part, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day21/{_queryParts[part]}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }
}