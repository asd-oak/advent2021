namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 2 - Dive!")]
[Route("[controller]")]
public class Day2Controller : ControllerBase
{

    public Day2Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
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
        var moveCommandTuples = System.IO.File.ReadLines(filePath).Select(commandText => (command: commandText.Split(" ").First(), distance: int.Parse(commandText.Split(" ").Last()))).ToList();
        var x = 0;
        var y = 0;
        moveCommandTuples.ForEach(pairing =>
        {
            _ = pairing switch
            {
                { command: "forward" } => x += pairing.distance,
                { command: "up", distance: var toofar } when toofar > y => y = 0,
                { command: "up" } => y -= pairing.distance,
                { command: "down" } => y += pairing.distance,
                _ => 0,
            };
        }
        );
        return x * y; //1693300
    }

    private int SolvePart2(string filePath)
    {
        var moveCommandTuples = System.IO.File.ReadLines(filePath).Select(commandText => (command: commandText.Split(" ").First(), distance: int.Parse(commandText.Split(" ").Last()))).ToList();
        var x = 0;
        var y = 0;
        var aim = 0;
        moveCommandTuples.ForEach(pairing =>
        {
            _ = pairing switch
            {
                { command: "forward", distance: var howFar } when aim * howFar + y < 0 => (x += pairing.distance, y = 0),
                { command: "forward" } => (x += pairing.distance, y += aim * pairing.distance),
                { command: "up" } => (aim -= pairing.distance, 0),
                { command: "down" } => (aim += pairing.distance, 0),
                _ => (0, 0),
            };
        }
        );
        return x * y; //1857958050
    }

}