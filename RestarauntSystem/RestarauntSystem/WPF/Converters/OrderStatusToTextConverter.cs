using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestarauntSystem.WPF.Converters
{
    public class OrderStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int statusId)
            {
                return statusId switch
                {
                    1 => "Принят",
                    2 => "Готовится",
                    3 => "Готов к подаче",
                    4 => "Подано",
                    5 => "Оплачено",
                    6 => "Отменен",
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
