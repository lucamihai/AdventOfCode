namespace AdventOfCode2022.Day06;

public static class Day06Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Day06", "Day06Input.txt");
        var input = File.ReadAllText(path);

        var startPosition = FindStartPosition(input);
        Console.WriteLine($"Start position: {startPosition}");

        var startOfMessagePosition = FindStartOfMessagePosition(input);
        Console.WriteLine($"Start of message position: {startOfMessagePosition}");
    }

    private static int FindStartPosition(string text)
    {
        const int characterWindow = 4;

        for (var i = 0; i < text.Length - characterWindow; i++)
        {
            if (NextNCharactersAreUnique(text, i, characterWindow))
            {
                return i + characterWindow;
            }
        }

        throw new ArgumentException("No start position has been found");
    }

    private static int FindStartOfMessagePosition(string text)
    {
        const int characterWindow = 14;

        for (var i = 0; i < text.Length - characterWindow; i++)
        {
            if (NextNCharactersAreUnique(text, i, characterWindow))
            {
                return i + characterWindow;
            }
        }

        throw new ArgumentException("No message start position has been found");
    }

    private static bool NextNCharactersAreUnique(string text, int startIndex, int n)
    {
        var characterHashset = new HashSet<char>();

        for (var i = startIndex; i < startIndex + n && i < text.Length; i++)
        {
            characterHashset.Add(text[i]);
        }

        return characterHashset.Count == n;
    }
}