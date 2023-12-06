using Products.Data;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        string list;
        bool zxc;
        public AddPage(string asd)
        {
            InitializeComponent();
            list = asd;
            if (list != null)
            {
                using (var cont = new DataEntities())
                {
                    var product = cont.Tovar.Find(list);
                    Art.Text = product.Art;
                    Name.Text = product.Name;
                    Price.Text = product.Price.ToString();
                    MaxD.Text = product.MaxDisc.ToString();
                    Proiz.Text = product.Proizv;
                    Post.Text = product.Post;
                    Kategory.Text = product.Category;
                    DayD.Text = product.DeisDisc.ToString();
                    Kolvo.Text = product.Kolvo.ToString();
                    Descript.Text = product.Descript;
                }
            }
            else
            {
                zxc = true;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Tovars("Администратор"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Art.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле Артикул.");
                return;
            }

            using (var cont = new DataEntities())
            {
                var Product = cont.Tovar.FirstOrDefault(p => p.Art == Art.Text);
                if (zxc)
                {
                    Tovar product = new Tovar();

                    product.Art = Art.Text;
                    product.EdIzm = "шт.";
                    product.Name = Name.Text;
                    product.Price = int.Parse(Price.Text);
                    product.MaxDisc = int.Parse(MaxD.Text);
                    product.Proizv = Proiz.Text;
                    product.Post = Post.Text;
                    product.Category = Kategory.Text;
                    product.DeisDisc = int.Parse(DayD.Text);
                    product.Kolvo = int.Parse(Kolvo.Text);
                    product.Descript = Descript.Text;
                    cont.Tovar.Add(product);
                    cont.SaveChanges();
                    MessageBox.Show("Изменения сохранены успешно.");
                    return;
                }

                if (Product != null)
                {
                    // Обновляем данные из текстбоксов
                    Product.EdIzm = "шт.";
                    Product.Name = Name.Text;
                    Product.Price = int.Parse(Price.Text);
                    Product.MaxDisc = int.Parse(MaxD.Text);
                    Product.Proizv = Proiz.Text;
                    Product.Post = Post.Text;
                    Product.Category = Kategory.Text;
                    Product.DeisDisc = int.Parse(DayD.Text);
                    Product.Kolvo = int.Parse(Kolvo.Text);
                    Product.Descript = Descript.Text;

                    // Сохраняем изменения в БД
                    cont.SaveChanges();

                    MessageBox.Show("Изменения сохранены успешно.");
                }
                else
                {
                    MessageBox.Show("Продукт с указанным Art не найден в базе данных.");
                }

            }
        }
    }
}
