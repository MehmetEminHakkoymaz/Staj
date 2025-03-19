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
using System.Reflection.Metadata;

namespace WpfApp1.EditPages
{
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

        // 1. InitializeTextBoxLimits metodunu güncelleme
        private Dictionary<string, (double Min, double Max)> InitializeTextBoxLimits()
        {
            return new Dictionary<string, (double Min, double Max)>
            {
                { "pHBaseBase", (0, 14) },     // TextBox adları değişmediği için bunları korumamız gerekiyor
                { "pHAcidAcid", (0, 14) },     // Çünkü XAML'da bu adlarla tanımlanmışlar
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
                // PID değerlerini ayarla - using EditpH variables
                pHP.Text = Properties.Settings.Default.EditpHP.ToString(CultureInfo.CurrentCulture);
                pHI.Text = Properties.Settings.Default.EditpHI.ToString(CultureInfo.CurrentCulture);
                pHILimit.Text = Properties.Settings.Default.EditpHILimit.ToString(CultureInfo.CurrentCulture);
                pHDeadband.Text = Properties.Settings.Default.EditpHDeadband.ToString(CultureInfo.CurrentCulture);
                pHNegFactor.Text = Properties.Settings.Default.EditpHNegFactor.ToString(CultureInfo.CurrentCulture);
                pHEvalTime.Text = Properties.Settings.Default.EditpHEvalTime.ToString(CultureInfo.CurrentCulture);

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
                // Cascade değerini sayısal değerden oku
                double cascadeValue = Properties.Settings.Default.EditpHCascade;

                // Sayısal değere göre ComboBox'ta öğeyi seç
                switch (cascadeValue)
                {
                    case 0: // None
                        contentComboBox.SelectedIndex = 0;
                        break;
                    case 1: // Base
                        contentComboBox.SelectedIndex = 1;
                        break;
                    case 2: // Acid
                        contentComboBox.SelectedIndex = 2;
                        break;
                    case 3: // Base->Acid
                        contentComboBox.SelectedIndex = 3;
                        break;
                    default:
                        contentComboBox.SelectedIndex = 0; // Varsayılan olarak None seç
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading last selected cascade: {ex.Message}");
                // Hata durumunda varsayılan seçimi yap
                contentComboBox.SelectedIndex = 0;
            }
        }

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
                if (contentComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a cascade value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Seçili cascade değerini kaydet
                if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string selectedContent = selectedItem.Content.ToString();

                    // Seçili içeriğe göre sayısal değeri ayarla
                    switch (selectedContent)
                    {
                        case "None":
                            Properties.Settings.Default.EditpHCascade = 0;
                            break;
                        case "Base":
                            Properties.Settings.Default.EditpHCascade = 1;
                            // HidePump2Border yerine Pump2TargetBorder kullan
                            Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                            Properties.Settings.Default.EditPump2Feature = 0;
                            break;
                        case "Acid":
                            Properties.Settings.Default.EditpHCascade = 2;
                            // HidePump1Border yerine Pump1TargetBorder kullan
                            Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                            Properties.Settings.Default.EditPump1Feature = 0;
                            break;
                        case "Base->Acid":
                            Properties.Settings.Default.EditpHCascade = 3;
                            // HidePump1Border yerine Pump1TargetBorder kullan
                            Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                            Properties.Settings.Default.EditPump1Feature = 0;
                            // HidePump2Border yerine Pump2TargetBorder kullan
                            Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                            Properties.Settings.Default.EditPump2Feature = 0;
                            break;
                    }
                }

                SaveCurrentValues();
                SavePIDSettings();
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
                    Properties.Settings.Default.EditpHP = pValue;
                }

                // I değeri
                if (double.TryParse(pHI.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double iValue))
                {
                    Properties.Settings.Default.EditpHI = iValue;
                }

                // ILimit değeri
                if (double.TryParse(pHILimit.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double iLimitValue))
                {
                    Properties.Settings.Default.EditpHILimit = iLimitValue;
                }

                // Deadband değeri
                if (double.TryParse(pHDeadband.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double deadbandValue))
                {
                    Properties.Settings.Default.EditpHDeadband = deadbandValue;
                }

                // Negfactor değeri
                if (double.TryParse(pHNegFactor.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double negfactorValue))
                {
                    Properties.Settings.Default.EditpHNegFactor = negfactorValue;
                }

