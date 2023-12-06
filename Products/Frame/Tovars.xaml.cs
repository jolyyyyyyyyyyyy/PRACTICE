using Products.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = (string)value;
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = "picture.png";
            }
            return new BitmapImage(new Uri("pack://application:,,,/img/" + imageName, UriKind.RelativeOrAbsolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Логика взаимодействия для Tovar.xaml
    /// </summary>
    public partial class Tovars : Page
    {
        public string rol;
        public Tovars(string rol)
        {
            InitializeComponent();
            this.rol = rol;
            LoadProducts(); 
            if (rol == null ||rol== "Клиент"||rol== "Менеджер")
            {
                Add.Visibility = Visibility.Hidden;
                Del.Visibility = Visibility.Hidden;
                Red.Visibility = Visibility.Hidden;

            }
        }

        private void LoadProducts()
        {
            ProductsListView.ItemsSource= DataEntities.GetContext().Tovar.ToList();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage(null));
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsListView.SelectedItem as Tovar;
            if (ProductsListView.SelectedItem != null)
            {
                var productId = selectedProduct.Art;
                using (var cont = DataEntities.GetContext())
                {
                    var productToDelete = cont.Tovar.Find(productId);
                    NavigationService.Navigate(new AddPage(productId));
                }
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedProduct = ProductsListView.SelectedItem as Tovar;
            if (selectedProduct != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Удаление товара", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var productId = selectedProduct.Art;
                    using (var cont = DataEntities.GetContext())
                    {
                        var productToDelete = cont.Tovar.Find(productId);
                        if (productToDelete != null)
                        {
                            cont.Tovar.Remove(productToDelete);
                            cont.SaveChanges();
                        }
                    }
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }
        private void UpdateDate()
        {
            var poisc = DataEntities.GetContext().Tovar.ToList();
            poisc = poisc.Where(p => p.Descript.ToLower().Contains(SearchBox.Text.ToLower())).ToList();
            ProductsListView.ItemsSource = poisc;
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDate();

        }

    }
}
