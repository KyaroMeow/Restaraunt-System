using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Infrastructure.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using Microsoft.Win32;
using RestarauntSystem.Infrastructure.Interfaces;
using System.Text;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;
        private readonly IJsonAdapter<DataTable> _jsonAdapter;

        public MainViewModel(DatabaseService dbService, IJsonAdapter<DataTable> jsonAdapter)
        {
            _dbService = dbService;
            _jsonAdapter = jsonAdapter;

            InitializeCommands();
            LoadTableNamesAsync();
        }
        private void InitializeCommands()
        {
            DeleteCommand = new AsyncRelayCommand(DeleteRecordAsync);
            SaveCommand = new AsyncRelayCommand(SaveChangesAsync);
            ImportCommand = new AsyncRelayCommand(ImportDataAsync);
            ExportCommand = new AsyncRelayCommand(ExportDataAsync);
            RefreshCommand = new AsyncRelayCommand(RefreshDataAsync);
        }

        // Свойства
        [ObservableProperty]
        private List<string> _tableNames;

        [ObservableProperty]
        private string _selectedTableName;

        [ObservableProperty]
        private DataTable _currentTable;

        [ObservableProperty]
        private DataRowView _selectedRow;

        // Команды (явное определение без атрибутов)
        public IAsyncRelayCommand DeleteCommand { get; private set; }
        public IAsyncRelayCommand SaveCommand { get; private set; }
        public IAsyncRelayCommand ImportCommand { get; private set; }
        public IAsyncRelayCommand ExportCommand { get; private set; }
        public IAsyncRelayCommand RefreshCommand { get; private set; }

        // Обработчик изменения выбранной таблицы
        partial void OnSelectedTableNameChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                LoadTableDataAsync(value);
            }
        }

        private async void LoadTableNamesAsync()
        {
            try
            {
                using var connection = await _dbService.GetConnectionAsync();
                TableNames = (await connection.QueryAsync<string>(
                    "SELECT table_name FROM information_schema.tables " +
                    "WHERE table_schema = 'public' AND table_type = 'BASE TABLE'"))
                    .ToList();

                SelectedTableName = TableNames.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка таблиц: {ex.Message}");
            }
        }

        private async void LoadTableDataAsync(string tableName)
        {
            try
            {
                CurrentTable = await _dbService.GetTableDataAsync(tableName);
                CurrentTable.TableName = tableName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных таблицы: {ex.Message}");
            }
        }

        private async Task DeleteRecordAsync()
        {
            if (SelectedRow == null) return;

            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить эту запись?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var pkValues = new Dictionary<string, object>();
                    var primaryKeys = await _dbService.GetPrimaryKeyColumnsAsync(
                        await _dbService.GetConnectionAsync(),
                        CurrentTable.TableName);

                    foreach (var pk in primaryKeys)
                    {
                        pkValues[pk] = SelectedRow[pk];
                    }

                    var success = await _dbService.DeleteRowByPkAsync(
                        CurrentTable.TableName,
                        pkValues);

                    if (success)
                    {
                        CurrentTable.Rows.Remove(SelectedRow.Row);
                        MessageBox.Show("Запись успешно удалена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _dbService.UpdateTableAsync(CurrentTable);
                MessageBox.Show("Изменения успешно сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        public async Task ImportDataAsync()
        {
            try
            {
                var openDialog = new OpenFileDialog
                {
                    Filter = "JSON files (*.json)|*.json",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (openDialog.ShowDialog() == true)
                {
                    string json = await File.ReadAllTextAsync(openDialog.FileName);
                    DataTable importedData = _jsonAdapter.Deserialize(json);

                    // Проверка структуры (сравниваем только имена колонок без учёта регистра)
                    var currentColumns = CurrentTable.Columns.Cast<DataColumn>()
                        .Select(c => c.ColumnName.ToLower()).ToList();

                    var importedColumns = importedData.Columns.Cast<DataColumn>()
                        .Select(c => c.ColumnName.ToLower()).ToList();

                    if (!currentColumns.SequenceEqual(importedColumns))
                    {
                        // Детальное сообщение об ошибке
                        var errorMsg = new StringBuilder();
                        errorMsg.AppendLine("Ошибка: структура таблицы не совпадает!");
                        errorMsg.AppendLine($"Ожидаемые колонки: {string.Join(", ", currentColumns)}");
                        errorMsg.AppendLine($"Полученные колонки: {string.Join(", ", importedColumns)}");

                        MessageBox.Show(errorMsg.ToString());
                        return;
                    }

                    // Копируем данные с проверкой типов
                    foreach (DataRow importedRow in importedData.Rows)
                    {
                        DataRow newRow = CurrentTable.NewRow();

                        foreach (DataColumn column in CurrentTable.Columns)
                        {
                            try
                            {
                                newRow[column.ColumnName] = importedRow[column.ColumnName];
                            }
                            catch
                            {
                                // Если тип не совпадает, преобразуем в строку
                                object value = importedRow[column.ColumnName];
                                newRow[column.ColumnName] = value != null ? value.ToString() : (object)DBNull.Value;
                            }
                        }

                        CurrentTable.Rows.Add(newRow);
                    }

                    MessageBox.Show($"Добавлено {importedData.Rows.Count} строк");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта: {ex.Message}\n\nДетали:\n{ex.InnerException?.Message}");
            }
        }

        private async Task ExportDataAsync()
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json",
                    FileName = $"{CurrentTable.TableName}_export.json",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (saveDialog.ShowDialog() == true)
                {
                    await _jsonAdapter.ExportToFileAsync(CurrentTable, saveDialog.FileName);
                    MessageBox.Show($"Данные экспортированы в {saveDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}");
            }
        }

        private async Task RefreshDataAsync()
        {
            if (!string.IsNullOrEmpty(SelectedTableName))
            {
                LoadTableDataAsync(SelectedTableName);
            }
        }
    }
}