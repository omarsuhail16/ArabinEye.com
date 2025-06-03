using System.ComponentModel.DataAnnotations;

namespace ByArabianEye.Models
{
    public class CountryOffer
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
