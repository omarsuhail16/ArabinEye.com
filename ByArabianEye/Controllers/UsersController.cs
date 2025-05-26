using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Data;
using ByArabianEye.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ByArabianEye.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: /Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // حذف مستخدم (اختياري)
        // GET: /Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
                _context.Users.Remove(user);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
