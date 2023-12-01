namespace AdventOfCode.Year2022.Day07.Entities;

public class MyDirectory
{
    public string Name { get; set; }
    public MyDirectory Parent { get; set; }

    public Dictionary<string, MyFile> Files { get; }
    public Dictionary<string, MyDirectory> Directories { get; }

    public int Size { get; private set; }

    public MyDirectory()
    {
        Files = new Dictionary<string, MyFile>();
        Directories = new Dictionary<string, MyDirectory>();
    }

    public void ComputeSizesRecursively()
    {
        var currentDirectorySize = Files.Values.Sum(x => x.Size);

        foreach (var directory in Directories.Values)
        {
            directory.ComputeSizesRecursively();
            currentDirectorySize += directory.Size;
        }

        Size = currentDirectorySize;
    }

    public List<MyDirectory> GetDirectoriesUnderThisSize(int maxSize)
    {
        var foundDirectories = new List<MyDirectory>();
        SearchDirectoriesUnderThisSize(this, maxSize, foundDirectories);

        return foundDirectories;
    }

    public MyDirectory FindSmallestDirectoryOfAtLeastThisSize(int minSize)
    {
        var currentBest = new MyDirectory { Size = int.MaxValue };
        SearchSmallestDirectoryOfAtLeastThisSize(this, minSize, ref currentBest);

        return currentBest;
    }

    private static void SearchDirectoriesUnderThisSize(MyDirectory directory, int maxSize, List<MyDirectory> foundDirectories)
    {
        if (directory.Size <= maxSize)
        {
            foundDirectories.Add(directory);
        }

        foreach (var subDirectory in directory.Directories.Values)
        {
            SearchDirectoriesUnderThisSize(subDirectory, maxSize, foundDirectories);
        }
    }

    private static void SearchSmallestDirectoryOfAtLeastThisSize(MyDirectory directory, int minSize, ref MyDirectory currentBest)
    {
        if (directory.Size >= minSize && directory.Size < currentBest.Size)
        {
            currentBest = directory;
        }

        foreach (var subDirectory in directory.Directories.Values)
        {
            SearchSmallestDirectoryOfAtLeastThisSize(subDirectory, minSize, ref currentBest);
        }
    }
}