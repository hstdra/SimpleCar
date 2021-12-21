namespace SimpleCar.Services.Interfaces;

public interface ICurrencyConverter
{
    decimal Convert(string fromCurrency, string toCurrency, decimal amount);
}
