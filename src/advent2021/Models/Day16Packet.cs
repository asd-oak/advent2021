namespace advent2021.Models;

public record Day16Packet(int Version, Day16PacketType PacketType)
{
    public List<Day16Packet> Children { get; } = new();

    public long LiteralValue { get; set; }

    public int VersionSum => Children.Sum(child => child.VersionSum) + Version;

    public long OverallValue
    {
        get => PacketType switch
        {
            Day16PacketType.Sum => Children.Sum(child => child.OverallValue),
            Day16PacketType.Product => Children.Aggregate(1L, (acc, child) => acc * child.OverallValue),
            Day16PacketType.Minimum => Children.Min(child => child.OverallValue),
            Day16PacketType.Maximum => Children.Max(child => child.OverallValue),
            Day16PacketType.Literal => LiteralValue,
            Day16PacketType.FirstGreaterThanSecond => (Children[0].OverallValue > Children[1].OverallValue ? 1 : 0),
            Day16PacketType.FirstLessThanSecond => (Children[0].OverallValue < Children[1].OverallValue ? 1 : 0),
            Day16PacketType.Equal => (Children[0].OverallValue == Children[1].OverallValue ? 1 : 0),
            _ => 0
        };
    }
}