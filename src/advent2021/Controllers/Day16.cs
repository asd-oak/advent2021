using advent2021.Models;

using System.Collections;

namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 16 - Packet Decoder")]
[Route("[controller]")]
public partial class Day16Controller : ControllerBase
{

    public Day16Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample.txt");
        Sample2FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample2.txt");
        Sample3FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample3.txt");
        Sample4FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample4.txt");
        Sample5FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample5.txt");
        Sample6FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample6.txt");
        Sample7FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample7.txt");
        Sample8FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample8.txt");
        Sample9FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample9.txt");
        Sample10FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample10.txt");
        Sample11FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample11.txt");
        Sample12FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample12.txt");
        FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}.txt");
        RequestTracer = trace;
    }

    private string SampleFilePath { get; set; }
    private string Sample2FilePath { get; set; }
    private string Sample3FilePath { get; set; }
    private string Sample4FilePath { get; set; }
    private string Sample5FilePath { get; set; }
    private string Sample6FilePath { get; set; }
    private string Sample7FilePath { get; set; }
    private string Sample8FilePath { get; set; }
    private string Sample9FilePath { get; set; }
    private string Sample10FilePath { get; set; }
    private string Sample11FilePath { get; set; }
    private string Sample12FilePath { get; set; }

    private string FilePath { get; set; }

    private Tracer RequestTracer { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolvePart1(SampleFilePath);

    [HttpGet("Part1-Sample2")]
    public long Part1Sample2() => SolvePart1(Sample2FilePath);

    [HttpGet("Part1-Sample3")]
    public long Part1Sample3() => SolvePart1(Sample3FilePath);

    [HttpGet("Part1-Sample4")]
    public long Part1Sample4() => SolvePart1(Sample4FilePath);

    [HttpGet("Part1")]
    public long Part1() => SolvePart1(FilePath);

    [HttpGet("Part2-Sample5")]
    public long Part2Sample5() => SolvePart2(Sample5FilePath);

    [HttpGet("Part2-Sample6")]
    public long Part2Sample6() => SolvePart2(Sample6FilePath);

    [HttpGet("Part2-Sample7")]
    public long Part2Sample7() => SolvePart2(Sample7FilePath);

    [HttpGet("Part2-Sample8")]
    public long Part2Sample8() => SolvePart2(Sample8FilePath);

    [HttpGet("Part2-Sample9")]
    public long Part2Sample9() => SolvePart2(Sample9FilePath);

    [HttpGet("Part2-Sample10")]
    public long Part2Sample10() => SolvePart2(Sample10FilePath);

    [HttpGet("Part2-Sample11")]
    public long Part2Sample11() => SolvePart2(Sample11FilePath);

    [HttpGet("Part2-Sample12")]
    public long Part2Sample12() => SolvePart2(Sample12FilePath);

    [HttpGet("Part2")]
    public long Part2() => SolvePart2(FilePath);

    private long SolvePart1(string filePath)
    {
        var rawInput = System.IO.File.ReadLines(filePath).First().Trim();

        var packets = new List<Day16Packet>();
        var bitGiver = new Day16BitDispenser(rawInput);

        packets.AddRange(GetPacketsByCount(1, bitGiver));

        return packets.Sum(packet => packet.VersionSum);
    }

    private List<Day16Packet> GetPacketsByCount(int howMany, Day16BitDispenser bitGiver)
    {
        List<Day16Packet> result = new();
        for (var i = 0; i < howMany; i++)
        {
            var newPacket = GetPacket(bitGiver);
            if (newPacket is null)
            {
                break;
            }
            result.Add(newPacket);
        }
        return result;
    }

    private List<Day16Packet> GetPacketsByLength(int stopAtBits, Day16BitDispenser bitGiver)
    {
        List<Day16Packet> result = new();
        while (bitGiver.BitsGiven < stopAtBits)
        {
            var newPacket = GetPacket(bitGiver);
            if (newPacket is null)
            {
                break;
            }
            result.Add(newPacket);
        }
        return result;
    }

    private Day16Packet? GetPacket(Day16BitDispenser bitGiver)
    {
        BitArray? versionBits = bitGiver.GetBits(3);
        BitArray? typeBits = bitGiver.GetBits(3);
        if (versionBits is null || typeBits is null)
        {
            return null;
        }
        var newPacket = new Day16Packet(versionBits.BitsToNumberAfterReverse(), (Day16PacketType)typeBits.BitsToNumberAfterReverse());
        if (newPacket.PacketType == Day16PacketType.Literal)
        {
            newPacket.LiteralValue = GetLiteral(bitGiver);
        }
        else
        {
            var lengthTypeBit = bitGiver.GetBits(1);
            if (lengthTypeBit![0] != true)
            {
                var subpacketBitLength = bitGiver.GetBits(15)!.BitsToNumberAfterReverse();
                newPacket.Children.AddRange(GetPacketsByLength(bitGiver.BitsGiven + subpacketBitLength, bitGiver));
            }
            else
            {
                var subpacketCount = bitGiver.GetBits(11)!.BitsToNumberAfterReverse();
                newPacket.Children.AddRange(GetPacketsByCount(subpacketCount, bitGiver));
            }
        }
        return newPacket;
    }

    private long GetLiteral(Day16BitDispenser bitGiver)
    {
        var resultBits = new Queue<bool>();

        BitArray literalChunk;

        do
        {
            literalChunk = bitGiver.GetBits(5)!;
            foreach (var index in Enumerable.Range(1, 4))
            {
                resultBits.Enqueue(literalChunk[index]);
            }

        } while (literalChunk[0] == true);

        // Remove unnecessary leading 0s
        while (resultBits.Peek() == false)
        {
            resultBits.Dequeue();
        }

        return resultBits.BitsToLong();
    }


    private long SolvePart2(string filePath)
    {
        var rawInput = System.IO.File.ReadLines(filePath).First().Trim();

        var packets = new List<Day16Packet>();
        var bitGiver = new Day16BitDispenser(rawInput);

        packets.AddRange(GetPacketsByCount(1, bitGiver));

        return packets.First().OverallValue;
    }

}