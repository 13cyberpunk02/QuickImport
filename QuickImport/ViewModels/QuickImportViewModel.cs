
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelDataReader;
using Microsoft.Win32;
using QuickImport.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

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
            var filename = dialog.FileName;
            var extension = Path.GetExtension(filename).ToLower();
            if (extension.Equals(".csv"))
                LoadFromCsv(filename);
            else if (extension.Equals(".xlsx") || extension.Equals(".xls"))
                LoadFromExcel(filename);
            else            
                MessageBox.Show("Неподдерживаемый формат файла.");                        
        }
    }

    private void LoadFromCsv(string filePath)
    {
        if (Objects is not null)
            Objects.Clear();
        // Простейший импорт CSV (разделитель – точка с запятой)
        var lines = File.ReadAllLines(filePath).Skip(1);
        try
        {
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
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при чтении файла либо структура файла не поддерживается\n: {ex.Message}");
        }
    }
    private void LoadFromExcel(string filePath)
    {
        if(Objects is not null)
            Objects.Clear();

        // Регистрация кодировок (требуется для ExcelDataReader)
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var conf = new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true  // Если в Excel есть заголовок
            }
        };
        var dataSet = reader.AsDataSet(conf);
        // Предположим, что данные находятся на первом листе
        DataTable dt = dataSet.Tables[0];
        for(int row = 0; row < dt.Rows.Count; row++)
        {
            try
            {
                var isDefected = dt.Rows[row][5].ToString().ToLower() == "yes";
                Objects.Add(new ObjectData
                (
                    Name: dt.Rows[row][0].ToString(),
                    Distance: double.Parse(dt.Rows[row][1].ToString()),
                    Angle: double.Parse(dt.Rows[row][2].ToString()),
                    Width: double.Parse(dt.Rows[row][3].ToString()),
                    Height: double.Parse(dt.Rows[row][4].ToString()),
                    IsDefect: isDefected
                ));
            }
            catch (Exception exRow)
            {
                // Логирование или уведомление об ошибке конкретной строки
                MessageBox.Show($"Ошибка при чтении строки: {exRow.Message}");
            }
        }       
    }
}
