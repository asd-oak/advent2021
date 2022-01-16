using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Xunit;

namespace advent2021.Tests;

public class AllTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public enum PuzzlePart
    {
        Part1,
        Part2,
    };

    public enum PuzzleInput
    {
        Problem,
        Sample,
        Sample2,
        Sample3,
        Sample4,
        Sample5,
        Sample6,
        Sample7,
        Sample8,
        Sample9,
        Sample10,
        Sample11,
        Sample12,
    }

    private string GetProblemPath(PuzzlePart part, PuzzleInput input)
    {
        if (input == PuzzleInput.Problem)
        {
            return part.ToString();
        }
        else
        {
            return $"{part}-{input}";
        }
    }

    public AllTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "7")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "1215")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "5")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "1150")]
    public async void TestDay1SonarSweepAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day1/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "150")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "1693300")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "900")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "1857958050")]
    public async void TestDay2DiveAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day2/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "198")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "3923414")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "230")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "5852595")]
    public async void TestDay3BinaryDiagnosticAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day3/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "4512")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "34506")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "1924")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "7686")]
    public async void TestDay4GiantSquidAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day4/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "5")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "5167")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "12")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "17604")]
    public async void TestDay5HydrothermalVentureAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day5/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "5934")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "360761")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "26984457539")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "1632779838045")]
    public async void TestDay6LanternfishAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day6/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "37")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "345197")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "168")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "96361606")]
    public async void TestDay7TheTreacheryOfWhalesAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day7/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "26")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "543")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "61229")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "994266")]
    public async void TestDay8SevenSegmentSearchAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day8/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "15")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "631")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "1134")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "821560")]
    public async void TestDay9SmokeBasinAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day9/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "26397")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "389589")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "288957")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "1190420163")]
    public async void TestDay10SyntaxScoringAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day10/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "1656")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "1642")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "195")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "320")]
    public async void TestDay11DumboOctopusAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day11/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "10")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample2, "19")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample3, "226")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "5756")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "36")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample2, "103")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample3, "3509")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "144603")]
    public async void TestDay12PassagePathingAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day12/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "17")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "666")]
    // No part 2 tests - return bitmap for user to view
    public async void TestDay13TransparentOrigamiAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day13/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "1588")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "2375")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "2188189693529")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "1976896901756")]
    public async void TestDay14ExtendedPolymerizationAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day14/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "40")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "717")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "315")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "2993")]
    public async void TestDay15ChitonAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day15/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "16")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample2, "12")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample3, "23")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample4, "31")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "891")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample5, "3")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample6, "54")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample7, "7")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample8, "9")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample9, "1")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample10, "0")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample11, "0")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample12, "1")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "673042777597")]
    public async void TestDay16PacketDecoderAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day16/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "35")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "5597")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "3351")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "18723")]
    public async void TestDay20TrenchMapAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day20/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }

    [Theory]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Sample, "739785")]
    [InlineData(PuzzlePart.Part1, PuzzleInput.Problem, "797160")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Sample, "444356092776315")]
    [InlineData(PuzzlePart.Part2, PuzzleInput.Problem, "27464148626406")]
    public async void TestDay21DiracDiceAsync(PuzzlePart part, PuzzleInput input, string answer)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Day21/{GetProblemPath(part, input)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(answer, responseContent);
    }
}