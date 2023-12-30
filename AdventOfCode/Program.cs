using BenchmarkDotNet.Running;

namespace AdventOfCode;

public static class Program
{
    public static void Main(string[] args)
    {
        Year2023.Day03.Solution.Solve();

        // Uncomment below line for benchmarking
        //BenchmarkRunner.Run<MyBenchmark>();

        Console.Read();
    }
}