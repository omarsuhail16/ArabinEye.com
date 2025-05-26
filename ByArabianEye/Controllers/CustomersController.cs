using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Models;
using ByArabianEye.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ByArabianEye.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض العملاء + بحث + تنبيهات
        public async Task<IActionResult> Index(string searchCode)
        {
            var customers = from c in _context.Customers select c;

            if (!string.IsNullOrEmpty(searchCode))
            {
                customers = customers.Where(c => c.ClientCode.Contains(searchCode));
            }

            // ✅ ترتيب حسب تاريخ الوصول (الأقدم أولاً)
            var customerList = await customers
                .OrderBy(c => c.ArrivalDate)
                .ToListAsync();

            ViewBag.UpcomingArrivals = customerList.Where(c => (c.ArrivalDate - System.DateTime.Today).TotalDays <= 2).ToList();
            ViewBag.UpcomingDepartures = customerList.Where(c => (c.DepartureDate - System.DateTime.Today).TotalDays <= 2).ToList();

            return View(customerList);
        }


        // عرض نموذج الإضافة
        public IActionResult Create()
        {
            return View();
        }

        // إضافة عميل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // عرض نموذج التعديل
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // حفظ التعديل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer updatedCustomer)
        {
            if (id != updatedCustomer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(updatedCustomer);
        }

        // حذف عميل
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
