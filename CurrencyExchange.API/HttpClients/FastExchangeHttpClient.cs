namespace CurrencyExchange.API.HttpClients
{
    public class FastExchangeHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public FastExchangeHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> ConvertCurrency(string from, string to, decimal amount)
        {
            string apiKey = _configuration["FastExchangeApi:ApiKey"]!;
            HttpResponseMessage response = await _httpClient.GetAsync($"/convert?api_key={apiKey}&&from={from}&to={to}&amount={amount}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
