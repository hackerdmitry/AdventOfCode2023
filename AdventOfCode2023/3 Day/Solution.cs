using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._3_Day;

public static class Solution
{
    public static string FirstPart(string[] lines)
    {
        var result = 0;

        var map = ParseMap(lines);
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                if (char.IsDigit(map[y, x]))
                {
                    var number = map[y, x] - '0';
                    var length = 1;

                    while (++x < map.GetLength(1) && char.IsDigit(map[y, x]))
                    {
                        number = number * 10 + map[y, x] - '0';
                        length++;
                    }

                    var inEngine = false;
                    for (var y1 = Math.Max(0, y - 1); y1 <= Math.Min(map.GetLength(0) - 1, y + 1); y1++)
                    {
                        for (var x1 = Math.Max(0, x - length - 1); x1 <= Math.Min(map.GetLength(1) - 1, x); x1++)
                        {
                            if (!char.IsDigit(map[y1, x1]) && map[y1, x1] != '.')
                            {
                                inEngine = true;
                                goto exit;
                            }
                        }
                    }

                    exit: ;
                    if (inEngine)
                    {
                        result += number;
                    }
                }
            }
        }

        return result.ToString();
    }

    public static string SecondPart(string[] lines)
    {
        var gears = new Dictionary<(int, int), List<int>>();

        var map = ParseMap(lines);
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                if (char.IsDigit(map[y, x]))
                {
                    var number = map[y, x] - '0';
                    var length = 1;

                    while (++x < map.GetLength(1) && char.IsDigit(map[y, x]))
                    {
                        number = number * 10 + map[y, x] - '0';
                        length++;
                    }

                    for (var y1 = Math.Max(0, y - 1); y1 <= Math.Min(map.GetLength(0) - 1, y + 1); y1++)
                    {
                        for (var x1 = Math.Max(0, x - length - 1); x1 <= Math.Min(map.GetLength(1) - 1, x); x1++)
                        {
                            if (map[y1, x1] == '*')
                            {
                                var pos = (y1, x1);
                                if (gears.ContainsKey(pos))
                                {
                                    gears[pos].Add(number);
                                }
                                else
                                {
                                    gears[pos] = new List<int> { number };
                                }
                                goto exit;
                            }
                        }
                    }

                    exit: ;
                }
            }
        }

        var suitGears = gears.Where(x => x.Value.Count == 2).Select(x => x.Value);
        return suitGears.Sum(x => x[0] * x[1]).ToString();
    }

    private static char[,] ParseMap(string[] lines)
    {
        var height = lines.Length;
        var width = lines[0].Length;

        var result = new char[height, width];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                result[y, x] = lines[y][x];
            }
        }

        return result;
    }
}
