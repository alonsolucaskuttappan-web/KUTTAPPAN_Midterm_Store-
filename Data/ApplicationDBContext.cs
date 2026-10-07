using Microsoft.EntityFrameworkCore;
using KUTTAPPAN_Midterm_Store.Models;

namespace KUTTAPPAN_Midterm_Store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}