using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._4_Day;

public static class Solution
{
    public static string FirstPart(string[] lines)
    {
        var result = 0;

        foreach (var line in lines)
        {
            var card = Card.Parse(line);
            var coincidenceResult = 0;
            foreach (var _ in card.WinNumbers.Intersect(card.OwnNumbers))
            {
                coincidenceResult = coincidenceResult == 0 ? 1 : coincidenceResult * 2;
            }
            result += coincidenceResult;
        }

        return result.ToString();
    }

    public static string SecondPart(string[] lines)
    {
        var cards = lines.Select(Card.Parse).ToArray();
        var cardsCount = cards.Select(_ => 1).ToArray();

        for (var i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            var coincidenceCount = card.WinNumbers.Intersect(card.OwnNumbers).Count();
            for (var j = i + 1; j < cards.Length && j < i + coincidenceCount + 1; j++)
            {
                cardsCount[j] += cardsCount[i];
            }
        }

        return cardsCount.Sum().ToString();
    }

    private class Card
    {
        public int Number { get; set; }
        public int[] WinNumbers { get; set; }
        public int[] OwnNumbers { get; set; }

        public static Card Parse(string line)
        {
            var number = int.Parse(Regex.Match(line, "(?<=Card\\s+)\\d+").Value);
            var winNumbers = Regex.Matches(line, "(?<=:.+)\\d+(?=.+\\|)").Select(x => int.Parse(x.Value)).ToArray();
            var ownNumbers = Regex.Matches(line, "(?<=\\|.+)\\d+").Select(x => int.Parse(x.Value)).ToArray();

            return new Card
            {
                Number = number,
                WinNumbers = winNumbers,
                OwnNumbers = ownNumbers,
            };
        }
    }
}
