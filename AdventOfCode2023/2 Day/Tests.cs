using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace AdventOfCode._2_Day;

[TestFixture(TestName = TestName)]
public class Tests
{
    private const string TestName = "2 Day";

    [Timeout(1000)]
    [TestCaseSource(nameof(FirstPartInput))]
    public void FirstPartTest(string[] input, string output)
    {
        var actual = Solution.FirstPart(input);
        Assert.AreEqual(output, actual);
    }

    [Timeout(1000)]
    [TestCaseSource(nameof(SecondPartInput))]
    public void SecondPartTest(string[] input, string output)
    {
        var actual = Solution.SecondPart(input);
        Assert.AreEqual(output, actual);
    }

    private static IEnumerable FirstPartInput()
    {
        return Input(new Dictionary<string, string>
        {
            { "testInput1.txt", "8" },
            { "input.txt", "2593" },
        });
    }

    private static IEnumerable SecondPartInput()
    {
        return Input(new Dictionary<string, string>
        {
            { "testInput2.txt", "2286" },
            { "input.txt", "54699" },
        });
    }

    private static IEnumerable Input(Dictionary<string, string> fileNames)
    {
        foreach (var (fileName, answer) in fileNames)
        {
            using var sr = new StreamReader(Path.Combine(TestName, "Inputs", fileName));
            var lines = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            yield return new object[] { lines, answer };
        }
    }
}
