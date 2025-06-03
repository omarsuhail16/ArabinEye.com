using ByArabianEye.Data;
using ByArabianEye.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ByArabianEye.Controllers
{
    public class CountryOffersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CountryOffersController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ عرض الصفحة مع كل العروض والنموذج (مقيد للإدمن فقط)
        public IActionResult Manage(int? id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "admin")
                return RedirectToAction("Index", "Home");

            var offers = _context.CountryOffers.ToList();
            CountryOffer selected = id.HasValue
                ? _context.CountryOffers.FirstOrDefault(x => x.Id == id)
                : new CountryOffer();

            ViewBag.SelectedOffer = selected;
            return View("Manage", offers);
        }

        // ✅ حفظ (إضافة أو تعديل) العرض (مقيد للإدمن فقط)
        [HttpPost]
        public async Task<IActionResult> Manage(CountryOffer offer, IFormFile ImageFile)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "admin")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var path = Path.Combine(_env.WebRootPath, "img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    offer.ImageUrl = $"/img/{fileName}";
                }

                if (offer.Id == 0)
                    _context.CountryOffers.Add(offer);
                else
                    _context.CountryOffers.Update(offer);

                await _context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }

            var offers = _context.CountryOffers.ToList();
            ViewBag.SelectedOffer = offer;
            return View("Manage", offers);
        }

        // ✅ حذف العرض (مقيد للإدمن فقط)
        public IActionResult Delete(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "admin")
                return RedirectToAction("Index", "Home");

            var offer = _context.CountryOffers.Find(id);
            if (offer == null)
                return NotFound();

            _context.CountryOffers.Remove(offer);
            _context.SaveChanges();
            return RedirectToAction("Manage");
        }
    }
}
