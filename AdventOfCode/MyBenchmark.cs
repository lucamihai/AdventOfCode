using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
[RankColumn]
public class MyBenchmark
{
    [Benchmark]
    public void Test()
    {
        Year2023.Day03.Solution.Solve();
    }
}