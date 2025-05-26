namespace ByArabianEye.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string To { get; set; }
        public string DriverName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        // ✅ تعيين قيمة افتراضية للملاحظات لتجنب الخطأ
        public List<CustomerNote> Notes { get; set; } = new List<CustomerNote>();
    }
}
