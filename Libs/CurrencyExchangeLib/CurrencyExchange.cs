namespace CurrencyExchangeLib;

public interface ICurrencyExchange
{
    decimal Convert(Currency from, Currency to, decimal amount);
}

public class CurrencyExchange : ICurrencyExchange
{
    private static readonly IDictionary<Currency, decimal> CurrencyRates = new Dictionary<Currency, decimal>
    {
        {Currency.USD, 1},
        {Currency.EUR, 1.1238903m},
        {Currency.GBP, 1.324776m},
    };
    
    public decimal Convert(Currency from, Currency to, decimal amount)
    {
        if (from == to)
        {
            return amount;
        }   

        var rate = CurrencyRates[from] / CurrencyRates[to];
        return amount * rate;
    }
}