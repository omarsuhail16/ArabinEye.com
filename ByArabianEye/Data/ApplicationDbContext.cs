using Microsoft.EntityFrameworkCore;
using ByArabianEye.Models;
using System;

namespace ByArabianEye.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }

        // إضافة DbSet لجدول سجل حذف الحجوزات
        public DbSet<BookingDeletionLog> BookingDeletionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    ClientCode = "C001",
                    ClientName = "Ahmed Ali",
                    PhoneNumber = "0551234567",
                    Country = "Saudi Arabia",
                    To = "Azerbaijan",
                    DriverName = "Kamal",
                    ArrivalDate = DateTime.Today.AddDays(2),
                    DepartureDate = DateTime.Today.AddDays(6)
                },
                new Customer
                {
                    Id = 2,
                    ClientCode = "C002",
                    ClientName = "Sara Mohammed",
                    PhoneNumber = "0509876543",
                    Country = "UAE",
                    To = "Russia",
                    DriverName = "Murad",
                    ArrivalDate = DateTime.Today.AddDays(1),
                    DepartureDate = DateTime.Today.AddDays(4)
                }
            );
        }
    }
}
