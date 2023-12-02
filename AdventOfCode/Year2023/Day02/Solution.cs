using AdventOfCode.Year2023.Day02.Entities;

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

        var sum = possibleGames.Select(x => x.Id).Sum();
        Console.WriteLine($"The sum of the IDs of possible games is: {sum}.");
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

            var roundCount = 1;

            foreach (var gameRoundToken in gameRoundTokens)
            {
                var colorTokens = gameRoundToken.Split(",");
                var currentRound = new Dictionary<string, int>();
                game.ColorCubesCountPerRound.Add(roundCount++, currentRound);

                foreach (var colorToken in colorTokens)
                {
                    var temp = colorToken.Trim().Split();
                    var count = int.Parse(temp[0]);
                    var color = temp[1];

                    currentRound.Add(color, count);
                }
            }

            games.Add(game);
        }

        return games;
    }

    private static List<GameDetails> GetPossibleGames(List<GameDetails> games)
    {
        return games
            .Where(IsGamePossible)
            .ToList();
    }

    private static bool IsGamePossible(GameDetails game)
    {
        foreach (var roundColorCubesCount in game.ColorCubesCountPerRound.Values)
        {
            foreach (var color in Colors)
            {
                if (!roundColorCubesCount.ContainsKey(color))
                {
                    continue;
                }

                if (roundColorCubesCount[color] > AllowedColorCubesCounts[color])
                {
                    return false;
                }
            }
        }

        return true;
    }
}