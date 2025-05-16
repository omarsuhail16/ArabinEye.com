using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Models;
using System.Text.Json;

namespace ByArabianEye.Controllers
{
    public class OffersController : Controller
    {
        private readonly string dataPath;

        public OffersController(IWebHostEnvironment env)
        {
            dataPath = Path.Combine(env.WebRootPath, "data", "offers.json");
        }

        public IActionResult Index()
        {
            var offers = ReadOffers();
            return View(offers);
        }

        public IActionResult OffersClient()
        {
            var offers = ReadOffers();
            return View("OffersClient", offers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Offer offer, IFormFile ImageFile)
        {
            var offers = ReadOffers();
            offer.Id = offers.Any() ? offers.Max(o => o.Id) + 1 : 1;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                offer.ImageUrl = $"/img/{fileName}";
            }

            offers.Add(offer);
            System.IO.File.WriteAllText(dataPath, JsonSerializer.Serialize(offers, new JsonSerializerOptions { WriteIndented = true }));

            TempData["Success"] = "✅ Offer added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var offer = ReadOffers().FirstOrDefault(o => o.Id == id);
            return offer == null ? NotFound() : View(offer);
        }

        public IActionResult Edit(int id)
        {
            var offer = ReadOffers().FirstOrDefault(o => o.Id == id);
            return offer == null ? NotFound() : View(offer);
        }

        [HttpPost]
        public IActionResult Edit(Offer updatedOffer)
        {
            var offers = ReadOffers();
            var existing = offers.FirstOrDefault(o => o.Id == updatedOffer.Id);
            if (existing == null) return NotFound();

            existing.Title = updatedOffer.Title;
            existing.Description = updatedOffer.Description;
            existing.Country = updatedOffer.Country;
            existing.OfferType = updatedOffer.OfferType;
            existing.Price = updatedOffer.Price;
            existing.Days = updatedOffer.Days;
            existing.ExpiryDate = updatedOffer.ExpiryDate;
            // ملاحظة: لا نغير الصورة إلا إذا أضفنا رفع جديد

            System.IO.File.WriteAllText(dataPath, JsonSerializer.Serialize(offers, new JsonSerializerOptions { WriteIndented = true }));
            TempData["Success"] = "✅ Offer updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var offer = ReadOffers().FirstOrDefault(o => o.Id == id);
            return offer == null ? NotFound() : View(offer);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var offers = ReadOffers();
            var offer = offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            offers.Remove(offer);
            System.IO.File.WriteAllText(dataPath, JsonSerializer.Serialize(offers, new JsonSerializerOptions { WriteIndented = true }));
            TempData["Success"] = "🗑️ Offer deleted successfully!";
            return RedirectToAction("Index");
        }

        private List<Offer> ReadOffers()
        {
            if (!System.IO.File.Exists(dataPath))
                return new List<Offer>();

            var json = System.IO.File.ReadAllText(dataPath);
            return JsonSerializer.Deserialize<List<Offer>>(json) ?? new List<Offer>();
        }
    }
}
