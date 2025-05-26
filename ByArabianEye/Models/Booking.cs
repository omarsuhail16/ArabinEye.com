using System;
using System.ComponentModel.DataAnnotations;

namespace ByArabianEye.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم العميل مطلوب")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "الدولة مطلوبة")]
        public string Country { get; set; }

        [Required(ErrorMessage = "الفندق مطلوب")]
        public string Hotel { get; set; }

        [Required(ErrorMessage = "نوع الباقة مطلوب")]
        public string PackageType { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "عدد الأشخاص مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب أن يكون العدد أكبر من صفر")]
        public int PeopleCount { get; set; }

        // ✅ نضيف القيمة الافتراضية هنا مباشرة
        public string Status { get; set; } = "قيد الانتظار";
    }
}
