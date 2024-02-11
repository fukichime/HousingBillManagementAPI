using HousingBillManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HousingBillManagement.API.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext(DbSet<Apartment> apartments, DbSet<Bill> bills, DbSet<Payment> payments)
        {
            Apartments = apartments;
            Bills = bills;
            Payments = payments;
        }

        public DbSet<Apartment> Apartments { get; set; } = default!;
        public DbSet<Bill> Bills { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
    }
}
