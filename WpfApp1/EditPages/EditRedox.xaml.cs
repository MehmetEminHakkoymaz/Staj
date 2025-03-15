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
    public partial class EditRedox : Window
    {
        private readonly DispatcherTimer clockTimer;
        private TextBox activeTextBox = null;
        public EditRedox()
        {
            InitializeComponent();

            clockTimer = InitializeClock();
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            LoadLastSelectedCascade();
            LoadPIDSettings();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
        }
        private void LoadPIDSettings()
        {
            try
            {
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                RedoxP.Text = Properties.Settings.Default.RedoxP.ToString(culture);
                RedoxI.Text = Properties.Settings.Default.RedoxI.ToString(culture);
                RedoxILimit.Text = Properties.Settings.Default.RedoxILimit.ToString(culture);
                RedoxDeadband.Text = Properties.Settings.Default.RedoxDeadband.ToString(culture);
                RedoxNegfactor.Text = Properties.Settings.Default.RedoxNegfactor.ToString(culture);
                RedoxEvalTime.Text = Properties.Settings.Default.RedoxEvalTime.ToString(culture);
                RegisterPIDTextBoxEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PID settings: {ex.Message}");
            }
        }
        private void RegisterPIDTextBoxEvents()
        {
            RedoxP.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxI.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxILimit.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxDeadband.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxNegfactor.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxEvalTime.PreviewTextInput += TextBox_PreviewTextInput;
            RedoxP.TextChanged += TextBox_TextChanged;
            RedoxI.TextChanged += TextBox_TextChanged;
            RedoxILimit.TextChanged += TextBox_TextChanged;
            RedoxDeadband.TextChanged += TextBox_TextChanged;
            RedoxNegfactor.TextChanged += TextBox_TextChanged;
            RedoxEvalTime.TextChanged += TextBox_TextChanged;
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
                string lastSelected = Properties.Settings.Default.LastSelectedRedoxCascadeItem;
                if (!string.IsNullOrEmpty(lastSelected))
                {
                    foreach (ComboBoxItem item in contentComboBox.Items)
                    {
                        if (item.Content.ToString() == lastSelected)
                        {
                            contentComboBox.SelectedItem = item;
                            return;
                        }
                    }
                }
                contentComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading last selected cascade: {ex.Message}");
                contentComboBox.SelectedIndex = 0;
            }
        }
        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.RedoxSelectedCascade = content;
                switch (content)
                {
                    case "Gas2":
                        Properties.Settings.Default.HideAirFlowBorder = false;
                        Properties.Settings.Default.HideGas2FlowBorder = true;
                        break;
                    case "AirFlow":
                        Properties.Settings.Default.HideAirFlowBorder = true;
                        Properties.Settings.Default.HideGas2FlowBorder = false;
                        break;
                    case "TotalFlow":
                        Properties.Settings.Default.HideAirFlowBorder = false;
                        Properties.Settings.Default.HideGas2FlowBorder = true;
                        break;
                }
                Properties.Settings.Default.Save();
                contentArea.Content = null;
                contentArea.ContentTemplate = null;
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
                    string normalizedValue = value.Replace(',', '.');
                    if (double.TryParse(normalizedValue, System.Globalization.NumberStyles.Any,
                                       System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        if (doubleValue >= min && doubleValue <= max)
                        {
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
                        KeypadPopup.IsOpen = true;
                        MessageBox.Show("Please enter a valid number.",
                                       "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
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
                if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string content = selectedItem.Content.ToString();
                    Properties.Settings.Default.LastSelectedRedoxCascadeItem = content;
                    Properties.Settings.Default.RedoxSelectedCascade = content;
                    switch (content)
                    {
                        case "Gas2":
                            Properties.Settings.Default.HideAirFlowBorder = false;
                            Properties.Settings.Default.HideGas2FlowBorder = true;
                            break;
                        case "AirFlow":
                            Properties.Settings.Default.HideAirFlowBorder = true;
                            Properties.Settings.Default.HideGas2FlowBorder = false;
                            break;
                        case "TotalFlow":
                            Properties.Settings.Default.HideAirFlowBorder = false;
                            Properties.Settings.Default.HideGas2FlowBorder = true;
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
                if (double.TryParse(RedoxP.Text, out double pValue))
                {
                    Properties.Settings.Default.RedoxP = pValue;
                }
                if (double.TryParse(RedoxI.Text, out double iValue))
                {
                    Properties.Settings.Default.RedoxI = iValue;
                }
                if (double.TryParse(RedoxILimit.Text, out double iLimitValue))
                {
                    Properties.Settings.Default.RedoxILimit = iLimitValue;
                }
                if (double.TryParse(RedoxDeadband.Text, out double deadbandValue))
                {
                    Properties.Settings.Default.RedoxDeadband = deadbandValue;
                }
                if (double.TryParse(RedoxNegfactor.Text, out double negfactorValue))
                {
                    Properties.Settings.Default.RedoxNegfactor = negfactorValue;
                }
                if (double.TryParse(RedoxEvalTime.Text, out double evalTimeValue))
                {
                    Properties.Settings.Default.RedoxEvalTime = evalTimeValue;
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                ValidateTextBoxValue(textBox);
            }
        }
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
                case "Gas2":
                    SetupTextBox(content, "RedoxGas2N2", Properties.Settings.Default.RedoxGas2N2);
                    break;
                case "AirFlow":
                    SetupTextBox(content, "RedoxAirFlowAir", Properties.Settings.Default.RedoxAirFlowAir);
                    break;
                case "TotalFlow":
                    SetupTextBox(content, "RedoxTotalFlowTotalFlow", Properties.Settings.Default.RedoxTotalFlowTotalFlow);
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
                Properties.Settings.Default.RedoxSelectedCascade = content;

                switch (content)
                {
                    case "Gas2":
                        SaveValue("RedoxGas2N2", "RedoxGas2N2");
                        break;
                    case "AirFlow":
                        SaveValue("RedoxAirFlowAir", "RedoxAirFlowAir");
                        break;
                    case "TotalFlow":
                        SaveValue("RedoxTotalFlowTotalFlow", "RedoxTotalFlowTotalFlow");
                        break;
                }
            }
        }
        private void SaveValue(string controlName, string settingsProperty)
        {
            var textBox = FindChild<TextBox>(contentArea, controlName);
            if (textBox != null && double.TryParse(textBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double value))
            {
                var property = Properties.Settings.Default.GetType().GetProperty(settingsProperty);
                if (property != null)
                {
                    property.SetValue(Properties.Settings.Default, value);
                }
            }
        }
        private void SetupTextBox(FrameworkElement parent, string name, double value)
        {
            var textBox = FindChild<TextBox>(parent, name);
            if (textBox != null)
            {
                textBox.GotFocus += TextBox_GotFocus;
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
        private void ResetPIDButton_Click(object sender, RoutedEventArgs e)
        {
            RedoxP.Text = "50";
            RedoxI.Text = "25";
            RedoxILimit.Text = "50";
            RedoxDeadband.Text = "5";
            RedoxNegfactor.Text = "100";
            RedoxEvalTime.Text = "60";
        }
    }
}
