using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PR53.Pages
{
    /// <summary>
    /// Логика взаимодействия для Chart.xaml
    /// </summary>
    public partial class Chart : Page
    {
        public double actualHeightCanvas = 0;
        public double maxValue = 0;
        double averageValue = 0;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Chart()
        {
            InitializeComponent();
            actualHeightCanvas = MainWindow.mainWindow.Height - 50d;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Tick += CreateNewValue;
            dispatcherTimer.Start();
            CreateChart();
            ColorChart();
        }
        public void CreateNewValue(object sender, EventArgs e)
        {
            Random random = new Random();
            double value = MainWindow.mainWindow.pointsInfo[MainWindow.mainWindow.pointsInfo.Count - 1].value;
            double newValue = value * (random.NextDouble() + 0.5d);
            MainWindow.mainWindow.pointsInfo.Add(new Classes.PointInfo(newValue));
            ControlCreateChart();
        }
        public void CreateChart()
        {
            canvas.Children.Clear();
            for (int i = 0; i < MainWindow.mainWindow.pointsInfo.Count; i++)
            {
                if (MainWindow.mainWindow.pointsInfo[i].value > maxValue) maxValue = MainWindow.mainWindow.pointsInfo[i].value;
            }
            for (int i = 0; i <= MainWindow.mainWindow.pointsInfo.Count; i++)
            {
                Line line = new Line();
                line.X1 = i * 20;
                line.X2 = (i + 1) * 20;
                if (i == 0) line.Y1 = actualHeightCanvas;
                else line.Y1 = actualHeightCanvas - ((MainWindow.mainWindow.pointsInfo[i].value / maxValue) * actualHeightCanvas);
                line.StrokeThickness = 2;
                MainWindow.mainWindow.pointsInfo[i].line = line;
                canvas.Children.Add(line);
            }
        }
        public void CreatePoint()
        {
            Line line = new Line();
            line.X1 = (MainWindow.mainWindow.pointsInfo.Count - 1) * 20;
            line.X2 = MainWindow.mainWindow.pointsInfo.Count * 20;
            line.Y1 = actualHeightCanvas - ((MainWindow.mainWindow.pointsInfo[(MainWindow.mainWindow.pointsInfo.Count - 2)].value / maxValue) * actualHeightCanvas);
            line.Y2 = actualHeightCanvas - ((MainWindow.mainWindow.pointsInfo[(MainWindow.mainWindow.pointsInfo.Count - 1)].value / maxValue) * actualHeightCanvas);
            line.StrokeThickness = 2;
            MainWindow.mainWindow.pointsInfo[(MainWindow.mainWindow.pointsInfo.Count - 1)].line = line;
            canvas.Children.Add(line);
        }
        public void ControlCreateChart()
        {
            double value = MainWindow.mainWindow.pointsInfo[MainWindow.mainWindow.pointsInfo.Count - 1].value;
            if (value < maxValue) CreatePoint();
            else CreateChart();
            ColorChart();
        }
        public void ColorChart()
        {
            double value = MainWindow.mainWindow.pointsInfo[MainWindow.mainWindow.pointsInfo.Count - 1].value;
            for (int i = 0; i < MainWindow.mainWindow.pointsInfo.Count; i++)
                averageValue += MainWindow.mainWindow.pointsInfo[i].value;
            averageValue = averageValue / MainWindow.mainWindow.pointsInfo.Count;
            for (int i = 0; i < MainWindow.mainWindow.pointsInfo.Count; i++)
            {
                if (value < averageValue) MainWindow.mainWindow.pointsInfo[i].line.Stroke = Brushes.Red;
                else MainWindow.mainWindow.pointsInfo[i].line.Stroke = Brushes.Green;
            }
            canvas.Width = MainWindow.mainWindow.pointsInfo.Count * 20 + 300;
            scroll.ScrollToHorizontalOffset(canvas.Width);
            current_Value.Content = "Тек. знач: " + Math.Round(value, 2);
            average_Value.Content = "Сред. знач: " + Math.Round(averageValue, 2);
        }
        public void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            actualHeightCanvas = MainWindow.mainWindow.Height - 50d;
            CreateChart();
            ColorChart();
        }
    }
}
