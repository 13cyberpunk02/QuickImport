
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using QuickImport.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace QuickImport.ViewModels;

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
        OpenFileDialog dialog = new OpenFileDialog
        {
            Filter = "Excel файлы|*.xls;*.xlsx|CSV файлы|*.csv",
            Title = "Выберите файл"
        };

        if (dialog.ShowDialog() == true)
        {
            string filename = dialog.FileName;
            try
            {
                if (Path.GetExtension(filename).ToLower() == ".csv")
                {
                    // Простейший импорт CSV (разделитель – запятая)
                    var lines = File.ReadAllLines(filename).Skip(1);
                    foreach (var line in lines)
                    {
                        // Ожидается 6 столбцов: Name, Distance, Angle, Width, Height, IsDefect
                        var parts = line.Split(';');
                        if (parts.Length < 6) continue;

                        Objects.Add(new ObjectData
                        (
                           Name: parts[0], 
                           Distance: double.Parse(parts[1]),
                           Angle: double.Parse(parts[2]),
                           Width: double.Parse(parts[3]),
                           Height: double.Parse(parts[4]),
                           IsDefect: parts[5].ToLower() == "yes"
                        ));
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
                MessageBox.Show($"Ошибка при чтении файла\n: {ex.Message}");
            }
        }
    }
}
