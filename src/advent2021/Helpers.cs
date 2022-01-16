namespace advent2021;

using System.Collections;
public static class Helpers
{
    public static string GetControllerName(this IHttpContextAccessor hca)
    {
        return hca!.HttpContext!.Request.RouteValues["controller"]!.ToString()!.ToLowerInvariant();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns>BitArray with high bit at index 0</returns>
    public static BitArray HexToBits(this char c)
    {
        var result = new BitArray(4);

        // https://stackoverflow.com/a/6617360
        var bitChars = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
        for (var i = 0; i < bitChars.Count(); i++)
        {
            result[i] = bitChars[i] == '1';
        }

        return result;
    }


    /// <summary>
    /// Input low bit must be at index 0
    /// </summary>
    /// <param name="inputBits"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int BitsToNumber(this BitArray inputBits)
    {
        if (inputBits.Length > 32)
        {
            throw new ArgumentOutOfRangeException("Max 32 bits");
        }
        var toInt = new int[1];
        inputBits.CopyTo(toInt, 0);
        return toInt[0];
    }

    /// <summary>
    /// Input low bit must be at index 0
    /// </summary>
    /// <param name="inputBits"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static long BitsToLong(this BitArray inputBits)
    {
        if (inputBits.Length > 64)
        {
            throw new ArgumentOutOfRangeException("Max 64 bits");
        }
        // https://stackoverflow.com/a/29446190
        var array = new byte[8];
        inputBits.CopyTo(array, 0);
        return BitConverter.ToInt64(array, 0);
    }

    /// <summary>
    /// Input high bit must be at index 0
    /// </summary>
    /// <param name="inputBits"></param>
    /// <returns></returns>
    public static int BitsToNumberAfterReverse(this BitArray inputBits)
    {
        var bitArr = new bool[inputBits.Length];
        inputBits.CopyTo(bitArr, 0);
        bitArr = bitArr.Reverse().ToArray();
        var newBits = new BitArray(bitArr);
        return newBits.BitsToNumber();
    }

    /// <summary>
    /// Input high bit must be at index 0
    /// </summary>
    /// <param name="inputBits"></param>
    /// <returns></returns>
    public static long BitsToLong(this Queue<bool> inputBits)
    {
        if (inputBits.Count == 0)
        {
            return 0;
        }
        var bits = new BitArray(inputBits.Count);
        foreach (var index in Enumerable.Range(0, inputBits.Count).Reverse())
        {
            bits[index] = inputBits.Dequeue();
        }
        return bits.BitsToLong();
    }
}