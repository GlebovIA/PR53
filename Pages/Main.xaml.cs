using PR53.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PR53.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }
        private void OpenPageChart(object sender, RoutedEventArgs e)
        {
            float value = Convert.ToInt32(valueTBx.Text);
            MainWindow.mainWindow.pointsInfo.Add(new PointInfo(value));
            MainWindow.mainWindow.OpenPages(MainWindow.pages.chart);
        }
    }
}
