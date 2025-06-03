using ByArabianEye.Data;
using ByArabianEye.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ByArabianEye.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Azerbaijan()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Azerbaijan")
                .ToList();

            return View(offers);
        }

        public IActionResult Georgia()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Georgia")
                .ToList();

            return View(offers);
        }

        public IActionResult Albania()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Albania")
                .ToList();

            return View(offers);
        }

        public IActionResult Kazakhstan()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Kazakhstan")
                .ToList();

            return View(offers);
        }

        public IActionResult Russia()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Russia")
                .ToList();

            return View(offers);
        }

        public IActionResult Uzbekistan()
        {
            var offers = _context.CountryOffers
                .Where(o => o.Country == "Uzbekistan")
                .ToList();

            return View(offers);
        }
    }
}
