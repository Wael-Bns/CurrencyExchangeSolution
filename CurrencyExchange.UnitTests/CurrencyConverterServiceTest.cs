using CurrencyExchange.API.HttpClients;
using CurrencyExchange.API.ServiceContracts;
using CurrencyExchange.API.Services;
using FluentAssertions;
using Moq;
namespace CurrencyExchange.UnitTests
{
    public class CurrencyConverterServiceTest
    {
        private readonly ICurrencyConverterService _currencyConverterService;
        private readonly IConversionProvider _conversionProvider;
        private readonly Mock<IConversionProvider> _conversionProviderMock;
        public CurrencyConverterServiceTest()
        {
            _conversionProviderMock = new Mock<IConversionProvider>();
            _conversionProvider = _conversionProviderMock.Object;
            _currencyConverterService = new CurrencyConverterService(_conversionProvider);
        }
        [Fact]
        public async Task ConvertCurrency_ShouldReturnConvertedAmount()
        {
            // Arrange
            string from = "USD";
            string to = "EUR";
            decimal amount = 100m;
            decimal expectedConvertedAmount = 85m;
            _conversionProviderMock.Setup(cp => cp.ConvertCurrencyAsync(from, to, amount))
                .ReturnsAsync(expectedConvertedAmount);
            // Act
            var actual = await _currencyConverterService.ConvertCurrency(from, to, amount);
            // Assert
            actual.Should().NotBeNull();
            actual.Result.Should().Be(expectedConvertedAmount);
        }
        [Fact]
        public async Task ConvertCurrency_ShouldThrowException_WhenConversionFails()
        {
            // Arrange
            string from = "USD";
            string to = "EUR";
            decimal amount = 100m;
            _conversionProviderMock.Setup(cp => cp.ConvertCurrencyAsync(from, to, amount))
                .ThrowsAsync(new InvalidOperationException("API Error"));
            // Act
            Func<Task> act = async () => await _currencyConverterService.ConvertCurrency(from, to, amount);
            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("API Error");
        }
    }
}
