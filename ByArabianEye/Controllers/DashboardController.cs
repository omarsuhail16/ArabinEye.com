using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ByArabianEye.Data;
using ByArabianEye.Models;
using System;
using System.Linq;

namespace ByArabianEye.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            // حماية الصفحة: فقط الأدمن
            if (role != "admin")
                return RedirectToAction("Login", "Account");

            ViewBag.Role = role;

            // إحصائيات عامة
            ViewBag.TotalBookings = _context.Bookings.Count();
            ViewBag.TotalOffers = _context.Offers.Count();
            ViewBag.TotalClients = _context.Customers.Count();

            var today = DateTime.Today;

            // قائمة العملاء كاملة (اختياري للعرض)
            var customers = _context.Customers.ToList();
            ViewBag.ClientList = customers;

            // العملاء القادمين خلال يومين
            ViewBag.UpcomingArrivals = customers
                .Where(c => (c.ArrivalDate - today).TotalDays <= 2 && (c.ArrivalDate - today).TotalDays >= 0)
                .ToList();

            // العملاء المغادرين خلال يومين
            ViewBag.UpcomingDepartures = customers
                .Where(c => (c.DepartureDate - today).TotalDays <= 2 && (c.DepartureDate - today).TotalDays >= 0)
                .ToList();

            // جلب آخر 10 سجلات حذف حجوزات (إن وجدت)
            var deletionLogs = _context.BookingDeletionLogs
                .OrderByDescending(log => log.DeletedAt)
                .Take(10)
                .ToList();
            ViewBag.DeletionLogs = deletionLogs;

            return View();
        }
    }
}
