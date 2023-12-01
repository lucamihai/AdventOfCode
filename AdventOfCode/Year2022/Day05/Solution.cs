using System.Text;
using AdventOfCode.Year2022.Day05.Entities;

namespace AdventOfCode.Year2022.Day05;

public static class Solution
{
    public static void Solve()
    {
        var path = Path.Combine("Year2022", "Day05", "Input.txt");
        var inputLines = File.ReadAllLines(path).ToList();
        ParseInput(inputLines, out var itemStacks, out var moves);

        HandleFirstPart(itemStacks, moves);
        HandleSecondPart(itemStacks, moves);
    }

    private static void ParseInput(List<string> inputLines, out List<Stack<char>> itemStacks, out List<Move> moves)
    {
        itemStacks = new List<Stack<char>>();
        moves = new List<Move>();
        
        var indexOfLastLineWithStackInfo = inputLines.FindLastIndex(x => x.StartsWith("["));
        var indexOfFirstMoveInfo = inputLines.FindIndex(x => x.StartsWith("move"));

        var stackCount = inputLines[indexOfLastLineWithStackInfo].Length / 4;
        for (var i = 0; i < stackCount + 1; i++)
        {
            itemStacks.Add(new Stack<char>());
        }

        for (var i = indexOfLastLineWithStackInfo; i >= 0; i--)
        {
            var inputLine = inputLines[i];

            for (var stackIndex = 0; stackIndex < stackCount + 1; stackIndex++)
            {
                var charIndex = 1 + stackIndex * 4;

                if (inputLine[charIndex] != ' ')
                {
                    itemStacks[stackIndex].Push(inputLine[charIndex]);
                }
            }
        }

        for (var i = indexOfFirstMoveInfo; i < inputLines.Count; i++)
        {
            var inputLine = inputLines[i];
            var tokens = inputLine.Split();

            // move {quantity} from {origin} to {destination}
            moves.Add(new Move
            {
                Quantity = int.Parse(tokens[1]),
                Origin = int.Parse(tokens[3]),
                Destination = int.Parse(tokens[5])
            });
        }
    }

    private static void HandleFirstPart(List<Stack<char>> itemStacks, List<Move> moves)
    {
        var itemStacksCopy = CopyItemStacks(itemStacks);

        foreach (var move in moves)
        {
            PerformMoveFirstPart(itemStacksCopy, move);
        }

        Console.WriteLine($"Part1 result: {GetResult(itemStacksCopy)}");
    }

    private static void HandleSecondPart(List<Stack<char>> itemStacks, List<Move> moves)
    {
        var itemStacksCopy = CopyItemStacks(itemStacks);

        foreach (var move in moves)
        {
            PerformMoveSecondPart(itemStacksCopy, move);
        }

        Console.WriteLine($"Part2 result: {GetResult(itemStacksCopy)}");
    }

    private static void PerformMoveFirstPart(List<Stack<char>> itemStacks, Move move)
    {
        var stackOrigin = itemStacks[move.Origin - 1];
        var stackDestination = itemStacks[move.Destination - 1];

        for (var i = 0; i < move.Quantity; i++)
        {
            stackDestination.Push(stackOrigin.Pop());
        }
    }

    private static void PerformMoveSecondPart(List<Stack<char>> itemStacks, Move move)
    {
        var stackOrigin = itemStacks[move.Origin - 1];
        var stackDestination = itemStacks[move.Destination - 1];

        var itemsToMove = new List<char>();

        for (var i = 0; i < move.Quantity; i++)
        {
            itemsToMove.Add(stackOrigin.Pop());
        }

        for (var index = itemsToMove.Count - 1; index >= 0; index--)
        {
            var itemToMove = itemsToMove[index];
            stackDestination.Push(itemToMove);
        }
    }

    private static List<Stack<char>> CopyItemStacks(List<Stack<char>> itemStacks)
    {
        var itemStacksCopy = new List<Stack<char>>();

        foreach (var itemStack in itemStacks)
        {
            itemStacksCopy.Add(new Stack<char>(itemStack.AsEnumerable().Reverse()));
        }

        return itemStacksCopy;
    }

    private static string GetResult(List<Stack<char>> itemStacks)
    {
        var stringBuilder = new StringBuilder();

        foreach (var itemStack in itemStacks)
        {
            stringBuilder.Append(itemStack.Peek());
        }

        return stringBuilder.ToString();
    }

    // Use for debug and/or fun
    private static void DisplayItemStacks(List<Stack<char>> itemStacks)
    {
        var stackMaxCount = itemStacks
            .Select(x => x.Count)
            .Max();

        var itemStackCopies = itemStacks.Select(x => new Queue<char>(x)).ToList();

        Console.WriteLine("-------------------------------------------");

        for (var i = 0; i < stackMaxCount; i++)
        {
            foreach (var itemStack in itemStackCopies)
            {
                var valueToDisplay = itemStack.TryDequeue(out var character)
                    ? character
                    : ' ';

                Console.Write($" [{valueToDisplay}]");
            }

            Console.WriteLine();
        }

        Console.WriteLine("-------------------------------------------");
    }
}