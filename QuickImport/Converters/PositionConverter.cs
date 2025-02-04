using System.Globalization;
using System.Windows.Data;

namespace QuickImport.Converters;

public class PositionConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2 &&
            values[0] is double position &&
            values[1] is double size &&
            parameter is string axis)
        {
            double scaleFactor = 20; // Масштаб
            double scaledPos = position * scaleFactor;
            double scaledSize = Math.Min(size * scaleFactor, 400); // Ограничиваем размер

            return scaledPos - (scaledSize / 2); // Центрируем
        }
        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
