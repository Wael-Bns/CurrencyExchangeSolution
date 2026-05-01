using System.Text.Json.Nodes;
using CurrencyExchange.API.DTO;
using CurrencyExchange.API.HttpClients;
using CurrencyExchange.API.ServiceContracts;

namespace CurrencyExchange.API.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly FastExchangeHttpClient _fastExchangeHttpClient;
        public CurrencyConverterService(FastExchangeHttpClient fastExchangeHttpClient)
        {
            _fastExchangeHttpClient = fastExchangeHttpClient;
        }
        public async Task<ConvertResultDTO> ConvertCurrency(string from, string to, decimal amount)
        {
            string responseBody = await _fastExchangeHttpClient.ConvertCurrency(from, to, amount);
            JsonNode jsonNode = JsonNode.Parse(responseBody);
            decimal? rate = jsonNode["result"]?["rate"]?.GetValue<decimal>();
            if (rate == null)
            {
                throw new Exception("Failed to parse exchange rate from response.");
            }
            else
            {
                decimal convertedAmount = amount * rate.Value;
                return new ConvertResultDTO(convertedAmount);
            }
        }
    }
}
