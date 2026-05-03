using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API.DTO
{
    public class ConvertRequestDTO
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be exactly 3 characters.")]
        public string From { get; set; } = string.Empty;
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be exactly 3 characters.")]
        public string To { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; } = 1;
    }
}
