using System.Collections;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 20 - Trench Map")]
[Route("[controller]")]
public class Day20Controller : ControllerBase
{

    public Day20Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\Day20-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\Day20.txt");
        RequestTracer = trace;
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }

    private Tracer RequestTracer { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolveBothParts(SampleFilePath, 2);

    [HttpGet("Part1")]
    public long Part1() => SolveBothParts(FilePath, 2);

    [HttpGet("Part2-Sample")]
    public long Part2Sample() => SolveBothParts(SampleFilePath, 50);

    [HttpGet("Part2")]
    public long Part2() => SolveBothParts(FilePath, 50);

    private long SolveBothParts(string filePath, int numIterations)
    {
        var enhancementsMap = System.IO.File.ReadLines(filePath).First();

        // Sample and input conveniently flip bit of what to do when a square and it's 8 surrounding are all off (map[0]) or on (map[^1])
        // Off to off true => empty bit and empty surroundings iterates to empty. On bit and surroundings iterates to on. Stability.
        // Off to off FALSE => empty bit and empty surroundings iterates to on. On bit and surroundings iterates to off. Unstable.
        // Off to off FALSE implies implies we'd only ever iterate in pairs if we want to count on bits. Odd iterations to count off bits.

        bool offToOff = enhancementsMap[0] == '.';

        var imageMap = System.IO.File.ReadLines(filePath).Skip(2).ToList();

        using var span = RequestTracer.StartSpan("Preparing");
        span.SetAttribute("enhancementsLength", enhancementsMap.Length);
        span.SetAttribute("imageMapRows", imageMap.Count());
        span.SetAttribute("imageMapCols", imageMap.First().Length);

        var startingGrid = new bool[imageMap.Count(), imageMap.First().Length];
        foreach (var i in Enumerable.Range(0, imageMap.Count()))
        {
            foreach (var j in Enumerable.Range(0, imageMap.First().Length))
            {
                if (imageMap[i][j] == '#')
                {
                    startingGrid[i, j] = true;
                }
            }
        }

        var updatedGrid = IterateGrid(enhancementsMap, startingGrid, numIterations, true, offToOff);

        var litPixels = CountLitPixesl(updatedGrid);
        span.SetAttribute("litPixels", litPixels);

        return litPixels;
    }

    private bool[,] IterateGrid(string enhancementsMap, bool[,] startingState, int iterationsLeft, bool newGridEmpty, bool isStable)
    {
        if (iterationsLeft == 0)
        {
            return startingState;
        }
        using var span = RequestTracer.StartSpan($"IterateGrid-{iterationsLeft}");
        span.SetAttribute("litPixelsInput", CountLitPixesl(startingState));
        span.SetAttribute("newGridEmpty", newGridEmpty);

        //New grid for results, padded once for the space we may grow into and again to bring in some of the infinite canvas

        var updatedGrid = PadTrimGrid(startingState, 2, newGridEmpty);
        var paddedStartingGrid = (bool[,])updatedGrid.Clone();

        foreach (var i in Enumerable.Range(1, paddedStartingGrid.GetLength(0) - 2))
        {
            foreach (var j in Enumerable.Range(1, paddedStartingGrid.GetLength(1) - 2))
            {
                var examineBits = new BitArray(32);
                var index = 8;
                foreach (var peekI in Enumerable.Range(i - 1, 3))
                {
                    foreach (var peekJ in Enumerable.Range(j - 1, 3))
                    {
                        if (paddedStartingGrid[peekI, peekJ])
                        {
                            examineBits[index] = true;
                        }
                        index--;
                    }
                }
                var toInt = new int[1];
                examineBits.CopyTo(toInt, 0);
                var mapIndex = toInt[0];

                updatedGrid[i, j] = (enhancementsMap[mapIndex] == '#');
            }
        }

        //Trim the outer row of infinite canvas, since we didn't update it
        updatedGrid = PadTrimGrid(updatedGrid, -1, newGridEmpty);

        span.SetAttribute("litPixels", CountLitPixesl(updatedGrid));
        span.SetAttribute("litPixelsReference", CountLitPixesl(paddedStartingGrid));

        return IterateGrid(enhancementsMap, updatedGrid, iterationsLeft - 1, isStable ? newGridEmpty : !newGridEmpty, isStable);
    }

    private bool[,] PadTrimGrid(bool[,] grid, int distance, bool newGridEmpty)
    {
        if (distance == 0)
        {
            return (bool[,])grid.Clone();
        }
        using var span = RequestTracer.StartSpan($"PadTrimGrid-{distance}");

        var doubleDistance = distance * 2;
        var newGrid = new bool[grid.GetLength(0) + doubleDistance, grid.GetLength(1) + doubleDistance];

        //When expanding, new space defaults to false. Initialize to true if needed.
        if (!newGridEmpty && distance > 0)
        {
            var litCount = 0;
            span.SetAttribute("expectedInitialization", 2 * distance * (newGrid.GetLength(0) + newGrid.GetLength(1)) - 4 * Math.Pow(distance, 2));
            foreach (var i in Enumerable.Range(0, newGrid.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, newGrid.GetLength(1)))
                {
                    if (i < distance || j < distance || newGrid.GetLength(0) - i <= distance || newGrid.GetLength(1) - j <= distance)
                    {
                        newGrid[i, j] = true;
                        litCount++;
                    }
                }
            }
            span.SetAttribute("observedInitialization", litCount);
        }

        //Expanding or shrinking, copy only the retained subgrid
        //Expanding, copy entire grid into new grid, offset by distance
        //Shrinking, offset into current grid to copy to new grid
        foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
        {
            foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
            {
                if (i + distance >= 0 && j + distance >= 0 && i + distance < newGrid.GetLength(0) && j + distance < newGrid.GetLength(1))
                {
                    newGrid[i + distance, j + distance] = grid[i, j];
                }
            }
        }
        return newGrid;
    }

    private int CountLitPixesl(bool[,] grid)
    {
        var litPixels = 0;
        foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
        {
            foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
            {
                if (grid[i, j])
                {
                    litPixels++;
                }
            }
        }
        return litPixels;
    }
}