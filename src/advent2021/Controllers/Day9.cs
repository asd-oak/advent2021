namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 9 - Smoke Basin")]
[Route("[controller]")]
public class Day9Controller : ControllerBase
{

    public Day9Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day9-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day9.txt");
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
        var width = System.IO.File.ReadLines(filePath).Select(row => row.Length).First();
        var height = System.IO.File.ReadLines(filePath).Count();
        var heightMatrix = new byte[height, width];

        var heights = System.IO.File
            .ReadLines(filePath)
            .ToList();

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                heightMatrix[i, j] = byte.Parse(heights[i][j].ToString());
            }
        }

        long risk = 0; // += local minima height + 1 
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var examineVal = heightMatrix[i, j];

                if (i > 0 && examineVal >= heightMatrix[i - 1, j])
                {
                    continue;
                }

                if (i < height - 1 && examineVal >= heightMatrix[i + 1, j])
                {
                    continue;
                }

                if (j > 0 && examineVal >= heightMatrix[i, j - 1])
                {
                    continue;
                }

                if (j < width - 1 && examineVal >= heightMatrix[i, j + 1])
                {
                    continue;
                }

                risk += examineVal + 1;
            }
        }

        return risk;
    }

    private long SolvePart2(string filePath)
    {
        var width = System.IO.File.ReadLines(filePath).Select(row => row.Length).First();
        var height = System.IO.File.ReadLines(filePath).Count();
        var heightMatrix = new char[height, width];

        var heights = System.IO.File
            .ReadLines(filePath)
            .ToList();

        //9 delineates basins; is not counted. Heights no longer matter.
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (heights[i][j] == '9')
                {
                    heightMatrix[i, j] = ' ';
                }
                else
                {
                    heightMatrix[i, j] = '0';
                }
            }
        }

        var basinCounts = new List<int>();

        //When examining a basin, tag the target square and adjacent non-empty squares with 1 (change from 0)
        //Loop over entire grid to tag adjacents, and keep looping until nothing is tagged.
        //Then loop to count and clear (1 => space).
        //Keep going until the grid is empty.

        var nonEmptyFound = false;
        do
        {
            nonEmptyFound = false;
            var taggedSomething = false;
            do
            {
                //Tagging
                taggedSomething = false;
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        var examineVal = heightMatrix[i, j];
                        if (examineVal == ' ')
                        {
                            continue;
                        }
                        if (taggedSomething == false && nonEmptyFound == false)
                        {
                            heightMatrix[i, j] = '1';
                            taggedSomething = true;
                        }
                        nonEmptyFound = true;

                        if (i > 0 && examineVal < heightMatrix[i - 1, j])
                        {
                            heightMatrix[i, j] = '1';
                            taggedSomething = true;
                        }

                        if (i < height - 1 && examineVal < heightMatrix[i + 1, j])
                        {
                            heightMatrix[i, j] = '1';
                            taggedSomething = true;
                        }

                        if (j > 0 && examineVal < heightMatrix[i, j - 1])
                        {
                            heightMatrix[i, j] = '1';
                            taggedSomething = true;
                        }

                        if (j < width - 1 && examineVal < heightMatrix[i, j + 1])
                        {
                            heightMatrix[i, j] = '1';
                            taggedSomething = true;
                        }
                    }
                }
            } while (taggedSomething == true);

            if (nonEmptyFound)
            {
                //Count and clear
                var basinSize = 0;
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        var examineVal = heightMatrix[i, j];
                        if (examineVal == ' ' || examineVal == '0')
                        {
                            continue;
                        }
                        else
                        {
                            basinSize++;
                            heightMatrix[i, j] = ' ';
                        }
                    }
                }
                basinCounts.Add(basinSize);
            }

        } while (nonEmptyFound == true);

        return basinCounts.OrderByDescending(i => i).Take(3).Aggregate(1, (acc, val) => acc * val);
    }

}