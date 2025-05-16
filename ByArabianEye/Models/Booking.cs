namespace ByArabianEye.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Country { get; set; }
        public string Hotel { get; set; } // 🆕 الفندق المختار
        public string PackageType { get; set; }
        public DateTime Date { get; set; }
        public int PeopleCount { get; set; }
        public string Status { get; set; }
    }
}
