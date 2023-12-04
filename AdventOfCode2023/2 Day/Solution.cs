using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2_Day;

public static class Solution
{
    public static string FirstPart(string[] lines)
    {
        var result = 0;

        var games = ParseInput(lines);
        var numGame = 0;
        foreach (var game in games)
        {
            numGame++;
            foreach (var gameRound in game.Rounds)
            {
                if (gameRound.RedCubes > 12 ||
                    gameRound.GreenCubes > 13 ||
                    gameRound.BlueCubes > 14)
                {
                    goto doesntFit;
                }
            }
            result += numGame;
            doesntFit: ;
        }

        return result.ToString();
    }

    public static string SecondPart(string[] lines)
    {
        var result = 0;

        var games = ParseInput(lines);
        var numGame = 0;
        foreach (var game in games)
        {
            var maxRedCubes = 0;
            var maxGreenCubes = 0;
            var maxBlueCubes = 0;
            foreach (var gameRound in game.Rounds)
            {
                maxRedCubes = Math.Max(maxRedCubes, gameRound.RedCubes);
                maxGreenCubes = Math.Max(maxGreenCubes, gameRound.GreenCubes);
                maxBlueCubes = Math.Max(maxBlueCubes, gameRound.BlueCubes);
            }

            var product = maxRedCubes * maxGreenCubes * maxBlueCubes;
            if (result == 0)
            {
                result = product;
            }
            else
            {
                result += product;
            }
        }

        return result.ToString();
    }

    private static List<Game> ParseInput(string[] lines)
    {
        var result = new List<Game>();

        foreach (var line in lines)
        {
            var game = new Game();
            result.Add(game);

            var kitOfCubes = line.Split(';');
            foreach (var kitOfCube in kitOfCubes)
            {
                var redCubesRegex = Regex.Match(kitOfCube, "\\d+(?= red)");
                var greenCubesRegex = Regex.Match(kitOfCube, "\\d+(?= green)");
                var blueCubesRegex = Regex.Match(kitOfCube, "\\d+(?= blue)");

                var redCubes = redCubesRegex.Success ? int.Parse(redCubesRegex.Value) : 0;
                var greenCubes = greenCubesRegex.Success ? int.Parse(greenCubesRegex.Value) : 0;
                var blueCubes = blueCubesRegex.Success ? int.Parse(blueCubesRegex.Value) : 0;

                game.Rounds.Add(new Round
                {
                    RedCubes = redCubes,
                    GreenCubes = greenCubes,
                    BlueCubes = blueCubes,
                });
            }
        }

        return result;
    }

    private class Game
    {
        public List<Round> Rounds { get; } = new();
    }

    private class Round
    {
        public int BlueCubes { get; set; }
        public int RedCubes { get; set; }
        public int GreenCubes { get; set; }
    }
}
