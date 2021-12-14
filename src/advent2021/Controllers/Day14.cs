namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 14 - Extended Polymerization")]
[Route("[controller]")]
public class Day14Controller : ControllerBase
{

    public Day14Controller(IWebHostEnvironment environment)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\Day14-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\Day14.txt");
    }

    private string SampleFilePath { get; set; }
    private string FilePath { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolveBothParts(SampleFilePath, 10);

    [HttpGet("Part1")]
    public long Part1() => SolveBothParts(FilePath, 10);

    [HttpGet("Part2-Sample")]
    public long Part2Sample() => SolveBothParts(SampleFilePath, 40);

    [HttpGet("Part2")]
    public long Part2() => SolveBothParts(FilePath, 40);

    private long SolveBothParts(string filePath, short numIterations) {
        //Approach 1: For each iteration, loop over and make a new string. Fine for part 1. For part 2, we're looking at ~2^40, which is over a terabyte of memory for that final string.
        //Approach 2: For each pair, descend recursively to the loop depth and at the bottom update counters. Faster than Approach 1, but still not good enough for Part 2.
        //Approach 3: (below) When a pair splits into two new pairs, each pair is needed to figure out the next iteration's splits, but the output string only takes the first character of each new pair, plus the original template's final char. 
        // Maintain a list of unique pairs and counts and update during each iteration.

        var templateAndRules = System.IO.File.ReadLines(filePath).ToList();
        
        var rules = templateAndRules.Skip(2).Select(row => row.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()).Select(pairing => (pairing.First(), pairing.Last().ToCharArray().First())).ToList();
        var ruleLookup = new Dictionary<string, char>();
        rules.ForEach(pair => ruleLookup.Add(pair.Item1, pair.Item2));

        var counts = new Dictionary<string, long>();
        
        var template = templateAndRules.First().ToCharArray();
        for(var i = 0; i < template.Count() - 1; i++) {
            var countKey = new string(new char[] {template[i], template[i+1]});
            if(!counts.ContainsKey(countKey)) {
                counts[countKey] = 1;
            } else {
                counts[countKey]++;
            }
        }
        
        for (var i = 0; i < numIterations; i++) {
            var newCounts = new Dictionary<string, long>();
            foreach(var kvp in counts) {
                var newChar = ruleLookup[kvp.Key];
                
                var firstNewPair = new string(new char[] {kvp.Key.First(), newChar});
                if(!newCounts.ContainsKey(firstNewPair)) {
                    newCounts[firstNewPair] = kvp.Value;
                } else {
                    newCounts[firstNewPair] += kvp.Value;
                }

                var secondNewPair = new string(new char[] {newChar, kvp.Key.Last()});
                 if(!newCounts.ContainsKey(secondNewPair)) {
                    newCounts[secondNewPair] = kvp.Value;
                } else {
                    newCounts[secondNewPair] += kvp.Value;
                }
            }
            counts = newCounts;
        }

        var countsByChar = new Dictionary<char, long>();
        foreach(var kvp in counts) {
            var charIndex = kvp.Key.First();
            if(!countsByChar.ContainsKey(charIndex)) {
                countsByChar[charIndex] = kvp.Value;
            } else {
                countsByChar[charIndex] += kvp.Value;
            }
        }
        countsByChar[template[^1]]++;

        return countsByChar.Values.Max() - countsByChar.Values.Min();
    }

}