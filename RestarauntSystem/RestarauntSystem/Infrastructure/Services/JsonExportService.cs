using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using RestarauntSystem.Core.Models;
using System.Collections;
using System.Reflection;

namespace RestarauntSystem.Infrastructure.Services
{
    public class JsonExportService
    {
        private bool IsNavigationProperty(PropertyInfo prop)
        {
            // Игнорируем:
            // 1. Все коллекции (ICollection<T>, List<T> и т.д.)
            // 2. Все классы, кроме строк и DateTime
            // 3. Виртуальные свойства (часто используются для ленивой загрузки)

            return (prop.PropertyType.IsClass &&
                   prop.PropertyType != typeof(string) &&
                   prop.PropertyType != typeof(DateTime) &&
                   prop.PropertyType != typeof(DateTime?)) ||
                   (prop.PropertyType.IsGenericType &&
                   typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)) ||
                   prop.GetMethod?.IsVirtual == true;
        }
        public async Task ExportToFileAsync<T>(string tableName, List<T> data, string directoryPath)
        {
            var filteredData = data.Select(item =>
            {
                var dict = new Dictionary<string, object>();
                foreach (var prop in typeof(T).GetProperties()
                         .Where(p => !IsNavigationProperty(p)))
                {
                    dict[prop.Name] = prop.GetValue(item) ?? string.Empty;
                }
                return dict;
            }).ToList();

            try
            {
                // Создаем шаблон для экспорта
                var exportData = new JsonExportTemplate<T>
                {
                    TableName = tableName,
                    RecordCount = data.Count,
                    Records = data
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Формируем имя файла
                var fileName = $"{tableName.ToLower()}_export_{DateTime.Now:yyyyMMddHHmmss}.json";
                var fullPath = Path.Combine(directoryPath, fileName);

                // Создаем директорию, если ее нет
                Directory.CreateDirectory(directoryPath);

                // Сериализуем и сохраняем
                var json = JsonSerializer.Serialize(exportData, options);
                await File.WriteAllTextAsync(fullPath, json);

                Console.WriteLine($"Data exported successfully to {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export error: {ex.Message}");
                throw;
            }

        }
    }
}
