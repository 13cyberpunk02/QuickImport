
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using QuickImport.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace QuickImport.ModelViews;

public partial class QuickImportViewModel : ObservableObject
{
    // Коллекция объектов для DataGrid
    public ObservableCollection<ObjectData> Objects { get; } = new ObservableCollection<ObjectData>();

    // Выбранный объект (для отображения деталей)
    [ObservableProperty]
    private ObjectData? selectedObject;

    // Команда импорта файла
    [RelayCommand]
    private void ImportFile()
    {
        OpenFileDialog dlg = new OpenFileDialog
        {
            Filter = "Excel файлы|*.xls;*.xlsx|CSV файлы|*.csv",
            Title = "Выберите файл"
        };

        if (dlg.ShowDialog() == true)
        {
            string filename = dlg.FileName;
            try
            {
                if (Path.GetExtension(filename).ToLower() == ".csv")
                {
                    // Простейший импорт CSV (разделитель – запятая)
                    var lines = File.ReadAllLines(filename);
                    foreach (var line in lines)
                    {
                        // Ожидается 6 столбцов: Name, Distance, Angle, Width, Height, IsDefect
                        var parts = line.Split(',');
                        if (parts.Length < 6) continue;

                        if (double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double distance) &&
                            double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double angle) &&
                            double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double width) &&
                            double.TryParse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double height) &&
                            bool.TryParse(parts[5], out bool isDefect))
                        {
                            Objects.Add(new ObjectData(parts[0], distance, angle, width, height, isDefect));
                        }
                    }
                }
                else
                {
                    // Для Excel можно использовать библиотеки, например, ExcelDataReader или EPPlus.
                    MessageBox.Show("Импорт Excel не реализован в данном примере.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте файла: {ex.Message}");
            }
        }
    }
}
