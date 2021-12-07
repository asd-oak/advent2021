namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 7 - The Treachery Of Whales")]
[Route("[controller]")]
public class Day7Controller : ControllerBase
{

    public Day7Controller(IWebHostEnvironment environment)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day7-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day7.txt");
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }


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
        var crabXPositions = System.IO.File
                .ReadLines(filePath)
                .Select(row => row
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse))
                .First().OrderBy(i => i).ToList();

        var maxPosition = crabXPositions.Max();

        var bestPosition = 0;
        var bestFuel = long.MaxValue;
        var medianPosition = crabXPositions[(crabXPositions.Count) / 2]; // Suspect this is the optimal position after sorting, but just brute force for now
        for (var i = 0; i < maxPosition; i++)
        {
            var fuelAmount = crabXPositions.Select(pos => Math.Abs(pos - i)).Sum();
            if (fuelAmount < bestFuel)
            {
                bestPosition = i;
                bestFuel = fuelAmount;
            }
        }

        return bestFuel; //345197
    }

    private long SolvePart2(string filePath)
    {
        var crabXPositions = System.IO.File
            .ReadLines(filePath)
            .Select(row => row
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse))
            .First().OrderBy(i => i).ToList();

        var maxPosition = crabXPositions.Max();

        var bestPosition = 0;
        var bestFuel = long.MaxValue;
        var medianPosition = (crabXPositions.Count) / 2;
        for (var i = 0; i < maxPosition; i++)
        {
            var fuelAmount = crabXPositions.Select(pos =>
            {
                // 1 + 2 + 3 + 4 + ... + n = n(n+1)/2
                var distance = Math.Abs(pos - i);
                var fuelCost = distance * (distance + 1) / 2;
                return fuelCost;
            }).Sum();
            if (fuelAmount < bestFuel)
            {
                bestPosition = i;
                bestFuel = fuelAmount;
            }
        }

        return bestFuel; //96361606
    }

}