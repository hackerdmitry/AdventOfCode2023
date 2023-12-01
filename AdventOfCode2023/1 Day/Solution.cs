using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._1_Day;

public static class Solution
{
    private static readonly Dictionary<string, int> Digits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    public static string FirstPart(string[] lines)
    {
        var result = 0;

        foreach (var line in lines)
        {
            var leftDigit = line.First(char.IsDigit) - '0';
            var rightDigit = line.Last(char.IsDigit) - '0';
            result += leftDigit * 10 + rightDigit;
        }

        return result.ToString();
    }

    public static string SecondPart(string[] lines)
    {
        var result = 0;

        foreach (var line in lines)
        {
            var leftDigit = 0;
            for (var i = 0; i < line.Length; i++)
            {
                var newLeftDigit = FindDigit(line, i, Digits);
                if (newLeftDigit.HasValue)
                {
                    leftDigit = newLeftDigit.Value;
                    break;
                }
            }

            var rightDigit = 0;
            for (var i = line.Length - 1; i >= 0; i--)
            {
                var newRightDigit = FindDigit(line, i, Digits);
                if (newRightDigit.HasValue)
                {
                    rightDigit = newRightDigit.Value;
                    break;
                }
            }

            result += leftDigit * 10 + rightDigit;
        }

        return result.ToString();
    }

    private static int? FindDigit(string line, int i, Dictionary<string, int> digits)
    {
        if (char.IsDigit(line[i]))
        {
            return line[i] - '0';
        }

        foreach (var (digitString, digitInt) in digits)
        {
            if (i + digitString.Length > line.Length)
            {
                continue;
            }

            for (var j = i; j < i + digitString.Length; j++)
            {
                if (digitString[j - i] != line[j])
                {
                    goto next;
                }
            }

            return digitInt;

            next: ;
        }

        return null;
    }
}
