using CurrencyExchangeLib;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Adapters;

public class CurrencyExchangeConverter : ICurrencyConverter
{
    private readonly ICurrencyExchange _currencyExchange;

    public CurrencyExchangeConverter(ICurrencyExchange currencyExchange)
    {
        _currencyExchange = currencyExchange;
    }

    public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
    {
        return _currencyExchange.Convert(
            Enum.Parse<Currency>(fromCurrency),
            Enum.Parse<Currency>(toCurrency),
            amount);
    }
}