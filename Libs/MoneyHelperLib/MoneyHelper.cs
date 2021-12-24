namespace MoneyHelperLib;

public interface IMoneyHelper
{
    decimal Convert(decimal amount, string fromCurrency, string toCurrency);
}

public class MoneyHelper : IMoneyHelper
{
    private static readonly IDictionary<string, decimal> CurrencyRates = new Dictionary<string, decimal>
    {
        {"USD", 1},
        {"EUR", 1.1238903m},
        {"GBP", 1.324776m},
    };

    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        decimal result;
        if (string.Equals(fromCurrency, toCurrency, StringComparison.CurrentCultureIgnoreCase))
        {
            result = amount;
        }
        else
        {
            var rate = CurrencyRates[fromCurrency.ToUpper()] / CurrencyRates[toCurrency.ToUpper()];
            result = amount * rate;
        }

        return ((int)(result / 100) * 100);
    }
}