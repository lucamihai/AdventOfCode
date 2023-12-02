namespace AdventOfCode.Year2023.Day01;

public static class Solution
{
    private static readonly Dictionary<string, int> DigitDictionary;
    private static readonly HashSet<string> DigitStrings;

    static Solution()
    {
        DigitDictionary = new Dictionary<string, int>
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },

            { "0", 0 },
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 }
        };

        DigitStrings = DigitDictionary.Keys.ToHashSet();
    }

    public static void Solve()
    {
        var calibrationValues = GetCalibrationValues();

        var sum = calibrationValues.Sum();
        Console.WriteLine($"The sum of the calibration values is: {sum}.");
    }

    private static List<int> GetCalibrationValues()
    {
        var path = Path.Combine("Year2023", "Day01", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        var values = new List<int>();

        foreach (var inputLine in inputLines)
        {
            if (inputLine == Environment.NewLine || string.IsNullOrEmpty(inputLine))
            {
                continue;
            }

            var firstDigitString = string.Empty;
            var firstDigitIndex = int.MaxValue;

            var lastDigitString = string.Empty;
            var lastDigitIndex = int.MinValue;

            foreach (var digitString in DigitStrings)
            {
                var firstIndexOf = inputLine.IndexOf(digitString);
                if (firstIndexOf != -1 && firstIndexOf < firstDigitIndex)
                {
                    firstDigitString = digitString;
                    firstDigitIndex = firstIndexOf;
                }

                var lastIndexOf = inputLine.LastIndexOf(digitString);
                if (lastIndexOf != -1 && lastIndexOf > lastDigitIndex)
                {
                    lastDigitString = digitString;
                    lastDigitIndex = lastIndexOf;
                }
            }

            var firstDigit = DigitDictionary[firstDigitString];
            var lastDigit = DigitDictionary[lastDigitString];
            var value = firstDigit * 10 + lastDigit;
            values.Add(value);
        }

        return values;
    }
}