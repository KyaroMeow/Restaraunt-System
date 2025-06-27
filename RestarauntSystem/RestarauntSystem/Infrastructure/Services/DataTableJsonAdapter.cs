using RestarauntSystem.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestarauntSystem.Infrastructure.Services
{
    public class DataTableJsonAdapter : IJsonAdapter<DataTable>
    {
        private readonly JsonSerializerOptions _options;

        public DataTableJsonAdapter()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public string Serialize(DataTable data)
        {
            var exportData = new
            {
                TableName = data.TableName,
                Rows = data.Rows.Cast<DataRow>().Select(row =>
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in data.Columns)
                    {
                        dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                    }
                    return dict;
                })
            };
            return JsonSerializer.Serialize(exportData, _options);
        }

        public DataTable Deserialize(string json)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Проверяем наличие обязательных полей
                if (!root.TryGetProperty("tableName", out var tableNameProp) ||
                    !root.TryGetProperty("rows", out var rowsProp))
                {
                    throw new InvalidDataException("Invalid JSON format - missing required fields");
                }

                var table = new DataTable(tableNameProp.GetString());

                // Добавляем колонки (только если есть строки)
                if (rowsProp.ValueKind == JsonValueKind.Array && rowsProp.GetArrayLength() > 0)
                {
                    var firstRow = rowsProp.EnumerateArray().First();
                    foreach (var prop in firstRow.EnumerateObject())
                    {
                        // Определяем тип данных колонки
                        Type columnType = prop.Value.ValueKind switch
                        {
                            JsonValueKind.String => typeof(string),
                            JsonValueKind.Number => typeof(decimal),
                            JsonValueKind.True => typeof(bool),
                            JsonValueKind.False => typeof(bool),
                            JsonValueKind.Null => typeof(object),
                            _ => typeof(string)
                        };

                        table.Columns.Add(prop.Name, columnType);
                    }

                    // Добавляем строки
                    foreach (var row in rowsProp.EnumerateArray())
                    {
                        var newRow = table.NewRow();
                        foreach (var prop in row.EnumerateObject())
                        {
                            if (table.Columns.Contains(prop.Name))
                            {
                                newRow[prop.Name] = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.GetDecimal(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => prop.Value.ToString()
                                };
                            }
                        }
                        table.Rows.Add(newRow);
                    }
                }

                return table;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error deserializing JSON to DataTable", ex);
            }
        }

        public async Task ExportToFileAsync(DataTable data, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await File.WriteAllTextAsync(filePath, Serialize(data));
        }

        public async Task<DataTable> ImportFromFileAsync(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            return Deserialize(json);
        }
    }
}
