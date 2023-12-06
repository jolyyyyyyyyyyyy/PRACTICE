using Products.Frame;
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
namespace Products
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MFR.Navigate(new avt(this));
        }

        private void Avtoriz_Click(object sender, RoutedEventArgs e)
        {
            MFR.Navigate(new avt(this));
            Menu.Navigate(new ClearFrame());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Naime.Content = "";
            Menu.Navigate(new ClearFrame());
            Back.Visibility = Visibility.Hidden; 
            Avtoriz.Visibility = Visibility.Visible;
            MFR.Navigate(new avt(this));
        }
    }
}
