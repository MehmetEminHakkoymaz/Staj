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
    }
}
