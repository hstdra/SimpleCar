namespace MoneyHelperLib;

public class MoneyHelper
{
    private static readonly IDictionary<string, decimal> CurrencyRates = new Dictionary<string, decimal>
    {
        {"USD", 1},
        {"EUR", 1.1238903m},
        {"VND", 0.000043488153m},
    };
    
    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        if (string.Equals(fromCurrency, toCurrency, StringComparison.CurrentCultureIgnoreCase))
        {
            return amount;
        }   

        var rate = CurrencyRates[fromCurrency.ToUpper()] / CurrencyRates[toCurrency.ToUpper()];
        return amount * rate;
    }
}