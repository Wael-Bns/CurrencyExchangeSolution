using CurrencyExchange.API.DTO;
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
        public async Task<IActionResult> Convert([FromQuery] ConvertRequestDTO convertRequestDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string cleanFrom = convertRequestDTO.From.Trim().ToUpperInvariant();
            string cleanTo = convertRequestDTO.To.Trim().ToUpperInvariant();
            decimal amount = convertRequestDTO.Amount;

            var result = await _currencyConverterService.ConvertCurrency(cleanFrom, cleanTo, amount);
            return Ok(result);
        }
    }
}
