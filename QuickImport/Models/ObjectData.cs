namespace QuickImport.Models;

public record ObjectData(
       string Name,
       double Distance, // Горизонтальная координата в метрах (0–20)
       double Angle,    // Вертикальная координата в часах (0–12)
       double Width,    // Горизонтальный размер (м)
       double Height,   // Вертикальный размер (ч)
       bool IsDefect);  // Является ли объект дефектом
