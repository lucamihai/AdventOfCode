namespace AdventOfCode.Year2023.Day03;

public static class Solution
{
    private static readonly HashSet<char> Digits = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private static readonly HashSet<char> Symbols = new() { '$', '#', '%', '*', '/', '+', '-', '=', '&', '@' };

    public static void Solve()
    {
        var allPartNumbers = GetAllPartNumbers();

        var sum = allPartNumbers.Sum();
        Console.WriteLine($"The sum of the part numbers is: {sum}.");
    }

    private static List<int> GetAllPartNumbers()
    {
        var path = Path.Combine("Year2023", "Day03", "Input.txt");
        var inputLines = File.ReadAllLines(path);
        var values = new List<int>();

        for (int i = 0; i < inputLines.Length; i++)
        {
            var previousLine = i > 0 ? inputLines[i - 1] : null;
            var currentLine = inputLines[i];
            var nextLine = i < inputLines.Length - 1 ? inputLines[i + 1] : null;

            values.AddRange(ExtractPartNumbers(previousLine, currentLine, nextLine));
        }

        return values;
    }

    private static List<int> ExtractPartNumbers(string previousLine, string currentLine, string nextLine)
    {
        var partNumbers = new List<int>();
        var currentNumberString = string.Empty;

        var scanRangePreviousLine = new HashSet<int>();
        var scanRangeCurrentLine = new HashSet<int>();
        var scanRangeNextLine = new HashSet<int>();

        for (int i = 0; i < currentLine.Length; i++)
        {
            if (IsDigit(currentLine[i]))
            {
                // Add left
                if (string.IsNullOrEmpty(currentNumberString) && i > 0)
                {
                    scanRangeCurrentLine.Add(i - 1);
                }

                currentNumberString += currentLine[i];

                // Add diagonals left
                if (i > 0)
                {
                    scanRangePreviousLine.Add(i - 1);
                    scanRangeNextLine.Add(i - 1);
                }

                // Add above & below
                scanRangePreviousLine.Add(i);
                scanRangeNextLine.Add(i);

                // Add diagonals right
                if (i < currentLine.Length - 1)
                {
                    scanRangePreviousLine.Add(i + 1);
                    scanRangeNextLine.Add(i + 1);
                }
            }
            else
            {
                // Add right
                scanRangeCurrentLine.Add(i);

                if (!string.IsNullOrEmpty(currentNumberString) && IsPartNumber(previousLine, currentLine, nextLine, scanRangePreviousLine, scanRangeCurrentLine, scanRangeNextLine))
                {
                    partNumbers.Add(int.Parse(currentNumberString));
                }

                currentNumberString = string.Empty;
                scanRangePreviousLine.Clear();
                scanRangeCurrentLine.Clear();
                scanRangeNextLine.Clear();
            }

            if (i == currentLine.Length - 1 && !string.IsNullOrEmpty(currentNumberString) && IsPartNumber(previousLine, currentLine, nextLine, scanRangePreviousLine, scanRangeCurrentLine, scanRangeNextLine))
            {
                partNumbers.Add(int.Parse(currentNumberString));
            }
        }

        return partNumbers;
    }

    private static bool IsPartNumber(string previousLine, string currentLine, string nextLine, HashSet<int> scanRangePreviousLine, HashSet<int> scanRangeCurrentLine, HashSet<int> scanRangeNextLine)
    {
        return Check(previousLine, scanRangePreviousLine)
               || Check(currentLine, scanRangeCurrentLine)
               || Check(nextLine, scanRangeNextLine);
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

    private static bool IsDigit(char c) => Digits.Contains(c);
    //private static bool IsDigit(char c) => char.IsAsciiDigit(c);
    private static bool IsSymbol(char c) => Symbols.Contains(c);
    //private static bool IsSymbol(char c) => !IsDigit(c) && c != '.';
}