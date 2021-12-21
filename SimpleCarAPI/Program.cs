using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using CurrencyExchangeLib;
using MoneyHelperLib;
using SimpleCar.Adapters;
using SimpleCar.Others;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
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

    //x.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
    //x.RegisterType<BridgeReportService>().As<IReportService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<BridgeReportService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ReportServiceProxy>().As<IReportService>().InstancePerLifetimeScope();
    containerBuilder.RegisterDecorator<CachedReportServiceDecorator, IReportService>();
    containerBuilder.RegisterDecorator<LoggingReportServiceDecorator, IReportService>();
});

var app = builder.Build();

var csl = new AutofacServiceLocator(app.Services.GetAutofacRoot());
ServiceLocator.SetLocatorProvider(() => csl);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
