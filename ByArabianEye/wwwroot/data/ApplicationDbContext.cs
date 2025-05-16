using Microsoft.EntityFrameworkCore;
using ByArabianEye.Models;

namespace ByArabianEye.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
