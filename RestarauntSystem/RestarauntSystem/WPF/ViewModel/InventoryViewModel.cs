using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class InventoryViewModel : ObservableObject
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        [ObservableProperty]
        private ObservableCollection<Inventory> _inventoryItems;

        [ObservableProperty]
        private ObservableCollection<Product> _products;

        [ObservableProperty]
        private ObservableCollection<Supplier> _suppliers;

        [ObservableProperty]
        private Inventory _selectedInventoryItem;

        public IAsyncRelayCommand LoadInventoryCommand { get; }
        public IAsyncRelayCommand UpdateStockCommand { get; }
        public IAsyncRelayCommand OrderSuppliesCommand { get; }

        public InventoryViewModel(
            IInventoryService inventoryService,
            IProductService productService,
            ISupplierService supplierService)
        {
            _inventoryService = inventoryService;
            _productService = productService;
            _supplierService = supplierService;

            InventoryItems = new ObservableCollection<Inventory>();
            Products = new ObservableCollection<Product>();
            Suppliers = new ObservableCollection<Supplier>();

            LoadInventoryCommand = new AsyncRelayCommand(LoadInventoryAsync);
            UpdateStockCommand = new AsyncRelayCommand(UpdateStockAsync);
        }

        private async Task LoadInventoryAsync()
        {
            try
            {
                var inventory = await _inventoryService.GetCurrentInventoryAsync();
                var products = await _productService.GetAllAsync();
                var suppliers = await _supplierService.GetAllAsync();

                InventoryItems.Clear();
                foreach (var item in inventory)
                {
                    InventoryItems.Add(item);
                }

                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }

                Suppliers.Clear();
                foreach (var supplier in suppliers)
                {
                    Suppliers.Add(supplier);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке инвентаря: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateStockAsync()
        {
            if (SelectedInventoryItem == null || StockAdjustment == 0)
            {
                MessageBox.Show("Выберите товар и укажите изменение количества", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _inventoryService.UpdateInventoryAsync(
                    SelectedInventoryItem.ProductId,
                    StockAdjustment);

                await LoadInventoryAsync();
                MessageBox.Show("Запас обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                StockAdjustment = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении запаса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        [ObservableProperty]
        private Product _selectedProduct;

        [ObservableProperty]
        private Supplier _selectedSupplier;

        [ObservableProperty]
        private decimal _stockAdjustment;

        [ObservableProperty]
        private decimal _orderQuantity;
    }
}
