using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Models;
using System.Text.Json;

namespace ByArabianEye.Controllers
{
    public class AccountController : Controller
    {
        private readonly string dataPath;

        public AccountController(IWebHostEnvironment env)
        {
            dataPath = Path.Combine(env.WebRootPath, "data", "clients.json");
        }

        // صفحة تسجيل الدخول
        public IActionResult Login()
        {
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            return View();
        }

        // POST تسجيل الدخول
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // ✅ دخول المدير
            if (username == "Admin" && password == "4654156")
            {
                HttpContext.Session.SetString("Role", "admin");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Dashboard");
            }

            // ✅ دخول العميل
            var clients = ReadClients();
            var found = clients.FirstOrDefault(c => c.Username == username && c.Password == password);

            if (found != null)
            {
                HttpContext.Session.SetString("Role", "client");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Home");
            }

            // ❌ بيانات غير صحيحة
            ViewBag.Error = "❌ Incorrect username or password | ❌ اسم المستخدم أو كلمة المرور خاطئة";
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // 🧼 تنظيف الجلسة
            return RedirectToAction("Login");
        }

        // صفحة التسجيل
        public IActionResult Register()
        {
            return View();
        }

        // POST التسجيل
        [HttpPost]
        public IActionResult Register(string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "❌ Passwords do not match | ❌ كلمات المرور غير متطابقة";
                return View();
            }

            var clients = ReadClients();

            if (clients.Any(c => c.Username == username))
            {
                ViewBag.Error = "❌ Username already exists | ❌ اسم المستخدم موجود مسبقاً";
                return View();
            }

            // ✅ إضافة تاريخ التسجيل
            clients.Add(new Client
            {
                Username = username,
                Password = password,
                RegistrationDate = DateTime.Now
            });

            System.IO.File.WriteAllText(dataPath, JsonSerializer.Serialize(clients));

            TempData["Success"] = "✅ Registration successful! | ✅ تم التسجيل بنجاح";
            return RedirectToAction("Login");
        }

        // قراءة بيانات العملاء من clients.json
        private List<Client> ReadClients()
        {
            if (!System.IO.File.Exists(dataPath))
                return new List<Client>();

            var json = System.IO.File.ReadAllText(dataPath);
            return JsonSerializer.Deserialize<List<Client>>(json) ?? new List<Client>();
        }
    }
}
