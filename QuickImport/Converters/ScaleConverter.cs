using System.Globalization;
using System.Windows.Data;

namespace QuickImport.Converters;

public class ScaleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double val && parameter is string factorStr && double.TryParse(factorStr, out double factor))
        {
            double scaled = val * factor;
            return Math.Min(scaled, 400); // Ограничиваем размер
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
