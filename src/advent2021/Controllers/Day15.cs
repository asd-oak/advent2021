using advent2021.Models;

namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 15 - Chiton")]
[Route("[controller]")]
public partial class Day15Controller : ControllerBase
{

    public Day15Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\Day15-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\Day15.txt");
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
        var cavernRiskRows = System.IO.File.ReadLines(filePath).ToList();

        var cavernSize = cavernRiskRows.Count();
        if (cavernSize != cavernRiskRows.First().Length)
        {
            throw new Exception("Solution assumes square grid");
        }

        var cavernMap = new Day15ChitonNode[cavernSize, cavernSize];

        // Quickly find the next node to visit. Only sorts during insert/delete--don't modify items while in set.
        var candidateNodesByPathRisk = new SortedSet<Day15ChitonNode>(new Day15ChitonComparer());

        foreach (var y in Enumerable.Range(0, cavernSize))
        {
            foreach (var x in Enumerable.Range(0, cavernSize))
            {
                cavernMap[y, x] = new Day15ChitonNode(y, x, int.Parse(new string(new char[] { cavernRiskRows[y][x] })));
            }
        }

        var nextNode = cavernMap[0, 0];
        nextNode.BestPathRisk = 0;
        while (nextNode != null)
        {
            VisitNode(nextNode, cavernMap, cavernSize, candidateNodesByPathRisk);
            nextNode = null;

            if (cavernMap[cavernSize - 1, cavernSize - 1].Visited)
            {
                break;
            }

            // Unvisited node with lowest path risk is our next visit
            if (candidateNodesByPathRisk.Any())
            {
                nextNode = candidateNodesByPathRisk.First();
            }
        }

        return cavernMap[cavernSize - 1, cavernSize - 1].BestPathRisk;
    }

    private void VisitNode(Day15ChitonNode node, Day15ChitonNode[,] cavernMap, int cavernSize, SortedSet<Day15ChitonNode> candidateNodesByPathRisk)
    {
        // We only visit a node once we are sure we've found the best path risk for it
        node.Visited = true;
        if (candidateNodesByPathRisk.Contains(node))
        {
            candidateNodesByPathRisk.Remove(node);
        }

        // Update estimates for neighbors

        // Right
        PeekNeighbor(node, FindNode(node.Y, node.X + 1, cavernMap, cavernSize), candidateNodesByPathRisk);

        // Down
        PeekNeighbor(node, FindNode(node.Y + 1, node.X, cavernMap, cavernSize), candidateNodesByPathRisk);

        // Left
        PeekNeighbor(node, FindNode(node.Y, node.X - 1, cavernMap, cavernSize), candidateNodesByPathRisk);

        // Up
        PeekNeighbor(node, FindNode(node.Y - 1, node.X, cavernMap, cavernSize), candidateNodesByPathRisk);
    }

    private Day15ChitonNode? FindNode(int y, int x, Day15ChitonNode[,] cavernMap, int cavernSize)
    {
        if (y < 0 || x < 0 || y > cavernSize - 1 || x > cavernSize - 1)
        {
            return null;
        }
        return cavernMap[y, x];
    }

    private void PeekNeighbor(Day15ChitonNode fromNode, Day15ChitonNode? peekNode, SortedSet<Day15ChitonNode> candidateNodesByPathRisk)
    {
        if (peekNode is null || peekNode.Visited)
        {
            return;
        }

        if (fromNode.BestPathRisk + peekNode.NodeRisk < peekNode.BestPathRisk)
        {
            if (candidateNodesByPathRisk.Contains(peekNode))
            {
                candidateNodesByPathRisk.Remove(peekNode);
            }
            peekNode.BestPathRisk = fromNode.BestPathRisk + peekNode.NodeRisk;
            peekNode.Parent = fromNode;
            candidateNodesByPathRisk.Add(peekNode);
        }
    }

    private long SolvePart2(string filePath)
    {
        var cavernRiskRows = System.IO.File.ReadLines(filePath).ToList();

        var inputSize = cavernRiskRows.Count();
        var cavernSize = inputSize * 5;
        if (inputSize != cavernRiskRows.First().Length)
        {
            throw new Exception("Solution assumes square grid");
        }

        var cavernMap = new Day15ChitonNode[cavernSize, cavernSize];

        // Quickly find the next node to visit. Only sorts during insert/delete--don't modify items while in set.
        var candidateNodesByPathRisk = new SortedSet<Day15ChitonNode>(new Day15ChitonComparer());

        foreach (var y in Enumerable.Range(0, cavernSize))
        {
            foreach (var x in Enumerable.Range(0, cavernSize))
            {
                // Repeating input 5 times, incrementing risk each time we go across or down
                var lookupX = x % inputSize;
                var lookupY = y % inputSize;
                var bonusRisk = y / inputSize + x / inputSize;
                var pointRisk = (bonusRisk + int.Parse(new string(new char[] { cavernRiskRows[lookupY][lookupX] })) - 1) % 9 + 1;
                cavernMap[y, x] = new Day15ChitonNode(y, x, pointRisk);
            }
        }

        var nextNode = cavernMap[0, 0];
        nextNode.BestPathRisk = 0;
        while (nextNode != null)
        {
            VisitNode(nextNode, cavernMap, cavernSize, candidateNodesByPathRisk);
            nextNode = null;

            if (cavernMap[cavernSize - 1, cavernSize - 1].Visited)
            {
                break;
            }

            // Unvisited node with lowest path risk is our next visit
            if (candidateNodesByPathRisk.Any())
            {
                nextNode = candidateNodesByPathRisk.First();
            }
        }

        return cavernMap[cavernSize - 1, cavernSize - 1].BestPathRisk;
    }

}