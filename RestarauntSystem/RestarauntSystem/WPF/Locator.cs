using Microsoft.Extensions.DependencyInjection;
using RestarauntSystem.Infrastructure.Data;
using RestarauntSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using RestarauntSystem.WPF.ViewModel;
using RestarauntSystem.Infrastructure.Interfaces;
using System.Data;


namespace RestarauntSystem.WPF
{
    public class Locator
    {
        private static readonly ServiceProvider _serviceProvider;

        static Locator()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<IJsonAdapter<DataTable>, DataTableJsonAdapter>();
            services.AddSingleton<DatabaseService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        // 6. Свойства для доступа к ViewModels
        public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();


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
