namespace AdventOfCode2022.Day03;

public static class Day03Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Day03", "Day03Input.txt");
        var inputLines = File.ReadAllLines(path);

        var allCommonCharacters = new List<char>();
        var highestScore = int.MinValue;
        
        foreach (var inputLine in inputLines)
        {
            var characterCount = inputLine.Length;
            var halfCount = characterCount / 2;

            var alreadyEncounteredCharacters = new HashSet<char>();
            var commonCharacters = new HashSet<char>();

            for (var i = 0; i < halfCount; i++)
            {
                var character = inputLine[i];
                alreadyEncounteredCharacters.Add(character);
            }

            for (var i = halfCount; i < characterCount; i++)
            {
                var character = inputLine[i];

                if (alreadyEncounteredCharacters.Contains(character))
                {
                    commonCharacters.Add(character);
                }
            }

            allCommonCharacters.AddRange(commonCharacters);
        }

        var score = allCommonCharacters.Select(GetCharacterPriority).Sum();

        if (score > highestScore)
        {
            highestScore = score;
        }
        Console.WriteLine($"Highest score found: {highestScore}");
    }

    private static int GetCharacterPriority(char character)
    {
        if (character is >= 'a' and <= 'z')
        {
            return character - 'a' + 1;
        }
        if (character is >= 'A' and <= 'Z')
        {
            return character - 'A' + 27;
        }
        
        throw new ArgumentException("Non-letter arguments not supported");
    }
}