﻿using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Models;
using System.Reflection;

namespace RestarauntSystem.Infrastructure.Data
{
    public class RestaurantDbContext : DbContext
    {
        // Таблицы
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<DishComponent> DishComponents { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<TableStatus> TableStatuses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FeedbackEntry> FeedbackEntries { get; set; }
        public DbSet<EntryType> EntryTypes { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConvertToSnakeCase(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // Применение всех конфигураций из сборки
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Настройка составных ключей
            modelBuilder.Entity<DishComponent>()
                .HasKey(dc => new { dc.DishId, dc.ProductId });

            modelBuilder.Entity<DeliveryItem>()
                .HasKey(di => new { di.DeliveryId, di.ProductId });

            modelBuilder.Entity<SupplierProduct>()
                .HasKey(sp => new { sp.SupplierId, sp.ProductId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.DishId });

            // Настройка отношений один-к-одному
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);




            // Конфигурация для Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(r => r.ReservationStatus) // Связь с ReservationStatus
                      .WithMany(s => s.Reservations)
                      .HasForeignKey(r => r.StatusId)
                      .HasConstraintName("reservations_status_id_fkey");
            });

            // Настройка каскадного удаления
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .OnDelete(DeleteBehavior.Cascade);
            // Настройка индексов
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderTime);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.ReservationTime);

            // Настройка ограничений
            modelBuilder.Entity<DishComponent>()
                .Property(dc => dc.Quantity)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .HasAnnotation("Range", new[] { 1, 5 });

            modelBuilder.Entity<Employee>()
            .Property(e => e.MiddleName)
            .IsRequired(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=restaurant_db;Username=postgres;Password=sa");
            }
        }
        private void ConvertToSnakeCase(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {

                // Преобразование имен столбцов
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()));
                }
            }
        }
        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return string.Concat(input.Select((c, i) =>
                i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString()))
                .ToLower();
        }
    }
}
