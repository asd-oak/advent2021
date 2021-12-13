using SixLabors.ImageSharp;

namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 13 - Transparent Origami")]
[Route("[controller]")]
public class Day13Controller : ControllerBase
{

    public Day13Controller(IWebHostEnvironment environment)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\Day13-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\Day13.txt");
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolvePart1(SampleFilePath);

    [HttpGet("Part1")]
    public long Part1() => SolvePart1(FilePath);

    [HttpGet("Part2-Sample")]
    public IActionResult Part2Sample() => SolvePart2(SampleFilePath);

    [HttpGet("Part2")]
    public IActionResult Part2() => SolvePart2(FilePath);

    private enum FoldDirection
    {
        VerticalUp,
        HorizontalLeft
    };

    private long SolvePart1(string filePath)
    {
        var pointsAndInstructions = System.IO.File.ReadLines(filePath).ToList(); //.Select(linePair => linePair.Split('-').ToList()).ToList();
        var pointsList = new List<List<short>>();
        var foldInstructions = new List<(FoldDirection, short)>();
        bool onInstructions = false;
        for (var i = 0; i < pointsAndInstructions.Count; i++)
        {
            var row = pointsAndInstructions[i];
            if (string.IsNullOrWhiteSpace(row))
            {
                onInstructions = true;
                continue;
            }
            if (onInstructions)
            {
                var rowParts = row.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (rowParts.First().EndsWith("x"))
                {
                    foldInstructions.Add((FoldDirection.HorizontalLeft, short.Parse(rowParts.Last())));
                }
                else
                {
                    foldInstructions.Add((FoldDirection.VerticalUp, short.Parse(rowParts.Last())));
                }
            }
            else
            {
                pointsList.Add(row.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(short.Parse).ToList());
            }
        }

        // Array.GetLength(0) = rows = Y
        // Array.GetLength(1) = cols = X
        var paperGrid = new bool[pointsList.Select(item => item.Last()).Max() + 1, pointsList.Select(item => item.First()).Max() + 1];

        // 2d array indexing is (y,x)
        pointsList.ForEach(pointPair =>
        {
            paperGrid[pointPair.Last(), pointPair.First()] = true;
        });

        //Part 1 challenge - first instruction only
        foldInstructions.RemoveRange(1, foldInstructions.Count - 1);

        foldInstructions.ForEach(instructionTuple =>
        {
            var orientation = instructionTuple.Item1;
            var location = instructionTuple.Item2;
            if (orientation == FoldDirection.VerticalUp)
            {

                //Wrong if fold might overlap into negative indices
                var newGrid = new bool[location, paperGrid.GetLength(1)];

                for (var y = 0; y < paperGrid.GetLength(0); y++)
                {
                    for (var x = 0; x < paperGrid.GetLength(1); x++)
                    {
                        if (y == location)
                        {
                            continue;
                        }
                        else if (y > location)
                        {
                            var newY = 2 * location - y;
                            if (newY < 0)
                            {
                                throw new Exception("We didn't expect to go negative");
                            }
                            else
                            {
                                if (!newGrid[newY, x])
                                {
                                    newGrid[newY, x] = paperGrid[y, x];
                                }
                            }
                        }
                        else
                        {
                            if (!newGrid[y, x])
                            {
                                newGrid[y, x] = paperGrid[y, x];
                            }
                        }

                    }
                }

                paperGrid = newGrid;
            }
            else
            {

                //Wrong if fold might overlap into negative indices
                var newGrid = new bool[paperGrid.GetLength(0), location];

                for (var y = 0; y < paperGrid.GetLength(0); y++)
                {
                    for (var x = 0; x < paperGrid.GetLength(1); x++)
                    {
                        if (x == location)
                        {
                            continue;
                        }
                        else if (x > location)
                        {
                            var newX = 2 * location - x;
                            if (newX < 0)
                            {
                                throw new Exception("We didn't expect to go negative");
                            }
                            else
                            {
                                if (!newGrid[y, newX])
                                {
                                    newGrid[y, newX] = paperGrid[y, x];
                                }
                            }
                        }
                        else
                        {
                            if (!newGrid[y, x])
                            {
                                newGrid[y, x] = paperGrid[y, x];
                            }
                        }

                    }
                }

                paperGrid = newGrid;
            }
        });

        var pointCount = 0;
        for (var y = 0; y < paperGrid.GetLength(0); y++)
        {
            for (var x = 0; x < paperGrid.GetLength(1); x++)
            {
                if (paperGrid[y, x])
                {
                    pointCount++;
                }

            }
        }

        return pointCount;
    }

