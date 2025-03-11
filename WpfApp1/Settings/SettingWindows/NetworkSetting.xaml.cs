using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NativeWifi;
using System.Windows.Threading;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Net.NetworkInformation;
using System.Net;
using System.Management;
using System.Diagnostics;
using System.Threading.Tasks;
using WpfApp1;
using System.Windows.Media.Animation;

namespace WpfApp1.Settings.SettingWindows
{
    /// <summary>
    /// Interaction logic for NetworkSetting.xaml
    /// </summary>
    /// 


    public partial class NetworkSetting : UserControl
    {
        private TextBox activeTextBox = null;
        private PasswordBox activePasswordBox = null;
        private bool isInitialized = false;

        public NetworkSetting()
        {
            try
            {
                InitializeComponent();

                // Kontrollerin yüklenmesini bekle
                this.Loaded += (s, e) =>
                {
                    if (!isInitialized)
                    {
                        InitializeControls();
                        isInitialized = true;
                    }
                };

                // Keypad kontrolünü başlat
                if (KeypadControl != null)
                {
                    KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in NetworkSetting constructor: {ex.Message}");
            }
        }

        private void InitializeControls()
        {
            try
            {
                // UI elementlerinin hazır olduğundan emin ol
                if (Ethernet != null && WLAN != null)
                {
                    Ethernet.IsChecked = true;
                    WLAN.IsChecked = false;
                }

                if (Autodhcp != null && Manual != null)
                {
                    Autodhcp.IsChecked = true;
                    Manual.IsChecked = false;
                }

                // Görünürlüğü güncelle
                UpdateVisibility();

                // Ağ ayarlarını yükle
                LoadCurrentNetworkSettings();

                // WLAN seçili ise ağları tara
                if (WLAN?.IsChecked == true)
                {
                    ScanWifiNetworks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing controls: {ex.Message}");
            }
        }


        private void NetworkSetting_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // UI elementlerinin hazır olduğundan emin ol
                if (Ethernet != null && WLAN != null)
                {
                    // Varsayılan seçimi yap
                    Ethernet.IsChecked = true;
                    WLAN.IsChecked = false;
                }

                if (Autodhcp != null && Manual != null)
                {
                    // Varsayılan seçimi yap
                    Autodhcp.IsChecked = true;
                    Manual.IsChecked = false;
                }

                // Görünürlüğü güncelle
                UpdateVisibility();

                // Ağ ayarlarını yükle
                LoadCurrentNetworkSettings();

                // WLAN seçili ise ağları tara
                if (WLAN?.IsChecked == true)
                {
                    ScanWifiNetworks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in NetworkSetting_Loaded: {ex.Message}");
            }
        }

        private async void LoadCurrentNetworkSettings()
        {
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        // Ethernet/WLAN kontrolü
                        bool isEthernet = nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet;
                        Ethernet.IsChecked = isEthernet;
                        WLAN.IsChecked = !isEthernet;

                        // DHCP kontrolü
                        IPInterfaceProperties ipProps = nic.GetIPProperties();
                        bool isDhcp = nic.GetIPProperties().GetIPv4Properties()?.IsDhcpEnabled ?? false;
                        Autodhcp.IsChecked = isDhcp;
                        Manual.IsChecked = !isDhcp;

                        // IP ve Subnet bilgilerini al
                        foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                        {
                            if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                string[] ipParts = addr.Address.ToString().Split('.');
                                if (ipParts.Length == 4)
                                {
                                    IpAddressTextBox1.Text = ipParts[0];
                                    IpAddressTextBox2.Text = ipParts[1];
                                    IpAddressTextBox3.Text = ipParts[2];
                                    IpAddressTextBox4.Text = ipParts[3];
                                }

                                string[] maskParts = addr.IPv4Mask.ToString().Split('.');
                                if (maskParts.Length == 4)
                                {
                                    SubnetMaskTextBox1.Text = maskParts[0];
                                    SubnetMaskTextBox2.Text = maskParts[1];
                                    SubnetMaskTextBox3.Text = maskParts[2];
                                    SubnetMaskTextBox4.Text = maskParts[3];
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading network settings: {ex.Message}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox focusedTextBox)
            {
                activeTextBox = focusedTextBox;
                activePasswordBox = null; // PasswordBox'ı temizle

                DependencyObject parent = VisualTreeHelper.GetParent(focusedTextBox);
                DependencyObject grandParent = parent != null ? VisualTreeHelper.GetParent(parent) : null;
                Grid parentGrid = grandParent as Grid;

                if (parentGrid != null)
                {
                    Label firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        string labelContent = firstLabel.Content.ToString();
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
            else if (sender is PasswordBox focusedPasswordBox)
            {
                activePasswordBox = focusedPasswordBox;
                activeTextBox = null; // TextBox'ı temizle
                KeypadPopup.IsOpen = true;
                KeypadControl.SetLabelContent("PASSWORD");
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                // IP ve Subnet için değer kontrolü
                if (int.TryParse(value, out int numValue) && numValue >= 0 && numValue <= 255)
                {
                    activeTextBox.Text = value;
                    MoveToNextTextBox();
                }
            }
            //else if (activePasswordBox != null)
            //{
            //    activePasswordBox.Password = value;
            //}
        }

        private void MoveToNextTextBox()
        {
            if (activeTextBox != null)
            {
                string currentName = activeTextBox.Name;
                if (currentName.Contains("TextBox"))
                {
                    int currentNumber = int.Parse(currentName[currentName.Length - 1].ToString());
                    if (currentNumber < 4)
                    {
                        string baseName = currentName.Substring(0, currentName.Length - 1);
                        string nextName = baseName + (currentNumber + 1);
                        TextBox nextBox = FindName(nextName) as TextBox;
                        if (nextBox != null)
                        {
                            nextBox.Focus();
                        }
                    }
                    else
                    {
                        KeypadPopup.IsOpen = false;
                    }
                }
            }
        }

        private async void RefreshWifiButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    button.IsEnabled = false;

                    // Animasyon başlat
                    var path = button.Content as Path;
                    if (path != null)
                    {
                        var rotation = new DoubleAnimation
                        {
                            From = 0,
                            To = 360,
                            Duration = TimeSpan.FromSeconds(1),
                            RepeatBehavior = RepeatBehavior.Forever
                        };
                        path.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotation);
                    }

                    // WiFi ağlarını tara
                    await ScanWifiNetworksAsync();

                    // Animasyonu durdur ve butonu aktif et
                    if (path != null)
                    {
                        path.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                    }
                    button.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing networks: {ex.Message}");
            }
        }

        private async Task ScanWifiNetworksAsync()
        {
            try
            {
                // ComboBox'ı temizle
                SsidComboBox.Items.Clear();

                // netsh komutunu çalıştır
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = "wlan show networks mode=Bssid",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8
                    };

                    process.Start();
                    string output = await process.StandardOutput.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    var networks = new List<string>();
                    string currentSsid = null;

                    foreach (string line in output.Split('\n'))
                    {
                        string trimmedLine = line.Trim();
                        if (trimmedLine.StartsWith("SSID"))
                        {
                            var parts = trimmedLine.Split(':', 2);
                            if (parts.Length == 2)
                            {
                                currentSsid = parts[1].Trim();
                                if (!string.IsNullOrEmpty(currentSsid) && !networks.Contains(currentSsid))
                                {
                                    networks.Add(currentSsid);
                                }
                            }
                        }
                    }

                    // UI thread'de ComboBox'ı güncelle
                    await Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var network in networks.OrderBy(n => n))
                        {
                            SsidComboBox.Items.Add(new ComboBoxItem { Content = network });
                        }

                        if (SsidComboBox.Items.Count > 0)
                        {
                            SsidComboBox.SelectedIndex = 0;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error scanning networks: {ex.Message}");
                });
            }
        }

        private async Task ScanWifiNetworks()
        {
            try
            {
                if (SsidComboBox == null) return;

                // ComboBox'ı temizle
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    SsidComboBox.Items.Clear();
                });

                // Ağları tara
                var networks = await ManageWifi.GetAvailableNetworks();

                // UI'ı güncelle
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (var network in networks.OrderBy(n => n)) // Ağları alfabetik sırala
                    {
                        var item = new ComboBoxItem
                        {
                            Content = network,
                            Tag = network
                        };
                        SsidComboBox.Items.Add(item);
                    }

                    if (SsidComboBox.Items.Count > 0)
                    {
                        SsidComboBox.SelectedIndex = 0;
                    }
                });
            }
            catch (Exception ex)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error scanning networks: {ex.Message}");
                });
            }
        }

        // ToggleButton_Checked metodunu güncelle
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isInitialized) return;

                var button = sender as ToggleButton;
                if (button == null) return;

                if (button == Ethernet || button == WLAN)
                {
                    Ethernet.IsChecked = (button == Ethernet);
                    WLAN.IsChecked = (button == WLAN);
                    UpdateVisibility();

                    if (button == WLAN)
                    {
                        ScanWifiNetworks();
                    }
                }
                else if (button == Autodhcp || button == Manual)
                {
                    Autodhcp.IsChecked = (button == Autodhcp);
                    Manual.IsChecked = (button == Manual);
                    UpdateVisibility();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in ToggleButton_Checked: {ex.Message}");
            }
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            try
            {
                // Tüm kontroller için null kontrolü
                if (Ethernet == null || WLAN == null || Manual == null ||
                    IpAddressGrid == null || SubnetMask == null ||
                    Ssid == null || PASSWORD == null)
                {
                    return;
                }

                bool isEthernet = Ethernet.IsChecked == true;
                bool isManual = Manual.IsChecked == true;

                // IP ve Subnet alanlarını manuel modda göster
                IpAddressGrid.Visibility = isManual ? Visibility.Visible : Visibility.Collapsed;
                SubnetMask.Visibility = isManual ? Visibility.Visible : Visibility.Collapsed;

                // SSID ve Password alanlarını WLAN modunda göster
                Ssid.Visibility = !isEthernet ? Visibility.Visible : Visibility.Collapsed;
                PASSWORD.Visibility = !isEthernet ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating visibility: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Manual.IsChecked == true)
                {
                    string ipAddress = $"{IpAddressTextBox1.Text}.{IpAddressTextBox2.Text}.{IpAddressTextBox3.Text}.{IpAddressTextBox4.Text}";
                    string subnetMask = $"{SubnetMaskTextBox1.Text}.{SubnetMaskTextBox2.Text}.{SubnetMaskTextBox3.Text}.{SubnetMaskTextBox4.Text}";

                    if (!IsValidIpAddress(ipAddress) || !IsValidIpAddress(subnetMask))
                    {
                        MessageBox.Show("Invalid IP address or subnet mask!");
                        return;
                    }

                    await SetStaticIp(ipAddress, subnetMask);
                }
                else
                {
                    await SetDhcp();
                }

                if (WLAN.IsChecked == true && SsidComboBox.SelectedItem != null)
                {
                    string ssid = ((ComboBoxItem)SsidComboBox.SelectedItem).Content.ToString();
                    await ConnectToWifi(ssid, PasswordTextBox.Password);
                }

                // Ayarları kaydet
                SaveSettings();

                MessageBox.Show("Network settings saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        private async Task SetStaticIp(string ipAddress, string subnetMask)
        {
            string interfaceName = Ethernet.IsChecked == true ? "Ethernet" : "Wi-Fi";
            string command = $"interface ip set address \"{interfaceName}\" static {ipAddress} {subnetMask}";
            await RunNetshCommand(command);
        }

        private async Task SetDhcp()
        {
            string interfaceName = Ethernet.IsChecked == true ? "Ethernet" : "Wi-Fi";
            string command = $"interface ip set address \"{interfaceName}\" dhcp";
            await RunNetshCommand(command);
        }

        private async Task ConnectToWifi(string ssid, string password)
        {
            // Password kontrolü
            if (PasswordTextBox is PasswordBox passwordBox)
            {
                password = passwordBox.Password;
            }

            string profileXml = $@"<?xml version=""1.0""?>
            <WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">
                <name>{ssid}</name>
                <SSIDConfig>
                    <SSID>
                        <name>{ssid}</name>
                    </SSID>
                </SSIDConfig>
                <connectionType>ESS</connectionType>
                <connectionMode>auto</connectionMode>
                <MSM>
                    <security>
                        <authEncryption>
                            <authentication>WPA2PSK</authentication>
                            <encryption>AES</encryption>
                            <useOneX>false</useOneX>
                        </authEncryption>
                        <sharedKey>
                            <keyType>passPhrase</keyType>
                            <protected>false</protected>
                            <keyMaterial>{password}</keyMaterial>
                        </sharedKey>
                    </security>
                </MSM>
            </WLANProfile>";

            string profilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wifi.xml");
            await System.IO.File.WriteAllTextAsync(profilePath, profileXml);

            await RunNetshCommand($"wlan add profile filename=\"{profilePath}\"");
            await RunNetshCommand($"wlan connect name=\"{ssid}\"");

            System.IO.File.Delete(profilePath);
        }

        private async Task RunNetshCommand(string arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                await process.WaitForExitAsync();
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.ConnectionType = Ethernet.IsChecked == true ? "Ethernet" : "WLAN";
            Properties.Settings.Default.Configuration = Autodhcp.IsChecked == true ? "Auto" : "Manual";

            if (Manual.IsChecked == true)
            {
                Properties.Settings.Default.IpAddress = $"{IpAddressTextBox1.Text}.{IpAddressTextBox2.Text}.{IpAddressTextBox3.Text}.{IpAddressTextBox4.Text}";
                Properties.Settings.Default.SubnetMask = $"{SubnetMaskTextBox1.Text}.{SubnetMaskTextBox2.Text}.{SubnetMaskTextBox3.Text}.{SubnetMaskTextBox4.Text}";
            }

            if (WLAN.IsChecked == true && SsidComboBox.SelectedItem != null)
            {
                Properties.Settings.Default.Ssid = ((ComboBoxItem)SsidComboBox.SelectedItem).Content.ToString();
                Properties.Settings.Default.Password = (PasswordTextBox as PasswordBox)?.Password ?? "";
            }

            Properties.Settings.Default.Save();
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            KeyboardPopup.IsOpen = true;
        }

        private void CustomKeyboard_KeyPressed(object sender, string key)
        {
            switch (key)
            {
                case "Backspace":
                    if (PasswordTextBox.Password.Length > 0)
                    {
                        PasswordTextBox.Password = PasswordTextBox.Password.Substring(0, PasswordTextBox.Password.Length - 1);
                    }
                    break;
                case "Shift":
                    // Shift işlevselliği
                    break;
                default:
                    PasswordTextBox.Password += key;
                    break;
            }
        }

    }

    public class ManageWifi
    {
        public static async Task<List<string>> GetAvailableNetworks()
        {
            var networks = new List<string>();

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = "wlan show networks",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8
                    };

                    process.Start();
                    string output = await process.StandardOutput.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    string[] lines = output.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains("SSID") && line.Contains(":"))
                        {
                            string ssid = line.Split(':')[1].Trim();
                            if (!string.IsNullOrEmpty(ssid))
                            {
                                networks.Add(ssid);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error scanning WiFi networks: {ex.Message}");
            }

            return networks;
        }
    }

}