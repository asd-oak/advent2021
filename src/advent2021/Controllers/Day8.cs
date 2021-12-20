namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 8 - Seven Segment Search")]
[Route("[controller]")]
public class Day8Controller : ControllerBase
{

    public Day8Controller(IWebHostEnvironment environment, Tracer trace)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, @"input\day8-sample.txt");
        FilePath = Path.Combine(environment.ContentRootPath, @"input\day8.txt");
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
        var displayPatterns = System.IO.File
                .ReadLines(filePath)
                .Select(row => row
                    .Split("|", StringSplitOptions.RemoveEmptyEntries)
                    .Select(rowPart => rowPart
                        .Trim()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(displayValue =>
                        {
                            var charred = displayValue.ToCharArray().ToList();
                            charred.Sort();
                            return new string(charred.ToArray());
                        }))
                ).ToList();
        var matches = new List<int> { 2, 3, 4, 7 };
        var findings = 0;
        displayPatterns.ForEach(row => findings += row.Last().Where(pattern => matches.Contains(pattern.Length)).Count());

        return findings; //543
    }

    private long SolvePart2(string filePath)
    {
        var displayPatterns = System.IO.File
                .ReadLines(filePath)
                .Select(row => row
                    .Split("|", StringSplitOptions.RemoveEmptyEntries)
                    .Select(rowPart => rowPart
                        .Trim()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(displayValue =>
                        {
                            var charred = displayValue.ToCharArray().ToList();
                            charred.Sort();
                            return charred;
                        }))
                ).ToList();

        /*
            1: cf - length 2
            7: Acf - length 3 - A now known
            4: bcdf - length 4
            9: AbcdfG - length 4 and contains the chars from #4 - G now known
            8: AbcdEfG - length 7 - E now known
            2: ACDEG - length 5 and contains A,G,E chars 
                - C known by matching against CF from #1
                - F known; the other char from #1
                - D known; the other from #2
                - B known; the last one
        */
        var decoded = new List<Dictionary<string, byte>>();

        displayPatterns.ForEach(row =>
        {
            var decodedRow = new Dictionary<string, byte>();
            var decodedPatterns = row.First().ToList();
            var segmentMap = new Dictionary<char, char>();

            var numberOne = decodedPatterns.Where(pattern => pattern.Count == 2).Single();
            decodedRow[new string(numberOne.ToArray())] = 1;

            var numberSeven = decodedPatterns.Where(pattern => pattern.Count == 3).Single();
            decodedRow[new string(numberSeven.ToArray())] = 7;
            segmentMap['a'] = numberSeven.Except(numberOne).Single();

            var numberFour = decodedPatterns.Where(pattern => pattern.Count == 4).Single();
            decodedRow[new string(numberFour.ToArray())] = 4;

            var numberNine = decodedPatterns.Where(pattern => pattern.Count == 6 && pattern.Except(numberFour).Count() == 2).Single();
            decodedRow[new string(numberNine.ToArray())] = 9;
            segmentMap['g'] = numberNine.Except(numberFour).Except(new List<char> { segmentMap['a'] }).Single();

            var numberEight = decodedPatterns.Where(pattern => pattern.Count == 7).Single();
            decodedRow[new string(numberEight.ToArray())] = 8;
            segmentMap['e'] = numberEight.Except(numberFour).Except(new List<char> { segmentMap['a'] }).Except(new List<char> { segmentMap['g'] }).Single();

            var numberTwo = decodedPatterns.Where(pattern => pattern.Count == 5 && pattern.Contains(segmentMap['a']) && pattern.Contains(segmentMap['g']) && pattern.Contains(segmentMap['e'])).Single();
            decodedRow[new string(numberTwo.ToArray())] = 2;
            segmentMap['c'] = numberTwo.Except(new List<char> { segmentMap['a'] }).Except(new List<char> { segmentMap['g'] }).Except(new List<char> { segmentMap['e'] }).Intersect(numberOne).Single();
            segmentMap['f'] = numberOne.Except(new List<char> { segmentMap['c'] }).Single();
            segmentMap['d'] = numberTwo.Except(new List<char> { segmentMap['a'] }).Except(new List<char> { segmentMap['c'] }).Except(new List<char> { segmentMap['e'] }).Except(new List<char> { segmentMap['g'] }).Single();
            segmentMap['b'] = numberEight.Except(new List<char> { segmentMap['a'] }).Except(new List<char> { segmentMap['c'] }).Except(new List<char> { segmentMap['d'] }).Except(new List<char> { segmentMap['e'] }).Except(new List<char> { segmentMap['f'] }).Except(new List<char> { segmentMap['g'] }).Single();

            var numberZero = new List<char> {
                segmentMap['a'],
                segmentMap['b'],
                segmentMap['c'],
                segmentMap['e'],
                segmentMap['f'],
                segmentMap['g'],
            };
            numberZero.Sort();
            decodedRow[new string(numberZero.ToArray())] = 0;

            var numberThree = new List<char> {
                segmentMap['a'],
                segmentMap['c'],
                segmentMap['d'],
                segmentMap['f'],
                segmentMap['g'],
            };
            numberThree.Sort();
            decodedRow[new string(numberThree.ToArray())] = 3;

            var numberFive = new List<char> {
                segmentMap['a'],
                segmentMap['b'],
                segmentMap['d'],
                segmentMap['f'],
                segmentMap['g'],
            };
            numberFive.Sort();
            decodedRow[new string(numberFive.ToArray())] = 5;

            var numberSix = new List<char> {
                segmentMap['a'],
                segmentMap['b'],
                segmentMap['d'],
                segmentMap['e'],
                segmentMap['f'],
                segmentMap['g'],
            };
            numberSix.Sort();
            decodedRow[new string(numberSix.ToArray())] = 6;


            decoded.Add(decodedRow);
        });

        var sum = 0;

        for (var i = 0; i < displayPatterns.Count; i++)
        {
            var legend = decoded[i];
            var numbers = displayPatterns[i].Last().ToList();
            var number =
                1000 * legend[new string(numbers[0].ToArray())]
                + 100 * legend[new string(numbers[1].ToArray())]
                + 10 * legend[new string(numbers[2].ToArray())]
                + legend[new string(numbers[3].ToArray())];
            sum += number;
        }

        return sum;
    }

}