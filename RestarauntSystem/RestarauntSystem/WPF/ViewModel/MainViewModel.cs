using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Infrastructure.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dapper;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;
        private readonly JsonExportService _exportService;
        private readonly JsonImportService _importService;

        public MainViewModel()
        {
            _dbService = new DatabaseService();
            _exportService = new JsonExportService();
            _importService = new JsonImportService();

            // Инициализация команд
            DeleteCommand = new AsyncRelayCommand(DeleteRecordAsync);
            SaveCommand = new AsyncRelayCommand(SaveChangesAsync);
            ImportCommand = new AsyncRelayCommand(ImportDataAsync);
            ExportCommand = new AsyncRelayCommand(ExportDataAsync);
            RefreshCommand = new AsyncRelayCommand(RefreshDataAsync);

            LoadTableNamesAsync();
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
        public IAsyncRelayCommand DeleteCommand { get; }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand ImportCommand { get; }
        public IAsyncRelayCommand ExportCommand { get; }
        public IAsyncRelayCommand RefreshCommand { get; }

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

        private async Task ImportDataAsync()
        {
            try
            {
                var openDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "JSON files (*.json)|*.json",
                    InitialDirectory = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.MyDocuments)
                };

                if (openDialog.ShowDialog() == true)
                {
                    var importedData = await _importService.ImportFromFileAsync(openDialog.FileName);
                    await _dbService.ImportDataAsync(CurrentTable.TableName, importedData);
                    await RefreshDataAsync();
                    MessageBox.Show($"Импортировано {importedData.Count} записей");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта: {ex.Message}");
            }
        }

        private async Task ExportDataAsync()
        {
            try
            {
                var savePath = System.IO.Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                    "RestaurantSystemExports");

                var data = CurrentTable.AsEnumerable().Select(row =>
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn column in CurrentTable.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    return dict;
                }).ToList();

                await _exportService.ExportToFileAsync(
                    CurrentTable.TableName,
                    data,
                    savePath);

                MessageBox.Show($"Данные экспортированы в {savePath}");
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