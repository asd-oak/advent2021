namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 11 - Dumbo Octopus")]
[Route("[controller]")]
public class Day11Controller : ControllerBase
{

    public Day11Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
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
        var octoLines = System.IO.File.ReadLines(filePath).ToList();

        var octoGrid = new byte[10, 10];
        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                octoGrid[i, j] = byte.Parse(octoLines[i].ToCharArray()[j].ToString());
            }
        }

        var stepTo = 100;
        long flashCount = 0;

        for (var step = 0; step < stepTo; step++)
        {
            //First increment
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    octoGrid[i, j]++;
                }
            }

            //Increment neighbors as long as new flashes happen
            var flashList = new List<(int, int)>();
            var flashed = false;
            do
            {
                flashed = false;

                for (var i = 0; i < 10; i++)
                {
                    for (var j = 0; j < 10; j++)
                    {
                        if (octoGrid[i, j] > 9 && !flashList.Contains((i, j)))
                        {
                            flashed = true;
                            flashCount++;
                            flashList.Add((i, j));
                            octoGrid[i, j] = 0;

                            if (i > 0 && i < 9 && j > 0 && j < 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i > 0 && i < 9 && j == 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i > 0 && i < 9 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j > 0 && j < 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j > 0 && j < 9)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j == 9)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j == 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                        }

                    }
                }
            } while (flashed);

            // Reset flashed to zero
            foreach (var coords in flashList)
            {
                octoGrid[coords.Item1, coords.Item2] = 0;
            }


        }
        return flashCount;
    }

    private long SolvePart2(string filePath)
    {
        var octoLines = System.IO.File.ReadLines(filePath).ToList();

        var octoGrid = new byte[10, 10];
        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                octoGrid[i, j] = byte.Parse(octoLines[i].ToCharArray()[j].ToString());
            }
        }

        int flashCount = 0;
        var step = 0;

        while (flashCount != 100)
        {
            flashCount = 0;
            //First increment
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    octoGrid[i, j]++;
                }
            }

            //Increment neighbors as long as new flashes happen
            var flashList = new List<(int, int)>();
            var flashed = false;
            do
            {
                flashed = false;

                for (var i = 0; i < 10; i++)
                {
                    for (var j = 0; j < 10; j++)
                    {
                        if (octoGrid[i, j] > 9 && !flashList.Contains((i, j)))
                        {
                            flashed = true;
                            flashCount++;
                            flashList.Add((i, j));
                            octoGrid[i, j] = 0;

                            if (i > 0 && i < 9 && j > 0 && j < 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i > 0 && i < 9 && j == 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i > 0 && i < 9 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j > 0 && j < 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j > 0 && j < 9)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 0 && j == 9)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                //octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                octoGrid[i + 1, j - 1]++;
                                octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j == 9)
                            {
                                octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                //octoGrid[i - 1, j + 1]++;
                                //octoGrid[i, j + 1]++;
                                octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                            else if (i == 9 && j == 0)
                            {
                                //octoGrid[i - 1, j - 1]++;
                                octoGrid[i - 1, j]++;
                                octoGrid[i - 1, j + 1]++;
                                octoGrid[i, j + 1]++;
                                //octoGrid[i, j - 1]++;
                                //octoGrid[i + 1, j - 1]++;
                                //octoGrid[i + 1, j]++;
                                //octoGrid[i + 1, j + 1]++;
                            }
                        }

                    }
                }
            } while (flashed);

            // Reset flashed to zero
            foreach (var coords in flashList)
            {
                octoGrid[coords.Item1, coords.Item2] = 0;
            }

            step++;
        }
        return step;
    }

}