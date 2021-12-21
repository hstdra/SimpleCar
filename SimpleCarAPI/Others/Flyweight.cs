using System.Collections.Concurrent;
using System.Text;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using CommonServiceLocator;
using SimpleCar.Models.Entities;
using SimpleCar.Services.Implementations;

namespace SimpleCar.Others;

public class TransactionReportFlyweight
{
    public Car Car { get; }
    public string CarCode { get; }

    public TransactionReportFlyweight(Car car)
    {
        Car = car;
        CarCode = $"{car.Brand} {car.Model} {car.Year}".ToSha256Hash();
    }

    public string GetReport(Customer customer, Transaction transaction)
    {
        return
            $"[{customer.Type}] {customer.Title}. {customer.Name} | {Car.Brand} {Car.Model} {Car.Year} | {decimal.Round(transaction.Amount)} {transaction.Currency} | {transaction.PurchasedDate.ToShortDateString()}";
    }
}

public class TransactionReportFlyweightFactory
{
    private readonly IDictionary<string, TransactionReportFlyweight> _flyweights =
        new ConcurrentDictionary<string, TransactionReportFlyweight>();

    public TransactionReportFlyweight GetTransactionReportFlyweight(Car car)
    {
        var key = $"{car.Brand} {car.Model} {car.Year}";
        if (_flyweights.TryGetValue(key, out TransactionReportFlyweight existedFlyWeight))
        {
            return existedFlyWeight;
        }

        var flyWeight = new TransactionReportFlyweight(car);
        _flyweights.Add(key, flyWeight);
        return flyWeight;
    }
}

public static class FlyweightTest
{
    public static Task<Summary> Run()
    {
        var result = BenchmarkRunner.Run<Benchmarks>(ManualConfig
            .Create(DefaultConfig.Instance)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator));
        return Task.FromResult(result);
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
        public async Task TestFlyWeight()
        {
            await _flyweightReportService!.GetTransactionReports("USD");
        }

        [Benchmark]
        public async Task TestNoFlyWeight()
        {
            await _bridgeReportService!.GetTransactionReports("USD");
        }
    }
}