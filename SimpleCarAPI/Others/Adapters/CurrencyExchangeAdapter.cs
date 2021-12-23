using CurrencyExchangeLib;
using MoneyHelperLib;

namespace SimpleCar.Others.Adapters;

public class CurrencyExchangeAdapter : IMoneyHelper
{
    private readonly ICurrencyExchange _currencyExchange;

    public CurrencyExchangeAdapter(ICurrencyExchange currencyExchange)
    {
        _currencyExchange = currencyExchange;
    }

    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        return _currencyExchange.Convert(
            Enum.Parse<Currency>(fromCurrency),
            Enum.Parse<Currency>(toCurrency),
            amount);
    }
}