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
using System.Globalization;
using WpfApp1;

namespace WpfApp1.EditPages
{
    /// <summary>
    /// Interaction logic for EditpH.xaml
    /// </summary>
    public partial class EditpH : Window
    {
        private DispatcherTimer clockTimer;
        private Dictionary<string, (double Min, double Max)> textBoxLimits;
        private TextBox activeTextBox = null;

        public EditpH()
        {
            InitializeComponent();
            InitializeClock();
            textBoxLimits = InitializeTextBoxLimits(); // Değeri değişkene ata

            // Event'leri bağla
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;

            // Son seçileni yükle - bu kısmı constructor'da yapmalıyız
            // çünkü SelectionChanged eventi bağlandıktan sonra çalışmalı
            LoadLastSelectedCascade();

            // PID ayarlarını yükle
            LoadPIDSettings();

            // Pencere ayarları
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }

        #region Initialization Methods
        private Dictionary<string, (double Min, double Max)> InitializeTextBoxLimits()
        {
            return new Dictionary<string, (double Min, double Max)>
            {
                { "pHBaseBase", (0, 14) },
                { "pHAcidAcid", (0, 14) },
                { "pHBaseAcidBase", (0, 14) },
                { "pHBaseAcidAcid", (0, 14) },
                { "pHP", (0, 1000) },
                { "pHI", (0, 1000) },
                { "pHILimit", (0, 1000) },
                { "pHDeadband", (0, 100) },
                { "pHNegFactor", (0, 1000) },
                { "pHEvalTime", (0, 1000) }
            };
        }

        private void LoadPIDSettings()
        {
            try
            {
                // PID değerlerini ayarla
                pHP.Text = Properties.Settings.Default.pHP.ToString(CultureInfo.CurrentCulture);
                pHI.Text = Properties.Settings.Default.pHI.ToString(CultureInfo.CurrentCulture);
                pHILimit.Text = Properties.Settings.Default.pHILimit.ToString(CultureInfo.CurrentCulture);
                pHDeadband.Text = Properties.Settings.Default.pHDeadband.ToString(CultureInfo.CurrentCulture);
                pHNegFactor.Text = Properties.Settings.Default.pHNegFactor.ToString(CultureInfo.CurrentCulture);
                pHEvalTime.Text = Properties.Settings.Default.pHEvalTime.ToString(CultureInfo.CurrentCulture);

                // PID TextBox'larına event'leri bağla
                RegisterPIDTextBoxEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PID settings: {ex.Message}");
            }
        }

        private void RegisterPIDTextBoxEvents()
        {
            // PID TextBox'larına event'leri bağla
            pHP.GotFocus += TextBox_GotFocus;
            pHP.TextChanged += TextBox_TextChanged;
            pHP.PreviewTextInput += TextBox_PreviewTextInput;

            pHI.GotFocus += TextBox_GotFocus;
            pHI.TextChanged += TextBox_TextChanged;
            pHI.PreviewTextInput += TextBox_PreviewTextInput;

            pHILimit.GotFocus += TextBox_GotFocus;
            pHILimit.TextChanged += TextBox_TextChanged;
            pHILimit.PreviewTextInput += TextBox_PreviewTextInput;

            pHDeadband.GotFocus += TextBox_GotFocus;
            pHDeadband.TextChanged += TextBox_TextChanged;
            pHDeadband.PreviewTextInput += TextBox_PreviewTextInput;

            pHNegFactor.GotFocus += TextBox_GotFocus;
            pHNegFactor.TextChanged += TextBox_TextChanged;
            pHNegFactor.PreviewTextInput += TextBox_PreviewTextInput;

            pHEvalTime.GotFocus += TextBox_GotFocus;
            pHEvalTime.TextChanged += TextBox_TextChanged;
            pHEvalTime.PreviewTextInput += TextBox_PreviewTextInput;
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
        private void LoadLastSelectedCascade()
        {
            try
            {
                // En son seçilen cascade değerini oku
                string lastSelected = Properties.Settings.Default.LastSelectedpHCascadeItem;

                if (!string.IsNullOrEmpty(lastSelected))
                {
                    // ComboBox'ta bu değeri bul ve seç
                    foreach (ComboBoxItem item in contentComboBox.Items)
                    {
                        if (item.Content.ToString() == lastSelected)
                        {
                            contentComboBox.SelectedItem = item;
                            return; // İşlem tamamlandı
                        }
                    }
                }

                // Eğer son seçim bulunamadı veya yoksa, varsayılan olarak ilk öğeyi seç
                contentComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading last selected cascade: {ex.Message}");
                // Hata durumunda da varsayılan seçimi yap
                contentComboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region Event Handlers
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Sistem saatini "HH:mm:ss" formatında güncelle
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Seçili cascade değerini kaydet
                if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    Properties.Settings.Default.LastSelectedpHCascadeItem = selectedItem.Content.ToString();
                }

                SaveCurrentValues();
                SavePIDSettings(); // Bu metot eklenmeli
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }
        private void SavePIDSettings()
        {
            try
            {
                // P değeri
                if (double.TryParse(pHP.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pValue))
                {
                    Properties.Settings.Default.pHP = pValue;
                }

                // I değeri
                if (double.TryParse(pHI.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double iValue))
                {
                    Properties.Settings.Default.pHI = iValue;
                }

                // ILimit değeri
                if (double.TryParse(pHILimit.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double iLimitValue))
                {
                    Properties.Settings.Default.pHILimit = iLimitValue;
                }

                // Deadband değeri
                if (double.TryParse(pHDeadband.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double deadbandValue))
                {
                    Properties.Settings.Default.pHDeadband = deadbandValue;
                }

                // Negfactor değeri
                if (double.TryParse(pHNegFactor.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double negfactorValue))
                {
                    Properties.Settings.Default.pHNegFactor = negfactorValue;
                }

                // EvalTime değeri
                if (double.TryParse(pHEvalTime.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double evalTimeValue))
                {
                    Properties.Settings.Default.pHEvalTime = evalTimeValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving PID settings: {ex.Message}");
            }
        }
        private void EditpH_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string savedCascade = Properties.Settings.Default.pHSelectedCascade;
                contentComboBox.SelectedValue = savedCascade;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved settings: {ex.Message}");
            }
        }

