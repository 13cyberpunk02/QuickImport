using CommunityToolkit.Mvvm.ComponentModel;

namespace QuickImport.Models;

public partial class ObjectData : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private double distance; // Горизонтальная координата в м (0–20)

    [ObservableProperty]
    private double angle;    // Вертикальная координата в часах (0–12)

    [ObservableProperty]
    private double width;    // Горизонтальный размер (м)

    [ObservableProperty]
    private double height;   // Вертикальный размер (ч)

    [ObservableProperty]
    private bool isDefect;   // Является ли объект дефектом


    public ObjectData(
        string name, 
        double distance, 
        double angle, 
        double width, 
        double height, 
        bool isDefect)
    {
        this.name = name;
        this.distance = distance;
        this.angle = angle;
        this.width = width;
        this.height = height;
        this.isDefect = isDefect;
    }
}
