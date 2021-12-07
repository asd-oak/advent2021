namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 4 - Giant Squid")]
[Route("[controller]")]
public class Day4Controller : ControllerBase
{

    public Day4Controller(IWebHostEnvironment environment)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day4-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day4.txt");
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
        var playsAndBoards = System.IO.File.ReadLines(filePath).ToList();
        var plays = playsAndBoards.First().Split(',').Select(int.Parse).ToList();

        //Represent boards twice, as the original 5x5 grid split into rows and again as the transposed grid split into rows
        //Remove row elements when they are called
        //If a row is empty, that grid wins
        //The last called number plus the remaining elements in the grid go into the result

        var boardgroups = playsAndBoards
            .Skip(2)
            .Select((row, index) => new { row, index })
            .GroupBy(pairing => pairing.index / 6, el => el.row);

        var boardList = new List<(List<List<int>> original, List<List<int>> transposed)>();
        foreach (var boardlines in boardgroups)
        {
            var boardMatrixTransposed = new int[5, 5];
            var originalLines = boardlines.Take(5).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    boardMatrixTransposed[j, i] = originalLines[i][j];
                }
            }
            var transposedLines = new List<List<int>>();
            for (var i = 0; i < 5; i++)
            {
                var newRow = new List<int>();
                for (var j = 0; j < 5; j++)
                {
                    newRow.Add(boardMatrixTransposed[i, j]);
                }
                transposedLines.Add(newRow);
            }
            boardList.Add((original: originalLines, transposed: transposedLines));
        }
        var winningPlay = 0;
        var winningSum = 0;
        plays.ForEach(currentPlay =>
        {
            boardList.ForEach(boardPair =>
            {
                if (winningPlay == 0 && winningSum == 0)
                {
                    boardPair.original.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));
                    boardPair.transposed.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));

                    if (boardPair.original.Any(boardRow => boardRow.Count == 0) || boardPair.transposed.Any(boardRow => boardRow.Count == 0))
                    {
                        var boardSum = boardPair.original.Sum(boardRow => boardRow.Sum());
                        winningSum = boardSum;
                        winningPlay = currentPlay;
                    }
                }
            });
        });

        return winningSum * winningPlay; // 34506
    }

    private int SolvePart2(string filePath)
    {
        var playsAndBoards = System.IO.File.ReadLines(filePath).ToList();
        var plays = playsAndBoards.First().Split(',').Select(int.Parse).ToList();

        //Represent boards twice, as the original 5x5 grid split into rows and again as the transposed grid split into rows
        //Remove row elements when they are called
        //If a row is empty, that grid wins
        //The last called number plus the remaining elements in the grid calculate the result

        var boardgroups = playsAndBoards
            .Skip(2)
            .Select((row, index) => new { row, index })
            .GroupBy(pairing => pairing.index / 6, el => el.row);

        var boardList = new List<(List<List<int>> original, List<List<int>> transposed, List<bool> won)>();
        foreach (var boardlines in boardgroups)
        {
            var boardMatrixTransposed = new int[5, 5];
            var originalLines = boardlines.Take(5).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    boardMatrixTransposed[j, i] = originalLines[i][j];
                }
            }
            var transposedLines = new List<List<int>>();
            for (var i = 0; i < 5; i++)
            {
                var newRow = new List<int>();
                for (var j = 0; j < 5; j++)
                {
                    newRow.Add(boardMatrixTransposed[i, j]);
                }
                transposedLines.Add(newRow);
            }
            boardList.Add((original: originalLines, transposed: transposedLines, won: new List<bool> { false }));
        }
        var winningPlay = 0;
        var winningSum = 0;

        // Difference from 4-1: List<bool> in the board tuple. A list can be modified within the foreach, which works, but ew.
        plays.ForEach(currentPlay =>
        {
            boardList.ForEach(boardPair =>
            {
                if (!boardPair.won.Any(el => el == true))
                {
                    boardPair.original.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));
                    boardPair.transposed.ForEach(boardRow => boardRow.RemoveAll(rowItem => rowItem == currentPlay));

                    if (boardPair.original.Any(boardRow => boardRow.Count == 0) || boardPair.transposed.Any(boardRow => boardRow.Count == 0))
                    {
                        var boardSum = boardPair.original.Sum(boardRow => boardRow.Sum());
                        winningSum = boardSum;
                        winningPlay = currentPlay;
                        boardPair.won[0] = true;
                    }
                }
            });
        });

        return winningSum * winningPlay; // 7686
    }

}