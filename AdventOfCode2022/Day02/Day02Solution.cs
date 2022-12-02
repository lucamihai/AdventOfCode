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

    private static readonly Dictionary<char, char> SymbolDefeatedBy = new Dictionary<char, char>
    {
        {'A', 'Y'}, // Rock < Paper
        {'B', 'Z'}, // Paper < Scissors
        {'C', 'X'}, // Scissors < Rock

        {'X', 'B'}, // Rock < Paper
        {'Y', 'C'}, // Paper < Scissors
        {'Z', 'A'}  // Scissors < Rock
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

        var scoreFirstPart = 0;
        var scoreSecondPart = 0;

        foreach (var inputLine in inputLines)
        {
            var tokens = inputLine.Split();

            var opponentChoice = tokens[0][0];
            var playerChoice = tokens[1][0];
            
            HandleFirstPart(opponentChoice, playerChoice, ref scoreFirstPart);
            HandleSecondPart(opponentChoice, playerChoice, ref scoreSecondPart);
        }

        Console.WriteLine($"Part1: Your score is {scoreFirstPart}.");
        Console.WriteLine($"Part2: Your score is {scoreSecondPart}.");
    }

    private static void HandleFirstPart(char opponentChoice, char playerChoice, ref int score)
    {
        HandleRound(opponentChoice, playerChoice, ref score);
    }

    private static void HandleSecondPart(char opponentChoice, char playerChoice, ref int score)
    {
        var actualPlayerChoice = ' ';

        if (PlayerNeedsToWin(playerChoice))
        {
            actualPlayerChoice = SymbolDefeatedBy[opponentChoice];
        }
        else if (PlayerNeedsToLose(playerChoice))
        {
            actualPlayerChoice = SymbolDefeats[opponentChoice];
        }
        else if (PlayerNeedsToEndUpInDraw(playerChoice))
        {
            actualPlayerChoice = SymbolEquals[opponentChoice];
        }

        HandleRound(opponentChoice, actualPlayerChoice, ref score);
    }

    private static void HandleRound(char opponentChoice, char playerChoice, ref int score)
    {
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

    private static bool PlayerNeedsToWin(char playerChoice)
    {
        return playerChoice == 'Z';
    }

    private static bool PlayerNeedsToLose(char playerChoice)
    {
        return playerChoice == 'X';
    }

    private static bool PlayerNeedsToEndUpInDraw(char playerChoice)
    {
        return playerChoice == 'Y';
    }
}