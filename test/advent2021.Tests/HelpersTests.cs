namespace advent2021.Tests;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class HelpersTests
{

    [Theory]
    [InlineData(new bool[4] { false, false, false, false }, 0)]
    [InlineData(new bool[4] { true, false, false, false }, 1)]
    [InlineData(new bool[4] { true, false, true, false }, 5)]
    [InlineData(new bool[4] { false, false, false, true }, 8)]
    [InlineData(new bool[4] { false, true, false, true }, 10)]
    [InlineData(new bool[8] { false, false, false, true, false, false, false, true }, 136)]
    [InlineData(new bool[16] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true }, 32768)]
    public void TestBitsToNumber(bool[] input, int output)
    {
        Assert.Equal(output, new BitArray(input).BitsToNumber());
    }

    [Theory]
    // 2^34 + 2^33
    [InlineData(new bool[35] {
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, true, true}, 25769803776)]
    public void TestBitsToLong(bool[] input, long output)
    {
        Assert.Equal(output, new BitArray(input).BitsToLong());
    }

    [Theory]
    [InlineData(new bool[4] { false, false, false, false }, 0)]
    [InlineData(new bool[4] { false, false, false, true }, 1)]
    [InlineData(new bool[4] { false, true, false, true }, 5)]
    [InlineData(new bool[4] { true, false, false, false }, 8)]
    [InlineData(new bool[4] { true, false, true, false }, 10)]
    [InlineData(new bool[8] { true, false, false, false, true, false, false, false }, 136)]
    [InlineData(new bool[16] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }, 32768)]
    public void TestBitsToNumberAfterReverse(bool[] input, int output)
    {
        Assert.Equal(output, new BitArray(input).BitsToNumberAfterReverse());
    }

    [Theory]
    [InlineData(new bool[4] { false, false, false, false }, 0)]
    [InlineData(new bool[4] { false, false, false, true }, 1)]
    [InlineData(new bool[4] { false, true, false, true }, 5)]
    [InlineData(new bool[4] { true, false, false, false }, 8)]
    [InlineData(new bool[4] { true, false, true, false }, 10)]
    [InlineData(new bool[8] { true, false, false, false, true, false, false, false }, 136)]
    [InlineData(new bool[16] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }, 32768)]
    // 2^34 + 2^33
    [InlineData(new bool[35] {
                            true, true, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false,
                            false, false, false, false, false}, 25769803776)]
    public void TestQBitsToLong(bool[] input, long output)
    {
        Assert.Equal(output, new Queue<bool>(input.ToList()).BitsToLong());
    }


    [Theory]
    [InlineData('0', new bool[4] { false, false, false, false })]
    [InlineData('1', new bool[4] { false, false, false, true })]
    [InlineData('A', new bool[4] { true, false, true, false })]
    [InlineData('F', new bool[4] { true, true, true, true })]
    public void TestHexToBits(char input, bool[] output)
    {
        Assert.Equal(0, input.HexToBits().Xor(new BitArray(output)).BitsToNumber());
    }

}