using CurrencyExchange.API.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly ICurrencyConverterService _currencyConverterService;
        public ConverterController(ICurrencyConverterService currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }
        [HttpGet]
        public async Task<IActionResult> Convert([FromQuery] string from, [FromQuery] string to, [FromQuery] decimal amount)
        {
            try
            {
                var result = await _currencyConverterService.ConvertCurrency(from, to, amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
