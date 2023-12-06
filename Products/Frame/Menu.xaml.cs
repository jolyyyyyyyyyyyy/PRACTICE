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

namespace Products.Frame
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public string rol;
        MainWindow mainWindow;
        public Menu(string rol, MainWindow main)
        {
            InitializeComponent();
            this.rol = rol; 
            this.mainWindow = main;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MFR.Navigate(new Tovars(rol));
        }
    }
}
