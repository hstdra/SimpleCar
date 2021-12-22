﻿using System.Text;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CommonServiceLocator;
using SimpleCar.Services.Implementations;

namespace SimpleCar.Others;

public static class FlyweightBenchmarks
{
    public static Task<string> Run()
    {
        var result = BenchmarkRunner.Run<Benchmarks>(ManualConfig
            .Create(DefaultConfig.Instance)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .WithOptions(ConfigOptions.DisableLogFile));

        var html = File.ReadAllText(
            $@"{result.ResultsDirectoryPath}\SimpleCar.Others.FlyweightBenchmarks.Benchmarks-report.html");
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
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAllServices();
            var container = containerBuilder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _flyweightReportService = ServiceLocator.Current.GetInstance<FlyweightReportService>();
            _bridgeReportService = ServiceLocator.Current.GetInstance<BridgeReportService>();
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