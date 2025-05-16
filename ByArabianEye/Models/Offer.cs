namespace ByArabianEye.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string OfferType { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Days { get; set; }             // ✅ جديد
        public string ImageUrl { get; set; }      // ✅ جديد
    }
}
