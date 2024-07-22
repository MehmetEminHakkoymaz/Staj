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

namespace WpfApp1.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private DispatcherTimer clockTimer;

        public SettingsWindow()
        {
            InitializeComponent();
            InitializeClock();
        }

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Sistem saatini "HH:mm:ss" formatında güncelle
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Vessel_Type_Button_Click(object sender, RoutedEventArgs e)
        {
            // VesselType UserControl'ünün bir örneğini oluştur
            var vesselTypeControl = new WpfApp1.Settings.SettingWindows.VesselType();
            // Sağ Grid'in içeriğini temizle
            RightGrid.Children.Clear();

            // VesselType UserControl'ünü Sağ Grid'e ekle
            RightGrid.Children.Add(vesselTypeControl);
        }

        private void Appearance_Button_Click(object sender, RoutedEventArgs e)
        {
            var appearanceControl = new WpfApp1.Settings.SettingWindows.Appearance();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(appearanceControl);
        }

        private void Network_Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            var networkSettingControl = new WpfApp1.Settings.SettingWindows.NetworkSetting();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(networkSettingControl);
        }

        private void Usb_Button_Click(object sender, RoutedEventArgs e)
        {
            var usbControl = new WpfApp1.Settings.SettingWindows.Usb();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(usbControl);
        }

        private void System_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            var systemInfoControl = new WpfApp1.Settings.SettingWindows.SystemInfo();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(systemInfoControl);
        }

        private void Service_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            var serviceMenuControl = new WpfApp1.Settings.SettingWindows.ServiceMenu();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(serviceMenuControl);
        }

        private void Pin_Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            var pinSettingControl = new WpfApp1.Settings.SettingWindows.PinSetting();
            RightGrid.Children.Clear();
            RightGrid.Children.Add(pinSettingControl);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
