using System.Text.Json;
using System.Text.Json.Serialization;

namespace CurrencyExchange.API.HttpClients
{
    public class FastExchangeHttpClient : IConversionProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public FastExchangeHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<decimal> ConvertCurrencyAsync(string from, string to, decimal amount)
        {
            string apiKey = _configuration["FAST_EXCHANGE_API_KEY"]!;
            HttpResponseMessage response = await _httpClient.GetAsync($"/convert?api_key={apiKey}&&from={from}&to={to}&amount={amount}");
            
            if(!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                FastExchangeApiErrorResponse? errorResponse = JsonSerializer.Deserialize<FastExchangeApiErrorResponse>(errorContent);
                string errorMessage = errorResponse != null ? 
                    $"{errorResponse.Error}" 
                    :
                    $"Unknown error occurred.";
                throw new InvalidOperationException(errorMessage);
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            FastExchangeApiSuccessResponse? apiData = JsonSerializer.Deserialize<FastExchangeApiSuccessResponse>(responseBody);

            if (apiData?.Result == null || !apiData.Result.ContainsKey(to))
            {
                throw new InvalidOperationException($"External API returned an unexpected structure or missing data. {nameof(FastExchangeHttpClient)}");
            }
            decimal convertedAmount = apiData.Result[to];
            return convertedAmount;
        }
        private class FastExchangeApiSuccessResponse
        {
            [JsonPropertyName("result")]
            public Dictionary<string, decimal>? Result { get; set; }
        }
        private class FastExchangeApiErrorResponse
        {
            [JsonPropertyName("error")]
            public string Error { get; set; }
        }
    }
}
