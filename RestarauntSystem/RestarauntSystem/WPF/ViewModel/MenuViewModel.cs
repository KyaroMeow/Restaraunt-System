using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class MenuViewModel : ObservableObject
    {
        private readonly IDishService _dishService;
        private readonly IDishCategoryService _categoryService;

        [ObservableProperty]
        private ObservableCollection<Dish> _dishes;

        [ObservableProperty]
        private ObservableCollection<DishCategory> _categories;

        [ObservableProperty]
        private Dish _selectedDish;

        [ObservableProperty]
        private DishCategory _selectedCategory;

        public IAsyncRelayCommand LoadMenuCommand { get; }
        public IAsyncRelayCommand AddDishCommand { get; }
        public IAsyncRelayCommand UpdateDishCommand { get; }
        public IAsyncRelayCommand DeleteDishCommand { get; }

        public MenuViewModel(IDishService dishService, IDishCategoryService categoryService)
        {
            _dishService = dishService;
            _categoryService = categoryService;

            Dishes = new ObservableCollection<Dish>();
            Categories = new ObservableCollection<DishCategory>();

            LoadMenuCommand = new AsyncRelayCommand(LoadMenuAsync);
            AddDishCommand = new AsyncRelayCommand(AddDishAsync);
            UpdateDishCommand = new AsyncRelayCommand(UpdateDishAsync);
            DeleteDishCommand = new AsyncRelayCommand(DeleteDishAsync);
        }

        private async Task LoadMenuAsync()
        {
            try
            {
                var dishes = await _dishService.GetAllAsync();
                var categories = await _categoryService.GetAllAsync();

                Dishes.Clear();
                foreach (var dish in dishes)
                {
                    Dishes.Add(dish);
                }

                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке меню: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddDishAsync()
        {
            if (string.IsNullOrWhiteSpace(NewDishName) || SelectedCategory == null)
            {
                MessageBox.Show("Введите название блюда и выберите категорию", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var dish = new Dish
                {
                    DishName = NewDishName,
                    Description = NewDishDescription,
                    CategoryId = SelectedCategory.CategoryId,
                    Price = NewDishPrice
                };

                await _dishService.AddAsync(dish);
                Dishes.Add(dish);

                // Сброс формы
                NewDishName = string.Empty;
                NewDishDescription = string.Empty;
                NewDishPrice = 0;

                MessageBox.Show("Блюдо успешно добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateDishAsync()
        {
            if (SelectedDish == null) return;

            try
            {
                SelectedDish.CategoryId = SelectedCategory?.CategoryId ?? SelectedDish.CategoryId;
                await _dishService.UpdateAsync(SelectedDish);
                MessageBox.Show("Блюдо успешно обновлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteDishAsync()
        {
            if (SelectedDish == null) return;

            try
            {
                await _dishService.DeleteAsync(SelectedDish.DishId);
                Dishes.Remove(SelectedDish);
                MessageBox.Show("Блюдо успешно удалено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [ObservableProperty]
        private string _newDishName;

        [ObservableProperty]
        private string _newDishDescription;

        [ObservableProperty]
        private decimal _newDishPrice;
    }
}
