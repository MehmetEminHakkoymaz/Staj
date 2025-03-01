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
        private bool isCapsLockOn = false;

        public User LoggedInUser { get; private set; }

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

    }
}
