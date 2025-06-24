using Microsoft.Extensions.DependencyInjection;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;
using RestarauntSystem.Infrastructure.Repositories;
using RestarauntSystem.Infrastructure.Services;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RestarauntSystem.WPF
{
    public class Locator
    {
        private static readonly ServiceProvider _serviceProvider;

        static Locator()
        {
            var services = new ServiceCollection();

            // 1. Регистрация контекста базы данных
            services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Database=restaurant_db;Username=restaurant_admin;Password=admin1234");
            });

            // 2. Регистрация репозиториев
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            // 3. Регистрация сервисов
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IInventoryService, InventoryService>();

            // 4. Регистрация ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<TablesViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<ReservationsViewModel>();
            services.AddTransient<EmployeesViewModel>();
            services.AddTransient<InventoryViewModel>();

            // 5. Специальные сервисы
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        // 6. Свойства для доступа к ViewModels
        public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();
        public OrdersViewModel OrdersViewModel => _serviceProvider.GetRequiredService<OrdersViewModel>();
        public TablesViewModel TablesViewModel => _serviceProvider.GetRequiredService<TablesViewModel>();
        public MenuViewModel MenuViewModel => _serviceProvider.GetRequiredService<MenuViewModel>();
        public ReservationsViewModel ReservationsViewModel => _serviceProvider.GetRequiredService<ReservationsViewModel>();
        public EmployeesViewModel EmployeesViewModel => _serviceProvider.GetRequiredService<EmployeesViewModel>();
        public InventoryViewModel InventoryViewModel => _serviceProvider.GetRequiredService<InventoryViewModel>();
        public ReportsViewModel ReportsViewModel => _serviceProvider.GetRequiredService<ReportsViewModel>();

        // 7. Метод для ручного разрешения зависимостей
        public static T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        // 8. Метод для очистки ресурсов
        public static void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
