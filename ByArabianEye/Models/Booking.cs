using System;
using System.ComponentModel.DataAnnotations;

namespace ByArabianEye.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Hotel is required")]
        public string Hotel { get; set; }

        [Required(ErrorMessage = "Package type is required")]
        public string PackageType { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Number of people is required")]
        [Range(1, int.MaxValue, ErrorMessage = "People count must be at least 1")]
        public int PeopleCount { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number format")]
        public string ContactNumber { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
