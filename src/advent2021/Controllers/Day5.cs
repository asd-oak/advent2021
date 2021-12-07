namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 5 - Hydrothermal Venture")]
[Route("[controller]")]
public class Day5Controller : ControllerBase
{

    public Day5Controller(IWebHostEnvironment environment)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day5-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day5.txt");
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }


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
        var ventVectors = System.IO.File
            .ReadLines(filePath)
            .Select(row => row
                .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                .Select(coords => coords
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)))
            .ToList();

        var maxGridLength = 0;
        ventVectors.ForEach(vpair =>
        {
            var max = Math.Max(vpair.First().Max(), vpair.Last().Max());
            if (maxGridLength <= max)
            {
                maxGridLength = max + 1;
            }
        });

        var grid = new int[maxGridLength, maxGridLength];

        ventVectors.ForEach(vpair =>
        {
            // Both coordinates match on X
            if (vpair.First().First() == vpair.Last().First())
            {
                var start = vpair.First().Last();
                var end = vpair.Last().Last();
                if (start > end)
                {
                    end = Interlocked.Exchange(ref start, end);
                }
                for (var i = start; i <= end; i++)
                {
                    grid[vpair.First().First(), i]++;
                }
            }
            else if (vpair.First().Last() == vpair.Last().Last())
            {
                var start = vpair.First().First();
                var end = vpair.Last().First();
                if (start > end)
                {
                    end = Interlocked.Exchange(ref start, end);
                }
                for (var i = start; i <= end; i++)
                {
                    grid[i, vpair.First().Last()]++;
                }
            }
        });

        var twoPlusCount = 0;
        for (var i = 0; i < maxGridLength; i++)
        {
            for (var j = 0; j < maxGridLength; j++)
            {
                if (grid[i, j] > 1)
                {
                    twoPlusCount++;
                }
            }
        }
        return twoPlusCount; //5167
    }

    private int SolvePart2(string filePath)
    {
        var ventVectors = System.IO.File
            .ReadLines(filePath)
            .Select(row => row
                .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                .Select(coords => coords
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)))
            .ToList();

        var maxGridLength = 0;
        ventVectors.ForEach(vpair =>
        {
            var max = Math.Max(vpair.First().Max(), vpair.Last().Max());
            if (maxGridLength <= max)
            {
                maxGridLength = max + 1;
            }
        });

        var grid = new int[maxGridLength, maxGridLength];

        ventVectors.ForEach(vpair =>
        {
            // Both coordinates match on X
            if (vpair.First().First() == vpair.Last().First())
            {
                var start = vpair.First().Last();
                var end = vpair.Last().Last();
                if (start > end)
                {
                    end = Interlocked.Exchange(ref start, end);
                }
                for (var i = start; i <= end; i++)
                {
                    grid[vpair.First().First(), i]++;
                }
            }
            else if (vpair.First().Last() == vpair.Last().Last())
            {
                //Coordinates match on Y
                var start = vpair.First().First();
                var end = vpair.Last().First();
                if (start > end)
                {
                    end = Interlocked.Exchange(ref start, end);
                }
                for (var i = start; i <= end; i++)
                {
                    grid[i, vpair.First().Last()]++;
                }
            }
            else
            {
                var x1 = vpair.First().First();
                var x2 = vpair.Last().First();

                var y1 = vpair.First().Last();
                var y2 = vpair.Last().Last();

                if (x1 > x2)
                {
                    x2 = Interlocked.Exchange(ref x1, x2);
                    y2 = Interlocked.Exchange(ref y1, y2);
                }

                bool ascending = (y1 < y2);

                var j = y1;
                for (var i = x1; i <= x2; i++)
                {
                    grid[i, j]++;
                    if (ascending)
                    {
                        j++;
                    }
                    else
                    {
                        j--;
                    }
                }
            };
        });

        var twoPlusCount = 0;
        for (var i = 0; i < maxGridLength; i++)
        {
            for (var j = 0; j < maxGridLength; j++)
            {
                if (grid[i, j] > 1)
                {
                    twoPlusCount++;
                }
            }
        }
        return twoPlusCount; //17604
    }

}