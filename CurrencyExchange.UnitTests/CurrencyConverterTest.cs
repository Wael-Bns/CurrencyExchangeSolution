using CurrencyExchange.API.ServiceContracts;
namespace CurrencyExchange.UnitTests
{
    public class CurrencyConverterTest
    {
        private readonly ICurrencyConverterService _currencyConverterService;
        public CurrencyConverterTest(ICurrencyConverterService currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }
    }
}
