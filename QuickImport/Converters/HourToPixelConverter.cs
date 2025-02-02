using System.Globalization;
using System.Windows.Data;

namespace QuickImport.Converters;

public class HourToPixelConverter : IValueConverter
{
    // 1 час = 20 пикселей (12 ч → 240 пикселей)
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double hours)
        {
            return hours * 20;
        }
        return 0;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
