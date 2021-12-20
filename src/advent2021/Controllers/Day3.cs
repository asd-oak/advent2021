namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 3 - Binary Diagnostic")]
[Route("[controller]")]
public class Day3Controller : ControllerBase
{

    public Day3Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day3-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day3.txt");
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
        var binaryStrings = System.IO.File.ReadLines(filePath).ToList();
        var listLength = binaryStrings.Count;
        var bitCount = binaryStrings.First().Length;
        var oneCounts = new int[bitCount];
        binaryStrings.ForEach(item =>
        {
            for (var i = 0; i < bitCount; i++)
            {
                if (item[i] == '1')
                {
                    oneCounts[i]++;
                }
            }
        });
        var mostCommon = oneCounts.Select(count => count > listLength / 2 ? "1" : "0");
        var leastCommon = mostCommon.Select(item => item == "1" ? "0" : "1");

        var gamma = Convert.ToInt32(string.Join("", mostCommon), 2);
        var epsilon = Convert.ToInt32(string.Join("", leastCommon), 2);

        return gamma * epsilon; //3923414
    }

    private int SolvePart2(string filePath)
    {
        var binaryStrings = System.IO.File.ReadLines(filePath).ToList();
        var bitCount = binaryStrings.First().Length;

        var oxygenBinary = "";
        var currentList = binaryStrings;
        for (var i = 0; i < bitCount; i++)
        {
            if (!currentList.Any())
            {
                throw new Exception("Uh oh");
            }

            var listLength = currentList.Count;
            if (listLength == 1)
            {
                oxygenBinary = currentList.Single();
                break;
            }

            // For oxygen, keep the rows with most common bit. Tie goes to 1.
            var onesCount = currentList.Where(item => item[i] == '1').Count();
            if (onesCount >= listLength / 2.0)
            {
                currentList = currentList.Where(item => item[i] == '1').ToList();
            }
            else
            {
                currentList = currentList.Where(item => item[i] == '0').ToList();
            }
        }
        if (string.IsNullOrWhiteSpace(oxygenBinary))
        {
            oxygenBinary = currentList.Single();
        }

        var co2Binary = "";
        currentList = binaryStrings;
        for (var i = 0; i < bitCount; i++)
        {
            if (!currentList.Any())
            {
                throw new Exception("Uh oh");
            }

            var listLength = currentList.Count;
            if (listLength == 1)
            {
                co2Binary = currentList.Single();
                break;
            }

            //For co2, keep the rows with least common bit. Tie goes to 0.
            var onesCount = currentList.Where(item => item[i] == '1').Count();
            if (onesCount > 0 && onesCount < listLength / 2.0)
            {
                currentList = currentList.Where(item => item[i] == '1').ToList();
            }
            else
            {
                currentList = currentList.Where(item => item[i] == '0').ToList();
            }
        }
        if (string.IsNullOrWhiteSpace(co2Binary))
        {
            co2Binary = currentList.Single();
        }

        var oxygen = Convert.ToInt32(string.Join("", oxygenBinary), 2);
        var co2 = Convert.ToInt32(string.Join("", co2Binary), 2);

        return oxygen * co2; //No: 5805891. Yes: 5852595
    }

}