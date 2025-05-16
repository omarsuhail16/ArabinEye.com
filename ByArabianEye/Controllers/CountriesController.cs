using Microsoft.AspNetCore.Mvc;

namespace ByArabianEye.Controllers
{
    public class CountriesController : Controller
    {
        public IActionResult Azerbaijan() => View();
        public IActionResult Georgia() => View();
        public IActionResult Albania() => View();
        public IActionResult Kazakhstan() => View();
        public IActionResult Russia() => View();
        public IActionResult Uzbekistan() => View();
    }
}