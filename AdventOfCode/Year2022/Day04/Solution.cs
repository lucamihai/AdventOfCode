﻿namespace AdventOfCode.Year2022.Day04;

public static class Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Year2022", "Day04", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        var fullyContainedRanges = 0;
        var overlappedRanges = 0;

        foreach (var inputLine in inputLines)
        {
            var assignmentPair = inputLine.Split(',');
            var firstAssignmentRange = assignmentPair[0].Split('-').Select(int.Parse).ToList();
            var secondAssignmentRange = assignmentPair[1].Split('-').Select(int.Parse).ToList();

            if (OneRangeIncludesTheOther(firstAssignmentRange, secondAssignmentRange))
            {
                fullyContainedRanges++;
            }

            if (RangesOverlap(firstAssignmentRange, secondAssignmentRange))
            {
                overlappedRanges++;
            }
        }

        Console.WriteLine($"Part1: Score is {fullyContainedRanges}");
        Console.WriteLine($"Part2: Score is {overlappedRanges}");
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

    private static bool RangesOverlap(List<int> firstAssignmentRange, List<int> secondAssignmentRange)
    {
        if (OneRangeIncludesTheOther(firstAssignmentRange, secondAssignmentRange))
        {
            return true;
        }

        var firstLower = firstAssignmentRange[0];
        var firstHigher = firstAssignmentRange[1];
        var secondLower = secondAssignmentRange[0];
        var secondHigher = secondAssignmentRange[1];

        if (firstHigher < secondLower || secondHigher < firstLower)
        {
            return false;
        }

        if (firstLower > secondHigher || secondLower > firstHigher)
        {
            return false;
        }

        return true;
    }
}