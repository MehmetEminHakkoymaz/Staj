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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for OpenAutoWindow.xaml
    /// </summary>
    public partial class OpenAutoWindow : Window
    {
        private MainWindow mainWindow;
        public OpenAutoWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
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
                if (activeTextBox.Tag is string tag && ParseRange(tag, out int min, out int max))
                {
                    if (int.TryParse(value, out int intValue) && intValue >= min && intValue <= max)
                    {
                        activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
                    }
                    else
                    {
                        KeypadPopup.IsOpen = false; // Hata durumunda KeyPad'i tekrar aç

                        MessageBox.Show($"Please enter a value between {min} and {max}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private bool ParseRange(string tag, out int min, out int max)
        {
            var parts = tag.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out min) && int.TryParse(parts[1], out max))
            {
                return true;
            }
            min = max = 0;
            return false;
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
