using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace SimpleCar.Others;

public static class FlyweightTest
{
    public static async Task Run()
    {
        BenchmarkRunner.Run<Benchmarks>(ManualConfig
            .Create(DefaultConfig.Instance)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator));
    }

    [MemoryDiagnoser]
    [DryJob]
    public class Benchmarks
    {
        [Benchmark]
        public byte[] EmptyArray() => Array.Empty<byte>();

        [Benchmark]
        public byte[] EightBytes() => new byte[8];

        [Benchmark]
        public byte[] SomeLinq()
        {
            return Enumerable
                .Range(0, 100)
                .Where(i => i % 2 == 0)
                .Select(i => (byte)i)
                .ToArray();
        }
    }
}
