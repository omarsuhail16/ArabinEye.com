using System;

namespace ByArabianEye.Models
{
    public class CustomerNote
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string NoteContent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }
    }
}
