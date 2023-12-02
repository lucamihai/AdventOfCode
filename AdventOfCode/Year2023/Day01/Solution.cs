namespace AdventOfCode.Year2023.Day01;

public static class Solution
{
    private static readonly HashSet<char> Digits = new HashSet<char>{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

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

            var foundDigits = inputLine.Where(Digits.Contains).ToList();

            if (foundDigits.Count < 1)
            {
                throw new Exception($"Could not find any digits in line '{inputLine}'");
            }

            var firstDigit = foundDigits.First() - '0';
            var lastDigit = foundDigits.Last() - '0';
            values.Add(firstDigit * 10 + lastDigit);
        }

        return values;
    }
}