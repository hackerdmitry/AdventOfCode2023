using System.Linq;

namespace AdventOfCode.Sample;

public static class Solution
{
    public static string FirstPart(string[] lines)
    {
        return lines[0];
    }

    public static string SecondPart(string[] lines)
    {
        return new string(lines[0].Reverse().ToArray());
    }
}
