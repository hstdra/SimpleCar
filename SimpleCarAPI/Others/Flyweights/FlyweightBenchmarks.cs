using System.Text;
using Autofac;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using SimpleCar.Others.Bridges;

namespace SimpleCar.Others.Flyweights;

public static class FlyweightBenchmarks
{
    public static Task<string> Run()
    {
        var result = BenchmarkRunner.Run<Benchmarks>(ManualConfig
            .Create(DefaultConfig.Instance)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .WithOptions(ConfigOptions.DisableLogFile));

        var html = File.ReadAllText(
            $@"{result.ResultsDirectoryPath}\SimpleCar.Others.Flyweights.FlyweightBenchmarks.Benchmarks-report.html");
        return Task.FromResult(html);
    }

    [MemoryDiagnoser]
    [DryJob]
    public class Benchmarks
    {
        private FlyweightReportService? _flyweightReportService;
        private BridgeReportService? _bridgeReportService;

        [GlobalSetup]
        public void Setup()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAllServices();
            var container = containerBuilder.Build();
            
            _flyweightReportService = container.Resolve<FlyweightReportService>();
            _bridgeReportService = container.Resolve<BridgeReportService>();
        }

        [Benchmark]
        public async Task TestNoFlyWeight()
        {
            await _bridgeReportService!.GetReports("USD");
        }

        [Benchmark]
        public async Task TestFlyWeight()
        {
            await _flyweightReportService!.GetReports("USD");
        }
    }
}