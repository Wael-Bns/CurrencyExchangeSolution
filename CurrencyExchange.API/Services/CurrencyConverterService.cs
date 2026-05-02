using CurrencyExchange.API.DTO;
using CurrencyExchange.API.HttpClients;
using CurrencyExchange.API.ServiceContracts;

namespace CurrencyExchange.API.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly IConversionProvider _conversionProvider;
        public CurrencyConverterService(
            IConversionProvider conversionProvider)
        {
            _conversionProvider = conversionProvider;
        }

        public async Task<ConvertResultDTO> ConvertCurrency(string from, string to, decimal amount)
        {
            decimal convertedAmount = await _conversionProvider.ConvertCurrencyAsync(from, to, amount);
                
            return new ConvertResultDTO(convertedAmount);
        }
    }
}
