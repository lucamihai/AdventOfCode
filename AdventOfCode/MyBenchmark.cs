using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
[RankColumn]
public class MyBenchmark
{
    [Benchmark]
    public void Test()
    {
        Year2023.Day02.Solution.Solve();
    }
}