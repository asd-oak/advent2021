namespace advent2021.Models;

public class Day15ChitonComparer : IComparer<Day15ChitonNode>
{
    // Returns:
    //     A signed integer that indicates the relative values of x and y, as shown in the
    //     following table.
    //     Value – Meaning
    //     Less than zero – x is less than y.
    //     Zero – x equals y.
    //     Greater than zero – x is greater than y.
    public int Compare(Day15ChitonNode? x, Day15ChitonNode? y)
    {
        ArgumentNullException.ThrowIfNull(x);
        ArgumentNullException.ThrowIfNull(y);

        if (x.BestPathRisk < y.BestPathRisk)
        {
            return -1;
        }
        else if (x.BestPathRisk > y.BestPathRisk)
        {
            return 1;
        }
        else if (x == y)
        {
            return 0;
        }
        else
        {
            return (x.X * 1000 + x.Y < y.X * 1000 + y.Y) ? 1 : -1;
        }
    }

}