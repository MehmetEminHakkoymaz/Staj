using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // İlk olarak LoginWindow'u göster
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            // Yeni bir MainWindow oluştur ama gösterme
            MainWindow mainWindow = new MainWindow(null);
            mainWindow.Hide();

            // Ana pencere olarak MainWindow'u ayarla
            this.MainWindow = mainWindow;
        }
    }

}
