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
using System.Globalization; // Kültür bilgisi için eklendi


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for OpenAutoWindow.xaml
    /// </summary>
    public partial class OpenAutoWindow : Window
    {
        private MainWindow mainWindow;
        private TextBox activeTextBox = null;

        public OpenAutoWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            // KeypadControl için event handler
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;

            // Sayfa yüklendiğinde değerleri göster
            LoadSavedValues();

            // TextBox event'lerini bağla
            RegisterTextBoxEvents();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;

            this.mainWindow = mainWindow;
        }
        private void LoadSavedValues()
        {
            try
            {
                // Kaydedilen değerleri TextBox'lara yükle
                Pump1Fill.Text = Properties.Settings.Default.Pump1FillValue.ToString(CultureInfo.CurrentCulture);
                Pump2Fill.Text = Properties.Settings.Default.Pump2FillValue.ToString(CultureInfo.CurrentCulture);
                Pump3Fill.Text = Properties.Settings.Default.Pump3FillValue.ToString(CultureInfo.CurrentCulture);
                Pump4Fill.Text = Properties.Settings.Default.Pump4FillValue.ToString(CultureInfo.CurrentCulture);

                Pump1Empty.Text = Properties.Settings.Default.Pump1EmptyValue.ToString(CultureInfo.CurrentCulture);
                Pump2Empty.Text = Properties.Settings.Default.Pump2EmptyValue.ToString(CultureInfo.CurrentCulture);
                Pump3Empty.Text = Properties.Settings.Default.Pump3EmptyValue.ToString(CultureInfo.CurrentCulture);
                Pump4Empty.Text = Properties.Settings.Default.Pump4EmptyValue.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading values: {ex.Message}");
            }
        }

        private void SaveValues()
        {
            try
            {
                // Fill değerlerini kaydet
                if (double.TryParse(Pump1Fill.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p1Fill))
                    Properties.Settings.Default.Pump1FillValue = p1Fill;

                if (double.TryParse(Pump2Fill.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p2Fill))
                    Properties.Settings.Default.Pump2FillValue = p2Fill;

                if (double.TryParse(Pump3Fill.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p3Fill))
                    Properties.Settings.Default.Pump3FillValue = p3Fill;

                if (double.TryParse(Pump4Fill.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p4Fill))
                    Properties.Settings.Default.Pump4FillValue = p4Fill;

                // Empty değerlerini kaydet
                if (double.TryParse(Pump1Empty.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p1Empty))
                    Properties.Settings.Default.Pump1EmptyValue = p1Empty;

                if (double.TryParse(Pump2Empty.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p2Empty))
                    Properties.Settings.Default.Pump2EmptyValue = p2Empty;

                if (double.TryParse(Pump3Empty.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p3Empty))
                    Properties.Settings.Default.Pump3EmptyValue = p3Empty;

                if (double.TryParse(Pump4Empty.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double p4Empty))
                    Properties.Settings.Default.Pump4EmptyValue = p4Empty;

                // Değişiklikleri kaydet
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving values: {ex.Message}");
            }
        }

        private void RegisterTextBoxEvents()
        {
            // TextBox'lara event handler'ları bağla
            Pump1Fill.PreviewTextInput += TextBox_PreviewTextInput;
            Pump2Fill.PreviewTextInput += TextBox_PreviewTextInput;
            Pump3Fill.PreviewTextInput += TextBox_PreviewTextInput;
            Pump4Fill.PreviewTextInput += TextBox_PreviewTextInput;

            Pump1Empty.PreviewTextInput += TextBox_PreviewTextInput;
            Pump2Empty.PreviewTextInput += TextBox_PreviewTextInput;
            Pump3Empty.PreviewTextInput += TextBox_PreviewTextInput;
            Pump4Empty.PreviewTextInput += TextBox_PreviewTextInput;

            // TextChanged event'leri
            Pump1Fill.TextChanged += TextBox_TextChanged;
            Pump2Fill.TextChanged += TextBox_TextChanged;
            Pump3Fill.TextChanged += TextBox_TextChanged;
            Pump4Fill.TextChanged += TextBox_TextChanged;

            Pump1Empty.TextChanged += TextBox_TextChanged;
            Pump2Empty.TextChanged += TextBox_TextChanged;
            Pump3Empty.TextChanged += TextBox_TextChanged;
            Pump4Empty.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Sadece rakam, nokta ve virgüle izin ver
            bool isValid = e.Text.All(c => char.IsDigit(c) || c == '.' || c == ',');

            // Eğer nokta veya virgül ise, TextBox'ta zaten bir tane var mı kontrol et
            if (isValid && (e.Text == "." || e.Text == ","))
            {
                if (sender is TextBox textBox)
                {
                    isValid = !textBox.Text.Contains(".") && !textBox.Text.Contains(",");
                }
            }

            e.Handled = !isValid;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Değer değiştikçe otomatik kaydet
            //SaveValues();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            activeTextBox = sender as TextBox;
            if (activeTextBox != null)
            {
                KeypadPopup.IsOpen = true;

                // TextBox içinde olduğumuz pompa adını al
                var parent = VisualTreeHelper.GetParent(activeTextBox);
                while (parent != null && !(parent is Grid))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent is Grid grid)
                {
                    var label = grid.Children.OfType<Label>().FirstOrDefault();
                    if (label != null)
                    {
                        KeypadControl.SetLabelContent(label.Content.ToString());
                    }
                }
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                // Nokta veya virgül içeren değerleri düzgün işle
                string normalizedValue = value.Replace(',', '.');

                // Tag'de belirtilen limit değerlerini kontrol et
                if (activeTextBox.Tag is string tag)
                {
                    string[] limits = tag.ToString().Split(',');

                    if (limits.Length == 2 &&
                        double.TryParse(limits[0], out double min) &&
                        double.TryParse(limits[1], out double max) &&
                        double.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        // Değer limitler içinde mi?
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            activeTextBox.Text = doubleValue.ToString(CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true; // Hata durumunda KeyPad'i açık tut
                            MessageBox.Show($"Lütfen {min} ile {max} arasında bir değer girin.",
                                "Geçersiz Giriş", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        activeTextBox.Text = normalizedValue;
                    }
                }
                else
                {
                    activeTextBox.Text = normalizedValue;
                }

                KeypadPopup.IsOpen = false;
            }
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveValues();
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            // İptal edildiğinde kaydetmeden kapat
            this.Close();
        }
    }
}
