using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Models;

namespace RestarauntSystem.Infrastructure.Data
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Employee> Employees { get; set; }
        // Остальные DbSet...

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=restaurant_db;Username=restaurant_admin;Password=admin1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурации моделей
            // Аналогично для других сущностей
        }
    }
}
