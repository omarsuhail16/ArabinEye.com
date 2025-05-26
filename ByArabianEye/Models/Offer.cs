using System.ComponentModel.DataAnnotations;

namespace ByArabianEye.Models  // ← أضيفي هذا السطر
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string OfferType { get; set; }

        [Required]
        [Range(1, 365)]
        public int Days { get; set; }

        [Required]
        [Range(0.01, 999999)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string? ImageUrl { get; set; }
    }
}
