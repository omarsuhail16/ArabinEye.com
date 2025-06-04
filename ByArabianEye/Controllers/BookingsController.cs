using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByArabianEye.Data;
using ByArabianEye.Models;

namespace ByArabianEye.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض الحجوزات (الإدمن فقط)
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "admin")
                return RedirectToAction("Login", "Account");

            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }

        // عرض صفحة إنشاء حجز (للعميل)
        public IActionResult Create(string country = null, string hotel = null)
        {
            if (HttpContext.Session.GetString("Role") != "client")
            {
                TempData["Error"] = " يجب تسجيل الدخول كعميل للحجز.";
                return RedirectToAction("Login", "Account");
            }

            var booking = new Booking
            {
                Country = country,
                Hotel = hotel,
                Date = DateTime.Today
            };

            return View(booking);
        }

        // تنفيذ الحجز (للعميل) - مع تعيين ClientName تلقائيًا
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (HttpContext.Session.GetString("Role") != "client")
            {
                TempData["Error"] = " You must be logged in as a customer to book..";
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                booking.Status = "قيد الانتظار";

                // تعيين اسم العميل تلقائيًا من الجلسة
                booking.ClientName = HttpContext.Session.GetString("FullName");

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                TempData["Success"] = " Reservation sent successfully!";
                return RedirectToAction("Success");
            }

            return View(booking);
        }

        // صفحة تأكيد الحجز
        public IActionResult Success()
        {
            return View();
        }

        // تفاصيل الحجز (للإدمن فقط)
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("Role") != "admin")
                return RedirectToAction("Login", "Account");

            var booking = await _context.Bookings.FindAsync(id);
            return booking == null ? NotFound() : View(booking);
        }

        // تعديل الحجز (للإدمن والعميل مع تحقق)
        public async Task<IActionResult> Edit(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            if (role == "client")
            {
                var name = HttpContext.Session.GetString("FullName");
                if (booking.ClientName != name)
                    return Unauthorized();
            }
            else if (role != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Booking updatedBooking)
        {
            var role = HttpContext.Session.GetString("Role");
            var existing = await _context.Bookings.FindAsync(updatedBooking.Id);
            if (existing == null)
                return NotFound();

            if (role == "client")
            {
                var name = HttpContext.Session.GetString("FullName");
                if (existing.ClientName != name)
                    return Unauthorized();
            }
            else if (role != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            existing.Country = updatedBooking.Country;
            existing.Hotel = updatedBooking.Hotel;
            existing.PackageType = updatedBooking.PackageType;
            existing.Date = updatedBooking.Date;
            existing.PeopleCount = updatedBooking.PeopleCount;
            existing.Status = updatedBooking.Status;

            await _context.SaveChangesAsync();
            TempData["Success"] = " reservation has been updated successfully!!";

            return role == "admin" ? RedirectToAction("Index") : RedirectToAction("MyProfile", "Account");
        }

        // حذف الحجز (للإدمن والعميل)
        public async Task<IActionResult> Delete(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            if (role == "client")
            {
                var name = HttpContext.Session.GetString("FullName");
                if (booking.ClientName != name)
                    return Unauthorized();
            }
            else if (role != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            if (role == "client")
            {
                var name = HttpContext.Session.GetString("FullName");
                if (booking.ClientName != name)
                    return Unauthorized();
            }
            else if (role != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            TempData["Success"] = " تم حذف الحجز بنجاح.";

            return role == "admin" ? RedirectToAction("Index") : RedirectToAction("MyProfile", "Account");
        }
    }
}
