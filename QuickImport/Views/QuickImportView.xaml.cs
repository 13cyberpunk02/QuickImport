using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace QuickImport.Views
{
    /// <summary>
    /// Interaction logic for QuickImportView.xaml
    /// </summary>
    public partial class QuickImportView : Window
    {
        public QuickImportView()
        {
            InitializeComponent();
            DrawGridLines(MyCanvas.Width, MyCanvas.Height, 20); // Шаг сетки 20 пикселей
        }

        private void DrawGridLines(double canvasWidth, double canvasHeight, double gridSpacing)
        {
            for (double x = 0; x <= canvasWidth; x += gridSpacing)
            {
                Line verticalLine = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = canvasHeight,
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.5
                };
                GridLinesCanvas.Children.Add(verticalLine);
            }

            for (double y = 0; y <= canvasHeight; y += gridSpacing)
            {
                Line horizontalLine = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = canvasWidth,
                    Y2 = y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.5
                };
                GridLinesCanvas.Children.Add(horizontalLine);
            }
        }
    }
}
