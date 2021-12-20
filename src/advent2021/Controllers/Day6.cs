namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 6 - Lanternfish")]
[Route("[controller]")]
public class Day6Controller : ControllerBase
{

    public Day6Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day6-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day6.txt");
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
        var fishAges = System.IO.File
            .ReadLines(filePath)
            .Select(row => row
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse))
            .First().ToList();

        for (var i = 0; i < 80; i++)
        {
            var newFishCount = fishAges.Count(fishAge => fishAge == 0);
            for (var fish = 0; fish < fishAges.Count; fish++)
            {
                fishAges[fish]--;
                if (fishAges[fish] < 0)
                {
                    fishAges[fish] = 6;
                }
            }
            var newFish = new List<int>();
            for (var k = 0; k < newFishCount; k++)
            {
                newFish.Add(8);
            }
            fishAges.AddRange(newFish);
        }

        return fishAges.Count; //360761
    }

    private long SolvePart2(string filePath)
    {
        var fishAges = System.IO.File
        .ReadLines(filePath)
        .Select(row => row
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse))
        .First().ToList();

        var fishCounts = new Dictionary<int, long>();
        for (var i = 0; i <= 8; i++)
        {
            fishCounts[i] = fishAges.Count(fa => fa == i);
        }

        for (var i = 0; i < 256; i++)
        {
            var newFishCount = fishCounts[0];
            for (var j = 0; j < 8; j++)
            {
                fishCounts[j] = fishCounts[j + 1];
            }
            fishCounts[6] += newFishCount;
            fishCounts[8] = newFishCount;
        }
        return fishCounts.Values.Sum(); //1632779838045
    }

}