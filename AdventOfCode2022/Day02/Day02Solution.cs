namespace AdventOfCode2022.Day02;

public static class Day02Solution
{
    private static readonly Dictionary<char, int> SymbolScores = new Dictionary<char, int>
    {
        {'A', 1}, // Rock
        {'B', 2}, // Paper
        {'C', 3}, // Scissors

        {'X', 1}, // Rock
        {'Y', 2}, // Paper
        {'Z', 3}  // Scissors
    };

    private static readonly Dictionary<char, char> SymbolDefeats = new Dictionary<char, char>
    {
        {'A', 'Z'}, // Rock > Scissors
        {'B', 'X'}, // Paper > Rock
        {'C', 'Y'}, // Scissors > Paper

        {'X', 'C'}, // Rock > Scissors
        {'Y', 'A'}, // Paper > Rock
        {'Z', 'B'}  // Scissors > Paper
    };

    private static readonly Dictionary<char, char> SymbolEquals = new Dictionary<char, char>
    {
        {'A', 'X'}, // Rock == Rock
        {'B', 'Y'}, // Paper == Paper
        {'C', 'Z'}  // Scissors == Scissors
    };

    public static void Solve()
    {
        var path = Path.Combine("Day02", "Day02Input.txt");
        var inputLines = File.ReadAllLines(path);

        var score = 0;

        foreach (var inputLine in inputLines)
        {
            var tokens = inputLine.Split();

            var opponentChoice = tokens[0][0];
            var playerChoice = tokens[1][0];
            
            var playerScore = SymbolScores[playerChoice];

            if (PlayerWon(opponentChoice, playerChoice))
            {
                score += 6 + playerScore;
            }
            else if (PlayerLost(opponentChoice, playerChoice))
            {
                score += 0 + playerScore;
            }
            else if (IsDraw(opponentChoice, playerChoice))
            {
                score += 3 + playerScore;
            }
            else
            {
                throw new InvalidOperationException("Wtf?");
            }
        }

        Console.WriteLine($"Your score is {score}.");
    }

    private static bool PlayerWon(char opponentChoice, char playerChoice)
    {
        return SymbolDefeats[playerChoice] == opponentChoice;
    }

    private static bool PlayerLost(char opponentChoice, char playerChoice)
    {
        return SymbolDefeats[opponentChoice] == playerChoice;
    }

    private static bool IsDraw(char opponentChoice, char playerChoice)
    {
        return SymbolEquals[opponentChoice] == playerChoice;
    }

    private static List<int> GetValues()
    {
        var path = Path.Combine("Day02", "Day02Input.txt");
        var inputLines = File.ReadAllLines(path); 
        var currentValue = 0;

        var values = new List<int>();

        foreach (var inputLine in inputLines)
        {
            if (inputLine == Environment.NewLine || string.IsNullOrEmpty(inputLine))
            {
                values.Add(currentValue);

                currentValue = 0;
            }
            else
            {
                var value = int.Parse(inputLine);
                currentValue += value;
            }
        }

        return values;
    }
}