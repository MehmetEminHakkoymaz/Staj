using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using NativeWifi;
using WpfApp1;

namespace WpfApp1.Settings.SettingWindows
{
    /// <summary>
    /// Interaction logic for NetworkSetting.xaml
    /// </summary>
    public partial class NetworkSetting : UserControl
    {
        public event EventHandler<string> ValueSelected;

        public NetworkSetting()
        {
            InitializeComponent();
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            LoadNetworkSettings();

            LoadWifiNetworks();


        }

        private TextBox activeTextBox = null;

        private TextBox currentTextBox = null;

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox focusedTextBox = sender as TextBox;
            if (focusedTextBox != null)
            {
                // TextBox'ın ebeveyninin ebeveynini bul (Grid varsayıyoruz)
                DependencyObject parent = VisualTreeHelper.GetParent(focusedTextBox);
                DependencyObject grandParent = parent != null ? VisualTreeHelper.GetParent(parent) : null;
                Grid parentGrid = grandParent as Grid;
                activeTextBox = sender as TextBox; // Odaklanan TextBox'ı aktif olarak ayarla
                if (parentGrid != null)
                {
                    // Grid içindeki ilk Label'ı bul
                    Label firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        // Label'ın içeriğini al
                        string labelContent = firstLabel.Content.ToString();
                        // KeyPad'e label içeriğini gönder
                        activeTextBox = sender as TextBox;
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (clickedButton == Ethernet && WLAN != null)
            {
                WLAN.IsChecked = false;
            }
            else if (clickedButton == WLAN && Ethernet != null)
            {
                Ethernet.IsChecked = false;
            }
            else if (clickedButton == Autodhcp && Manual != null)
            {
                Manual.IsChecked = false;
            }
            else if (clickedButton == Manual && Autodhcp != null)
            {
                Autodhcp.IsChecked = false;
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (!Ethernet.IsChecked.Value && !WLAN.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
            if (!Autodhcp.IsChecked.Value && !Manual.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Connection Type
            //string connectionType = ((ComboBoxItem)ConnectionTypeComboBox.SelectedItem).Content.ToString();
            string connectionType = Ethernet.IsChecked == true ? "Ethernet" : (WLAN.IsChecked == true ? "WLAN" : "None");

            // Configuration
            //string configuration = ((ComboBoxItem)ConfigurationComboBox.SelectedItem).Content.ToString();
            string configuration = Autodhcp.IsChecked == true ? "Auto" : (Manual.IsChecked == true ? "Manual" : "None");
            // IP Address
            string ipAddress = $"{IpAddressTextBox1.Text}.{IpAddressTextBox2.Text}.{IpAddressTextBox3.Text}.{IpAddressTextBox4.Text}";

            // Subnet Mask
            string subnetMask = $"{SubnetMaskTextBox1.Text}.{SubnetMaskTextBox2.Text}.{SubnetMaskTextBox3.Text}.{SubnetMaskTextBox4.Text}";

            // SSID
            string ssid = ((ComboBoxItem)SsidComboBox.SelectedItem).Content.ToString();

            // Password
            string password = PasswordTextBox.Text;

            // Ayarları doğrulayın ve kaydedin
            if (IsValidIpAddress(ipAddress) && IsValidIpAddress(subnetMask))
            {
                SaveNetworkSettings(connectionType, configuration, ipAddress, subnetMask, ssid, password);
                MessageBox.Show("Settings saved successfully.");
            }
            else
            {
                MessageBox.Show("Invalid IP address or subnet mask.");
            }

            LoadNetworkSettings();

        }

        private bool IsValidIpAddress(string ipAddress)
        {
            // IP adresi doğrulama işlemi
            System.Net.IPAddress temp;
            return System.Net.IPAddress.TryParse(ipAddress, out temp);
        }

        private void SaveNetworkSettings(string connectionType, string configuration, string ipAddress, string subnetMask, string ssid, string password)
        {
            // Ayarları kaydetme veya uygulama işlemi
            // Örneğin, ayarları bir dosyaya yazabilir veya bir sınıfın özelliklerine atayabilirsiniz
            Properties.Settings.Default.ConnectionType = connectionType;
            Properties.Settings.Default.Configuration = configuration;
            Properties.Settings.Default.IpAddress = ipAddress;
            Properties.Settings.Default.SubnetMask = subnetMask;
            Properties.Settings.Default.Ssid = ssid;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Save();

            MessageBox.Show("Settings saved successfully.");

        }

        private void LoadNetworkSettings()
        {
            // Retrieve the saved settings
            string connectionType = Properties.Settings.Default.ConnectionType;
            string configuration = Properties.Settings.Default.Configuration;
            string ipAddress = Properties.Settings.Default.IpAddress;
            string subnetMask = Properties.Settings.Default.SubnetMask;
            string ssid = Properties.Settings.Default.Ssid;
            string password = Properties.Settings.Default.Password;

            // Set the values to the UI components
            Ethernet.IsChecked = connectionType == "Ethernet";
            WLAN.IsChecked = connectionType == "WLAN";
            Autodhcp.IsChecked = configuration == "Auto";
            Manual.IsChecked = configuration == "Manual";

            var ipParts = ipAddress.Split('.');
            if (ipParts.Length == 4)
            {
                IpAddressTextBox1.Text = ipParts[0];
                IpAddressTextBox2.Text = ipParts[1];
                IpAddressTextBox3.Text = ipParts[2];
                IpAddressTextBox4.Text = ipParts[3];
            }

            var subnetParts = subnetMask.Split('.');
            if (subnetParts.Length == 4)
            {
                SubnetMaskTextBox1.Text = subnetParts[0];
                SubnetMaskTextBox2.Text = subnetParts[1];
                SubnetMaskTextBox3.Text = subnetParts[2];
                SubnetMaskTextBox4.Text = subnetParts[3];
            }

            foreach (ComboBoxItem item in SsidComboBox.Items)
            {
                if (item.Content.ToString() == ssid)
                {
                    SsidComboBox.SelectedItem = item;
                    break;
                }
            }

            PasswordTextBox.Text = password;
        }


        private void LoadWifiNetworks()
        {
            try
            {
                WlanClient wlanClient = new WlanClient();
                foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
                {
                    Wlan.WlanAvailableNetwork[] networks = wlanInterface.GetAvailableNetworkList(0);
                    foreach (Wlan.WlanAvailableNetwork network in networks)
                    {
                        string ssid = GetStringForSSID(network.dot11Ssid);
                        if (!string.IsNullOrEmpty(ssid))
                        {
                            SsidComboBox.Items.Add(new ComboBoxItem { Content = ssid });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Wi-Fi networks: {ex.Message}");
            }
        }

        private string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return new string(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
        }

    }
}