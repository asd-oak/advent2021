namespace advent2021.Models;
public class Day12Path
{
    public List<Day12Cavern> CavernItinerary {get;set;}

    public string CavernItineraryFlat {get;set;} = string.Empty;

    public string RevisitedCavernId {get;set;} = string.Empty;

    public Day12Path(Day12Cavern start) {
        CavernItinerary = new List<Day12Cavern>();
        AddVisit(start);
    }

    public void AddVisit(Day12Cavern neighbor) {
        if(CavernItinerary.Any(c => c.Id == neighbor.Id && neighbor.Id.ToUpperInvariant() != neighbor.Id)) {
            RevisitedCavernId = neighbor.Id;
        }
        CavernItinerary.Add(neighbor);
        SetItineraryFlat();
    }

    public Day12Cavern? Backtrack() 
    {
        if(CavernItinerary.Count == 1) {
            return null;
        }

        var lastCavern = CavernItinerary.Last();
        if(lastCavern.Id == RevisitedCavernId) {
            RevisitedCavernId = string.Empty;
        }
        CavernItinerary.RemoveAt(CavernItinerary.Count - 1);
        SetItineraryFlat();

        return lastCavern; 
    }

    private void SetItineraryFlat() {
        CavernItineraryFlat = string.Join(string.Empty, CavernItinerary.Select(c => c.Id));
    }

}