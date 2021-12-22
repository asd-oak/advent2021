namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 1 - Sonar Sweep")]
[Route("[controller]")]
public class Day1Controller : ControllerBase
{

    public Day1Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}.txt");
        RequestTracer = trace;
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }

    private Tracer RequestTracer { get; set; }


    [HttpGet("Part1-Sample")]
    public int Part1Sample() => SolvePart1(SampleFilePath);

    [HttpGet("Part1")]
    public int Part1() => SolvePart1(FilePath);

    [HttpGet("Part2-Sample")]
    public int Part2Sample() => SolvePart2(SampleFilePath);

    [HttpGet("Part2")]
    public int Part2() => SolvePart2(FilePath);

    private int SolvePart1(string filePath)
    {
        var depthReadings = System.IO.File.ReadLines(filePath).Select(row => int.Parse(row)).ToList();
        var comparisons = depthReadings.Zip(depthReadings.Skip(1), (first, second) => second - first);
        var deeper = comparisons.Where(difference => difference > 0);
        var deeperCount = deeper.Count();
        return deeperCount;
    }

    private int SolvePart2(string filePath)
    {
        var depthReadings = System.IO.File.ReadLines(filePath).Select(row => int.Parse(row)).ToList();
        var tripleSums = depthReadings.Zip(depthReadings.Skip(1), (first, second) => first + second).Zip(depthReadings.Skip(2), (firstTwo, third) => firstTwo + third);
        var comparisons = tripleSums.Zip(tripleSums.Skip(1), (first, second) => second - first);
        var deeper = comparisons.Where(difference => difference > 0);
        var deeperCount = deeper.Count();
        return deeperCount;
    }

}