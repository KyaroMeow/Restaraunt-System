using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestarauntSystem.WPF.Converters
{
    public class DishCategoryIdToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int statusId)
            {
                return statusId switch
                {
                    1 => "Закуски",
                    2 => "Салаты",
                    3 => "Супы",
                    4 => "Основные блюда",
                    5 => "Гарниры",
                    6 => "Десерты",
                    7 => "Напитки",
                    8 => "Алкогольные напитки",
                    9 => "Детское меню",
                    10 => "Бизнес-ланч",
                    _ => "Неизвестно"
                };
            }
            return "Неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
