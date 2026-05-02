using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API.DTO
{
    public class ConvertRequestDTO
    {
        [Required]
        public string From { get; set; } = string.Empty;
        [Required]
        public string To { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; } = 1;
    }
}
