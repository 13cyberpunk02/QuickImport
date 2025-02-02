using System.Globalization;
using System.Windows.Data;

namespace QuickImport.Converters;

public class MeterToPixelConverter : IValueConverter
{
    // 1 метр = 20 пикселей (20 м → 400 пикселей)
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double meters)
        {
            return meters * 20;
        }
        return 0;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}