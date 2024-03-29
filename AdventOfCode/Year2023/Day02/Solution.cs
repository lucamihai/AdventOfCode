﻿using AdventOfCode.Year2023.Day02.Entities;

namespace AdventOfCode.Year2023.Day02;

public static class Solution
{
    private static readonly HashSet<string> Colors;
    private static readonly Dictionary<string, int> AllowedColorCubesCounts;

    static Solution()
    {
        AllowedColorCubesCounts = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        Colors = AllowedColorCubesCounts.Keys.ToHashSet();
    }

    public static void Solve()
    {
        var games = GetGames();
        var possibleGames = GetPossibleGames(games);

        var sumOfIdsOfPossibleGames = possibleGames.Select(x => x.Id).Sum();
        Console.WriteLine($"The sum of the IDs of possible games is: {sumOfIdsOfPossibleGames}.");

        games.ForEach(x => x.ComputeMinimumCubesRequiredPerColor(Colors));
        var sumOfGamesSetPower = games.Select(GetGameSetPower).Sum();
        Console.WriteLine($"The sum of the games set power is: {sumOfGamesSetPower}.");
    }

    private static List<GameDetails> GetGames()
    {
        var path = Path.Combine("Year2023", "Day02", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        var games = new List<GameDetails>();

        foreach (var inputLine in inputLines)
        {
            var tokens = inputLine.Split(":");
            var game = new GameDetails { Id = int.Parse(tokens[0].Trim().Split().Last()) };
            var gameRoundTokens = tokens[1].Split(";");

            foreach (var gameRoundToken in gameRoundTokens)
            {
                var colorTokens = gameRoundToken.Split(",");
                var currentRound = new RoundDetails();
                game.Rounds.Add(currentRound);

                foreach (var colorToken in colorTokens)
                {
                    var temp = colorToken.Trim().Split();
                    var count = int.Parse(temp[0]);
                    var color = temp[1];

                    currentRound.ColorCubesCount.Add(color, count);
                }
            }

            games.Add(game);
        }

        return games;
    }

    private static IEnumerable<GameDetails> GetPossibleGames(IEnumerable<GameDetails> games)
    {
        return games.Where(IsGamePossible);
    }

    private static int GetGameSetPower(GameDetails game)
    {
        var setPower = 1;

        foreach (var count in game.MinimumCubesRequiredPerColor.Values)
        {
            setPower *= count;
        }

        return setPower;
    }

    private static bool IsGamePossible(GameDetails game)
    {
        foreach (var round in game.Rounds)
        {
            foreach (var color in Colors)
            {
                if (!round.ColorCubesCount.TryGetValue(color, out var count))
                {
                    continue;
                }

                if (count > AllowedColorCubesCounts[color])
                {
                    return false;
                }
            }
        }

        return true;
    }
}