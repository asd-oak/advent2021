using advent2021.Models;

namespace advent2021.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Day 12 - Passage Pathing")]
[Route("[controller]")]
public class Day12Controller : ControllerBase
{

    public Day12Controller(IWebHostEnvironment environment, Tracer trace, IHttpContextAccessor hca)
    {
        SampleFilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample.txt");
        SampleFilePath2 = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample2.txt");
        SampleFilePath3 = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}-sample3.txt");
        FilePath = Path.Combine(environment.ContentRootPath, "input", $"{hca.GetControllerName()}.txt");
        RequestTracer = trace;
    }

    private string SampleFilePath { get; set; }
    private string SampleFilePath2 { get; set; }
    private string SampleFilePath3 { get; set; }
    private string FilePath { get; set; }

    private Tracer RequestTracer { get; set; }


    [HttpGet("Part1-Sample")]
    public long Part1Sample() => SolvePart1(SampleFilePath);


    [HttpGet("Part1-Sample2")]
    public long Part1Sample2() => SolvePart1(SampleFilePath2);


    [HttpGet("Part1-Sample3")]
    public long Part1Sample3() => SolvePart1(SampleFilePath3);

    [HttpGet("Part1")]
    public long Part1() => SolvePart1(FilePath);

    [HttpGet("Part2-Sample")]
    public long Part2Sample() => SolvePart2(SampleFilePath);

    [HttpGet("Part2-Sample2")]
    public long Part2Sample2() => SolvePart2(SampleFilePath2);

    [HttpGet("Part2-Sample3")]
    public long Part2Sample3() => SolvePart2(SampleFilePath3);

    [HttpGet("Part2")]
    public long Part2() => SolvePart2(FilePath);

    private void LogEndAndBackup(Day12Cavern currentCavern, Day12Path currentItinerary, SortedSet<string> pathLog)
    {
        if (!pathLog.Contains(currentItinerary.CavernItineraryFlat))
        {
            pathLog.Add(currentItinerary.CavernItineraryFlat);
        }
        var lastCavern = currentItinerary.Backtrack();
    }

    private void AdvanceCavernJourneyPart1(Day12Cavern currentCavern, Day12Path currentItinerary, SortedSet<string> pathLog)
    {
        if (currentCavern.Id == "end" || pathLog.Contains(currentItinerary.CavernItineraryFlat))
        {
            LogEndAndBackup(currentCavern, currentItinerary, pathLog);
            return;
        }

        foreach (var nextCavern in currentCavern.NeighboringCaverns)
        {
            // Cannot revisit lowercase caverns
            if (nextCavern.Id.ToUpperInvariant() != nextCavern.Id && currentItinerary.CavernItinerary.Any(c => c.Id == nextCavern.Id))
            {
                continue;
            }

            // Tentatively visit
            currentItinerary.AddVisit(nextCavern);
            AdvanceCavernJourneyPart1(nextCavern, currentItinerary, pathLog);
        }

        //Nowhere to go from this cavern; log path, back out of this cavern, and see if there is another route in the prior cavern
        LogEndAndBackup(currentCavern, currentItinerary, pathLog);
    }

    private void AdvanceCavernJourneyPart2(Day12Cavern currentCavern, Day12Path currentItinerary, SortedSet<string> pathLog)
    {
        if (currentCavern.Id == "end" || pathLog.Contains(currentItinerary.CavernItineraryFlat))
        {
            LogEndAndBackup(currentCavern, currentItinerary, pathLog);
            return;
        }

        foreach (var nextCavern in currentCavern.NeighboringCaverns)
        {
            // Cannot revisit lowercase caverns, except one
            if (nextCavern.Id.ToUpperInvariant() != nextCavern.Id && currentItinerary.CavernItinerary.Any(c => c.Id == nextCavern.Id))
            {
                if (nextCavern.Id == "start" || nextCavern.Id == "end" || !string.IsNullOrWhiteSpace(currentItinerary.RevisitedCavernId))
                {
                    continue;
                }
            }

            // Tentatively visit
            currentItinerary.AddVisit(nextCavern);
            AdvanceCavernJourneyPart2(nextCavern, currentItinerary, pathLog);
        }

        //Nowhere to go from this cavern; log path, back out of this cavern, and see if there is another route in the prior cavern
        LogEndAndBackup(currentCavern, currentItinerary, pathLog);
    }

    private long SolvePart1(string filePath)
    {
        var cavernLinkedPairs = System.IO.File.ReadLines(filePath).Select(linePair => linePair.Split('-').ToList()).ToList();

        var caverns = new List<Day12Cavern>();
        foreach (var cavernLinkedPair in cavernLinkedPairs)
        {
            var firstCavern = caverns.SingleOrDefault(c => c.Id == cavernLinkedPair.First());
            var secondCavern = caverns.SingleOrDefault(c => c.Id == cavernLinkedPair.Last());
            if (firstCavern == null)
            {
                firstCavern = new Day12Cavern(cavernLinkedPair.First());
                caverns.Add(firstCavern);
            }
            if (secondCavern == null)
            {
                secondCavern = new Day12Cavern(cavernLinkedPair.Last());
                caverns.Add(secondCavern);
            }
            firstCavern.AddReciprocalLink(secondCavern);
        }

        var allPathsTaken = new SortedSet<string>();

        var thisCavern = caverns.Single(c => c.Id == "start");
        var thisItinerary = new Day12Path(thisCavern);

        AdvanceCavernJourneyPart1(thisCavern, thisItinerary, allPathsTaken);

        var goodPathsTaken = allPathsTaken.Where(path => path.EndsWith("end")).ToList();

        return goodPathsTaken.Count;
    }

    private long SolvePart2(string filePath)
    {
        var cavernLinkedPairs = System.IO.File.ReadLines(filePath).Select(linePair => linePair.Split('-').ToList()).ToList();

        var caverns = new List<Day12Cavern>();
        foreach (var cavernLinkedPair in cavernLinkedPairs)
        {
            var firstCavern = caverns.SingleOrDefault(c => c.Id == cavernLinkedPair.First());
            var secondCavern = caverns.SingleOrDefault(c => c.Id == cavernLinkedPair.Last());
            if (firstCavern == null)
            {
                firstCavern = new Day12Cavern(cavernLinkedPair.First());
                caverns.Add(firstCavern);
            }
            if (secondCavern == null)
            {
                secondCavern = new Day12Cavern(cavernLinkedPair.Last());
                caverns.Add(secondCavern);
            }
            firstCavern.AddReciprocalLink(secondCavern);
        }

        var allPathsTaken = new SortedSet<string>();

        var thisCavern = caverns.Single(c => c.Id == "start");
        var thisItinerary = new Day12Path(thisCavern);

        AdvanceCavernJourneyPart2(thisCavern, thisItinerary, allPathsTaken);

        var goodPathsTaken = allPathsTaken.Where(path => path.EndsWith("end")).ToList();

        return goodPathsTaken.Count;
    }

}