    private IActionResult SolvePart2(string filePath)
    {
        var pointsAndInstructions = System.IO.File.ReadLines(filePath).ToList(); //.Select(linePair => linePair.Split('-').ToList()).ToList();
        var pointsList = new List<List<short>>();
        var foldInstructions = new List<(FoldDirection, short)>();
        bool onInstructions = false;
        for (var i = 0; i < pointsAndInstructions.Count; i++)
        {
            var row = pointsAndInstructions[i];
            if (string.IsNullOrWhiteSpace(row))
            {
                onInstructions = true;
                continue;
            }
            if (onInstructions)
            {
                var rowParts = row.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (rowParts.First().EndsWith("x"))
                {
                    foldInstructions.Add((FoldDirection.HorizontalLeft, short.Parse(rowParts.Last())));
                }
                else
                {
                    foldInstructions.Add((FoldDirection.VerticalUp, short.Parse(rowParts.Last())));
                }
            }
            else
            {
                pointsList.Add(row.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(short.Parse).ToList());
            }
        }

        // Array.GetLength(0) = rows = Y
        // Array.GetLength(1) = cols = X
        var paperGrid = new bool[pointsList.Select(item => item.Last()).Max() + 1, pointsList.Select(item => item.First()).Max() + 1];

        // 2d array indexing is (y,x)
        pointsList.ForEach(pointPair =>
        {
            paperGrid[pointPair.Last(), pointPair.First()] = true;
        });

        //Part 1 challenge - first instruction only
        //foldInstructions.RemoveRange(1,foldInstructions.Count-1);

        foldInstructions.ForEach(instructionTuple =>
        {
            var orientation = instructionTuple.Item1;
            var location = instructionTuple.Item2;
            if (orientation == FoldDirection.VerticalUp)
            {

                //Wrong if fold might overlap into negative indices
                var newGrid = new bool[location, paperGrid.GetLength(1)];

                for (var y = 0; y < paperGrid.GetLength(0); y++)
                {
                    for (var x = 0; x < paperGrid.GetLength(1); x++)
                    {
                        if (y == location)
                        {
                            continue;
                        }
                        else if (y > location)
                        {
                            var newY = 2 * location - y;
                            if (newY < 0)
                            {
                                throw new Exception("We didn't expect to go negative");
                            }
                            else
                            {
                                if (!newGrid[newY, x])
                                {
                                    newGrid[newY, x] = paperGrid[y, x];
                                }
                            }
                        }
                        else
                        {
                            if (!newGrid[y, x])
                            {
                                newGrid[y, x] = paperGrid[y, x];
                            }
                        }

                    }
                }

                paperGrid = newGrid;
            }
            else
            {

                //Wrong if fold might overlap into negative indices
                var newGrid = new bool[paperGrid.GetLength(0), location];

                for (var y = 0; y < paperGrid.GetLength(0); y++)
                {
                    for (var x = 0; x < paperGrid.GetLength(1); x++)
                    {
                        if (x == location)
                        {
                            continue;
                        }
                        else if (x > location)
                        {
                            var newX = 2 * location - x;
                            if (newX < 0)
                            {
                                throw new Exception("We didn't expect to go negative");
                            }
                            else
                            {
                                if (!newGrid[y, newX])
                                {
                                    newGrid[y, newX] = paperGrid[y, x];
                                }
                            }
                        }
                        else
                        {
                            if (!newGrid[y, x])
                            {
                                newGrid[y, x] = paperGrid[y, x];
                            }
                        }

                    }
                }

                paperGrid = newGrid;
            }
        });

        var responseImage = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(paperGrid.GetLength(1), paperGrid.GetLength(0));
        for (var y = 0; y < paperGrid.GetLength(0); y++)
        {
            for (var x = 0; x < paperGrid.GetLength(1); x++)
            {
                if(paperGrid[y,x]) {
                    responseImage[x,y] = SixLabors.ImageSharp.Color.AliceBlue;
                }
            }
        }
        Stream responseFile = new MemoryStream();
        responseImage.SaveAsJpeg(responseFile);
        responseFile.Position = 0;
        return File(responseFile, "image/jpeg");
    }

}