        private void InitializeComboBox()
        {
            // ComboBox'a SelectionChanged event'ini ekle
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;

            // Başlangıçta None seçili olsun
            contentComboBox.SelectedIndex = 0;
        }

        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.pHSelectedCascade = content;

                contentArea.Content = null;
                contentArea.ContentTemplate = null;

                switch (content)
                {
                    case "None":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["NoneTemplate"];
                        break;
                    case "Base":
                        var baseTamplate = (DataTemplate)contentArea.Resources["BaseTemplate"];
                        var baseContent = baseTamplate.LoadContent() as FrameworkElement;
                        if (baseContent != null)
                        {
                            var textBox = FindChild<TextBox>(baseContent, "pHBaseBase");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pHBaseBase.ToString();
                            }
                            contentArea.Content = baseContent;
                        }
                        break;
                    case "Acid":
                        var acidTamplate = (DataTemplate)contentArea.Resources["AcidTemplate"];
                        var acidContent = acidTamplate.LoadContent() as FrameworkElement;
                        if (acidContent != null)
                        {
                            var textBox = FindChild<TextBox>(acidContent, "pHAcidAcid");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pHAcidAcid.ToString();
                            }
                            contentArea.Content = acidContent;
                        }
                        break;
                    case "Base->Acid":
                        var baseAcidTemplate = (DataTemplate)contentArea.Resources["BaseAcidTemplate"];
                        var baseAcidContent = baseAcidTemplate.LoadContent() as FrameworkElement;
                        if (baseAcidContent != null)
                        {
                            var textBoxBase = FindChild<TextBox>(baseAcidContent, "pHBaseAcidBase");
                            if (textBoxBase != null)
                            {
                                textBoxBase.GotFocus += TextBox_GotFocus;
                                textBoxBase.Text = Properties.Settings.Default.pHBaseAcidBase.ToString();
                            }
                            var textBoxAcid = FindChild<TextBox>(baseAcidContent, "pHBaseAcidAcid");
                            if (textBoxAcid != null)
                            {
                                textBoxAcid.GotFocus += TextBox_GotFocus;
                                textBoxAcid.Text = Properties.Settings.Default.pHBaseAcidAcid.ToString();
                            }
                            contentArea.Content = baseAcidContent;
                        }
                        break;
                }
            }
        }

        private void LoadBaseValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.pHBaseBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveBaseValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
            if (basee != null && double.TryParse(basee.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double baseValue))
            {
                Properties.Settings.Default.pHBaseBase = baseValue;
            }
        }

        private void LoadAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.pHAcidAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveAcidValues()
        {
            var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
            if (acid != null && double.TryParse(acid.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double acidValue))
            {
                Properties.Settings.Default.pHAcidAcid = acidValue;
            }
        }
        private void LoadBaseAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.pHBaseAcidBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
                var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.pHBaseAcidAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveBaseAcidValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
            if (basee != null && double.TryParse(basee.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double baseValue))
            {
                Properties.Settings.Default.pHBaseAcidBase = baseValue;
            }

            var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
            if (acid != null && double.TryParse(acid.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double acidValue))
            {
                Properties.Settings.Default.pHBaseAcidAcid = acidValue;
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Hem tamsayı hem ondalıklı sayılar için geçerli karakterleri kontrol et
            // Geçerli karakterler: rakamlar, nokta ve virgül
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
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                ValidateTextBoxValue(textBox);
            }
        }

        private void ValidateTextBoxValue(TextBox textBox)
        {
            if (textBoxLimits.TryGetValue(textBox.Name, out var limits))
            {
                string normalizedText = textBox.Text.Replace(',', '.');

                if (double.TryParse(normalizedText, NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out double value))
                {
                    if (value < limits.Min)
                        textBox.Text = limits.Min.ToString(CultureInfo.CurrentCulture);
                    else if (value > limits.Max)
                        textBox.Text = limits.Max.ToString(CultureInfo.CurrentCulture);
                }
            }
        }

        private void SaveCurrentValues()
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.pHSelectedCascade = content;

                switch (content)
                {
                    case "Base":
                        SaveBaseValues();
                        break;
                    case "Acid":
                        SaveAcidValues();
                        break;
                    case "Base->Acid":
                        SaveBaseAcidValues();
                        break;
                }
            }
        }

        private static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T childType)
                {
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                    {
                        foundChild = childType;
                        break;
                    }
                }

                foundChild = FindChild<T>(child, childName);
                if (foundChild != null) break;
            }
            return foundChild;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                activeTextBox = textBox;

                // Label içeriğini bulmak için parent container'ı kontrol et
                var parent = VisualTreeHelper.GetParent(textBox);
                while (parent != null)
                {
                    if (parent is Grid grid)
                    {
                        var label = grid.Children.OfType<Label>().FirstOrDefault();
                        if (label != null)
                        {
                            KeypadPopup.IsOpen = true;
                            KeypadControl.SetLabelContent(label.Content.ToString());
                            break;
                        }
                    }
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
        }
        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                if (activeTextBox.Tag is string tag && ParseRange(tag, out double min, out double max))
                {
                    // Nokta veya virgül içeren değerleri düzgün işle
                    string normalizedValue = value.Replace(',', '.');

                    if (double.TryParse(normalizedValue, NumberStyles.Any,
                                       CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        // Değer sınırlar içinde mi kontrol et
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            // Yerel kültüre göre değeri TextBox'a ayarla
                            activeTextBox.Text = doubleValue.ToString(CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true; // Hata durumunda KeyPad'i tekrar aç
                            MessageBox.Show($"Please enter a value between {min} and {max}.",
                                          "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        // Sayısal değer değilse uyarı ver
                        KeypadPopup.IsOpen = true;
                        MessageBox.Show("Please enter a valid number.",
                                      "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    // Tag yoksa veya geçersizse, direkt değeri ata
                    activeTextBox.Text = value;
                }
            }
        }

        private bool ParseRange(string tag, out double min, out double max)
        {
            min = max = 0;
            if (string.IsNullOrEmpty(tag)) return false;

            var parts = tag.Split(',');
            if (parts.Length == 2 &&
                double.TryParse(parts[0], NumberStyles.Any,
                              CultureInfo.InvariantCulture, out min) &&
                double.TryParse(parts[1], NumberStyles.Any,
                              CultureInfo.InvariantCulture, out max))
            {
                return true;
            }

            return false;
        }
        private void RegisterTextBoxEvents(DependencyObject parent)
        {
            // Verilen parent içindeki tüm TextBox'ları bul
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                    // TextBox'a GotFocus event'ini bağla
                    textBox.GotFocus += TextBox_GotFocus;
                }
                else
                {
                    // Recursive olarak diğer container'ları kontrol et
                    RegisterTextBoxEvents(child);
                }
            }
        }

        private void pHResetPIDButton_Click(object sender, RoutedEventArgs e)
        {
            // Varsayılan değerleri ayarla
            pHP.Text = "50";
            pHI.Text = "25";
            pHILimit.Text = "50";
            pHDeadband.Text = "5";
            pHNegFactor.Text = "100";
            pHEvalTime.Text = "60";
        }
        #endregion
    }
}