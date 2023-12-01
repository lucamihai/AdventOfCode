namespace AdventOfCode.Year2022.Day01;

public static class Solution
{
    public static void Solve()
    {
        var values = GetValues();
        values = values.OrderByDescending(x => x).ToList();

        var maxValue = values.First();
        Console.WriteLine($"The elf with the most amount of calories has {maxValue} calories.");

        var sumOfTop3MaxValues = values[0] + values[1] + values[2];
        Console.WriteLine($"The top 3 elves with the most amount of calories have {sumOfTop3MaxValues} calories in total.");
    }

    private static List<int> GetValues()
    {
        var path = Path.Combine("Year2022", "Day01", "Input.txt");
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