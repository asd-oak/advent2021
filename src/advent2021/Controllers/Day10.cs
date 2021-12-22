namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 10 - Syntax Scoring")]
[Route("[controller]")]
public class Day10Controller : ControllerBase
{

    public Day10Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
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
        var chunkyLines = System.IO.File.ReadLines(filePath).ToList();

        var scoreCard = new Dictionary<char, int>() {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

        var closersToOpeners = new Dictionary<char, char>() {
            {')', '('},
            {']', '['},
            {'}', '{'},
            {'>', '<'}
        };

        var openers = new List<char>() { '(', '[', '{', '<' };

        long score = 0;
        foreach (var row in chunkyLines)
        {
            var pairStack = new Stack<char>();
            for (var i = 0; i < row.Length; i++)
            {
                char currentItem = row[i];
                if (!pairStack.Any() || openers.Contains(currentItem))
                {
                    pairStack.Push(currentItem);
                    continue;
                }
                else if (pairStack.Peek() == closersToOpeners[currentItem])
                {
                    pairStack.Pop();
                    continue;
                }
                else
                {
                    score += scoreCard[currentItem];
                    break;
                }
            }

        }

        return score;
    }

    private long SolvePart2(string filePath)
    {
        var chunkyLines = System.IO.File.ReadLines(filePath).ToList();

        var scoreCard = new Dictionary<char, int>() {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4}
        };

        var closersToOpeners = new Dictionary<char, char>() {
            {')', '('},
            {']', '['},
            {'}', '{'},
            {'>', '<'}
        };

        var openersToClosers = new Dictionary<char, char>() {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        var openers = new List<char>() { '(', '[', '{', '<' };

        var scores = new List<long>();
        foreach (var row in chunkyLines)
        {
            var pairStack = new Stack<char>();

            bool invalid = false;
            for (var i = 0; i < row.Length; i++)
            {
                char currentItem = row[i];
                if (!pairStack.Any() || openers.Contains(currentItem))
                {
                    pairStack.Push(currentItem);
                    continue;
                }
                else if (pairStack.Peek() == closersToOpeners[currentItem])
                {
                    pairStack.Pop();
                    continue;
                }
                else
                {
                    invalid = true;
                    break;
                }
            }
            if (invalid)
            {
                continue;
            }

            long score = 0;
            while (pairStack.Any())
            {
                score *= 5;
                score += scoreCard[openersToClosers[pairStack.Pop()]];
            }
            scores.Add(score);

        }

        scores.Sort();

        return scores[scores.Count / 2];
    }

}