using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
[RankColumn]
public class MyBenchmark
{
    [Benchmark]
    public void Test()
    {
        Year2023.Day04.Solution.Solve();
    }
}