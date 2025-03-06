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
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.Settings.SettingWindows;
using WpfApp1;


namespace WpfApp1.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private DispatcherTimer clockTimer;
        private SystemInfo systemInfo;

        // Singleton pattern için static instance
        public static SettingsWindow Instance { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();
            InitializeClock();
            Instance = this;

            // Window özelliklerini ayarla
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;

            // SystemInfo kontrolünü oluştur
            systemInfo = new SystemInfo();
        }

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Vessel_Type_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAndAddControl(new VesselType());
        }

        private void Appearance_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAndAddControl(new Appearance());
        }

        private void Network_Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAndAddControl(new NetworkSetting());
        }

        private void Usb_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAndAddControl(new Usb());
        }

        private void System_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            // SystemInfo kontrolünü kullan
            ClearAndAddControl(systemInfo);
        }

        private void Pin_Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // MainWindow'a erişim sağla
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mainWindow != null)
                {
                    // Yeni AdminPanel örneği oluştur ve göster
                    var adminPanel = new AdminPanel(mainWindow);
                    adminPanel.Show();

                    // İsteğe bağlı: SettingsWindow'u kapat
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("MainWindow not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening AdminPanel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Yardımcı metod - kontrolleri temizle ve yeni kontrol ekle
        private void ClearAndAddControl(UserControl control)
        {
            if (RightGrid != null)
            {
                RightGrid.Children.Clear();
                RightGrid.Children.Add(control);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            CleanupAndClose();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            CleanupAndClose();
        }

        private void CleanupAndClose()
        {
            // Timer'ı durdur
            clockTimer?.Stop();

            // SystemInfo'yu temizle
            if (systemInfo != null && RightGrid.Children.Contains(systemInfo))
            {
                RightGrid.Children.Remove(systemInfo);
            }

            this.Close();
        }

        // SystemInfo'ya public erişim için property
        public SystemInfo SystemInfoControl
        {
            get { return systemInfo; }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            clockTimer?.Stop();
            Instance = null;
        }
    }
}
