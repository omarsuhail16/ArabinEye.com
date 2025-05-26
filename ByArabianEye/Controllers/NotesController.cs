using Microsoft.AspNetCore.Mvc;
using ByArabianEye.Models;
using ByArabianEye.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace ByArabianEye.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ جلب الملاحظات لعميل
        public async Task<IActionResult> GetNotes(int customerId)
        {
            var notes = await _context.CustomerNotes
                .Where(n => n.CustomerId == customerId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return PartialView("_NotesPartial", notes);
        }

        // ✅ إضافة ملاحظة
        [HttpPost]
        public async Task<IActionResult> AddNote(int customerId, string noteContent)
        {
            if (string.IsNullOrWhiteSpace(noteContent))
                return BadRequest("الملاحظة لا يمكن أن تكون فارغة.");

            var note = new CustomerNote
            {
                CustomerId = customerId,
                NoteContent = noteContent,
                CreatedAt = DateTime.Now
            };

            _context.CustomerNotes.Add(note);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ✅ حذف ملاحظة
        [HttpPost]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.CustomerNotes.FindAsync(id);
            if (note == null)
                return NotFound();

            _context.CustomerNotes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
