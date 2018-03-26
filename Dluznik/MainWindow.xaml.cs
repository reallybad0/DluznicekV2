using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
namespace Dluznik
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new PrvniVerze();
        }

        private void OpenFirst(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PrvniVerze();
        }

        private void OpenSecond(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DruhaVerze();
        }
        private void OpenAdd(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Pridavani();
        }
    }
}
