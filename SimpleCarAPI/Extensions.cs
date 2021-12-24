using Autofac;
using CurrencyExchangeLib;
using MoneyHelperLib;
using SimpleCar.Adapters;
using SimpleCar.Others.Adapters;
using SimpleCar.Others.Bridges;
using SimpleCar.Others.Composites;
using SimpleCar.Others.Decorators;
using SimpleCar.Others.Facades;
using SimpleCar.Others.Flyweights;
using SimpleCar.Others.Proxies;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

public static class Extensions
{
    public static string ToSha256Hash(this string text)
    {
        return string.IsNullOrEmpty(text)
            ? string.Empty
            : BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
    }

    public static void RegisterAllServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<TransactionService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CustomerService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CarService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<FacadeReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CurrencyExchange>().As<ICurrencyExchange>().SingleInstance();
        containerBuilder.RegisterType<MoneyHelper>().As<IMoneyHelper>().SingleInstance();

        containerBuilder.RegisterType<CurrencyExchangeAdapter>().As<IMoneyHelper>().SingleInstance();

        containerBuilder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CarService>().As<ICarService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CurrencyExchangeConverter>().As<ICurrencyConverter>().SingleInstance();
        containerBuilder.RegisterType<BridgeReportService>().As<IReportService>().InstancePerLifetimeScope();

        containerBuilder.RegisterDecorator<CachedReportServiceDecorator, IReportService>();
        containerBuilder.RegisterDecorator<LoggingReportServiceDecorator, IReportService>();

        containerBuilder.RegisterType<CustomerServiceProxy>().As<ICustomerService>().InstancePerLifetimeScope();

        containerBuilder.RegisterType<CarCompositeTests>().InstancePerLifetimeScope();

        containerBuilder.RegisterType<BridgeReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<FlyweightReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<ReportFlyweightFactory>().InstancePerLifetimeScope();
    }
}