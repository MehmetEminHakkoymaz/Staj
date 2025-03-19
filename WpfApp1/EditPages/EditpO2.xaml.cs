using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditpO2 : Window
    {
        #region Fields
        private readonly DispatcherTimer clockTimer;
        private readonly Dictionary<string, (int Min, int Max)> textBoxLimits;
        private TextBox activeTextBox = null;
        #endregion

        #region Constructor
        public EditpO2()
        {
            InitializeComponent();

            // UI bileşenlerini başlat
            textBoxLimits = InitializeTextBoxLimits();
            clockTimer = InitializeClock();

            // Event'leri bağla
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;

            // Son seçileni yükle - bu kısmı constructor'da yapmalıyız
            // çünkü SelectionChanged eventi bağlandıktan sonra çalışmalı
            LoadLastSelectedCascade();
            LoadPIDSettings();

            // Pencere ayarları
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
        }
        #endregion

        #region Initialization Methods

        // Bu metodu ekleyin - PID ayarlarını yüklemek için
        private void LoadPIDSettings()
        {
            try
            {
                // P, I, ILimit, Deadband, Negfactor, EvalTime ayarlarını yükle
                // Kültür ayarlarını dikkate alarak formatla
                var culture = System.Globalization.CultureInfo.CurrentCulture;

                pO2P.Text = Properties.Settings.Default.EditpO2P.ToString(culture);
                pO2I.Text = Properties.Settings.Default.EditpO2I.ToString(culture);
                pO2ILimit.Text = Properties.Settings.Default.EditpO2ILimit.ToString(culture);
                pO2Deadband.Text = Properties.Settings.Default.EditpO2Deadband.ToString(culture);
                pO2Negfactor.Text = Properties.Settings.Default.EditpO2NegFactor.ToString(culture);
                pO2EvalTime.Text = Properties.Settings.Default.EditpO2EvalTime.ToString(culture);

                // PID TextBox'larına event'leri bağla
                RegisterPIDTextBoxEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PID settings: {ex.Message}");
            }
        }
        // Bu metodu ekleyin - PID TextBox'larına event'leri bağlamak için
        private void RegisterPIDTextBoxEvents()
        {
            // TextBox'lara event'leri bağla
            pO2P.PreviewTextInput += TextBox_PreviewTextInput;
            pO2I.PreviewTextInput += TextBox_PreviewTextInput;
            pO2ILimit.PreviewTextInput += TextBox_PreviewTextInput;
            pO2Deadband.PreviewTextInput += TextBox_PreviewTextInput;
            pO2Negfactor.PreviewTextInput += TextBox_PreviewTextInput;
            pO2EvalTime.PreviewTextInput += TextBox_PreviewTextInput;

            pO2P.TextChanged += TextBox_TextChanged;
            pO2I.TextChanged += TextBox_TextChanged;
            pO2ILimit.TextChanged += TextBox_TextChanged;
            pO2Deadband.TextChanged += TextBox_TextChanged;
            pO2Negfactor.TextChanged += TextBox_TextChanged;
            pO2EvalTime.TextChanged += TextBox_TextChanged;
        }


        private Dictionary<string, (int Min, int Max)> InitializeTextBoxLimits()
        {
            return new Dictionary<string, (int Min, int Max)>
        {
            {"pO2StirrerRPM", (0, 1400)},
            {"pO2TotalFlowTotalFlow", (0, 4000)},
            {"pO2GasMixGasMix", (21, 100)},
            {"pO2StirrerTotalFlowRPM", (0, 1400)},
            {"pO2StirrerTotalFlowTotalFlow", (0, 4000)},
            {"pO2StirrerGasMixRPM", (0, 1400)},
            {"pO2StirrerGasMixGasMix", (21, 100)},
            {"pO2StirrerTotalFlowGasMixRPM", (0, 1400)},
            {"pO2StirrerTotalFlowGasMixTotalFlow", (0, 4000)},
            {"pO2StirrerTotalFlowGasMixGasMix", (21, 100)},
            {"pO2P", (0, 1000)}, // TextBox adları değişmediği için, aynı kalabilir
            {"pO2I", (0, 1000)},
            {"pO2ILimit", (0, 1000)},
            {"pO2Deadband", (0, 100)},
            {"pO2Negfactor", (0, 1000)},
            {"pO2EvalTime", (0, 1000)}
        };
        }


        private DispatcherTimer InitializeClock()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += ClockTimer_Tick;
            timer.Start();
            return timer;
        }

        private void LoadLastSelectedCascade()
        {
            try
            {
                // EditpO2Cascade değerine göre ComboBox'ı ayarla
                double cascadeValue = Properties.Settings.Default.EditpO2Cascade;

                // İlgili index'i seç
                switch ((int)cascadeValue)
                {
                    case 0: // None
                        contentComboBox.SelectedIndex = 0;
                        break;
                    case 1: // Stirrer
                        contentComboBox.SelectedIndex = 1;
                        break;
                    case 2: // TotalFlow
                        contentComboBox.SelectedIndex = 2;
                        break;
                    case 3: // GasMix
                        contentComboBox.SelectedIndex = 3;
                        break;
                    case 4: // Stirrer->TotalFlow
                        contentComboBox.SelectedIndex = 4;
                        break;
                    case 5: // Stirrer->GasMix
                        contentComboBox.SelectedIndex = 5;
                        break;
                    default:
                        contentComboBox.SelectedIndex = 0; // Varsayılan olarak None
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cascade setting: {ex.Message}");
                contentComboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region Event Handlers
        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();

                // EditpO2Cascade değerini ayarla
                switch (content)
                {
                    case "None":
                        Properties.Settings.Default.EditpO2Cascade = 0;
                        Properties.Settings.Default.StirrerTargetBorder = 0;
                        Properties.Settings.Default.AirFlowTargetBorder = 0;
                        break;
                    case "Stirrer":
                        Properties.Settings.Default.EditpO2Cascade = 1;
                        Properties.Settings.Default.StirrerTargetBorder = 1;
                        Properties.Settings.Default.AirFlowTargetBorder = 0;
                        break;
                    case "TotalFlow":
                        Properties.Settings.Default.EditpO2Cascade = 2;
                        Properties.Settings.Default.StirrerTargetBorder = 0;
                        Properties.Settings.Default.AirFlowTargetBorder = 1;
                        break;
                    case "GasMix":
                        Properties.Settings.Default.EditpO2Cascade = 3;
                        Properties.Settings.Default.StirrerTargetBorder = 0;
                        Properties.Settings.Default.AirFlowTargetBorder = 0;
                        break;
                    case "Stirrer->TotalFlow":
                        Properties.Settings.Default.EditpO2Cascade = 4;
                        Properties.Settings.Default.StirrerTargetBorder = 1;
                        Properties.Settings.Default.AirFlowTargetBorder = 1;
                        break;
                    case "Stirrer->GasMix":
                        Properties.Settings.Default.EditpO2Cascade = 5;
                        Properties.Settings.Default.StirrerTargetBorder = 1;
                        Properties.Settings.Default.AirFlowTargetBorder = 0;
                        break;
                    default:
                        Properties.Settings.Default.EditpO2Cascade = 0;
                        Properties.Settings.Default.StirrerTargetBorder = 0;
                        Properties.Settings.Default.AirFlowTargetBorder = 0;
                        break;
                }

                // Settings'i kaydet
                Properties.Settings.Default.Save();

                // Önce mevcut içeriği temizle
                contentArea.Content = null;
                contentArea.ContentTemplate = null;

                // Seçilen template'i yükle
                ApplyTemplate(content);
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
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

                    if (double.TryParse(normalizedValue, System.Globalization.NumberStyles.Any,
                                       System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        // Değer sınırlar içinde mi kontrol et
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            // Yerel kültüre göre değeri TextBox'a ayarla
                            activeTextBox.Text = doubleValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
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
                double.TryParse(parts[0], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out min) &&
                double.TryParse(parts[1], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out max))
            {
                return true;
            }

            return false;
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
                    string content = selectedItem.Content.ToString();

                    // EditpO2Cascade değerini ayarla
                    switch (content)
                    {
                        case "None":
                            Properties.Settings.Default.EditpO2Cascade = 0;
                            Properties.Settings.Default.pO2Ellipse = 0;
                            Properties.Settings.Default.pO2TargetBorder = 1;
                            Properties.Settings.Default.StirrerTargetBorder = 0;
                            Properties.Settings.Default.AirFlowTargetBorder = 0;
                            break;
                        case "Stirrer":
                            Properties.Settings.Default.EditpO2Cascade = 1;
                            Properties.Settings.Default.pO2TargetBorder = 0;
                            Properties.Settings.Default.StirrerTargetBorder = 1;
                            Properties.Settings.Default.AirFlowTargetBorder = 0;
                            break;
                        case "TotalFlow":
                            Properties.Settings.Default.EditpO2Cascade = 2;
                            Properties.Settings.Default.pO2TargetBorder = 0;
                            Properties.Settings.Default.StirrerTargetBorder = 0;
                            Properties.Settings.Default.AirFlowTargetBorder = 1;
                            break;
                        case "GasMix":
                            Properties.Settings.Default.EditpO2Cascade = 3;
                            Properties.Settings.Default.pO2TargetBorder = 0;
                            Properties.Settings.Default.StirrerTargetBorder = 0;
                            Properties.Settings.Default.AirFlowTargetBorder = 0;
                            break;
                        case "Stirrer->TotalFlow":
                            Properties.Settings.Default.EditpO2Cascade = 4;
                            Properties.Settings.Default.pO2TargetBorder = 0;
                            Properties.Settings.Default.StirrerTargetBorder = 1;
                            Properties.Settings.Default.AirFlowTargetBorder = 1;
                            break;
                        case "Stirrer->GasMix":
                            Properties.Settings.Default.EditpO2Cascade = 5;
                            Properties.Settings.Default.pO2TargetBorder = 0;
                            Properties.Settings.Default.StirrerTargetBorder = 1;
                            Properties.Settings.Default.AirFlowTargetBorder = 0;
                            break;
                    }
                }

                // Diğer değerleri kaydet
                SaveCurrentValues();
                SavePIDSettings();

                // Ayarları kalıcı olarak kaydet
                Properties.Settings.Default.Save();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        // Bu metodu ekleyin - PID ayarlarını kaydetmek için
        private void SavePIDSettings()
        {
            try
            {
                // P değeri
                if (double.TryParse(pO2P.Text, out double pValue))
                {
                    Properties.Settings.Default.EditpO2P = pValue;
                }

                // I değeri
                if (double.TryParse(pO2I.Text, out double iValue))
                {
                    Properties.Settings.Default.EditpO2I = iValue;
                }

                // ILimit değeri
                if (double.TryParse(pO2ILimit.Text, out double iLimitValue))
                {
                    Properties.Settings.Default.EditpO2ILimit = iLimitValue;
                }

                // Deadband değeri
                if (double.TryParse(pO2Deadband.Text, out double deadbandValue))
                {
                    Properties.Settings.Default.EditpO2Deadband = deadbandValue;
                }

                // Negfactor değeri
                if (double.TryParse(pO2Negfactor.Text, out double negfactorValue))
                {
                    Properties.Settings.Default.EditpO2NegFactor = negfactorValue;
                }

                // EvalTime değeri
                if (double.TryParse(pO2EvalTime.Text, out double evalTimeValue))
                {
                    Properties.Settings.Default.EditpO2EvalTime = evalTimeValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving PID settings: {ex.Message}");
            }
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
        //    {
        //        ValidateTextBoxValue(textBox);
        //    }
        //}
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                ValidateTextBoxValue(textBox);
            }
        }
        #endregion

        #region Helper Methods
        private void ApplyTemplate(string templateName)
        {
            string resourceKey = $"{templateName}Template".Replace("->", "");

            try
            {
                var template = contentArea.Resources[resourceKey] as DataTemplate;
                if (template != null)
                {
                    var content = template.LoadContent() as FrameworkElement;
                    if (content != null)
                    {
                        SetupTemplateControls(content, templateName);
                        contentArea.Content = content;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying template: {ex.Message}");
            }
        }

        private void SetupTemplateControls(FrameworkElement content, string templateName)
        {
            switch (templateName)
            {
                case "Stirrer":
                    SetupTextBox(content, "pO2StirrerRPM", Properties.Settings.Default.EditpO2Stirrer);
                    break;
                case "TotalFlow":
                    SetupTextBox(content, "pO2TotalFlowTotalFlow", Properties.Settings.Default.EditpO2TotalFlow);
                    break;
                case "GasMix":
                    SetupTextBox(content, "pO2GasMixGasMix", Properties.Settings.Default.EditpO2GasMix);
                    break;
                case "Stirrer->TotalFlow":
                    SetupTextBox(content, "pO2StirrerTotalFlowRPM", Properties.Settings.Default.EditpO2Stirrer);
                    SetupTextBox(content, "pO2StirrerTotalFlowTotalFlow", Properties.Settings.Default.EditpO2TotalFlow);
                    break;
                case "Stirrer->GasMix":
                    SetupTextBox(content, "pO2StirrerGasMixRPM", Properties.Settings.Default.EditpO2Stirrer);
                    SetupTextBox(content, "pO2StirrerGasMixGasMix", Properties.Settings.Default.EditpO2GasMix);
                    break;

            }
        }

        private void ValidateTextBoxValue(TextBox textBox)
        {
            if (textBox.Tag is string tag && ParseRange(tag, out double min, out double max))
            {
                if (double.TryParse(textBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double value))
                {
                    if (value < min)
                        textBox.Text = min.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    else if (value > max)
                        textBox.Text = max.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }

        private void SaveCurrentValues()
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();

                switch (content)
                {
                    case "Stirrer":
                        SaveStirrerValue("pO2StirrerRPM");
                        break;
                    case "TotalFlow":
                        SaveTotalFlowValue("pO2TotalFlowTotalFlow");
                        break;
                    case "GasMix":
                        SaveGasMixValue("pO2GasMixGasMix");
                        break;
                    case "Stirrer->TotalFlow":
                        SaveStirrerValue("pO2StirrerTotalFlowRPM");
                        SaveTotalFlowValue("pO2StirrerTotalFlowTotalFlow");
                        break;
                    case "Stirrer->GasMix":
                        SaveStirrerValue("pO2StirrerGasMixRPM");
                        SaveGasMixValue("pO2StirrerGasMixGasMix");
                        break;
                }
            }
        }
        private void SaveStirrerValue(string controlName)
        {
            var textBox = FindChild<TextBox>(contentArea, controlName);
            if (textBox != null && double.TryParse(textBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double value))
            {
                Properties.Settings.Default.EditpO2Stirrer = value;
            }
        }
        private void SaveTotalFlowValue(string controlName)
        {
            var textBox = FindChild<TextBox>(contentArea, controlName);
            if (textBox != null && double.TryParse(textBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double value))
            {
                Properties.Settings.Default.EditpO2TotalFlow = value;
            }
        }
        private void SaveGasMixValue(string controlName)
        {
            var textBox = FindChild<TextBox>(contentArea, controlName);
            if (textBox != null && double.TryParse(textBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double value))
            {
                Properties.Settings.Default.EditpO2GasMix = value;
            }
        }
        //private void SaveValue(string controlName, string settingsProperty)
        //{
        //    var textBox = FindChild<TextBox>(contentArea, controlName);
        //    if (textBox != null && double.TryParse(textBox.Text,
        //        System.Globalization.NumberStyles.Any,
        //        System.Globalization.CultureInfo.CurrentCulture,
        //        out double value))
        //    {
        //        var property = Properties.Settings.Default.GetType().GetProperty(settingsProperty);
        //        if (property != null)
        //        {
        //            property.SetValue(Properties.Settings.Default, value);
        //        }
        //    }
        //}

        private void SetupTextBox(FrameworkElement parent, string name, double value)
        {
            var textBox = FindChild<TextBox>(parent, name);
            if (textBox != null)
            {
                textBox.GotFocus += TextBox_GotFocus;

                // Kültüre göre formatla
                textBox.Text = value.ToString(System.Globalization.CultureInfo.CurrentCulture);
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
        #endregion

        private void ResetPIDButton_Click(object sender, RoutedEventArgs e)
        {
            // Varsayılan değerleri yükle
            pO2P.Text = "50";
            pO2I.Text = "25";
            pO2ILimit.Text = "50";
            pO2Deadband.Text = "5";
            pO2Negfactor.Text = "100";
            pO2EvalTime.Text = "60";
        }
    }
}
