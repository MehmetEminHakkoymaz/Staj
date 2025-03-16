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
    public partial class EditTurbidity : Window
    {
        private readonly DispatcherTimer clockTimer;
        private TextBox activeTextBox = null;
        public EditTurbidity()
        {
            InitializeComponent();
            clockTimer = InitializeClock();
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            LoadLastSelectedCascade();
            LoadPIDSettings();
            UpdateUIForVesselType(); // Add this line to update UI based on vessel type

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
                TurbidityP.Text = Properties.Settings.Default.TurbidityP.ToString(culture);
                TurbidityI.Text = Properties.Settings.Default.TurbidityI.ToString(culture);
                TurbidityILimit.Text = Properties.Settings.Default.TurbidityILimit.ToString(culture);
                TurbidityDeadband.Text = Properties.Settings.Default.TurbidityDeadband.ToString(culture);
                TurbidityNegfactor.Text = Properties.Settings.Default.TurbidityNegfactor.ToString(culture);
                TurbidityEvalTime.Text = Properties.Settings.Default.TurbidityEvalTime.ToString(culture);
                RegisterPIDTextBoxEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PID settings: {ex.Message}");
            }
        }
        private void RegisterPIDTextBoxEvents()
        {
            TurbidityP.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityI.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityILimit.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityDeadband.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityNegfactor.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityEvalTime.PreviewTextInput += TextBox_PreviewTextInput;
            TurbidityP.TextChanged += TextBox_TextChanged;
            TurbidityI.TextChanged += TextBox_TextChanged;
            TurbidityILimit.TextChanged += TextBox_TextChanged;
            TurbidityDeadband.TextChanged += TextBox_TextChanged;
            TurbidityNegfactor.TextChanged += TextBox_TextChanged;
            TurbidityEvalTime.TextChanged += TextBox_TextChanged;
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
                string lastSelected = Properties.Settings.Default.LastSelectedTurbidityCascadeItem;
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
        //private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
        //    {
        //        string content = selectedItem.Content.ToString();
        //        Properties.Settings.Default.TurbiditySelectedCascade = content;

        //        // Otomatik olarak EditPump4Feature'ı ayarla
        //        if (content == "Feed")
        //        {
        //            Properties.Settings.Default.EditPump4Feature = "Turbidity";
        //        }
        //        else if (content == "None")
        //        {
        //            Properties.Settings.Default.EditPump4Feature = "Feed";
        //        }

        //        switch (content)
        //        {
        //            case "Harvesting->Inoculate":
        //                Properties.Settings.Default.TurbiditySelectedCascade = "None";
        //                Properties.Settings.Default.EditPump4Feature = "Feed";
        //                LoadLastSelectedCascade();
        //                MessageBox.Show("To use Harvesting->Inoculate mode, an external peristaltic pump must be attached.",
        //                              "Configuration Required",
        //                              MessageBoxButton.OK,
        //                              MessageBoxImage.Warning);
        //                return;
        //        }

        //        Properties.Settings.Default.Save();
        //        contentArea.Content = null;
        //        contentArea.ContentTemplate = null;
        //        ApplyTemplate(content);
        //    }
        //}
        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.TurbiditySelectedCascade = content;

                // EditPump4Feature ayarını HEMEN güncelle ve kaydet
                if (content == "Feed")
                {
                    Properties.Settings.Default.EditPump4Feature = "Turbidity";
                }
                else
                {
                    // Feed değilse, Turbidity özelliğini kullanamaz
                    if (Properties.Settings.Default.EditPump4Feature == "Turbidity")
                        Properties.Settings.Default.EditPump4Feature = "Feed";
                }

                // Harvesting->Inoculate kontrolü
                if (content == "Harvesting->Inoculate")
                {
                    Properties.Settings.Default.TurbiditySelectedCascade = "None";
                    Properties.Settings.Default.EditPump4Feature = "Feed";
                    LoadLastSelectedCascade();
                    MessageBox.Show("To use Harvesting->Inoculate mode, an external peristaltic pump must be attached.",
                                  "Configuration Required",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                // Ayarları derhal kaydet
                Properties.Settings.Default.Save();

                // Tam emin olmak için EditPump4 ayarlarını senkronize et
                try
                {
                    // Eğer EditPump4 sınıfı erişilebilirse, onun senkronizasyon metodu çağrılabilir
                    if (typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static) != null)
                    {
                        typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
                    }
                }
                catch
                {
                    // Sessizce hatayı yok say
                }

                // Şablonu uygula
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
                            // Set the value
                            activeTextBox.Text = doubleValue.ToString(System.Globalization.CultureInfo.CurrentCulture);

                            // Additional validation for Harvesting->Inoculate
                            if (activeTextBox.Name == "TurbidityHarvestingInoculateInoculate")
                            {
                                ValidateInoculateValue();
                            }
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true;
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

        private void ValidateInoculateValue()
        {
            var content = contentArea.Content as FrameworkElement;
            if (content == null) return;

            var harvestingTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateHarvesting");
            var inoculateTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateInoculate");

            if (harvestingTextBox == null || inoculateTextBox == null) return;

            // Parse values
            if (!double.TryParse(harvestingTextBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double harvestingValue))
                return;

            if (!double.TryParse(inoculateTextBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double inoculateValue))
                return;

            // If inoculate value is greater than harvesting value, set it to harvesting value
            if (inoculateValue > harvestingValue)
            {
                inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);

                // Show a message to inform the user
                MessageBox.Show("Inoculate value cannot be greater than Harvesting value. The value has been adjusted.",
                              "Value Adjusted",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
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
        //private void Ok_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
        //        {
        //            string content = selectedItem.Content.ToString();
        //            Properties.Settings.Default.LastSelectedTurbidityCascadeItem = content;
        //            Properties.Settings.Default.TurbiditySelectedCascade = content;

        //            // None seçildiğinde EditPump4Feature'ı Feed olarak ayarla
        //            if (content == "None")
        //            {
        //                Properties.Settings.Default.EditPump4Feature = "Feed";
        //            }
        //            // Feed seçildiğinde de EditPump4Feature'ı Turbidity olarak ayarla
        //            else if (content == "Feed")
        //            {
        //                Properties.Settings.Default.EditPump4Feature = "Turbidity";
        //            }
        //        }
        //        SaveCurrentValues();
        //        SavePIDSettings();
        //        Properties.Settings.Default.Save();
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving settings: {ex.Message}");
        //    }
        //}
        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string content = selectedItem.Content.ToString();
                    Properties.Settings.Default.LastSelectedTurbidityCascadeItem = content;
                    Properties.Settings.Default.TurbiditySelectedCascade = content;

                    // EditPump4Feature ayarını güncelle - bu kritik!
                    if (content == "None" || content == "Harvesting->Inoculate")
                    {
                        Properties.Settings.Default.EditPump4Feature = "Feed";
                    }
                    else if (content == "Feed")
                    {
                        Properties.Settings.Default.EditPump4Feature = "Turbidity";
                    }
                }

                SaveCurrentValues();
                SavePIDSettings();
                Properties.Settings.Default.Save();

                // Ayarları derhal kaydet ve EditPump4 için senkronize et
                try
                {
                    // Eğer EditPump4 sınıfı erişilebilirse
                    if (typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static) != null)
                    {
                        typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
                    }
                }
                catch
                {
                    // Sessizce hatayı yok say
                }

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
                if (double.TryParse(TurbidityP.Text, out double pValue))
                {
                    Properties.Settings.Default.TurbidityP = pValue;
                }
                if (double.TryParse(TurbidityI.Text, out double iValue))
                {
                    Properties.Settings.Default.TurbidityI = iValue;
                }
                if (double.TryParse(TurbidityILimit.Text, out double iLimitValue))
                {
                    Properties.Settings.Default.TurbidityILimit = iLimitValue;
                }
                if (double.TryParse(TurbidityDeadband.Text, out double deadbandValue))
                {
                    Properties.Settings.Default.TurbidityDeadband = deadbandValue;
                }
                if (double.TryParse(TurbidityNegfactor.Text, out double negfactorValue))
                {
                    Properties.Settings.Default.TurbidityNegfactor = negfactorValue;
                }
                if (double.TryParse(TurbidityEvalTime.Text, out double evalTimeValue))
                {
                    Properties.Settings.Default.TurbidityEvalTime = evalTimeValue;
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

                        UpdateTemplateForVesselType(content);
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
                case "Feed":
                    SetupTextBox(content, "TurbidityFeedml", Properties.Settings.Default.TurbidityFeedml);

                    break;
                case "Harvesting->Inoculate":
                    //SetupTextBox(content, "TurbidityHarvestingInoculateHarvesting", Properties.Settings.Default.TurbidityHarvestingInoculateHarvesting);
                    //SetupTextBox(content, "TurbidityHarvestingInoculateInoculate", Properties.Settings.Default.TurbidityHarvestingInoculateInoculate);
                    var harvestingTextBox = SetupTextBox(content, "TurbidityHarvestingInoculateHarvesting", Properties.Settings.Default.TurbidityHarvestingInoculateHarvesting);
                    var inoculateTextBox = SetupTextBox(content, "TurbidityHarvestingInoculateInoculate", Properties.Settings.Default.TurbidityHarvestingInoculateInoculate);

                    // Add TextChanged event handlers for both TextBoxes
                    if (harvestingTextBox != null)
                        harvestingTextBox.TextChanged += HarvestingInoculate_TextChanged;

                    if (inoculateTextBox != null)
                        inoculateTextBox.TextChanged += HarvestingInoculate_TextChanged;

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
                Properties.Settings.Default.TurbiditySelectedCascade = content;

                // None seçildiğinde EditPump4Feature'ı Feed olarak ayarla
                if (content == "None")
                {
                    Properties.Settings.Default.EditPump4Feature = "Feed";
                }
                // Feed seçildiğinde de EditPump4Feature'ı Turbidity olarak ayarla
                else if (content == "Feed")
                {
                    Properties.Settings.Default.EditPump4Feature = "Turbidity";
                }

                switch (content)
                {
                    case "Feed":
                        SaveValue("TurbidityFeedml", "TurbidityFeedml");
                        break;
                    case "Harvesting->Inoculate":
                        SaveValue("TurbidityHarvestingInoculateHarvesting", "TurbidityHarvestingInoculateHarvesting");
                        SaveValue("TurbidityHarvestingInoculateInoculate", "TurbidityHarvestingInoculateInoculate");
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
        private TextBox SetupTextBox(FrameworkElement parent, string name, double value)
        {
            var textBox = FindChild<TextBox>(parent, name);
            if (textBox != null)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.Text = value.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
            return textBox;
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
            TurbidityP.Text = "50";
            TurbidityI.Text = "25";
            TurbidityILimit.Text = "50";
            TurbidityDeadband.Text = "5";
            TurbidityNegfactor.Text = "100";
            TurbidityEvalTime.Text = "60";
        }
        private void UpdateUIForVesselType()
        {
            // Get the current vessel type from settings
            string vesselType = Properties.Settings.Default.SelectedVesselType;

            // Update the template setup method to handle vessel type specifics
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                ApplyTemplate(content);
            }
        }

        private void UpdateTemplateForVesselType(FrameworkElement content)
        {
            string vesselType = Properties.Settings.Default.SelectedVesselType;

            // Find the label and textbox only in the Feed template
            var mlLabel = FindChild<Label>(content, "ml");
            var feedTextBox = FindChild<TextBox>(content, "TurbidityFeedml");

            if (vesselType == "500ml")
            {
                // Update for 500ml vessel type
                if (mlLabel != null)
                {
                    mlLabel.Content = "(MAX 500ml)";
                }

                if (feedTextBox != null)
                {
                    feedTextBox.Tag = "0,500";
                    // Make sure the value doesn't exceed the new maximum
                    if (double.TryParse(feedTextBox.Text, out double currentValue) && currentValue > 500)
                    {
                        feedTextBox.Text = "500";
                    }
                }
            }
            else if (vesselType == "1L")
            {
                // Update for 500ml vessel type
                if (mlLabel != null)
                {
                    mlLabel.Content = "(MAX 1000ml)";
                }

                if (feedTextBox != null)
                {
                    feedTextBox.Tag = "0,1000";
                    // Make sure the value doesn't exceed the new maximum
                    if (double.TryParse(feedTextBox.Text, out double currentValue) && currentValue > 500)
                    {
                        feedTextBox.Text = "1000";
                    }
                }
            }
            else if (vesselType == "2L")
            {
                // Update for 500ml vessel type
                if (mlLabel != null)
                {
                    mlLabel.Content = "(MAX 2000ml)";
                }

                if (feedTextBox != null)
                {
                    feedTextBox.Tag = "0,2000";
                    // Make sure the value doesn't exceed the new maximum
                    if (double.TryParse(feedTextBox.Text, out double currentValue) && currentValue > 500)
                    {
                        feedTextBox.Text = "2000";
                    }
                }
            }
            else if (vesselType == "3L")
            {
                // Update for 500ml vessel type
                if (mlLabel != null)
                {
                    mlLabel.Content = "(MAX 3000ml)";
                }

                if (feedTextBox != null)
                {
                    feedTextBox.Tag = "0,3000";
                    // Make sure the value doesn't exceed the new maximum
                    if (double.TryParse(feedTextBox.Text, out double currentValue) && currentValue > 500)
                    {
                        feedTextBox.Text = "3000";
                    }
                }
            }
        }
        private bool isUpdatingInoculateText = false;

        private void HarvestingInoculate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingInoculateText) return;

            var content = contentArea.Content as FrameworkElement;
            if (content == null) return;

            var harvestingTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateHarvesting");
            var inoculateTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateInoculate");

            if (harvestingTextBox == null || inoculateTextBox == null) return;

            // If this is the harvesting TextBox and it's changing, validate the inoculate value
            if (sender == harvestingTextBox)
            {
                // Parse values
                if (!double.TryParse(harvestingTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double harvestingValue))
                    return;

                if (!double.TryParse(inoculateTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double inoculateValue))
                    return;

                // If inoculate value is greater than harvesting value, set it to harvesting value
                if (inoculateValue > harvestingValue)
                {
                    isUpdatingInoculateText = true;
                    inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    isUpdatingInoculateText = false;

                    MessageBox.Show("Inoculate value has been reduced to match the new Harvesting value.",
                                 "Value Adjusted",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Information);
                }
            }
            // If this is the inoculate TextBox changing, make sure it doesn't exceed harvesting
            else if (sender == inoculateTextBox)
            {
                // Parse values
                if (!double.TryParse(harvestingTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double harvestingValue))
                    return;

                if (!double.TryParse(inoculateTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double inoculateValue))
                    return;

                // If inoculate value is greater than harvesting value, set it to harvesting value
                if (inoculateValue > harvestingValue)
                {
                    isUpdatingInoculateText = true;
                    inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    isUpdatingInoculateText = false;

                    MessageBox.Show("Inoculate value cannot be greater than Harvesting value. The value has been adjusted.",
                                 "Value Adjusted",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Information);
                }
            }
        }

    }
}
