using CurrencyExchangeLib;
using MoneyHelperLib;
using SimpleCar.Adapters;
using SimpleCar.Services;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICurrencyExchange, CurrencyExchange>();
builder.Services.AddSingleton<IMoneyHelper, CurrencyExchangeAdapter>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();