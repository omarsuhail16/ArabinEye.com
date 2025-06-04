using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Data;
using ByArabianEye.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ByArabianEye.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Email", user.Email); // ✅ تم إضافته هنا
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "admin")
                    return RedirectToAction("Index", "Dashboard");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = " Incorrect email or password.";
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string fullName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = " Passwords do not match.";
                return View();
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email already in use.";
                return View();
            }

            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                Role = "client"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["Success"] = "Your account has been created successfully.";
            return RedirectToAction("Login");
        }

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/MyProfile
        public IActionResult MyProfile()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound();

            var bookings = _context.Bookings
                .Where(b => b.ClientName == user.FullName)
                .ToList();

            ViewBag.Bookings = bookings;

            return View(user);
        }

        // GET
        public IActionResult EditProfile()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST
        [HttpPost]
        public IActionResult EditProfile(User updatedUser)
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound();

            // تحقق من البريد الجديد ما يكون مستخدم من غير هذا المستخدم
            bool emailExists = _context.Users.Any(u => u.Email == updatedUser.Email && u.UserId != user.UserId);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "This email is already used by another user.");
                return View(user);
            }

            // تحديث الاسم والبريد الإلكتروني
            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;

            // تحديث كلمة المرور إذا كتبها
            if (!string.IsNullOrEmpty(updatedUser.Password))
                user.Password = updatedUser.Password;

            _context.SaveChanges();

            // تحديث الجلسة
            HttpContext.Session.SetString("FullName", user.FullName);
            HttpContext.Session.SetString("Email", user.Email);

            TempData["Success"] = "✅ Profile updated successfully.";
            return RedirectToAction("MyProfile");
        }
    }
}
