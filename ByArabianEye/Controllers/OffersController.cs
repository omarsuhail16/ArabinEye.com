using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByArabianEye.Models;
using ByArabianEye.Data;

namespace ByArabianEye.Controllers
{
    public class OffersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public OffersController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ عرض العروض للعميل
        public IActionResult OffersClient()
        {
            var offers = _context.Offers.ToList();
            return View(offers); // يتوقع OffersClient.cshtml في Views/Offers
        }

        // ✅ عرض كل العروض للمسؤول
        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers.ToListAsync();
            return View(offers);
        }

        // ✅ عرض صفحة إنشاء عرض جديد
        public IActionResult Create()
        {
            return View();
        }

        // ✅ إضافة عرض جديد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer offer, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    offer.ImageUrl = $"/img/{fileName}";
                }

                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Offer added successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(offer);
        }

        // ✅ عرض تفاصيل عرض معين
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            return offer == null ? NotFound() : View(offer);
        }

        // ✅ عرض صفحة تعديل عرض
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            return offer == null ? NotFound() : View(offer);
        }

        // ✅ تنفيذ التعديل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Offer updatedOffer, IFormFile? ImageFile)
        {
            var offer = await _context.Offers.FindAsync(updatedOffer.Id);
            if (offer == null) return NotFound();

            if (ModelState.IsValid)
            {
                offer.Title = updatedOffer.Title;
                offer.Description = updatedOffer.Description;
                offer.Country = updatedOffer.Country;
                offer.OfferType = updatedOffer.OfferType;
                offer.Price = updatedOffer.Price;
                offer.Days = updatedOffer.Days;
                offer.ExpiryDate = updatedOffer.ExpiryDate;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    offer.ImageUrl = $"/img/{fileName}";
                }

                _context.Offers.Update(offer);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Offer updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(updatedOffer);
        }

        // ✅ عرض صفحة تأكيد الحذف
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            return offer == null ? NotFound() : View(offer);
        }

        // ✅ تنفيذ الحذف
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null) return NotFound();

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            TempData["Success"] = "🗑️ Offer deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
