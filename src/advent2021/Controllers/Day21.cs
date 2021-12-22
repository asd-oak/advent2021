namespace advent2021.Controllers;

using advent2021.Models;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 21 - Dirac Dice")]
[Route("[controller]")]
public class Day21Controller : ControllerBase
{

    public Day21Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}.txt");
        RequestTracer = trace;
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }

    private Tracer RequestTracer { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolvePart1(SampleFilePath);

    [HttpGet("Part1")]
    public long Part1() => SolvePart1(FilePath);

    [HttpGet("Part2-Sample")]
    public long Part2Sample() => SolvePart2(SampleFilePath);

    [HttpGet("Part2")]
    public long Part2() => SolvePart2(FilePath);

    private long SolvePart1(string filePath)
    {
        using var span = RequestTracer.StartSpan("Solving");
        var startingPositions = System.IO.File.ReadLines(filePath).ToList();

        var p1Position = int.Parse(startingPositions.First().Split(' ').ElementAt(^1)) - 1;
        var p2Position = int.Parse(startingPositions.Last().Split(' ').ElementAt(^1)) - 1;
        span.SetAttribute("p1Position", p1Position);
        span.SetAttribute("p2Position", p2Position);
        var p1Score = 0;
        var p2Score = 0;
        var diceValue = 1;
        var diceRolls = 0;

        while (true)
        {
            var playerRollSum = 0;
            foreach (var i in Enumerable.Range(0, 3))
            {
                playerRollSum += diceValue;
                diceValue++;
                diceValue = diceValue % 100;
                diceRolls++;
            }
            p1Position = (playerRollSum + p1Position) % 10;
            p1Score += p1Position + 1;
            if (p1Score >= 1000)
            {
                break;
            }

            playerRollSum = 0;
            foreach (var i in Enumerable.Range(0, 3))
            {
                playerRollSum += diceValue;
                diceValue++;
                diceValue = diceValue % 100;
                diceRolls++;
            }
            p2Position = (playerRollSum + p2Position) % 10;
            p2Score += p2Position + 1;
            if (p2Score >= 1000)
            {
                break;
            }
        }
        span.SetAttribute("p1Score", p1Score);
        span.SetAttribute("p2Score", p2Score);
        span.SetAttribute("diceRolls", diceRolls);

        return Math.Min(p1Score, p2Score) * diceRolls;
    }

    private long SolvePart2(string filePath)
    {
        using var span = RequestTracer.StartSpan("Solving");
        var startingPositions = System.IO.File.ReadLines(filePath).ToList();

        var p1Position = int.Parse(startingPositions.First().Split(' ').ElementAt(^1)) - 1;
        var p2Position = int.Parse(startingPositions.Last().Split(' ').ElementAt(^1)) - 1;
        span.SetAttribute("p1Position", p1Position);
        span.SetAttribute("p2Position", p2Position);

        var startingUniverse = new Day21Universe(p1Position, p2Position, 0, 0, true, 1);


        long p1Winnings = startingUniverse.p1Wins;
        long p2Winnings = startingUniverse.p2Wins;

        return Math.Max(p1Winnings, p2Winnings);
    }

}