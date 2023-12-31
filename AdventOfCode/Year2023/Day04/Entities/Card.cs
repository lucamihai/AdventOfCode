namespace AdventOfCode.Year2023.Day04.Entities;

public class Card
{
    public HashSet<int> WinningNumbers { get; set; } = new();
    public HashSet<int> PlayedNumbers { get; set; } = new();
}