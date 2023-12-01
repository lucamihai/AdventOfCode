namespace AdventOfCode.Year2022.Day03;

public static class Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Year2022", "Day03", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        HandleFirstPart(inputLines);
        HandleSecondPart(inputLines);
    }

    private static void HandleFirstPart(string[] inputLines)
    {
        var allCommonCharacters = new List<char>();

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
        Console.WriteLine($"Part1: Score is {score}");
    }

    private static void HandleSecondPart(string[] inputLines)
    {
        var score = 0;

        for (var lineIndex = 0; lineIndex < inputLines.Length; lineIndex+= 3)
        {
            var groupCommonCharacters = new HashSet<char>();
            groupCommonCharacters.UnionWith(inputLines[lineIndex]);

            for (var groupLineIndex = lineIndex; groupLineIndex < lineIndex + 3; groupLineIndex++)
            {
                var inputLine = inputLines[groupLineIndex];
                var characterCount = inputLine.Length;

                var alreadyEncounteredCharacters = new HashSet<char>();

                for (var characterIndex = 0; characterIndex < characterCount; characterIndex++)
                {
                    alreadyEncounteredCharacters.Add(inputLine[characterIndex]);
                }

                groupCommonCharacters.IntersectWith(alreadyEncounteredCharacters);
            }

            if (groupCommonCharacters.Count != 1)
            {
                throw new InvalidOperationException($"Should have found 1 common element, found {groupCommonCharacters.Count} instead for group starting at index {lineIndex}");
            }

            var badge = groupCommonCharacters.First();
            score += GetCharacterPriority(badge);
        }
        
        Console.WriteLine($"Part2: Score is {score}");
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