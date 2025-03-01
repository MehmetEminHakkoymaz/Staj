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
using WpfApp1.Data;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;
        public User LoggedInUser { get; private set; }

        // Klavye için gerekli değişkenler
        private bool isCapsLockOn = false;
        private Control focusedControl;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            DatabaseHelper.InitializeDatabase();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userService.Login(
                UsernameTextBox.Text,
                PasswordBox.Password);

            if (user != null)
            {
                LoggedInUser = user;

                // MainWindow'u bul
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    // Yeni bir MainWindow oluştur
                    var newMainWindow = new MainWindow(user);

                    // Yeni pencereyi ana pencere olarak ayarla
                    Application.Current.MainWindow = newMainWindow;

                    // Yeni pencereyi göster
                    newMainWindow.Show();

                    // Login penceresini kapat
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        // Klavye işlevleri için gerekli metodlar
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            focusedControl = sender as Control;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            focusedControl = sender as Control;
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (focusedControl == null) return;

            var button = sender as Button;
            if (button == null) return;

            var character = button.Content.ToString();

            // CapsLock durumuna göre karakteri büyük/küçük yap
            if (isCapsLockOn)
            {
                character = character.ToUpper();
            }
            else
            {
                character = character.ToLower();
            }

            // Seçili kontrole karakteri ekle
            if (focusedControl is TextBox textBox)
            {
                textBox.Text += character;
                textBox.CaretIndex = textBox.Text.Length; // İmleci sona taşı
            }
            else if (focusedControl is PasswordBox passwordBox)
            {
                passwordBox.Password += character;
            }
        }
        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            isCapsLockOn = !isCapsLockOn; // Durumu tersine çevir
            var button = sender as Button;
            if (button != null)
            {
                // CapsLock aktif/pasif durumuna göre buton görünümünü güncelle
                if (isCapsLockOn)
                {
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4049"));
                    // Opsiyonel: CapsLock aktif olduğunu belirtmek için content'i güncelle
                    button.Content = "CAPS ON";
                }
                else
                {
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AF0101"));
                    // Opsiyonel: CapsLock pasif olduğunu belirtmek için content'i güncelle
                    button.Content = "caps off";
                }
            }
        }
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (focusedControl == null) return;

            if (focusedControl is TextBox textBox)
            {
                if (textBox.Text.Length > 0)
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                    textBox.CaretIndex = textBox.Text.Length; // İmleci sona taşı
                }
            }
            else if (focusedControl is PasswordBox passwordBox)
            {
                if (passwordBox.Password.Length > 0)
                {
                    passwordBox.Password = passwordBox.Password.Substring(0, passwordBox.Password.Length - 1);
                }
            }
        }

        // Enter tuşu için method ekleyebiliriz
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton_Click(sender, e);
        }

        // Esc tuşu için method
        private void EscapeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Pencere taşıma için mouse eventi
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
