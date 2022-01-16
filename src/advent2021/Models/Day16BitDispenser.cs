namespace advent2021.Models;

using System.Collections;
public record Day16BitDispenser(string rawInput)
{
    private Dictionary<char, BitArray> HexBits { get; } = new();

    private CharEnumerator inputEnumerator = rawInput.GetEnumerator();

    public int BitsGiven { get; set; } = 0;

    private Queue<bool> currentBits = new();

    public BitArray? GetBits(int howMany)
    {
        while (currentBits.Count < howMany)
        {
            if (!AddBits())
            {
                return null;
            };
        }

        var result = new BitArray(howMany);
        foreach (var index in Enumerable.Range(0, howMany))
        {
            result[index] = currentBits.Dequeue();
        }
        BitsGiven += howMany;

        return result;
    }

    private bool AddBits()
    {
        if (!inputEnumerator.MoveNext())
        {
            return false;
        }
        var c = inputEnumerator.Current;
        BitArray hexAsBits;
        if (HexBits.ContainsKey(c))
        {
            hexAsBits = HexBits[c];
        }
        else
        {
            hexAsBits = c.HexToBits();
            HexBits[c] = hexAsBits;
        }
        foreach (bool newBit in hexAsBits)
        {
            currentBits.Enqueue(newBit);
        }
        return true;
    }

}