using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using CurrencyExchangeLib;
using MoneyHelperLib;
using SimpleCar.Adapters;
using SimpleCar.Decorators;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x =>
{
    x.RegisterType<CurrencyExchange>().As<ICurrencyExchange>().SingleInstance();
    x.RegisterType<CurrencyExchangeAdapter>().As<IMoneyHelper>().SingleInstance();
    x.RegisterType<CurrencyExchangeConverter>().As<ICurrencyConverter>().SingleInstance();

    x.RegisterType<TransactionService>().InstancePerLifetimeScope();
    x.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
    x.RegisterType<CustomerService>().InstancePerLifetimeScope();
    x.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
    x.RegisterType<CarService>().InstancePerLifetimeScope();
    x.RegisterType<CarService>().As<ICarService>().InstancePerLifetimeScope();

    //x.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
    //x.RegisterType<BridgeReportService>().As<IReportService>().InstancePerLifetimeScope();
    x.RegisterType<BridgeReportService>().InstancePerLifetimeScope();
    x.RegisterType<ReportServiceProxy>().As<IReportService>().InstancePerLifetimeScope();
    x.RegisterDecorator<CachedReportServiceDecorator, IReportService>();
    x.RegisterDecorator<LoggingReportServiceDecorator, IReportService>();
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