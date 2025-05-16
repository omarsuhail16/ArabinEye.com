using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Models;
using System.Text.Json;

namespace ByArabianEye.Controllers
{
    public class BookingsController : Controller
    {
        private readonly string _jsonPath;

        public BookingsController(IWebHostEnvironment env)
        {
            _jsonPath = Path.Combine(env.WebRootPath, "data", "bookings.json");
        }

        // 🔹 قراءة كل الحجوزات من الملف
        private List<Booking> ReadBookings()
        {
            if (!System.IO.File.Exists(_jsonPath))
                return new List<Booking>();

            var json = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
        }

        // 🔸 حفظ الحجوزات في الملف
        private void SaveBookings(List<Booking> bookings)
        {
            var json = JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_jsonPath, json);
        }

        public IActionResult Index()
        {
            var bookings = ReadBookings();
            return View(bookings);
        }

        public IActionResult Create(string country, string hotel)
        {
            ViewBag.Country = country;
            ViewBag.Hotel = hotel;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var bookings = ReadBookings();
            booking.Id = bookings.Any() ? bookings.Max(b => b.Id) + 1 : 1;
            booking.Status = "Pending";
            bookings.Add(booking);
            SaveBookings(bookings);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var bookings = ReadBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var bookings = ReadBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking updatedBooking)
        {
            var bookings = ReadBookings();
            var existing = bookings.FirstOrDefault(b => b.Id == updatedBooking.Id);

            if (existing != null)
            {
                existing.ClientName = updatedBooking.ClientName;
                existing.Country = updatedBooking.Country;
                existing.PackageType = updatedBooking.PackageType;
                existing.Date = updatedBooking.Date;
                existing.PeopleCount = updatedBooking.PeopleCount;
                existing.Status = updatedBooking.Status;

                SaveBookings(bookings);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var bookings = ReadBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            return View(booking);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var bookings = ReadBookings();
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                bookings.Remove(booking);
                SaveBookings(bookings);
            }
            return RedirectToAction("Index");
        }
    }
}
