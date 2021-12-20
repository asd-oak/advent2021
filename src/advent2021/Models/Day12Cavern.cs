namespace advent2021.Models;
public class Day12Cavern
{

    public string Id { get; set; }

    public List<Day12Cavern> NeighboringCaverns { get; set; }

    public Day12Cavern(string id)
    {
        Id = id;
        NeighboringCaverns = new List<Day12Cavern>();
    }

    public void AddReciprocalLink(Day12Cavern neighbor)
    {
        if (!NeighboringCaverns.Any(cavern => cavern.Id == neighbor.Id))
        {
            NeighboringCaverns.Add(neighbor);
        }
        if (!neighbor.NeighboringCaverns.Any(cavern => cavern.Id == Id))
        {
            neighbor.AddReciprocalLink(this);
        }
    }


}