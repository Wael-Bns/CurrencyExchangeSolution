using System.Text.Json;
using CurrencyExchange.API.DTO;
using FluentAssertions;

namespace CurrencyExchange.IntegrationTests
{
    public class ConverterControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public ConverterControllerTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task Convert_ShouldReturnConvertedAmount()
        {
            // Arrange
            string from = "USD";
            string to = "EUR";
            decimal amount = 100m;
            string requestUri = $"/api/converter?from={from}&to={to}&amount={amount}";
            // Act
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            // Assert
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ConvertResultDTO? result = JsonSerializer.Deserialize<ConvertResultDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            result.Should().NotBeNull();
            result!.Result.Should().BeGreaterThan(0);
        }
        [Fact]
        public async Task Convert_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            string requestUri = $"/api/converter?from=&to=EUR&amount=-100";
            // Act
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
