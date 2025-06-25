
using System.Globalization;
using System.Windows.Data;

namespace RestarauntSystem.WPF.Converters
{
    public class NullToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] texts = parameter.ToString().Split('|');
            return value == null ? texts[0] : texts[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
