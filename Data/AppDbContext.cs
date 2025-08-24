using Microsoft.EntityFrameworkCore;
using Pc1_Linares.Models;

namespace Pc1_Linares.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductoCredito> ProductosCredito { get; set; }
    }
}
