using AdventOfCode.Year2022.Day07.Entities;

namespace AdventOfCode.Year2022.Day07;

public static class Solution
{
    private const int FileSystemSize = 70000000;
    private const int RequiredFreeSpace = 30000000;

    public static void Solve()
    {
        var path = Path.Combine("Year2022", "Day07", "Input.txt");
        var inputLines = File.ReadAllLines(path).ToList();

        var baseDirectory = BuildDirectoryStructure(inputLines);
        baseDirectory.ComputeSizesRecursively();

        var directoriesUnderThisSize = baseDirectory.GetDirectoriesUnderThisSize(100000);
        var sum = directoriesUnderThisSize.Sum(x => x.Size);
        Console.WriteLine($"Part1: Total size: {sum}");

        var alreadyFreeSpace = FileSystemSize - baseDirectory.Size;
        var differenceRequired = RequiredFreeSpace - alreadyFreeSpace;
        var smallestDirectoryOfAtLeastThisSize = baseDirectory.FindSmallestDirectoryOfAtLeastThisSize(differenceRequired);
        Console.WriteLine($"Part2: Found directory size: {smallestDirectoryOfAtLeastThisSize.Size}");
    }

    private static MyDirectory BuildDirectoryStructure(List<string> inputLines)
    {
        var baseDirectory = new MyDirectory();
        var currentDirectory = baseDirectory;

        foreach (var inputLine in inputLines)
        {
            var tokens = inputLine.Split();

            if (tokens[0] == "$")
            {
                if (tokens[1] == "cd")
                {
                    var destination = tokens[2];

                    if (destination == "/")
                    {
                        currentDirectory = baseDirectory;
                    }
                    else if (destination == "..")
                    {
                        currentDirectory = currentDirectory.Parent;
                    }
                    else
                    {
                        currentDirectory = currentDirectory.Directories[destination];
                    }
                }
            }

            else if (tokens[0] == "dir")
            {
                var directoryName = tokens[1];
                var newDirectory = new MyDirectory { Name = directoryName, Parent = currentDirectory };
                var succeeded = currentDirectory.Directories.TryAdd(directoryName, newDirectory);

                // TODO: Just for testing, remove this later
                if (!succeeded)
                {

                }
            }
            else
            {
                var fileSize = int.Parse(tokens[0]);
                var fileName = tokens[1];
                var newFile = new MyFile
                {
                    Name = fileName,
                    Size = fileSize
                };

                var succeeded = currentDirectory.Files.TryAdd(fileName, newFile);

                // TODO: Just for testing, remove this later
                if (!succeeded)
                {

                }
            }
        }

        return baseDirectory;
    }
}