                // EvalTime değeri
                if (double.TryParse(pHEvalTime.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double evalTimeValue))
                {
                    Properties.Settings.Default.EditpHEvalTime = evalTimeValue;
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
        // Cascade değerine göre ayarları yap
        double cascadeValue = Properties.Settings.Default.EditpHCascade;

        switch (cascadeValue)
        {
            case 1: // Base
                contentComboBox.SelectedIndex = 1;
                // HidePump2Border yerine Pump2TargetBorder kullan
                Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                Properties.Settings.Default.EditPump2Feature = 0;
                Properties.Settings.Default.Save();
                break;
            case 2: // Acid
                contentComboBox.SelectedIndex = 2;
                // HidePump1Border yerine Pump1TargetBorder kullan
                Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                Properties.Settings.Default.EditPump1Feature = 0;
                Properties.Settings.Default.Save();
                break;
            case 3: // Base->Acid
                contentComboBox.SelectedIndex = 3;
                // HidePump1Border yerine Pump1TargetBorder kullan
                Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                Properties.Settings.Default.EditPump1Feature = 0;
                // HidePump2Border yerine Pump2TargetBorder kullan
                Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                Properties.Settings.Default.EditPump2Feature = 0;
                Properties.Settings.Default.Save();
                break;
            default: // None veya geçersiz değer
                contentComboBox.SelectedIndex = 0;
                break;
        }
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

        // 8. ContentComboBox_SelectionChanged metodunda TextBox'lara atanan değerleri güncelleme
        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();

                // Cascade değerini içeriğe göre ayarla
                switch (content)
                {
                    case "None":
                        Properties.Settings.Default.EditpHCascade = 0;
                        break;
                    case "Base":
                        Properties.Settings.Default.EditpHCascade = 1;
                        // HidePump2Border yerine Pump2TargetBorder kullan
                        Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                        Properties.Settings.Default.EditPump2Feature = 0;
                        break;
                    case "Acid":
                        Properties.Settings.Default.EditpHCascade = 2;
                        // HidePump1Border yerine Pump1TargetBorder kullan
                        Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                        Properties.Settings.Default.EditPump1Feature = 0;
                        break;
                    case "Base->Acid":
                        Properties.Settings.Default.EditpHCascade = 3;
                        // HidePump1Border yerine Pump1TargetBorder kullan
                        Properties.Settings.Default.Pump1TargetBorder = 1; // Collapsed olması için 1
                        Properties.Settings.Default.EditPump1Feature = 0;
                        // HidePump2Border yerine Pump2TargetBorder kullan
                        Properties.Settings.Default.Pump2TargetBorder = 1; // Collapsed olması için 1
                                                                           // Bu kısımda string yerine numeric değer kullanılmalı
                        Properties.Settings.Default.EditPump2Feature = 0;
                        break;
                }

                Properties.Settings.Default.Save(); // Değişiklikleri hemen kaydet

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
                                textBox.Text = Properties.Settings.Default.EditpHBase.ToString();
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
                                textBox.Text = Properties.Settings.Default.EditpHAcid.ToString();
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
                                textBoxBase.Text = Properties.Settings.Default.EditpHBase.ToString();
                            }
                            var textBoxAcid = FindChild<TextBox>(baseAcidContent, "pHBaseAcidAcid");
                            if (textBoxAcid != null)
                            {
                                textBoxAcid.GotFocus += TextBox_GotFocus;
                                textBoxAcid.Text = Properties.Settings.Default.EditpHAcid.ToString();
                            }
                            contentArea.Content = baseAcidContent;
                        }
                        break;
                }
            }
        }

        // 2. LoadBaseValues metodunu güncelleme
        private void LoadBaseValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.EditpHBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        // 3. SaveBaseValues metodunu güncelleme
        private void SaveBaseValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
            if (basee != null && double.TryParse(basee.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double baseValue))
            {
                Properties.Settings.Default.EditpHBase = baseValue;
            }
        }

        // 4. LoadAcidValues metodunu güncelleme
        private void LoadAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.EditpHAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        // 5. SaveAcidValues metodunu güncelleme
        private void SaveAcidValues()
        {
            var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
            if (acid != null && double.TryParse(acid.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double acidValue))
            {
                Properties.Settings.Default.EditpHAcid = acidValue;
            }
        }
        // 6. LoadBaseAcidValues metodunu güncelleme
        private void LoadBaseAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.EditpHBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
                var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.EditpHAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        // 7. SaveBaseAcidValues metodunu güncelleme
        private void SaveBaseAcidValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
            if (basee != null && double.TryParse(basee.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double baseValue))
            {
                Properties.Settings.Default.EditpHBase = baseValue;
            }

            var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
            if (acid != null && double.TryParse(acid.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double acidValue))
            {
                Properties.Settings.Default.EditpHAcid = acidValue;
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

                // String olarak pHSelectedCascade değerini artık kullanmıyoruz
                // Properties.Settings.Default.pHSelectedCascade = content;

                // Bu değer zaten ContentComboBox_SelectionChanged metodunda ayarlandı
                // ama burada da doğru değeri korumak için ayarlıyoruz
                switch (content)
                {
                    case "None":
                        Properties.Settings.Default.EditpHCascade = 0;
                        break;
                    case "Base":
                        Properties.Settings.Default.EditpHCascade = 1;
                        SaveBaseValues();
                        break;
                    case "Acid":
                        Properties.Settings.Default.EditpHCascade = 2;
                        SaveAcidValues();
                        break;
                    case "Base->Acid":
                        Properties.Settings.Default.EditpHCascade = 3;
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