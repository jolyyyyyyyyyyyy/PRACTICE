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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Products.Frame
{
    /// <summary>
    /// Логика взаимодействия для avt.xaml
    /// </summary>
    public partial class avt : Page
    {
        MainWindow _main;
        private DispatcherTimer timer;
        private bool isLoginButtonBlocked = false;
        private int secondsLeft = 10;
        public avt( MainWindow main)
        {
            InitializeComponent();
            _main = main;
            InitializeTimer();
            _main.Avtoriz.Visibility = Visibility.Hidden;
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;

            if (secondsLeft <= 0)
            {
                timer.Stop();
                isLoginButtonBlocked = false;
                secondsLeft = 10;
            }
        }
        
        private readonly string captchaChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly Random random = new Random();
       
        public int counter = 0;
        private void GenerateCaptcha()
        {
            string captchaText = GenerateRandomString(4); // Выберите длину капчи
            CaptchaLabel.Content = captchaText;

            // Применяем случайный эффект к тексту капчи
            ApplyRandomEffect();
        }
        private string GenerateRandomString(int length)
        {
            return new string(Enumerable.Repeat(captchaChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void ApplyRandomEffect()
        {
            // Применяем случайный эффект к тексту капчи
            int effectType = random.Next(2); // Выбираем случайный тип эффекта

            switch (effectType)
            {
                case 0:
                    CaptchaLabel.Effect = new BlurEffect() { Radius = 3 };
                    break;
                case 1:
                    CaptchaLabel.Effect = new DropShadowEffect() { ShadowDepth = 2, BlurRadius = 3, Color = Colors.Black };
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isLoginButtonBlocked)
            {
                MessageBox.Show($"Подождите {secondsLeft} секунд, чтобы попробовать еще.");
                return;
            }
            if (counter == 0)
            {
                if (string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(Pass.Password))
                {
                    MessageBox.Show("Введите логин или пароль");

                }
                else
                {
                    using (var db = new DataEntities())
                    {
                        var user = db.User
                            .AsNoTracking()
                            .FirstOrDefault(u => u.Login == Login.Text && u.Pass == Pass.Password);
                        if (user == null)
                        {
                            MessageBox.Show("Пользователь с такими данными не найден!");
                            counter=1;
                            capt.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            NavigationService.Navigate(new ClearFrame());
                            _main.Menu.Navigate(new Menu(user.Rol,_main));
                            _main.Avtoriz.Visibility = Visibility.Hidden;
                            _main.Back.Visibility = Visibility.Visible;
                            _main.Naime.Content = user.Name;
                            MessageBox.Show("Добро пожаловать!");

                        }
                    }
                }
            }
            else
            {
                capt.Visibility = Visibility.Visible;
                GenerateCaptcha();
                if(string.IsNullOrEmpty(CaptchaInputTextBox.Text))
                {
                    MessageBox.Show("введите капчу");
                    return;
                }    
                if (string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(Pass.Password))
                {
                    MessageBox.Show("Введите логин или пароль");

                }
                else
                {
                    using (var db = new DataEntities())
                    {
                        var user = db.User
                            .AsNoTracking()
                            .FirstOrDefault(u => u.Login == Login.Text && u.Pass == Pass.Password);
                        if (user == null)
                        {
                           
                            isLoginButtonBlocked = true;
                            timer.Start();
                            MessageBox.Show("неверно введен логин, пароль тлт капча, ждите 10 секунд");
                            counter++;
                        }
                        else
                        {
                            capt.Visibility= Visibility.Hidden;
                            counter=0;
                            NavigationService.Navigate(new ClearFrame());
                            _main.Menu.Navigate(new Menu(user.Rol, _main));
                            _main.Avtoriz.Visibility = Visibility.Hidden;
                            _main.Back.Visibility = Visibility.Visible;
                            _main.Naime.Content = user.Name;
                            MessageBox.Show("Добро пожаловать!");

                        }
                    }
                }
            }
        }

        private void Grid_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClearFrame());
            _main.Menu.Navigate(new Menu(null, _main));
            _main.Visibility = Visibility.Visible;
            _main.Naime.Visibility = Visibility.Visible;
            _main.Naime.Content = "Гость";
            _main.Back.Visibility = Visibility.Visible;
        }
    }
}
