using CurrencyExchange.API.DTO;

namespace CurrencyExchange.API.ServiceContracts
{
    public interface ICurrencyConverterService
    {
        Task<ConvertResultDTO> ConvertCurrency(string from, string to, decimal amount);
    }
}
