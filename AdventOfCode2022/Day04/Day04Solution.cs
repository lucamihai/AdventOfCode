namespace AdventOfCode2022.Day04;

public static class Day04Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Day04", "Day04Input.txt");
        var inputLines = File.ReadAllLines(path);

        var fullyContainedRanges = 0;

        foreach (var inputLine in inputLines)
        {
            var assignmentPair = inputLine.Split(',');
            var firstAssignmentRange = assignmentPair[0].Split('-').Select(int.Parse).ToList();
            var secondAssignmentRange = assignmentPair[1].Split('-').Select(int.Parse).ToList();

            if (OneRangeIncludesTheOther(firstAssignmentRange, secondAssignmentRange))
            {
                fullyContainedRanges++;
            }
        }

        Console.WriteLine($"Part1: Score is {fullyContainedRanges}");
    }

    private static bool OneRangeIncludesTheOther(List<int> firstAssignmentRange, List<int> secondAssignmentRange)
    {
        var firstLower = firstAssignmentRange[0];
        var firstHigher = firstAssignmentRange[1];
        var secondLower = secondAssignmentRange[0];
        var secondHigher = secondAssignmentRange[1];

        if (secondLower >= firstLower && secondHigher <= firstHigher)
        {
            return true;
        }

        if (firstLower >= secondLower && firstHigher <= secondHigher)
        {
            return true;
        }

        return false;
    }
}