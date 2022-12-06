namespace AdventOfCode2022.Day06;

public static class Day06Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Day06", "Day06Input.txt");
        var input = File.ReadAllText(path);

        for (var i = 0; i < input.Length - 4; i++)
        {
            if (Next4CharactersAreUnique(input, i))
            {
                Console.WriteLine($"Starting position: {i + 4}");
                break;
            }
        }
    }

    private static bool Next4CharactersAreUnique(string text, int startIndex)
    {
        var characterHashset = new HashSet<char>();

        for (var i = startIndex; i < startIndex + 4 && i < text.Length; i++)
        {
            characterHashset.Add(text[i]);
        }

        return characterHashset.Count == 4;
    }
}