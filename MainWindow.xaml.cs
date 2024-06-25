using PR53.Classes;
using System.Collections.Generic;
using System.Windows;

namespace PR53
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<PointInfo> pointsInfo = new List<PointInfo>();
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
