using AdventOfCode.Year2023.Day03.Entities;

namespace AdventOfCode.Year2023.Day03;

public static class Solution
{
    private static readonly HashSet<char> Digits = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private static readonly HashSet<char> Symbols = new() { '$', '#', '%', '*', '/', '+', '-', '=', '&', '@' };

    public static void Solve()
    {
        var (partNumbers, gearPositions) = GetPartNumbersAndGearPositions();
        var partNumbersSum = partNumbers.Select(x => x.Value).Sum();
        Console.WriteLine($"The sum of the part numbers is: {partNumbersSum}.");

        var gearRatios = GetGearRatios(partNumbers, gearPositions);
        var gearRatiosSum = gearRatios.Sum();
        Console.WriteLine($"The sum of the gear ratios is: {gearRatiosSum}.");
    }

    private static (List<PartNumber> partNumbers, List<(int row, int column)> gearPositions) GetPartNumbersAndGearPositions()
    {
        var path = Path.Combine("Year2023", "Day03", "Input.txt");
        var inputLines = File.ReadAllLines(path);

        var partNumbers = new List<PartNumber>();
        var gearPositions = new List<(int row, int column)>();

        for (int i = 0; i < inputLines.Length; i++)
        {
            var previousLine = i > 0 ? inputLines[i - 1] : null;
            var currentLine = inputLines[i];
            var nextLine = i < inputLines.Length - 1 ? inputLines[i + 1] : null;

            var (localPartNumbers, localGearPositions) = ExtractLocalPartNumbersAndGearPositions(row: i, previousLine, currentLine, nextLine);

            partNumbers.AddRange(localPartNumbers);
            gearPositions.AddRange(localGearPositions);
        }

        return (partNumbers, gearPositions);
    }

    private static (List<PartNumber> partNumbers, List<(int row, int column)> gearPositions) ExtractLocalPartNumbersAndGearPositions(int row, string previousLine, string currentLine, string nextLine)
    {
        var partNumbers = new List<PartNumber>();
        var gearPositions = new List<(int row, int column)>();

        var currentNumberString = string.Empty;
        var currentNumberPositionStart = -1;
        var currentNumberPositionEnd = -1;

        var scanAreaPreviousLine = new HashSet<int>();
        var scanAreaCurrentLine = new HashSet<int>();
        var scanAreaNextLine = new HashSet<int>();

        for (int i = 0; i < currentLine.Length; i++)
        {
            var currentCharacter = currentLine[i];

            if (IsDigit(currentCharacter))
            {
                if (currentNumberPositionStart >= 0)
                {
                    currentNumberPositionEnd = i;
                }
                else
                {
                    currentNumberPositionStart = i;
                    currentNumberPositionEnd = i;
                }

                // Add left
                if (string.IsNullOrEmpty(currentNumberString) && i > 0)
                {
                    scanAreaCurrentLine.Add(i - 1);
                }

                currentNumberString += currentCharacter;

                // Add diagonals left
                if (i > 0)
                {
                    scanAreaPreviousLine.Add(i - 1);
                    scanAreaNextLine.Add(i - 1);
                }

                // Add above & below
                scanAreaPreviousLine.Add(i);
                scanAreaNextLine.Add(i);

                // Add diagonals right
                if (i < currentLine.Length - 1)
                {
                    scanAreaPreviousLine.Add(i + 1);
                    scanAreaNextLine.Add(i + 1);
                }
            }
            else
            {
                if (IsGear(currentCharacter))
                {
                    gearPositions.Add((row: row, column: i));
                }

                // Add right
                scanAreaCurrentLine.Add(i);

                if (!string.IsNullOrEmpty(currentNumberString) && IsPartNumber(previousLine, currentLine, nextLine, scanAreaPreviousLine, scanAreaCurrentLine, scanAreaNextLine))
                {
                    partNumbers.Add(new PartNumber
                    {
                        Value = int.Parse(currentNumberString),
                        Row = row,
                        PositionStart = currentNumberPositionStart,
                        PositionEnd = currentNumberPositionEnd
                    });
                }

                currentNumberString = string.Empty;
                currentNumberPositionStart = -1;
                currentNumberPositionEnd = -1;

                scanAreaPreviousLine.Clear();
                scanAreaCurrentLine.Clear();
                scanAreaNextLine.Clear();
            }

            if (i == currentLine.Length - 1 && !string.IsNullOrEmpty(currentNumberString) && IsPartNumber(previousLine, currentLine, nextLine, scanAreaPreviousLine, scanAreaCurrentLine, scanAreaNextLine))
            {
                partNumbers.Add(new PartNumber
                {
                    Value = int.Parse(currentNumberString),
                    Row = row,
                    PositionStart = currentNumberPositionStart,
                    PositionEnd = currentNumberPositionEnd
                });
            }
        }

        return (partNumbers, gearPositions);
    }

    private static bool IsPartNumber(string previousLine, string currentLine, string nextLine, HashSet<int> scanAreaPreviousLine, HashSet<int> scanAreaCurrentLine, HashSet<int> scanAreaNextLine)
    {
        return Check(previousLine, scanAreaPreviousLine)
               || Check(currentLine, scanAreaCurrentLine)
               || Check(nextLine, scanAreaNextLine);
    }

    private static bool Check(string line, HashSet<int> scanRange)
    {
        if (string.IsNullOrEmpty(line))
        {
            return false;
        }

        foreach (var index in scanRange)
        {
            if (IsSymbol(line[index]))
            {
                return true;
            }
        }
        
        return false;
    }

    private static List<int> GetGearRatios(List<PartNumber> partNumbers, List<(int row, int column)> gearPositions)
    {
        var gearRatios = new List<int>();

        foreach (var gearPosition in gearPositions)
        {
            var neighborPartNumbers = partNumbers.Where(partNumber => PartNumberIsNeighborToGear(partNumber, gearPosition)).ToList();

            if (neighborPartNumbers.Count == 2)
            {
                gearRatios.Add(neighborPartNumbers[0].Value * neighborPartNumbers[1].Value);
            }
        }

        return gearRatios;
    }

    private static bool PartNumberIsNeighborToGear(PartNumber partNumber, (int row, int column) gearPosition)
    {
        // Check north and north diagonals
        if (partNumber.Row == gearPosition.row - 1 && gearPosition.column >= partNumber.PositionStart - 1 && gearPosition.column <= partNumber.PositionEnd + 1)
        {
            return true;
        }

        // Check south and south diagonals
        if (partNumber.Row == gearPosition.row + 1 && gearPosition.column >= partNumber.PositionStart - 1 && gearPosition.column <= partNumber.PositionEnd + 1)
        {
            return true;
        }

        // Check left and right
        if (partNumber.Row == gearPosition.row && (gearPosition.column == partNumber.PositionStart - 1 || gearPosition.column == partNumber.PositionEnd + 1))
        {
            return true;
        }

        return false;
    }

    private static bool IsDigit(char c) => Digits.Contains(c);
    //private static bool IsDigit(char c) => char.IsAsciiDigit(c);
    private static bool IsSymbol(char c) => Symbols.Contains(c);
    //private static bool IsSymbol(char c) => !IsDigit(c) && c != '.';
    private static bool IsGear(char c) => c == '*';
    
}