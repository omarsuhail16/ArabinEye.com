using System;

namespace ByArabianEye.Models
{
    public class BookingDeletionLog
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string ClientName { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
