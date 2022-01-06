namespace advent2021.Models;

public record Day15ChitonNode(int Y, int X, int NodeRisk)
{
    public int BestPathRisk { get; set; } = int.MaxValue;
    public Day15ChitonNode? Parent { get; set; }
    public bool Visited { get; set; }
}