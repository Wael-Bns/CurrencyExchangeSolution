
namespace CurrencyExchange.API.HttpClients
{
    public interface IConversionProvider
    {
        Task<decimal> ConvertCurrencyAsync(string from, string to, decimal amount);
    }
}