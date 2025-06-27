using Microsoft.Extensions.DependencyInjection;
using RestarauntSystem.Infrastructure.Data;
using RestarauntSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;
using RestarauntSystem.Infrastructure.Repositories;
using RestarauntSystem.WPF.ViewModel;


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
                options.UseNpgsql("Host=localhost;Database=restaurant_db;Username=postgres;Password=sa");
            });

            // 2. Регистрация репозиториев
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<IDishCategoryRepository, DishCategoryRepository>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IWorkScheduleRepository, WorkScheduleRepository>();



            // 3. Регистрация сервисов
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDishCategoryService, DishCategoryService>();
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ITableService, TableService>();

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
            services.AddSingleton<JsonExportService>();
            services.AddSingleton<JsonImportService>();
            services.AddSingleton<DatabaseService>();

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
