using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ByArabianEye.Models;

namespace ByArabianEye.Controllers
{
    public class DashboardController : Controller
    {
        private readonly string bookingsPath;
        private readonly string offersPath;
        private readonly string clientsPath;
        private readonly IWebHostEnvironment _env;

        public DashboardController(IWebHostEnvironment env)
        {
            _env = env;
            bookingsPath = Path.Combine(env.WebRootPath, "data", "bookings.json");
            offersPath = Path.Combine(env.WebRootPath, "data", "offers.json");
            clientsPath = Path.Combine(env.WebRootPath, "data", "clients.json"); // ✅ جديد
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.Role = role ?? "client";

            if (role == "admin")
            {
                // ✅ قراءة البيانات
                var bookings = ReadList<Booking>(bookingsPath);
                var offers = ReadList<Offer>(offersPath);
                var clients = ReadList<Client>(clientsPath);

                ViewBag.TotalBookings = bookings.Count;
                ViewBag.TotalOffers = offers.Count;
                ViewBag.TotalClients = clients.Count;

                // ✅ إرسال القائمة إذا حبيتِ تعرضي أسماءهم في الجدول
                ViewBag.ClientList = clients;
            }
            else
            {
                var offers = ReadList<Offer>(offersPath);
                ViewBag.AllOffers = offers.Select(o => o.Title).ToList();
            }

            return View();
        }

        private List<T> ReadList<T>(string path)
        {
            if (!System.IO.File.Exists(path))
                return new List<T>();

            var json = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
