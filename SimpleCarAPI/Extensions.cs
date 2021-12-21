using Autofac;
using CurrencyExchangeLib;
using MoneyHelperLib;
using SimpleCar.Adapters;
using SimpleCar.Others;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;

public static class Extensions
{
    public static void RegisterAllServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<CurrencyExchange>().As<ICurrencyExchange>().SingleInstance();
        containerBuilder.RegisterType<CurrencyExchangeAdapter>().As<IMoneyHelper>().SingleInstance();
        containerBuilder.RegisterType<CurrencyExchangeConverter>().As<ICurrencyConverter>().SingleInstance();

        containerBuilder.RegisterType<TransactionService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CustomerService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CarService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<CarService>().As<ICarService>().InstancePerLifetimeScope();

        //containerBuilder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<BridgeReportService>().As<IReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<BridgeReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<FlyweightReportService>().InstancePerLifetimeScope();
        containerBuilder.RegisterDecorator<CachedReportServiceDecorator, IReportService>();
        containerBuilder.RegisterDecorator<LoggingReportServiceDecorator, IReportService>();

        containerBuilder.RegisterType<TransactionReportFlyweightFactory>().InstancePerLifetimeScope();
    }

    public static string ToSha256Hash(this string text) =>
        string.IsNullOrEmpty(text)
            ? string.Empty
            : BitConverter
                .ToString(new System.Security.Cryptography.SHA256Managed().ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
}